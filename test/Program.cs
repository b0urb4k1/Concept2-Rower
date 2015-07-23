using System;
using System.Reactive.Linq;
using Concept2.Commands.PM3;
using Concept2.Commands.CSafe;
using Concept2.Commands;
using Concept2.Types;
using System.Reactive.Subjects;
using Concept2;
using Concept2.Enums;

namespace dispatcher
{
    using CurrentVersion = Concept2.Enums;
}

namespace test
{
    class MainClass
    {
        private static CommandSet m_100HzCommands = new CommandSet();
        private static CommandSet m_10HzCommands = new CommandSet();
        private static CommandSet m_2HzCommands = new CommandSet();
        private static CommandSet everything = new CommandSet();

        private static StrokeStateCommand m_StrokeStateCommand = new StrokeStateCommand();
        private static CadenceCommand m_CadenceCommand = new CadenceCommand();
        private static CaloriesCommand m_CaloriesCommand = new CaloriesCommand();
        private static DistanceCommand m_DistanceCommand = new DistanceCommand();
        private static DragFactorCommand m_DragFactorCommand = new DragFactorCommand();
        private static PaceCommand m_PaceCommand = new PaceCommand();
        private static PowerCommand m_PowerCommand = new PowerCommand();
        private static TimeCommand m_TimeCommand = new TimeCommand();
        private static WorkDistanceCommand m_WorkDistanceCommand = new WorkDistanceCommand();
        private static WorkoutStateCommand m_WorkoutStateCommand = new WorkoutStateCommand();
        private static WorkoutTypeCommand m_WorkoutTypeCommand = new WorkoutTypeCommand();
        private static WorkTimeCommand m_WorkTimeCommand = new WorkTimeCommand();
        private static Concept2.Commands.PM3.ForcePlotDataCommand m_ForcePlotDataCommand = new Concept2.Commands.PM3.ForcePlotDataCommand();

        private static readonly ResponseReader m_RspReader = new ResponseReader(64);

        private static Connection m_Connection;

        public static void PrepareCommands()
        {
            m_100HzCommands.Add(m_ForcePlotDataCommand);
            m_100HzCommands.Prepare();

            m_10HzCommands.Add(m_DistanceCommand);
            m_10HzCommands.Add(m_TimeCommand);
            m_10HzCommands.Add(m_WorkDistanceCommand);
            m_10HzCommands.Add(m_WorkTimeCommand);
            m_10HzCommands.Prepare();

            m_2HzCommands.Add(m_CadenceCommand);
            m_2HzCommands.Add(m_CaloriesCommand);
            m_2HzCommands.Add(m_DragFactorCommand);
            m_2HzCommands.Add(m_PaceCommand);
            m_2HzCommands.Add(m_PowerCommand);
            m_2HzCommands.Add(m_WorkoutStateCommand);
            m_2HzCommands.Prepare();

            everything.Add(m_StrokeStateCommand);
            everything.Add(m_DistanceCommand);
            everything.Add(m_TimeCommand);
            everything.Add(m_WorkDistanceCommand);
            everything.Add(m_WorkTimeCommand);
            everything.Add(m_CadenceCommand);
            everything.Add(m_CaloriesCommand);
            everything.Add(m_DragFactorCommand);
            everything.Add(m_PaceCommand);
            everything.Add(m_PowerCommand);
            everything.Add(m_WorkoutStateCommand);
            everything.Add(m_ForcePlotDataCommand);
            everything.Prepare();
        }

        static IConnectableObservable<StrokeState> MakeStrokeStream(IConnection connection)
        {
            return Observable.Interval(TimeSpan.FromMilliseconds(10))
            .Select(_ =>
                {
                    var rspDataCount = m_RspReader.Capacity;

                    if (connection.SendCSAFECommand(m_100HzCommands.Buffer, m_100HzCommands.Size, m_RspReader.Buffer, ref rspDataCount))
                    {
                        // Reset the reader to the correct length
                        m_RspReader.Reset(rspDataCount);
                        m_100HzCommands.Read(m_RspReader);
                    }

                    return m_StrokeStateCommand.StrokeState;
                })
            //.Scan(Tuple.Create(StrokeState.Catch, StrokeState.Catch), (p, c) => Tuple.Create(p.Item2, c))
            //.Where(e => e.Item1 != e.Item2)
            //.Select(e => e.Item1)
            .Publish();
        }

        static IConnectableObservable<Tuple<Distance, Time, Distance, Time>> Make10HzStream(IConnection connection)
        {
            return Observable.Interval(TimeSpan.FromMilliseconds(10)).Select(_ =>
            {
                var rspDataCount = m_RspReader.Capacity;

                if (connection.SendCSAFECommand(m_10HzCommands.Buffer, m_10HzCommands.Size, m_RspReader.Buffer, ref rspDataCount))
                {
                    // Reset the reader to the correct length
                    m_RspReader.Reset(rspDataCount);
                    m_10HzCommands.Read(m_RspReader);
                }

                return Tuple.Create
                    ( m_DistanceCommand.Distance
                    , m_TimeCommand.Time
                    , m_WorkDistanceCommand.WorkDistance
                    , m_WorkTimeCommand.WorkTime
                    );
            })
            .Publish();
        }

        static IConnectableObservable<Tuple<uint, uint, uint, Time, uint, WorkoutState>> Make2HzStream(IConnection connection)
        {
            return Observable.Interval(TimeSpan.FromMilliseconds(10)).Select(_ =>
            {
                var rspDataCount = m_RspReader.Capacity;

                if (connection.SendCSAFECommand(m_2HzCommands.Buffer, m_2HzCommands.Size, m_RspReader.Buffer, ref rspDataCount))
                {
                    // Reset the reader to the correct length
                    m_RspReader.Reset(rspDataCount);
                    m_2HzCommands.Read(m_RspReader);
                }

                return Tuple.Create
                    (m_CadenceCommand.StrokeRate
                    , m_CaloriesCommand.Calories
                    , m_DragFactorCommand.DragFactor
                    , m_PaceCommand.Pace
                    , m_PowerCommand.Power
                    , m_WorkoutStateCommand.WorkoutState
                    );
            })
            .Publish();
        }

        static IConnectableObservable<Tuple<StrokeState, uint, Time, uint, uint, uint[]>> MakeEverythingStream(IConnection connection)
        {
            return Observable.Interval(TimeSpan.FromMilliseconds(10)).Select(_ =>
                {
                    var rspDataCount = m_RspReader.Capacity;

                    if (connection.SendCSAFECommand(everything.Buffer, everything.Size, m_RspReader.Buffer, ref rspDataCount))
                    {
                        // Reset the reader to the correct length
                        m_RspReader.Reset(rspDataCount);
                        everything.Read(m_RspReader);
                    }

                    return Tuple.Create
                        (m_StrokeStateCommand.StrokeState
                        , m_CaloriesCommand.Calories
                        , m_PaceCommand.Pace
                        , m_PowerCommand.Power
                        , m_CadenceCommand.StrokeRate
                        , m_ForcePlotDataCommand.Data
                        );
                })
                .Publish();
        }

        private static Tuple<ushort, uint[]> accumulateForceCurve()
        {
            var cmd_data = new uint[64];
            var rsp_data = new uint[64];
            ushort rsp_data_size = 64;
            ushort cmd_data_size;
            cmd_data_size = 0;
            cmd_data[cmd_data_size++] = (uint)CSAFE.SETUSERCFG1_CMD;
            cmd_data[cmd_data_size++] = 0x3;
            cmd_data[cmd_data_size++] = (uint)CSAFE.PM_GET_FORCEPLOTDATA;
            cmd_data[cmd_data_size++] = 0x01;
            cmd_data[cmd_data_size++] = 0x20;

            m_Connection.SendCSAFECommand(cmd_data, cmd_data_size, rsp_data, ref rsp_data_size);

            return Tuple.Create(rsp_data_size, rsp_data);
        }

        public static void Main(string[] args)
        {
            PrepareCommands();

            m_Connection = new Connection();
            m_Connection.Open();

            //var strokes = MakeStrokeStream(m_Connection);
            //strokes.Subscribe(s => accumulateForceCurve());
            //strokes.Connect();

            Observable.Interval(TimeSpan.FromMilliseconds(500)).Subscribe(_ => Console.WriteLine(accumulateForceCurve()));

            //var Hz10 = Make10HzStream(m_Connection);
            //Hz10.Subscribe(s => Console.WriteLine(string.Format("{0} {1} {2} {3}", s.Item1, s.Item2, s.Item3, s.Item4)));
            //Hz10.Connect();

            //var Hz2 = Make2HzStream(m_Connection);
            //Hz2.Subscribe(s => Console.WriteLine(string.Format("{0} {1} {2} {3}", s.Item6, s.Item1, s.Item2, s.Item3)));
            //Hz2.Connect();

            //var e = MakeEverythingStream(m_Connection);
            //e.Subscribe(s => Console.WriteLine(string.Format("{0}", s.Item6[0])));
            //e.Connect();

            while (true)
            {
            }
            Console.WriteLine("Hello World!");
        }
    }
}

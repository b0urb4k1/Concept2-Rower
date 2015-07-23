using System.Collections.Generic;
using System.Diagnostics;
using Concept2.Commands.PM3;
using Concept2.Exceptions;

namespace Concept2.Commands
{
    public class CommandSet
    {
        public CommandSet()
        {
            _pm3Commands = new List<Command>();
            _cSafeCommands = new List<Command>();
            _commandWriter = new CommandWriter(64);

            _open = true;
        }

        public bool CanAdd => _open;
        public bool CanSend => !_open;
        public uint[] Buffer => _commandWriter.Buffer;
        public ushort Size => (ushort)_commandWriter.Size;

        public void Reset()
        {
            _pm3Commands.Clear();
            _cSafeCommands.Clear();
            _commandWriter.Reset();

            _open = true;
        }

        public void Add(Command cmd)
        {
            if (_open)
            {
                if (cmd is Pm3Command)
                    _pm3Commands.Add(cmd);
                else
                    _cSafeCommands.Add(cmd);
            }
            else
                throw new CommandSetException("Cannot add new commands once set is prepared.");
        }

        public void Prepare()
        {
            if (_open)
            {
                if (_pm3Commands.Count > 0)
                {
                    // Add PM3 commands
                    _commandWriter.WriteByte((uint)Enums.Csafe.SETUSERCFG1_CMD);
                    _commandWriter.WriteByte((uint)_pm3Commands.Count);

                    foreach (Command cmd in _pm3Commands)
                        cmd.Write(_commandWriter);
                }

                // Add CSAFE commands
                foreach (Command cmd in _cSafeCommands)
                    cmd.Write(_commandWriter);

                // Ensure no more commands are added
                _open = false;
            }
            else
            {
                throw new CommandSetException("Set is already prepared.");
            }
        }

        public bool Read(ResponseReader reader)
        {
            if (_open)
                throw new CommandSetException("Attempting to read set before it has been prepared.");

            var success = false;

            try
            {
                if (_pm3Commands.Count > 0)
                {
                    // Read the PM3 custom command marker and size
                    if (reader.ReadByte() == (uint)Enums.Csafe.SETUSERCFG1_CMD)
                        // Read the size
                        reader.ReadByte();

                    // Read PM3 commands
                    foreach (Command cmd in _pm3Commands)
                        cmd.Read(reader);
                }

                // Read CSAFE commands
                foreach (Command cmd in _cSafeCommands)
                    cmd.Read(reader);

                // Ensure whole response has been read
                success = (reader.Position == reader.Size);
            }
            catch (BufferExceededException e)
            {
                Debug.WriteLine(string.Format("[CommandSet.Read] {0}", e.Message));
            }

            return success;
        }

        private readonly List<Command> _pm3Commands;
        private readonly List<Command> _cSafeCommands;
        private readonly CommandWriter _commandWriter;
        private bool _open;
    }
}

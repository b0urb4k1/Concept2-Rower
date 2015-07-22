namespace Concept2.Types
{
    public struct Time
    {
        public uint Hours => TotalSeconds / 3600;

        public uint TotalMinutes => TotalSeconds / 60;
        public uint Minutes => TotalMinutes - 60 * Hours;

        public uint TotalSeconds => TotalHundreths / 100;
        public uint Seconds => TotalSeconds - 60 * TotalMinutes;

        public uint TotalTenths => TotalHundreths / 10;
        public uint Tenths => Hundreths / 10;

        public uint TotalHundreths { get; set; }
        public uint Hundreths => TotalHundreths - 100 * TotalSeconds;

        public double Double
        {
            get { return 0.01 * (double)TotalHundreths; }
            set { TotalHundreths = (uint)(100.0 * value); }
        }


        public override string ToString() => string.Format("{0}:{1:D02}:{2:D02}.{3:D02}", Hours, Minutes, Seconds, Hundreths);

        public string Concise
        {
            get
            {
                if (Hours > 0)
                {
                    return string.Format("{0}:{1:D02}:{2:D02}", Hours, Minutes, Seconds);
                }
                else if (Minutes > 0)
                {
                    return string.Format("{0}:{1:D02}", Minutes, Seconds);
                }
                return string.Format(":{0:D02}", Seconds);
            }
        }

        public string ToTenths
        {
            get
            {
                if (Hours > 0)
                {
                    return string.Format("{0}:{1:D02}:{2:D02}.{3}", Hours, Minutes, Seconds, Tenths);
                }
                else if (Minutes > 0)
                {
                    return string.Format("{0}:{1:D02}.{2}", Minutes, Seconds, Tenths);
                }
                return string.Format(":{0:D02}.{1}", Seconds, Tenths);
            }
        }
    }
}

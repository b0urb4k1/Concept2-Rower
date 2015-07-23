namespace Concept2.Types
{
    public struct Distance
    {
        public uint Metres => TotalTenths / 10;

        public uint TotalTenths { get; set; }
        public uint Tenths => TotalTenths - 10 * Metres;

        public double Double
        {
            get { return 0.1 * TotalTenths; }
            set { TotalTenths = (uint)(10.0 * value); }
        }

        public override string ToString() => string.Format("{0}.{1}", Metres, Tenths);
    }
}

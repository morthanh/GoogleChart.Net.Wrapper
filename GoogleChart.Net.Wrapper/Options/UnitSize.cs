namespace GoogleChart.Net.Wrapper.Options
{
    public sealed class UnitSize
    {
        public string? Value { get; }

        internal object? Parent { get; set; }

        private UnitSize(string value)
        {
            Value = value;
        }

        public static UnitSize Pixel(int pixels) => new UnitSize(pixels.ToString());
        public static UnitSize Percent(double percent) => new UnitSize(percent.ToString("0.##\\%"));
        public static UnitSize Em(double em) => new UnitSize(em.ToString("0.##em"));

        public static implicit operator UnitSize(int i) => Pixel(i);
    }
}

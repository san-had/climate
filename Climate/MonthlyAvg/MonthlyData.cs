namespace MonthlyAvg
{
    public record MonthlyData
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public double Temperature { get; set; }

        public double Pressure { get; set; }

        public int Precipitation { get; set; }
    }
}
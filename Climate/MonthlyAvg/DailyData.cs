namespace MonthlyAvg
{
    public record DailyData
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        public int Temperature { get; set; }

        public int Pressure { get; set; }

        public int Precipitation { get; set; }
    }
}
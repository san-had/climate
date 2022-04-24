namespace MonthlyAvg
{
    public record YearlyData
    {
        public int Year { get; set; }

        public double Temperature { get; set; }

        public double Pressure { get; set; }

        public int Precipitation { get; set; }
    }
}
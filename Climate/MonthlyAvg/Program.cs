using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MonthlyAvg
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dataCollection = ReadFile();

            //var filteredData = dataCollection.Where(d => d.Year == 2022 && d.Month == 4);

            var monthlyDatas = GetMonthlyDatas(dataCollection);

            WriteMonthlyDatas(monthlyDatas);

            var yearlyDatas = GetYearlyDatas(dataCollection);

            WriteYearlyDatas(yearlyDatas);

        }

        private static List<DailyData> ReadFile()
        {
            string[] lines = File.ReadAllLines(@"D:\GittHub\climate\Climate\MonthlyAvg\omsza.txt");

            List<DailyData> dailyDataList = new List<DailyData>();

            foreach (var line in lines.Where(line => line.Length > 0))
            {
                string[] data = line.Trim().Split(';');
                var dailyData = new DailyData();
                dailyData.Year = Int32.Parse(data[0]);
                dailyData.Month = Int32.Parse(data[1]);
                dailyData.Day = Int32.Parse(data[2]);
                dailyData.Temperature = Int32.Parse(data[3]);
                dailyData.Pressure = Int32.Parse(data[4]);
                dailyData.Precipitation = Int32.Parse(string.IsNullOrEmpty(data[5]) ? "0" : data[5]);
                dailyDataList.Add(dailyData);
            }
            return dailyDataList;
        }

        private static List<MonthlyData> GetMonthlyDatas(List<DailyData> dailyDatas)
        {
            List<MonthlyData> monthlyDataList = new List<MonthlyData>();

            int[] years = dailyDatas.Select(x => x.Year).Distinct().ToArray();

            foreach (var year in years)
            {
                int[] months = dailyDatas.Where(x => x.Year == year).Select(x => x.Month).Distinct().ToArray();
                foreach (var month in months)
                {
                    var filteredData = dailyDatas.Where(x => x.Year == year && x.Month == month);
                    var avgTemp = filteredData.Select(fd => fd.Temperature).Average();
                    var avgPressure = filteredData.Select(fd => fd.Pressure).Average();
                    var sumPrecipitation = filteredData.Select(fd => fd.Precipitation).Sum();
                    var monthlyData = new MonthlyData
                    {
                        Year = year,
                        Month = month,
                        Temperature = Math.Round(avgTemp, 1),
                        Pressure = Math.Round(avgPressure, 1),
                        Precipitation = sumPrecipitation
                    };
                    monthlyDataList.Add(monthlyData);
                }
            }
            return monthlyDataList;
        }

        private static List<YearlyData> GetYearlyDatas(List<DailyData> dailyDatas)
        {
            List<YearlyData> yearlyDataList = new List<YearlyData>();

            int[] years = dailyDatas.Select(x => x.Year).Distinct().ToArray();

            foreach (var year in years)
            {
                var filteredData = dailyDatas.Where(x => x.Year == year);
                var avgTemp = filteredData.Select(fd => fd.Temperature).Average();
                var avgPressure = filteredData.Select(fd => fd.Pressure).Average();
                var sumPrecipitation = filteredData.Select(fd => fd.Precipitation).Sum();
                var yearlyData = new YearlyData
                {
                    Year = year,
                    Temperature = Math.Round(avgTemp, 1),
                    Pressure = Math.Round(avgPressure, 1),
                    Precipitation = sumPrecipitation
                };
                yearlyDataList.Add(yearlyData);
            }
            return yearlyDataList;
        }

        private static void WriteMonthlyDatas(List<MonthlyData> monthlyDatas)
        {
            using StreamWriter outputStream = new StreamWriter("monthlyData.csv");
            foreach (var monthlyData in monthlyDatas)
            {
                outputStream.WriteLine($"{monthlyData.Year};{monthlyData.Month};{monthlyData.Temperature};{monthlyData.Pressure};{monthlyData.Precipitation}");
            }
        }

        private static void WriteYearlyDatas(List<YearlyData> yearlyDatas)
        {
            using StreamWriter outputStream = new StreamWriter("yearlyDatas.csv");
            foreach (var yearlyData in yearlyDatas)
            {
                outputStream.WriteLine($"{yearlyData.Year};{yearlyData.Temperature};{yearlyData.Pressure};{yearlyData.Precipitation}");
            }
        }
    }
}
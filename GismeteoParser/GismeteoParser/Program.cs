using GismeteoParser.Parsers;
using GismeteoParser.Weathers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;

namespace GismeteoParser
{
    class Program
    {
        private static bool _isRunning;

        public static void Main(string[] args)
        {
            ParserRun();
            Timer timer = new Timer()
            {
                AutoReset = true,
                Interval = 60 * 60 * 60 * 24,
            };
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            Console.Read();
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ParserRun();
        }

        private static async void ParserRun()
        {
            await Task.Run(() =>
            {
                if (_isRunning)
                    return;
                try
                {
                    _isRunning = true;
                    HomePageParser parser = new HomePageParser();
                    IEnumerable<City> сities = parser.GetCitiesOnHomePage();
                    PrepareWeather(сities);
                    SaveForecast.Save(сities);
                }
                finally
                {
                    _isRunning = false;
                }
            });
        }

        private static void PrepareWeather(IEnumerable<City> сities)
        {
            foreach (City city in сities)
            {
                var args = new List<IWeather>()
                {
                    new Temperature(),
                    new Wind(),
                    new Pressure()
                };
                var weatherParser = new WeatherParser(city);
                weatherParser.Run(args);
            }
        }
    }
}

using System;
using System.Collections.Generic;

namespace GismeteoParser
{
    public class City
    {
        public const string DataId = "data-id";
        public const string DataName = "data-name";
        public const string DataUrl = "data-url";

        public long? Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        /// <summary>Прогноз погоды на дату</summary>
        public DateTime DateForecast { get; set; }
        public Dictionary<Time, ForecastByСity> Forecast { get; set; }

        public City() 
        {
            FillForecast();
            DateForecast = DateTime.Today.AddDays(1);
        }

        private void FillForecast() 
        {
            Forecast = new Dictionary<Time, ForecastByСity>()
            {
                { Time.One, new ForecastByСity() },
                { Time.Four, new ForecastByСity() },
                { Time.Seven, new ForecastByСity() },
                { Time.Ten, new ForecastByСity() },
                { Time.Thirteen, new ForecastByСity() },
                { Time.Sixteen, new ForecastByСity() },
                { Time.Nineteen, new ForecastByСity() },
                { Time.TwentyTwo, new ForecastByСity() },
            };
        }
    }
}

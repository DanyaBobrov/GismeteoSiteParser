using GismeteoParser.Exceptions;
using GismeteoParser.Extensions;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace GismeteoParser.Weathers
{
    public class Temperature : IWeather
    {
        private const string Forecast = "forecast";
        private const string Class = "class";
        private const string Unit = "unit unit_temperature_c";

        public void LookWeatherForecast(City sity, IEnumerable<HtmlNode> weatherContainer)
        {
            HtmlNode forecast = weatherContainer
                .Where(x => x.Attributes.Where(y => y.Value == Forecast).Any()).FirstOrDefault();

            if (forecast == null)
                throw new NotFoundException(nameof(HtmlNode));

            foreach (KeyValuePair<Time, string> item in GetPairsTemperature(forecast))
                sity.Forecast[item.Key].Temperature = item.Value;
        }

        private Dictionary<Time, string> GetPairsTemperature(HtmlNode forecast)
        {
            string[] array = forecast.Descendants()
                .Where(x => x.HasAttribute(Class) && x.Attributes[Class].Value == Unit)
                .Select(x => x.InnerText)
                .ToArray();

            return WeatherHelper.GetWeaterAsPairs(array);
        }
    }
}

using GismeteoParser.Exceptions;
using GismeteoParser.Extensions;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace GismeteoParser.Weathers
{
    public class Pressure : IWeather
    {
        private const string PressureName = "pressure";
        private const string Class = "class";
        private const string Values = "values";
        private const string Unit = "unit unit_pressure_mm_hg_atm";

        public void LookWeatherForecast(City sity, IEnumerable<HtmlNode> weatherContainer)
        {
            HtmlNode pressure = weatherContainer
                .Where(x => x.Attributes.Where(y => y.Value == PressureName).Any()).FirstOrDefault();

            if (pressure == null)
                throw new NotFoundException(nameof(HtmlNode));

            foreach (KeyValuePair<Time, string> item in GetPairsPressure(pressure))
                sity.Forecast[item.Key].Pressure = item.Value;
        }

        private Dictionary<Time, string> GetPairsPressure(HtmlNode pressure)
        {
            HtmlNode widgetPressure = pressure.Descendants()
                .Where(x => x.HasAttribute(Class) && x.Attributes[Class].Value == Values)
                .FirstOrDefault();

            if (widgetPressure == null)
                throw new NotFoundException(nameof(HtmlNode));

            string[] array = widgetPressure.Descendants()
                .Where(x => x.HasAttribute(Class) && x.Attributes[Class].Value == Unit)
                .Select(x => x.InnerText)
                .ToArray();

            return WeatherHelper.GetWeaterAsPairs(array);
        }
    }
}

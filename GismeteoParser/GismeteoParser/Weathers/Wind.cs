using GismeteoParser.Exceptions;
using GismeteoParser.Extensions;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace GismeteoParser.Weathers
{
    public class Wind : IWeather
    {
        private const string WindName = "wind";
        private const string Class = "class";
        private const string Unit = "unit unit_wind_m_s";
        private const string WidgetRowWind = "widget__row widget__row_table widget__row_wind";

        public void LookWeatherForecast(City sity, IEnumerable<HtmlNode> weatherContainer)
        {
            HtmlNode wind = weatherContainer
                .Where(x => x.Attributes.Where(y => y.Value == WindName).Any()).FirstOrDefault();

            if (wind == null)
                throw new NotFoundException(nameof(HtmlNode));

            foreach (KeyValuePair<Time, string> item in GetPairsWind(wind))
                sity.Forecast[item.Key].Wind = item.Value;
        }

        private Dictionary<Time, string> GetPairsWind(HtmlNode wind)
        {
            HtmlNode widgetWind = wind.Descendants()
                .Where(x => x.HasAttribute(Class) && x.Attributes[Class].Value == WidgetRowWind)
                .FirstOrDefault();

            if (widgetWind == null)
                throw new NotFoundException(nameof(HtmlNode));

            string[] array = widgetWind.Descendants()
                .Where(x => x.HasAttribute(Class) && x.Attributes[Class].Value == Unit)
                .Select(x => x.InnerText)
                .ToArray();

            return WeatherHelper.GetWeaterAsPairs(array);
        }
    }
}

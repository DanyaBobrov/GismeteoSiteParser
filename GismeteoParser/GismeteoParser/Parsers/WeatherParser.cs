using GismeteoParser.Exceptions;
using GismeteoParser.Extensions;
using GismeteoParser.Weathers;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace GismeteoParser.Parsers
{
    internal class WeatherParser : BaseParser
    {
        private const string AdditionPath = "tomorrow/";
        private const string Section = "section";
        private const string DataWidgetId = "data-widget-id";

        private City _city;

        public WeatherParser(City city) : base(Path + city.Url + AdditionPath)
        {
            _city = city;
        }

        public void Run(IEnumerable<IWeather> weathers) 
        {
            HtmlNode section = FirstDescendant(Section);
            if (section == null)
                throw new NotFoundException(nameof(HtmlNode));

            var weatherContainer = section.Descendants().Where(y => y.HasAttribute(DataWidgetId));

            foreach (IWeather item in weathers)
                item.LookWeatherForecast(_city, weatherContainer);
        }
    }
}

using HtmlAgilityPack;
using System.Collections.Generic;

namespace GismeteoParser.Weathers
{
    public interface IWeather
    {
        void LookWeatherForecast(City sity, IEnumerable<HtmlNode> weatherContainer);
    }
}

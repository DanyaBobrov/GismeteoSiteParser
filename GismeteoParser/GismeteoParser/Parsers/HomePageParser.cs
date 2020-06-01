using GismeteoParser.Exceptions;
using GismeteoParser.Extensions;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace GismeteoParser.Parsers
{
    internal class HomePageParser : BaseParser
    {
        private const string Noscript = "noscript";
        private const string Id = "id";

        public HomePageParser() : base() { }

        public IEnumerable<City> GetCitiesOnHomePage() 
        {
            HtmlNode nodeWithCities = Descendants(Noscript)
                .Where(x => x.HasAttribute(Id))
                .FirstOrDefault(x => x.Attributes[Id].Value == Noscript);

            if (nodeWithCities == null)
                throw new NotFoundException(nameof(HtmlNode));

            return GetCities(nodeWithCities);
        }

        private IEnumerable<City> GetCities(HtmlNode node)
        {
            var result = new List<City>();
            foreach (HtmlNode item in node.ChildNodes)
            {
                if (!item.HasAttribute(City.DataId) || !item.HasAttribute(City.DataUrl))
                    continue;

                City sity = new City()
                {
                    Id =long.Parse(item.Attributes[City.DataId].Value),
                    Url = item.Attributes[City.DataUrl].Value,
                    Name = item.HasAttribute(City.DataName) ? item.Attributes[City.DataName].Value : string.Empty
                };
                result.Add(sity);
            }
            return result;
        }
    }
}

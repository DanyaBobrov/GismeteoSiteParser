using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace GismeteoParser.Parsers
{
    internal abstract class BaseParser
    {
        protected const string Path = "https://www.gismeteo.ru/";

        private HtmlDocument _document;

        protected BaseParser() : this(Path) { }

        protected BaseParser(string path) 
        {
            _document = new HtmlWeb().Load(path);
        }

        protected HtmlNode FirstDescendant() => _document.DocumentNode.Descendants().FirstOrDefault();
        protected HtmlNode FirstDescendant(string name) => _document.DocumentNode.Descendants(name).FirstOrDefault();
        protected IEnumerable<HtmlNode> Descendants() => _document.DocumentNode.Descendants();
        protected IEnumerable<HtmlNode> Descendants(string name) => _document.DocumentNode.Descendants(name);
    }
}

using HtmlAgilityPack;

namespace GismeteoParser.Extensions
{
    public static class HtmlNodeExtension
    {
        public static bool HasAttribute(this HtmlNode node, string name) 
        {
            return node != null 
                && node.HasAttributes 
                && node.Attributes[name] != null;
        }
    }
}

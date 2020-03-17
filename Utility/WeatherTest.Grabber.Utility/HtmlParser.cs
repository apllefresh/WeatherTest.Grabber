using HtmlAgilityPack;

namespace WeatherTest.Grabber.Utility
{
    public static class HtmlParser
    {
        public static HtmlNodeCollection GetNodesByXpathExpression(HtmlNode node, string expression)
        {
            return node.SelectNodes(expression);
        }
    }
}
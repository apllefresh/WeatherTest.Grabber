using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using WeatherTest.Grabber.Utility.Models;

namespace WeatherTest.Grabber.Utility
{
    public static class HtmlParser
    {
        public static HtmlNode GetSingleNode(HtmlNode htmlNode,TagSelector selector)
        {
            var expression = XpathExpressionBuilder.GetExpressionByTag(selector);
            return htmlNode.SelectSingleNode(expression);
        }

        public static IEnumerable<HtmlNode> GetNodes(HtmlNode htmlNode, IEnumerable<TagSelector> tagSelectors)
        {
            var expression = XpathExpressionBuilder.GetExpressionByTags(tagSelectors);
            return htmlNode.SelectNodes(expression);
        }

        public static string[] GetValues(HtmlNode htmlNode, IEnumerable<TagSelector> tagSelectors)
        {
            var expression = XpathExpressionBuilder.GetExpressionByTags(tagSelectors);

            return htmlNode.SelectNodes(expression)
                .Select(e => e.InnerHtml)
                .ToArray();
        }
    }
}
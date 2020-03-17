using System.Collections.Generic;
using System.Linq;

namespace WeatherTest.Grabber.Utility
{
    public static class XpathExpressionBuilder
    {
        private static string GetContainExpression(string property, string value)
        {
            return $"[contains(@{property}, '{value}')]";
        }

        private static string GetMultiContainExpression(IEnumerable<(string, string)> propertyValueList, bool isAnyCondition)
        {
            return propertyValueList.Aggregate(
                string.Empty,
                (current, tuple) => current + 
                                    ((current.Length > 0 ? (isAnyCondition ? " or " : " and ") : "") +
                                    $"[contains(@{tuple.Item1}, '{tuple.Item2}')]"));
        }

        public static string GetContainExpressionByTag(string property, string value, HtmlElementTag elementTag = HtmlElementTag.Div)
        {
            return $"//{elementTag.ToString().ToLower()}{GetContainExpression(property, value)}";
        }
        
        public static string GetMultiContainExpressionByTag(IEnumerable<(string, string)> propertyValueList, bool isAnyCondition, HtmlElementTag elementTag = HtmlElementTag.Div)
        {
            return $"//{elementTag.ToString().ToLower()}{GetMultiContainExpression(propertyValueList, isAnyCondition)}";
        }

    }
}
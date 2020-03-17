using System.Collections.Generic;
using System.Linq;

namespace WeatherTest.Grabber.Utility
{
    public static class XpathExpressionBuilder
    {
        private static string GetExpression(IEnumerable<TagProperty> propertyValueList)
        {
            var expression = propertyValueList.Aggregate(
                string.Empty,
                (current, tuple) => current +
                                    ((current.Length > 0 ? " and " : "") +
                                     $"contains(@{tuple.Name}, '{tuple.Value}')"));

            return $"[{expression}]";
        }

        public static string GetExpressionByTag(TagSelector elementTag)
        {
            return $"//{elementTag.Tag.ToString().ToLower()}{GetExpression(elementTag.Properties)}";
        }

        public static string GetExpressionByTags(IEnumerable<TagSelector> propertyValuesSelector)
        {
            var expression = "";
            foreach (var selector in propertyValuesSelector)
            {
                expression += $"//{selector.Tag.ToString().ToLower()}";
                if (selector.Properties != null && selector.Properties.Any())
                {
                    expression += $"{GetExpression(selector.Properties)}";
                }
            }

            return expression;
        }
    }
}
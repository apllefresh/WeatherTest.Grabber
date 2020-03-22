using System.Collections.Generic;
using WeatherTest.Grabber.BusinessLogic.Contract.Services;
using WeatherTest.Grabber.Utility;

namespace WeatherTest.Grabber.BusinessLogic.Services
{
    // TODO: move setting to db
    public class SettingService : ISettingService
    {
        public string GetCityCatalogPage()
        {
            return @"https://www.gismeteo.ru/catalog/russia/";
        }

        public string GetDomainUrlPath()
        {
            return $"https://www.gismeteo.ru";
        }

        public string GetMinusControlChar()
        {
            return "&minus;";
        }

        public TagSelector GetTagSelectorForCityWeatherParentNode()
        {
            return new TagSelector
            {
                Tag = HtmlElementTag.Div,
                Properties = new List<TagProperty>
                {
                    new TagProperty()
                    {
                        Name = "class",
                        Value = "widget js_widget"
                    },
                    new TagProperty
                    {
                        Name = "data-widget-id",
                        Value = "forecast"
                    }
                }
            };
        }

        public IEnumerable<TagSelector> GetTagSelectorsForCityWeatherDegree(TagSelector parentNodeSelector)
        {
            return new List<TagSelector>
            {
                parentNodeSelector,
                new TagSelector
                {
                    Tag = HtmlElementTag.Span,
                    Properties = new List<TagProperty>
                    {
                        new TagProperty
                        {
                            Name = "class",
                            Value = "unit unit_temperature_c"
                        }
                    }
                }
            };
        }

        public IEnumerable<TagSelector> GetTagSelectorsForCityWeatherTime(TagSelector parentNodeSelector)
        {
            return new List<TagSelector>
            {
                parentNodeSelector,
                new TagSelector
                {
                    Tag = HtmlElementTag.Div,
                    Properties = new List<TagProperty>
                    {
                        new TagProperty
                        {
                            Name = "class",
                            Value = "w_time"
                        }
                    }
                },
                new TagSelector
                {
                    Tag = HtmlElementTag.Span
                }
            };
        }

        public IEnumerable<TagSelector> GetTagSelectorsForParseCities()
        {
            return new List<TagSelector>
            {
                new TagSelector
                {
                    Tag = HtmlElementTag.Section,
                    Properties = new List<TagProperty>
                    {
                        new TagProperty
                        {
                            Name = "class",
                            Value = "catalog_block catalog_popular"
                        }
                    }
                },
                new TagSelector
                {
                    Tag = HtmlElementTag.A
                }
            };
        }

        public string GetTomorrowUrlPostfix()
        {
            return "/tomorrow/";
        }
    }
}

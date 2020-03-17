using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using WeatherTest.Grabber.BusinessLogic.Contract.Models;
using WeatherTest.Grabber.BusinessLogic.Contract.Services;
using WeatherTest.Grabber.Utility;

namespace WeatherTest.Grabber.BusinessLogic.Services
{
    public class CityService : ICityService
    {
        private readonly string _url;
        private readonly HtmlWeb _web;
        private readonly IEnumerable<TagSelector> _tagSelector;

        public CityService()
        {
            _url = @"https://www.gismeteo.ru/catalog/russia/";
            _web = new HtmlWeb();
            _tagSelector = new List<TagSelector>
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

        public IEnumerable<City> Get()
        {
            var doc = _web.Load(_url);

            var nodes = HtmlParser.GetNodes(doc.DocumentNode, _tagSelector);

            return nodes.Select(n => new City
                {
                    Name = ParseCityName(n.InnerHtml),
                    Url = ParseCityUrl(n.Attributes["href"]?.Value)
                })
                .ToList();
        }

        private string ParseCityUrl(string value)
        {
            return $"https://www.gismeteo.ru{value}";
        }

        private string ParseCityName(string value)
        {
            return value
                .Replace("\n", "")
                .Trim();
        }

        public Task UpdateCities(IEnumerable<City> cities)
        {
            throw new System.NotImplementedException();
        }
    }
}
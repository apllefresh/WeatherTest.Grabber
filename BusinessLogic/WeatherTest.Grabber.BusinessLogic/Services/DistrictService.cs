using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using WeatherTest.Grabber.BusinessLogic.Contract.Models;
using WeatherTest.Grabber.BusinessLogic.Contract.Services;
using WeatherTest.Grabber.Utility;

namespace WeatherTest.Grabber.BusinessLogic.Services
{
    public class DistrictService : IDistrictService
    {
        private readonly string _url;
        private readonly HtmlWeb _web;
        private readonly IEnumerable<TagSelector> _tagSelector;

        public DistrictService()
        {
            _web = new HtmlWeb();
            _url = @"https://www.gismeteo.ru/catalog/russia/";
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
                            Value = "catalog_block"
                        }
                    }
                },
                new TagSelector
                {
                    Tag = HtmlElementTag.Div,
                    Properties = new List<TagProperty>
                    {
                        new TagProperty
                        {
                            Name = "class",
                            Value = "catalog_item"
                        }
                    }
                },
                new TagSelector
                {
                    Tag = HtmlElementTag.A
                    
                }
            };
        }

        public async Task<IEnumerable<District>> Get()
        {
            var doc = await _web.LoadFromWebAsync(_url);

            var nodes = HtmlParser.GetNodes(doc.DocumentNode, _tagSelector);

            return nodes.Select(n => new District
                {
                    Name = ParseDistrict(n.InnerHtml),
                    Url = ParseUrl(n.Attributes["href"]?.Value)
                })
                .ToList();
        }

        private string ParseDistrict(string value)
        {
            return value
                .Replace("\n","")
                .Trim();
        }
        
        private string ParseUrl(string value)
        {
            return $"https://www.gismeteo.ru{value}";
        }
    }
}
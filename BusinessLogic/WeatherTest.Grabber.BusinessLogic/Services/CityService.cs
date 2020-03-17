using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HtmlAgilityPack;
using WeatherTest.Grabber.BusinessLogic.Contract.Models;
using WeatherTest.Grabber.BusinessLogic.Contract.Services;
using WeatherTest.Grabber.DataAccess.Contract.Repositories;
using WeatherTest.Grabber.Utility;

namespace WeatherTest.Grabber.BusinessLogic.Services
{
    public class CityService : ICityService
    {
        private readonly string _url;
        private readonly HtmlWeb _web;
        private readonly IEnumerable<TagSelector> _tagSelector;
        private readonly ICityRepository _repository;
        private readonly IMapper _mapper;

        public CityService(ICityRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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

        public async Task<IEnumerable<City>> UpdateCities(IEnumerable<City> cities)
        {
            var result = await _repository.Update(_mapper.Map<IEnumerable<DataAccess.Contract.Models.City>>(cities));
            return _mapper.Map<IEnumerable<City>>(result);
        }
    }
}
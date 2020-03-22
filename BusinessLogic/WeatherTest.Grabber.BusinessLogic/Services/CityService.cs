using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using WeatherTest.Grabber.BusinessLogic.Contract.Models;
using WeatherTest.Grabber.BusinessLogic.Contract.Services;
using WeatherTest.Grabber.DataAccess.Contract.Repositories;
using WeatherTest.Grabber.Utility;
using WeatherTest.Grabber.Utility.Models;

namespace WeatherTest.Grabber.BusinessLogic.Services
{
    public class CityService : ICityService
    {
        private readonly string _url;
        private readonly HtmlWeb _web;
        private readonly IEnumerable<TagSelector> _tagSelector;
        private readonly string _domainUrlPath;

        private readonly ICityRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CityService> _logger;
        private readonly ISettingService _settingService;

        public CityService(
            ICityRepository repository,
            IMapper mapper,
            ILogger<CityService> logger,
            ISettingService settingService)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _settingService = settingService;

            _url = _settingService.GetCityCatalogPage();
            _web = new HtmlWeb();
            _tagSelector = _settingService.GetTagSelectorsForParseCities();
            _domainUrlPath = _settingService.GetDomainUrlPath();
        }

        public IEnumerable<City> Get()
        {
            try
            {
                var doc = _web.Load(_url);

                var nodes = HtmlParser.GetNodes(doc.DocumentNode, _tagSelector);

                return nodes.Select(n => new City
                    (
                        id: 0,
                        name: ParseCityName(n.InnerHtml),
                        url: ParseCityUrl(n.Attributes["href"]?.Value)
                    ))
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Catch error when parse cities");
                _logger.LogError($"Error:{ex.Message}");
                throw;
            }
        }

        private string ParseCityUrl(string value)
        {
            return $"{_domainUrlPath}{value}";
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
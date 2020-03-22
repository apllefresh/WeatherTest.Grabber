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
    public class CityWeatherService : ICityWeatherService
    {
        private readonly TagSelector _parentNodeSelector;
        private readonly IEnumerable<TagSelector> _childTimeSelector;
        private readonly IEnumerable<TagSelector> _childTemperatureSelector;
        private readonly HtmlWeb _web;
        private readonly string _tomorrowUrlPostfix;
        private readonly string _minusControlChar;

        private readonly ICityWeatherRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CityWeatherService> _logger;


        public CityWeatherService(
            ICityWeatherRepository repository,
            IMapper mapper,
            ILogger<CityWeatherService> logger,
            ISettingService settingService)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;

            _web = new HtmlWeb();

            _parentNodeSelector = settingService.GetTagSelectorForCityWeatherParentNode();
            _childTimeSelector = settingService.GetTagSelectorsForCityWeatherTime(_parentNodeSelector);
            _childTemperatureSelector = settingService.GetTagSelectorsForCityWeatherDegree(_parentNodeSelector);
            _tomorrowUrlPostfix = settingService.GetTomorrowUrlPostfix();
            _minusControlChar = settingService.GetMinusControlChar();
        }

        public CityWeather Get(City city)
        {
            _logger.LogInformation($"Parse weather for city: {city.Name}");
            try
            {
                var tomorrow = DateTime.Now.AddDays(1).Date;
                // get page
                var doc = _web.Load($"{city.Url}{_tomorrowUrlPostfix}");

                var parentNode = HtmlParser.GetSingleNode(doc.DocumentNode, _parentNodeSelector);

                var times = HtmlParser.GetValues(parentNode, _childTimeSelector);
                var temperature = HtmlParser.GetValues(parentNode, _childTemperatureSelector);

                var temperatures = times
                    .Select((t, i) => new Temperature
                    (
                        dateTime: tomorrow.AddHours(int.Parse(t)),
                        degree: ParseTemperature(temperature[i])
                    ))
                    .ToList();

                return new CityWeather
                (
                    city: city,
                    temperatures: temperatures
                );
            }
            catch (Exception ex)
            {
                _logger.LogError($"Catch error when parsing city '{city.Name}'");
                _logger.LogError($"Error:{ex.Message}");
                throw;
            }
        }

        public async Task Update(IEnumerable<CityWeather> cityWeathers)
        {
            try
            {
                await _repository.Update(
                    _mapper.Map<IEnumerable<DataAccess.Contract.Models.CityWeather>>(cityWeathers));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Catch error when update city: {cityWeathers.FirstOrDefault()?.City?.Name ?? "N/A"}");
                _logger.LogError($"Error:{ex.Message}");
                throw;
            }
        }

        private int ParseTemperature(string value)
        {
            var stringValue = value.Replace(_minusControlChar, "-");
            if (!int.TryParse(stringValue, out var result))
            {
                throw new InvalidCastException($"Can't parse temperature from value: {value}'");
            }

            return result;
        }
    }
}
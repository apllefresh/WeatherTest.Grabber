using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using WeatherTest.Grabber.BusinessLogic.Contract.Models;
using WeatherTest.Grabber.BusinessLogic.Contract.Services;
using WeatherTest.Grabber.Utility;

namespace WeatherTest.Grabber.BusinessLogic.Services
{
    public class CityWeatherService : ICityWeatherService
    {
        private readonly TagSelector _parentNodeSelector;
        private readonly IEnumerable<TagSelector> _childTimeSelector;
        private readonly IEnumerable<TagSelector> _childTemperatureSelector;
        private readonly HtmlWeb _web;

        public CityWeatherService()
        {
            _web = new HtmlWeb();
            
            _parentNodeSelector = new TagSelector
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

            _childTimeSelector = new List<TagSelector>
            {
                _parentNodeSelector,
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
            
            _childTemperatureSelector = new List<TagSelector>
            {
                _parentNodeSelector,
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

        public CityWeather Get(City city)
        {
            var today = DateTime.Now.Date;
            // get page
            var doc = _web.Load(city.Url);

            var parentNode = HtmlParser.GetSingleNode(doc.DocumentNode, _parentNodeSelector);

            var times = HtmlParser.GetValues(parentNode,_childTimeSelector);
            var temperature = HtmlParser.GetValues(parentNode, _childTemperatureSelector);

            var temperatures = times
                .Select((t, i) => new Temperature
                {
                    DateTime = today.AddHours(int.Parse(t)),
                    Degree = ParseTemperature(temperature[i])
                })
                .ToList();
            
            return new CityWeather
            {
                City = city,
                Temperatures = temperatures
            };
        }

        public Task Update(CityWeather cityWeather)
        {
            throw new System.NotImplementedException();
        }

        private int ParseTemperature(string value)
        {
            var stringValue = value.Replace("&minus;", "-");
            if (!int.TryParse(stringValue, out var result))
            {
                throw new InvalidCastException($"Can't parse temperature from value: {value}'");
            }

            return result;
        }
    }
}
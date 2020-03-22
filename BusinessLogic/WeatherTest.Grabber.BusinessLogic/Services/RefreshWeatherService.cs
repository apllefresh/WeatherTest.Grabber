using System.Linq;
using WeatherTest.Grabber.BusinessLogic.Contract.Services;
using Microsoft.Extensions.Logging;

namespace WeatherTest.Grabber.BusinessLogic.Services
{
    public class RefreshWeatherService : IRefreshWeatherService
    {
        private readonly ICityService _cityService;
        private readonly ICityWeatherService _cityWeatherService;
        private readonly ILogger<RefreshWeatherService> _loger;

        public RefreshWeatherService(ICityService cityService, ICityWeatherService cityWeatherService, ILogger<RefreshWeatherService> logger)
        {
            _cityService = cityService;
            _cityWeatherService = cityWeatherService;
            _loger = logger;
        }

        public void RefreshWeather()
        {
            _loger.LogInformation("Start refresh wether data");
            _loger.LogInformation("Start parse cities");
            var cities = _cityService.Get();

            var cityList = cities.ToList();
            _loger.LogInformation($"Parsed cities count: {cities.Count()} ");
            _loger.LogInformation("Update cities");
            var actualCities = _cityService.UpdateCities(cityList)
                .GetAwaiter()
                .GetResult();

            _loger.LogInformation("Start parse city weather");
            var cityWeathers = actualCities
                .Select(city => _cityWeatherService.Get(city))
                .Where(cityWeather => cityWeather != null)
                .ToList();

            _loger.LogInformation($"Parsed weather data count: {cityWeathers.Count()}");
            _loger.LogInformation("Update city weather");
            _cityWeatherService.Update(cityWeathers)
                .GetAwaiter()
                .GetResult();
            
            _loger.LogInformation("End refresh wether data");
        }
    }
}
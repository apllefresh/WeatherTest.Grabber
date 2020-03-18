using System.Linq;
using WeatherTest.Grabber.BusinessLogic.Contract.Services;

namespace WeatherTest.Grabber.BusinessLogic.Services
{
    public class RefreshWeatherService : IRefreshWeatherService
    {
        private readonly ICityService _cityService;
        private readonly ICityWeatherService _cityWeatherService;

        public RefreshWeatherService(ICityService cityService, ICityWeatherService cityWeatherService)
        {
            _cityService = cityService;
            _cityWeatherService = cityWeatherService;
        }

        public void RefreshWeather()
        {
            var cities = _cityService.Get();

            var cityList = cities.ToList();
            var actualCities = _cityService.UpdateCities(cityList)
                .GetAwaiter()
                .GetResult();

            var cityWeathers = actualCities
                .Select(city => _cityWeatherService.Get(city))
                .Where(cityWeather => cityWeather != null)
                .ToList();

            _cityWeatherService.Update(cityWeathers)
                .GetAwaiter()
                .GetResult();
        }
    }
}
using System.Threading.Tasks;
using WeatherTest.Grabber.BusinessLogic.Contract.Models;
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
            var cities = _cityService.GetCities()
                .GetAwaiter()
                .GetResult();
            
            _cityService.UpdateCities(cities)
                .GetAwaiter()
                .GetResult();
            
            foreach (var city in cities)
            {
                var task = Task.Run(async () => 
                        await _cityWeatherService.Get(new City
                                {
                                    Name = "Saint-Petersburg",
                                    Url = @"https://www.gismeteo.ru/weather-sankt-peterburg-4079/"
                                })
                    ).GetAwaiter()
                    .GetResult();
                
                var cityWeather = _cityWeatherService.Get(city)
                    .GetAwaiter()
                    .GetResult();
                
                _cityWeatherService.Update(cityWeather)
                    .GetAwaiter()
                    .GetResult();
            }
        }
    }
}
using Microsoft.Extensions.Logging;
using Quartz;
using System.Linq;
using System.Threading.Tasks;
using WeatherTest.Grabber.BusinessLogic.Contract.Services;

namespace WeatherTest.Grabber.Jobs
{
    public class RefreshWeatherJob : IJob
    {
        private readonly ICityService _cityService;
        private readonly ICityWeatherService _cityWeatherService;
        private readonly ILogger<RefreshWeatherJob> _loger;

        public RefreshWeatherJob(ICityService cityService, ICityWeatherService cityWeatherService, ILogger<RefreshWeatherJob> logger)
        {
            _cityService = cityService;
            _cityWeatherService = cityWeatherService;
            _loger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _loger.LogInformation("Start refresh weather data");
            _loger.LogInformation("Start parse cities");
            var cities = _cityService.Get();

            var cityList = cities.ToList();
            _loger.LogInformation($"Parsed cities count: {cityList.Count()} ");
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

            _loger.LogInformation("End refresh weather data");
            return Task.FromResult(true);
        }
    }
}

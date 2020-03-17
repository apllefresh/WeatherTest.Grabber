using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherTest.Grabber.BusinessLogic.Contract.Models;

namespace WeatherTest.Grabber.BusinessLogic.Contract.Services
{
    public interface ICityWeatherService
    {
        CityWeather Get(City city);
        Task Update(IEnumerable<CityWeather> cityWeather);
    }
}
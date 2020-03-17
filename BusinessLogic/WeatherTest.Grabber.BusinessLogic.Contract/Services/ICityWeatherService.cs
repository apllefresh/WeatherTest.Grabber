using System.Threading.Tasks;
using WeatherTest.Grabber.BusinessLogic.Contract.Models;

namespace WeatherTest.Grabber.BusinessLogic.Contract.Services
{
    public interface ICityWeatherService
    {
        Task<CityWeather> Get(City city);
        Task Update(CityWeather cityWeather);
    }
}
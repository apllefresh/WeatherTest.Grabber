using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherTest.Grabber.DataAccess.Contract.Models;

namespace WeatherTest.Grabber.DataAccess.Contract.Repositories
{
    public interface ICityWeatherRepository
    {
        Task Update(IEnumerable<CityWeather> cityWeathers);
    }
}
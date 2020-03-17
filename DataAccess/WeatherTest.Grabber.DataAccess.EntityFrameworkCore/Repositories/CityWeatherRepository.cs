using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherTest.Grabber.DataAccess.Contract.Models;
using WeatherTest.Grabber.DataAccess.Contract.Repositories;

namespace WeatherTest.Grabber.DataAccess.EntityFrameworkCore.Repositories
{
    public class CityWeatherRepository : ICityWeatherRepository
    {
        public Task Update(IEnumerable<CityWeather> cityWeathers)
        {
            throw new System.NotImplementedException();
        }
    }
}
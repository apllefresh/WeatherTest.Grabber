using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherTest.Grabber.BusinessLogic.Contract.Models;
using WeatherTest.Grabber.BusinessLogic.Contract.Services;

namespace WeatherTest.Grabber.BusinessLogic.Services
{
    public class CityService : ICityService
    {
        public Task<IEnumerable<City>> GetCities()
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateCities(IEnumerable<City> cities)
        {
            throw new System.NotImplementedException();
        }
    }
}
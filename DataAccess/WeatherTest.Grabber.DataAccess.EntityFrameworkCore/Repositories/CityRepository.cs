using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherTest.Grabber.DataAccess.Contract.Models;
using WeatherTest.Grabber.DataAccess.Contract.Repositories;

namespace WeatherTest.Grabber.DataAccess.EntityFrameworkCore.Repositories
{
    public class CityRepository : ICityRepository
    {
        public Task<List<City>> Update(IEnumerable<City> cities)
        {
            throw new System.NotImplementedException();
        }
    }
}
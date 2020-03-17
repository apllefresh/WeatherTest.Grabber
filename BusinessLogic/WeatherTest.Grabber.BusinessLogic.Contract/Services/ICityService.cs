using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherTest.Grabber.BusinessLogic.Contract.Models;

namespace WeatherTest.Grabber.BusinessLogic.Contract.Services
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetCities();
        Task UpdateCities(IEnumerable<City> cities);
    }
}
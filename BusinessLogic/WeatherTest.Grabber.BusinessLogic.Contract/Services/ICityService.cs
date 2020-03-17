using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherTest.Grabber.BusinessLogic.Contract.Models;

namespace WeatherTest.Grabber.BusinessLogic.Contract.Services
{
    public interface ICityService
    {
        IEnumerable<City> Get();
        Task<IEnumerable<City>> UpdateCities(IEnumerable<City> cities);
    }
}
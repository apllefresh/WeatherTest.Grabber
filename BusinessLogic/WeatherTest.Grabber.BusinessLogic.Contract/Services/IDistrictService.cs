using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherTest.Grabber.BusinessLogic.Contract.Models;

namespace WeatherTest.Grabber.BusinessLogic.Contract.Services
{
    public interface IDistrictService
    {
        Task<IEnumerable<District>> Get();
    }
}
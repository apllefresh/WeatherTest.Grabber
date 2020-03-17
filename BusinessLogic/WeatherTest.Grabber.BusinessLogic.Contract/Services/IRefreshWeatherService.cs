using System.Threading.Tasks;
using WeatherTest.Grabber.BusinessLogic.Contract.Models;

namespace WeatherTest.Grabber.BusinessLogic.Contract.Services
{
    public interface IRefreshWeatherService
    {
        Task RefreshWeather();
    }
}
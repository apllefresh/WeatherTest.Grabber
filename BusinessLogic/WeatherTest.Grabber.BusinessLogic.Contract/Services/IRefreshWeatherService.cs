using System.Threading.Tasks;

namespace WeatherTest.Grabber.BusinessLogic.Contract.Services
{
    public interface IRefreshWeatherService
    {
        Task RefreshWeather();
    }
}
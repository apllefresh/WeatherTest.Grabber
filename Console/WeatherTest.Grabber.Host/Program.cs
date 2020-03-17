using Microsoft.Extensions.DependencyInjection;
using WeatherTest.Grabber.BusinessLogic.Contract.Services;
using WeatherTest.Grabber.BusinessLogic.DI;
using WeatherTest.Grabber.DataAccess.DI;

namespace WeatherTest.Grabber.Host
{
    internal static class Program
    {
        private static void Main()
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddBusinessLogicServices()
                .AddDataAccessServices()
                .BuildServiceProvider();


            var refreshWeatherService = serviceProvider.GetService<IRefreshWeatherService>();

            refreshWeatherService.RefreshWeather();
        }
    }
}
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using WeatherTest.Grabber.BusinessLogic.Contract.Services;
using WeatherTest.Grabber.BusinessLogic.DI;
using WeatherTest.Grabber.DataAccess.DI;
using Microsoft.Extensions.Configuration;

namespace WeatherTest.Grabber.Host
{
    internal static class Program
    {
        private static void Main()
        {
            var builder = new ConfigurationBuilder();

            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddBusinessLogicServices()
                .AddDataAccessServices(config)
                .AddAutoMapper(typeof(BusinessLogicAutoMapperProfile).Assembly)
                .BuildServiceProvider();

            var refreshWeatherService = serviceProvider.GetService<IRefreshWeatherService>();

            refreshWeatherService.RefreshWeather();
        }
    }
}
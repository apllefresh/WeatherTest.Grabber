using Microsoft.Extensions.DependencyInjection;
using WeatherTest.Grabber.BusinessLogic.Contract.Services;
using WeatherTest.Grabber.BusinessLogic.Services;

namespace WeatherTest.Grabber.BusinessLogic.DI
{
    public static class BusinessLogicServicesRegistration
    {
        public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
        {
            services.AddScoped<ICityWeatherService, CityWeatherService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IRefreshWeatherService, RefreshWeatherService>();

            return services;
        }
    }
}
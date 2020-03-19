using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherTest.DataContext.DI;
using WeatherTest.Grabber.DataAccess.Contract.Repositories;
using WeatherTest.Grabber.DataAccess.EntityFrameworkCore.Repositories;

namespace WeatherTest.Grabber.DataAccess.DI
{
    public static class DataAccessServicesRegistration
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ICityWeatherRepository, CityWeatherRepository>();
            services.AddDbContextServices(configuration);

            return services;
        }
    }
}
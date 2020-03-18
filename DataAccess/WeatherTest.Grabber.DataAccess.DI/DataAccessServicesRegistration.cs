using Microsoft.Extensions.DependencyInjection;
using WeatherTest.DataContext;
using WeatherTest.Grabber.DataAccess.Contract.Repositories;
using WeatherTest.Grabber.DataAccess.EntityFrameworkCore.Repositories;

namespace WeatherTest.Grabber.DataAccess.DI
{
    public static class DataAccessServicesRegistration
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ICityWeatherRepository, CityWeatherRepository>();
            services.AddTransient<WeatherTestDbContext>();

            return services;
        }
    }
}
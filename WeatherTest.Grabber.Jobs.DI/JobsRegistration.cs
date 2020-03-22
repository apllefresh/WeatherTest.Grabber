using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace WeatherTest.Grabber.Jobs.DI
{
    public static class JobsRegistration
    {
        public static IServiceCollection AddJobsServices(this IServiceCollection services)
        {
            // Add Quartz services
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            // Add our job
            services.AddSingleton<RefreshWeatherJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(RefreshWeatherJob),
                cronExpression: "1 * * * * ?")); // run every 1 minute

            return services;
        }
    }
}

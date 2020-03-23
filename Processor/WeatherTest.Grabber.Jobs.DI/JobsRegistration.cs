using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace WeatherTest.Grabber.Jobs.DI
{
    public static class JobsRegistration
    {
        private const string JOB_SCHEDULER_INTERVAL_IN_SECONDS_SECTION = "JobSchedulerIntervalInSeconds";
        
        public static IServiceCollection AddJobsServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add Quartz services
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            var jobsConfigurationSection = configuration.GetSection("Jobs");
            var jobSchedulerIntervalInSeconds = int.Parse(jobsConfigurationSection[JOB_SCHEDULER_INTERVAL_IN_SECONDS_SECTION]);
            
            // Add our job
            services.AddSingleton<RefreshWeatherJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(RefreshWeatherJob),
                seconds: jobSchedulerIntervalInSeconds));

            return services;
        }
    }
}

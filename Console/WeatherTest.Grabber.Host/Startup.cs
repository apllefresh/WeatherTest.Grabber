using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeatherTest.Grabber.BusinessLogic.DI;
using WeatherTest.Grabber.DataAccess.DI;
using WeatherTest.Grabber.Jobs.DI;
using Microsoft.Extensions.Logging;

namespace WeatherTest.Grabber.Host
{
    public static class Startup
    {
        public static void ConfigureServices(HostBuilderContext hostBuilderContext, IServiceCollection services)
        {
            services.AddOptions();
            services.AddHostedService<QuartzHostedService>();

            services.AddJobsServices();
            services.AddBusinessLogicServices();
            services.AddDataAccessServices(hostBuilderContext.Configuration);
            services.AddLogging(configure => configure.AddConsole());

            services.AddAutoMapper(typeof(BusinessLogicAutoMapperProfile).Assembly);
        }
    }
}
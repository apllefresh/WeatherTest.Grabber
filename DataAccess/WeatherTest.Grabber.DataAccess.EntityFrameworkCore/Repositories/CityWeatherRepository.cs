using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherTest.DataContext;
using WeatherTest.Grabber.DataAccess.Contract.Models;
using WeatherTest.Grabber.DataAccess.Contract.Repositories;

namespace WeatherTest.Grabber.DataAccess.EntityFrameworkCore.Repositories
{
    public class CityWeatherRepository : ICityWeatherRepository
    {
        private readonly WeatherTestDbContext _dbContext;

        public CityWeatherRepository(WeatherTestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Update(IEnumerable<CityWeather> cityWeathers)
        {
            var entities = new List<DataContext.Entities.Temperature>();
            foreach (var cityWeather in cityWeathers)
            {
                entities.AddRange(cityWeather.Temperatures
                    .Select(temperature => new DataContext.Entities.Temperature
                    {
                        CityId = cityWeather.City.Id,
                        Degree = temperature.Degree,
                        DateTime = temperature.DateTime
                    }));
            }

            await _dbContext.BulkMergeAsync(entities, options => options.ColumnPrimaryKeyExpression = c => new
            {
                c.CityId,
                c.DateTime
            });
        }
    }
}
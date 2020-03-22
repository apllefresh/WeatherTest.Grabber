using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WeatherTest.DataContext;
using WeatherTest.Grabber.DataAccess.Contract.Models;
using WeatherTest.Grabber.DataAccess.Contract.Repositories;

namespace WeatherTest.Grabber.DataAccess.EntityFrameworkCore.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly WeatherTestDbContext _dbContext;
        private readonly ILogger<CityRepository> _logger;

        public CityRepository(WeatherTestDbContext dbContext, ILogger<CityRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<City>> Update(IEnumerable<City> cities)
        {
            _logger.LogInformation($"Update {cities.Count()} cities");
            var entities = cities.Select(c => new DataContext.Entities.City
            {
                Name = c.Name,
                Url = c.Url
            });

            await _dbContext.BulkMergeAsync(entities, options => options.ColumnPrimaryKeyExpression = c => new
            {
                c.Name,
                c.Url
            });
            return await Get();
        }

        private async Task<List<City>> Get()
        {
            var entities = await _dbContext.Cities.ToListAsync();
            return entities.Select(c => new City
                {
                    Id = c.Id,
                    Name = c.Name,
                    Url = c.Url
                })
                .ToList();
        }
    }
}
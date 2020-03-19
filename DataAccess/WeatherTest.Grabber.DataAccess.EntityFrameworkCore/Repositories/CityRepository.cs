using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeatherTest.DataContext;
using WeatherTest.Grabber.DataAccess.Contract.Models;
using WeatherTest.Grabber.DataAccess.Contract.Repositories;

namespace WeatherTest.Grabber.DataAccess.EntityFrameworkCore.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly WeatherTestDbContext _dbContextTemp1;

        public CityRepository(WeatherTestDbContext dbContextTemp1)
        {
            _dbContextTemp1 = dbContextTemp1;
        }

        public async Task<List<City>> Update(IEnumerable<City> cities)
        {
            var entities = cities.Select(c => new DataContext.Entities.City
            {
                Name = c.Name,
                Url = c.Url
            });

            await _dbContextTemp1.BulkMergeAsync(entities, options => options.ColumnPrimaryKeyExpression = c => new
            {
                c.Name,
                c.Url
            });
            return await Get();
        }

        private async Task<List<City>> Get()
        {
            var entities = await _dbContextTemp1.Cities.ToListAsync();
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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WeatherTest.DataContext;
using WeatherTest.Grabber.DataAccess.Contract.Models;
using WeatherTest.Grabber.DataAccess.Contract.Repositories;

namespace WeatherTest.Grabber.DataAccess.EntityFrameworkCore.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly WeatherTestDbContext _dbContext;
        private readonly IMapper _mapper;

        public CityRepository(WeatherTestDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<City>> Update(IEnumerable<City> cities)
        {
            var entities = _mapper.Map<IEnumerable<DataContext.Entities.City>>(cities);

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
            return _mapper.Map<IEnumerable<City>>(entities).ToList();
        }
    }
}
using System.Collections.Generic;
using AutoMapper;
using Entity = WeatherTest.DataContext.Entities;
using DataAccessModel = WeatherTest.Grabber.DataAccess.Contract.Models;

namespace WeatherTest.Grabber.DataAccess.DI
{
    public class DataAccessAutoMapperProfile : Profile
    {
        public DataAccessAutoMapperProfile()
        {
            MapToDataBase();
            MapToDataAccess();
        }

        private void MapToDataAccess()
        {
            CreateMap<Entity.City, DataAccessModel.City>();
            CreateMap<IEnumerable<Entity.City>, List<DataAccessModel.City>>();
        }

        private void MapToDataBase()
        {
            CreateMap<DataAccessModel.City, Entity.City>()
                .ForMember(dest => dest.Temperatures, opt => opt.Ignore());
            CreateMap<IEnumerable<DataAccessModel.City>, List<Entity.City>>();
        }
    }
}
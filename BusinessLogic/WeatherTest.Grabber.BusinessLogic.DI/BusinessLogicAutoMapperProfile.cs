using System.Collections.Generic;
using AutoMapper;
using BusinessModel = WeatherTest.Grabber.BusinessLogic.Contract.Models;
using DataAccessModel = WeatherTest.Grabber.DataAccess.Contract.Models;

namespace WeatherTest.Grabber.BusinessLogic.DI
{
    public class BusinessLogicAutoMapperProfile : Profile
    {
        public BusinessLogicAutoMapperProfile()
        {
            MapToBusiness();
            MapToDataAccess();
        }

        private void MapToDataAccess()
        {
            CreateMap<IEnumerable<BusinessModel.City>, IEnumerable<DataAccessModel.City>>();
            CreateMap<IEnumerable<BusinessModel.CityWeather>, IEnumerable<DataAccessModel.CityWeather>>();
        }

        private void MapToBusiness()
        {
            CreateMap<IEnumerable<DataAccessModel.City>, IEnumerable<BusinessModel.City>>();
        }
    }
}
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
            CreateMap<BusinessModel.City, DataAccessModel.City>();
            CreateMap<List<BusinessModel.City>, List<DataAccessModel.City>>();
            CreateMap<List<BusinessModel.CityWeather>, List<DataAccessModel.CityWeather>>();
        }

        private void MapToBusiness()
        {
            CreateMap<List<DataAccessModel.City>, List<BusinessModel.City>>();
        }
    }
}
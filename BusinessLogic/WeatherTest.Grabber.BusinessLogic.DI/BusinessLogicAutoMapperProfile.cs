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
            CreateMap<BusinessModel.Temperature, DataAccessModel.Temperature>();
            CreateMap<BusinessModel.CityWeather, DataAccessModel.CityWeather>()
                .ForMember(dest => dest.Temperatures, opt => opt.MapFrom(src => src.Temperatures));
            CreateMap<IEnumerable<BusinessModel.City>, List<DataAccessModel.City>>();
            CreateMap<IEnumerable<BusinessModel.CityWeather>, List<DataAccessModel.CityWeather>>();
        }

        private void MapToBusiness()
        {
            CreateMap<DataAccessModel.City, BusinessModel.City>();
            CreateMap<IEnumerable<DataAccessModel.City>, List<BusinessModel.City>>();
        }
    }
}
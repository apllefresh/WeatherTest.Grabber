using System.Collections.Generic;

namespace WeatherTest.Grabber.DataAccess.Contract.Models
{
    public class CityWeather
    {
        public CityWeather(City city, IReadOnlyCollection<Temperature> temperatures)
        {
            City = city;
            Temperatures = temperatures;
        }

        public City City { get; }
        public IReadOnlyCollection<Temperature> Temperatures { get; }
    }
}
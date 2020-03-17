using System.Collections.Generic;

namespace WeatherTest.Grabber.DataAccess.Contract.Models
{
    public class CityWeather
    {
        public City City { get; set; }
        public IReadOnlyCollection<Temperature> Temperatures { get; set; }
    }
}
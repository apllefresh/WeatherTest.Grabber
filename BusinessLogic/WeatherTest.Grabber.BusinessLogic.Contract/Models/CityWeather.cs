using System.Collections.Generic;

namespace WeatherTest.Grabber.BusinessLogic.Contract.Models
{
    public class CityWeather
    {
        public City City { get; set; }
        public IReadOnlyCollection<Temperature> Temperatures { get; set; }
    }
}
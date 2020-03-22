using System;

namespace WeatherTest.Grabber.BusinessLogic.Contract.Models
{
    public class Temperature
    {
        public Temperature(int degree, DateTime dateTime)
        {
            Degree = degree;
            DateTime = dateTime;
        }

        public DateTime DateTime { get; }
        public int Degree { get; }
        
    }
}
using System;

namespace WeatherTest.Grabber.DataAccess.Contract.Models
{
    public class Temperature
    {
        public Temperature(DateTime dateTime, int degree)
        {
            DateTime = dateTime;
            Degree = degree;
        }

        public DateTime DateTime { get;}
        public int Degree { get;}
        
    }
}
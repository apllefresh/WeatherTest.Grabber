using System;

namespace WeatherTest.Grabber.Jobs
{
    public class JobSchedule
    {
        public JobSchedule(Type jobType, int seconds)
        {
            JobType = jobType;
            Seconds = seconds;
        }

        public Type JobType { get; }
        public int Seconds { get; }
    }
}

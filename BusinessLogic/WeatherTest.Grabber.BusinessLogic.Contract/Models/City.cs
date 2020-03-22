namespace WeatherTest.Grabber.BusinessLogic.Contract.Models
{
    public class City
    {
        public City(int id, string name, string url)
        {
            Id = id;
            Name = name;
            Url = url;
        }

        public int Id { get; }
        public string Name { get; }
        public string Url { get; }
    }
}
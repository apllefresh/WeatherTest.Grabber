using System.Collections.Generic;

namespace WeatherTest.Grabber.Utility.Models
{
    public class TagSelector
    {
        public HtmlElementTag Tag { get; set; }
        public IEnumerable<TagProperty> Properties { get; set; }
    }
}
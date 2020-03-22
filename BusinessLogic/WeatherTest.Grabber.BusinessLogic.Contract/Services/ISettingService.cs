using System.Collections.Generic;
using WeatherTest.Grabber.Utility;

namespace WeatherTest.Grabber.BusinessLogic.Contract.Services
{
    public interface ISettingService
    {
        string GetCityCatalogPage();
        IEnumerable<TagSelector> GetTagSelectorsForParseCities();
        string GetDomainUrlPath();
        TagSelector GetTagSelectorForCityWeatherParentNode();
        IEnumerable<TagSelector> GetTagSelectorsForCityWeatherTime(TagSelector parentNodeSelector);
        IEnumerable<TagSelector> GetTagSelectorsForCityWeatherDegree(TagSelector parentNodeSelector);
        string GetTomorrowUrlPostfix();
        string GetMinusControlChar();
    }
}

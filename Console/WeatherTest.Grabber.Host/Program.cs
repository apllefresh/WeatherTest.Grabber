using HtmlAgilityPack;
using System;
using System.Linq;

namespace WeatherTest.Grabber.Host
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var url = @"https://www.gismeteo.ru/weather-sankt-peterburg-4079/";

            //var url = "http://html-agility-pack.net/";
            var web = new HtmlWeb();
            var doc = web.Load(url);

            //var nodes = doc.DocumentNode.SelectNodes("//td[@class = 'widget__container']]");
            
            var t = doc.DocumentNode.SelectNodes("//div[contains(@class, 'widget js_widget')]");
            var p = t.Select(s => s.Id).ToList();
            //foreach (var node in nodes)
            //    Console.WriteLine(node.InnerText);

        }
    }
}

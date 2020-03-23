using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using WeatherTest.Grabber.Utility;
using WeatherTest.Grabber.Utility.Models;
using Xunit;

namespace GrabberTests
{
    public class HtmlParserTests
    {
        private readonly HtmlDocument _htmlDocument;
        
        public HtmlParserTests()
        {
            _htmlDocument = new HtmlDocument();
            _htmlDocument.Load(@"HtmlPages\TestHtmlPage.html");
        }
        
        [Fact]
        public void GetSingleNodeTest()
        {
            var selector = new TagSelector
            {
                Tag = HtmlElementTag.Div,
                Properties = new List<TagProperty>()
                {
                    new TagProperty
                    {
                        Name = "class",
                        Value = "div4"
                    }
                }
            };
            var result = HtmlParser.GetSingleNode(_htmlDocument.DocumentNode, selector);

            Assert.NotNull(result);
            Assert.Equal("<a href=\"http://url\"></a>", result.InnerHtml.Trim());
        }

        [Fact]
        public void GetNodesTest()
        {
            var selector = new List<TagSelector>
            {
                new TagSelector
                {
                    Tag = HtmlElementTag.Div,
                    Properties = new List<TagProperty>()
                    {
                        new TagProperty
                        {
                            Name = "class",
                            Value = "div3"
                        }
                    }
                },
                new TagSelector
                {
                    Tag = HtmlElementTag.Span,
                }
            };
            var result = HtmlParser.GetNodes(_htmlDocument.DocumentNode, selector).ToList();

            Assert.NotEmpty(result);
            Assert.Equal(4, result.Count());
            Assert.Equal("<span id=\"1\">5</span>",result.First().OuterHtml.Trim());
        }
        
        [Fact]
        public void GetValuesTest()
        {
            var selector = new List<TagSelector>
            {
                new TagSelector
                {
                    Tag = HtmlElementTag.Section,
                    Properties = new List<TagProperty>()
                    {
                        new TagProperty
                        {
                            Name = "class",
                            Value = "section1"
                        }
                    }
                },
                new TagSelector
                {
                    Tag = HtmlElementTag.Div,
                    Properties = new List<TagProperty>()
                    {
                        new TagProperty
                        {
                            Name = "class",
                            Value = "div3"
                        }
                    }
                },
                new TagSelector
                {
                    Tag = HtmlElementTag.A,
                },
                new TagSelector
                {
                    Tag = HtmlElementTag.Span,
                }
            };
            var result = HtmlParser.GetValues(_htmlDocument.DocumentNode, selector).ToList();

            Assert.NotEmpty(result);
            Assert.Equal(4, result.Count());
            Assert.Equal("5", result[0]);
            Assert.Equal("8", result[3]);
        }
    }
}
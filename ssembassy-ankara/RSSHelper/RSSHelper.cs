using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using ssembassy_ankara.Models;

namespace ssembassy_ankara.RSSHelper
{
    public class RSSHelper
    {
        public static List<RSSFeed> Read(string url)
        {
            var itemsList = new List<RSSFeed>();
            try
            {
                XDocument xdoc = XDocument.Load(url);
                XNamespace dc = "http://purl.org/dc/elements/1.1/";
                XNamespace content = "http://purl.org/rss/1.0/modules/content/";

                xdoc.Descendants("item").Select(p => new RSSFeed
                {
                    Title = p.Element("title")?.Value,
                    Link = p.Element("link")?.Value,
                    Guid = p.Element("guid")?.Value,
                    DateCreated = p.Element(dc + "date")?.Value,
                    Language = p.Element(dc + "language")?.Value,
                    Description = p.Element("description")?.Value,
                    Subject = p.Element(dc + "subject")?.Value,
                    EncodedContent = p.Element(content + "encoded")?.Value
                }).ToList().ForEach(p =>
                {
                    itemsList.Add(p);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return itemsList;
        }
    }
}
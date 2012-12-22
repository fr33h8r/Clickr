using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace ClickrAPI
{
    public class HtmlHelper
    {
        private const string address = "http://www.dota2.com/heroes/";
        public HtmlDocument document { get; set; }

        public HtmlHelper()
        {
            document = new HtmlDocument();
            document.LoadHtml(new WebClient().DownloadString(address));
        }

        public IEnumerable<HtmlNode> GetHeroesAttributes()
        {
            return document.DocumentNode.Descendants("a")
                           .SelectMany(a => a.Attributes.Where(b => b.Value.Contains("link_")).Select(b => b.OwnerNode));
        }

        public IEnumerable<string> GetHeroesNames()
        {
            return document.DocumentNode.Descendants("a")
                           .SelectMany(a => a.Attributes.Where(b => b.Value.Contains("link_"))
                                             .Select(c => c.Value.Split('_')[1].ToUpperInvariant()));
        }
    }
}

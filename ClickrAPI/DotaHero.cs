using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace ClickrAPI
{
    public class DotaHero
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public string ImgLink { get; set; }
        public List<int> Ultimate { get; set; }

        public List<int> GetUltimateValues()
        {
            var html = new HtmlDocument();
            html.LoadHtml(new WebClient().DownloadString(Link));

            var attributes = HtmlHelper.GetNodesByAttributeValue(html, "div", "cooldown");

            return attributes.SelectMany(a =>
                a.OwnerNode.InnerText.Split(':').Select(b => b.Trim()).ToList())
                    .Last().Split('/').Select(int.Parse).Where(c => c > 15).ToList();
        }
    }
}
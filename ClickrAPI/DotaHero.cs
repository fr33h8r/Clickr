using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace ClickrAPI
{
    public class DotaHero
    {
        private HtmlDocument html;
        public string Name { get; set; }
        public string Link { get; set; }
        public string ImgLink { get; set; }
        public Image Img { get; set; }
        public List<int> Ultimate { get; set; }

        public DotaHero() { }

        public DotaHero(string name, string link)
        {
            Name = name;
            Link = link;
            html = new HtmlDocument();
            html.LoadHtml(new WebClient().DownloadString(link));
        }

        public List<int> GetUltimateValues(string address)
        {
            var html = new HtmlDocument();
            html.LoadHtml(new WebClient().DownloadString(address));

            var attributes = HtmlHelper.GetNodesByAttributeValue(html, "div", "cooldown");

            return attributes.SelectMany(a =>
                a.OwnerNode.InnerText.Split(':').Select(b => b.Trim()).ToList())
                    .Last().Split('/').Select(int.Parse).Where(c => c > 15).ToList();
        }

        public string GetImage(string name)
        {
            var hero = new DotaHero { Name = name };
            var htmlAttribute = HtmlHelper.GetNodesByAttributeValue(html, "a", name).First();

            return htmlAttribute.Value;

//            var childNodes = htmlAttribute.OwnerNode.ChildNodes.ToList();
            //            var values = childNodes.SelectMany(c => c.Attributes.Where(a => a.Name == "src").ToList()).ToList();

//            values.ForEach(pair =>
//            {
//                if (!pair.OwnerNode.Attributes.First(a => a.Name.Contains("id"))
//                         .Value.Contains("hover")) return;
//
//                var stream = new WebClient().OpenRead(pair.Value);
//                hero.Img = Image.FromStream(stream);
//            });

//            hero.Ultimate = GetUltimateValues(hero.Link);
        }
    }
}
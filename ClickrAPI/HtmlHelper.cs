using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace ClickrAPI
{
    public class HtmlHelper
    {
        private const string listHeroesAddress = "http://www.dota2.com/heroes/";
        private HtmlDocument document { get; set; }

        public HtmlHelper()
        {
            document = new HtmlDocument();
            document.LoadHtml(new WebClient().DownloadString(listHeroesAddress));
        }

        public IEnumerable<HtmlNode> GetHeroesNodes()
        {
            return document.DocumentNode.Descendants("a")
                           .SelectMany(a => a.Attributes.Where(b => b.Value.Contains("link_")).Select(b => b.OwnerNode));
        }

        public IEnumerable<string> GetHeroes()
        {
            return document.DocumentNode.Descendants("a")
                           .SelectMany(a => a.Attributes
                               .Where(b => b.Value.Contains("/hero/"))
                               .Select(b => b.Value.Split('/')
                                   .Last(c => !string.IsNullOrEmpty(c))));
        }

        public List<int> GetUltimateValues(string address)
        {
            var html = new HtmlDocument();
            html.LoadHtml(new WebClient().DownloadString(address));

            var attributes = html.DocumentNode.Descendants("div")
                                 .SelectMany(a => a.Attributes.Where(b => b.Value == "cooldown")).ToList();

            return attributes.SelectMany(a =>
                a.OwnerNode.InnerText.Split(':').Select(b => b.Trim()).ToList())
                    .Last().Split('/').Select(int.Parse).Where(c => c > 15).ToList();
        }

        public DotaHero GetHeroInfo(string name)
        {
            var hero = new DotaHero { Name = name };
            var htmlAttribute = document.DocumentNode.Descendants("a")
                .SelectMany(a => a.Attributes.Where(b => b.Value.Contains(name))).First();
            var childNodes = htmlAttribute.OwnerNode.ChildNodes.ToList();

            hero.Link = htmlAttribute.Value;
            var values = childNodes.SelectMany(c => c.Attributes.Where(a => a.Name == "src").ToList()).ToList();

            values.ForEach(pair =>
                               {
                                   if (!pair.OwnerNode.Attributes.First(a => a.Name.Contains("id"))
                                            .Value.Contains("hover")) return;

                                   var stream = new WebClient().OpenRead(pair.Value);
                                   hero.Img = Image.FromStream(stream);
                               });

            hero.Ultimate = GetUltimateValues(hero.Link);
            return hero;
        }
    }

    public class DotaHero
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public Image Img { get; set; }
        public List<int> Ultimate { get; set; }
    }

    class Program
    {
        static void Main()
        {
            Console.ReadLine();
        }
    }
}

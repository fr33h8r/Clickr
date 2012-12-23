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
        private const string address = "http://www.dota2.com/heroes/";
        public HtmlDocument document { get; set; }
        private Hero hero;

        public HtmlHelper()
        {
            hero = new Hero();
            document = new HtmlDocument();
            document.LoadHtml(new WebClient().DownloadString(address));
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

        public Hero GetHeroInfo(string name)
        {
            hero.Name = name;
            var htmlAttribute = document.DocumentNode.Descendants("a")
                .SelectMany(a => a.Attributes.Where(b => b.Value.Contains(name))).First();
            var childNodes = htmlAttribute.OwnerNode.ChildNodes.ToList();

            hero.Link = htmlAttribute.Value;
            var values = childNodes.SelectMany(c => c.Attributes.Where(a => a.Name == "src")
                                                     .ToDictionary(n => n.OwnerNode.Attributes.First(b => b.Name == "id").Value, src => src.Value)).ToList();
            values.ForEach(pair => hero.HeroImage.Add(pair.Key, pair.Value));
            return hero;
        }
    }

    public class Hero
    {
        public Hero() { }
        public Hero(string name, string link, Dictionary<string, string> heroImage)
        {
            Name = name;
            Link = link;
            HeroImage = heroImage;
        }

        public string Name { get; set; }
        public string Link { get; set; }
        public Dictionary<string, string> HeroImage { get; set; }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter hero name: ");
            var input = Console.ReadLine();
            var helper = new HtmlHelper();
            var result = helper.GetHeroesNodes();
            foreach (var htmlNode in result.Where(htmlNode => htmlNode.Attributes[0].Value.Contains(input)))
            {
                Console.WriteLine(new WebClient().DownloadString(htmlNode.Attributes[2].Value));
            }

            Console.ReadLine();
        }
    }
}

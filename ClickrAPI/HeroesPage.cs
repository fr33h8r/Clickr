using System.Collections.Generic;
using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace ClickrAPI
{
    public class HeroesPage
    {
        private const string listHeroesAddress = "http://www.dota2.com/heroes/";
        private HtmlDocument html { get; set; }
        public List<DotaHero> Heroes { get; set; } 

        public HeroesPage()
        {
            Heroes = new List<DotaHero>();
            html = new HtmlDocument();
            html.LoadHtml(new WebClient().DownloadString(listHeroesAddress));
        }

        public string GetHeroImage(string name)
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


        public IEnumerable<HtmlNode> GetHeroesNodes()
        {
            return GetAttributes("a", "link_")
                .Select(attribute => attribute.OwnerNode)
                .ToList();
        }

        public IEnumerable<string> GetListHeroesLinks()
        {
            return GetAttributes("a", "/hero/")
                .Select(attribute => attribute.Value)
                .ToList();
        }

        public IEnumerable<HtmlAttribute> GetAttributes(string node, string value)
        {
            return HtmlHelper.GetNodesByAttributeValue(html, node, value);
        }
    }
}

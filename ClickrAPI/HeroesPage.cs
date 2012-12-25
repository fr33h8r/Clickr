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

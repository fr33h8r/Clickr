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
            return HtmlHelper.GetNodesByAttributeValue(html, "a", "link_")
                .Select(b => b.OwnerNode).ToList();
        }

        public IEnumerable<string> GetListHeroesLinks()
        {
            return HtmlHelper.GetNodesByAttributeValue(html, "a", "/hero/")
                .Select(a => a.Value).ToList();
        }
    }
}

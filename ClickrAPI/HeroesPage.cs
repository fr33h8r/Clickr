using System;
using System.Collections.Generic;
using System.Drawing;
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

        public string GetHeroImageLink(string name)
        {
            var childNodes = GetOneHeroNode(name).ChildNodes.ToList();
            var listOfLinkAttributes = childNodes.Select(node => 
                node.Attributes.Where(attr => 
                    attr.Value.Contains("hphover")))
                    .ToList();
            return listOfLinkAttributes.SelectMany(a => a.Select(b => b.Value)).First();
        }

        public HtmlNode GetOneHeroNode(string name)
        {
            return GetAttributes("a", name).Last().OwnerNode;
        }

        public List<HtmlNode> GetListHeroesNodes()
        {
            return GetAttributes("a", "/hero/")
                .Select(attribute => attribute.OwnerNode)
                .ToList();
        }

        public List<string> GetListHeroesLinks()
        {
            return GetAttributes("a", "/hero/")
                .Select(attribute => attribute.Value)
                .ToList();
        }

        public List<HtmlAttribute> GetAttributes(string node, string value)
        {
            return HtmlHelper.GetNodesByAttributeValue(html, node, value).ToList();
        }
    }
}

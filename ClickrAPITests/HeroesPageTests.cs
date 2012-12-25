using System;
using System.Net;
using ClickrAPI;
using FluentAssertions;
using HtmlAgilityPack;
using Xunit;
using System.Linq;
using Xunit.Extensions;

namespace ClickrAPITests
{
    public class HeroesPageTests
    {
        readonly HeroesPage page = new HeroesPage();

        [Fact]
        public void should_return_list_of_hero_links()
        {
            //act
            var result = page.GetListHeroesLinks();
            //assert
            result.Should().Contain("http://www.dota2.com/hero/Sven/");
        }

        [Fact]
        public void should_return_one_hero_node()
        {
            //act
            var result = page.GetOneHeroNode("Clockwerk");
            //assert
            result.Attributes.ToList().ForEach(attr => Console.Out.WriteLine(attr.Name + " -> " + attr.Value));
        }

        [Fact]
        public void should_return_list_of_heroes_nodes()
        {
            //act
            var result = page.GetListHeroesNodes();
            //assert
            result.Should().NotBeEmpty();
        }

        [Theory]
        [InlineData("Bloodseeker", "http://media.steampowered.com/apps/dota2/images/heroes/bloodseeker_hphover.png")]
        [InlineData("Clockwerk", "http://media.steampowered.com/apps/dota2/images/heroes/rattletrap_hphover.png")]
        public void should_return_list_of_hero_image_links(string name, string expected)
        {
            //act
            var result = page.GetHeroImageLink(name);
            //assert
            result.Should().Be(expected);
        }

        [Theory]
//        [InlineData("http://www.dota2.com/hero/Earthshaker/")]
        [InlineData("http://www.dota2.com/hero/Tiny/")]
        public void should_return_name_of_ability(string address)
        {
            var html = new HtmlDocument();
            html.LoadHtml(new WebClient().DownloadString(address));
            var htmlNodes = html.DocumentNode.Descendants("div").SelectMany(a =>
                a.Attributes.Where(b => b.Value.Contains("abilityHeaderRowDescription")).Select(c => c.OwnerNode)).ToList();
            
            htmlNodes.ForEach(a => Console.Out.WriteLine(a.Id));
            
//            html.DocumentNode.Descendants("h2").Where(a => a.ParentNode.Attributes[0].Value.Contains("ability")).ToList().ForEach(a => Console.Out.WriteLine(a.Name + " -> " + a.InnerText));
        }
    }
}

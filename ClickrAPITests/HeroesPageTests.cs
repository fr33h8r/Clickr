using System;
using System.Collections.Generic;
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
        readonly HeroesPage helper = new HeroesPage();

        [Fact]
        public void should_return_all_values_of_attribute_in_node()
        {
            //act
//            var result = helper.GetNodesByAttributeValue()
        }

        [Fact]
        public void should_get_list_of_heroes()
        {
            //act
            var result = helper.GetListHeroesNames();
            //assert
            var list = result.ToList();
            list.Should().Contain("Tiny");
            list.Count.Should().Be(95);
        }

        [Fact]
        public void should_get_heroes_attributes()
        {
            //act
            var result = helper.GetHeroesNodes();
            //assert
            result.ToList().Should().NotBeEmpty();
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

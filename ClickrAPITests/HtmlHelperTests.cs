using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ClickrAPI;
using FluentAssertions;
using Xunit;
using System.Linq;

namespace ClickrAPITests
{
    public class HtmlHelperTests
    {
        readonly HtmlHelper helper = new HtmlHelper();

        [Fact]
        public void should_get_list_of_heroes()
        {
            //act
            var result = helper.GetHeroes();
            //assert
            var list = result.ToList();
            list.ForEach(Console.Out.WriteLine);
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

        [Fact]
        public void should_show_hero_info()
        {
            //act
            var hero = helper.GetHeroInfo("Earthshaker");
            //assert
            hero.Link.Should().Be("http://www.dota2.com/hero/Earthshaker/");
        }
    }
}

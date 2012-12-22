using System;
using ClickrAPI;
using FluentAssertions;
using HtmlAgilityPack;
using Xunit;
using System.Linq;

namespace ClickrAPITests
{
    public class HtmlHelperTests
    {
        [Fact]
        public void should_get_list_of_heroes()
        {
            //arrange
            var helper = new HtmlHelper();
            //act
            var result = helper.GetHeroesNames();
            //assert
            result.ToList().Should().Contain("TINY");
        }

        [Fact]
        public void should_get_heroes_attributes()
        {
            //arrange
            var helper = new HtmlHelper();
            //act
            var result = helper.GetHeroesAttributes();
            //assert
            result.ToList().ForEach(a => a.Attributes.ToList().ForEach(b => Console.WriteLine(b.Name + " -> " + b.Value)));

            result.ToList().Should().NotBeEmpty();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace ClickrAPI
{
    public static class HtmlHelper
    {
        public static readonly Func<HtmlDocument, string, List<HtmlNode>> Descendants =
            (html, s) => html.DocumentNode.Descendants(s).ToList();

        public static IEnumerable<HtmlAttribute> GetNodesByAttributeValue(HtmlDocument html, string node, string valueOfAttribute)
        {
            return Descendants(html, node)
                .SelectMany(a => a.Attributes
                    .Where(b => b.Value.Contains(valueOfAttribute)))
                    .ToList();
        }

        public static string GetNameFromLink(string link)
        {
            return link.Split('/').Last(c => !string.IsNullOrEmpty(c));
        }
    }
}
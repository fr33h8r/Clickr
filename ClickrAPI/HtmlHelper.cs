using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace ClickrAPI
{
    public static class HtmlHelper
    {
        public static readonly Func<HtmlDocument, string, List<HtmlNode>> Descendants =
            (html, s) => html.DocumentNode.Descendants(s).ToList();

        public static List<HtmlAttribute> GetNodesByAttributeValue(HtmlDocument html, string node, string valueOfAttribute)
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

        public static Image GetImage(string url)
        {
            var stream = new WebClient().OpenRead(url);
            return new Bitmap(stream);
        }
    }
}
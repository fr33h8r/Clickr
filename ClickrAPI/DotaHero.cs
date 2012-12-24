using System.Collections.Generic;
using System.Drawing;

namespace ClickrAPI
{
    public class DotaHero
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public Image Img { get; set; }
        public List<int> Ultimate { get; set; }
    }
}
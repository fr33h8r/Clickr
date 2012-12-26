using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ClickrAPI;

namespace DotaClickr
{
    public partial class Dota2Clickr : Form
    {
        private Dictionary<string, DotaHero> heroes;
        private HeroesPage heroesPage;
        private List<string> heroLinks;

        public Dota2Clickr()
        {
            InitializeComponent();
            InitializeHeroes();
        }

        private void InitializeHeroes()
        {
            heroesPage = new HeroesPage();
            heroes = new Dictionary<string, DotaHero>();
            heroLinks = heroesPage.GetListHeroesLinks().ToList();
            heroLinks.Sort();
            heroLinks.ForEach(link =>
                                  {
                                      var name = HtmlHelper.GetNameFromLink(link);
                                      heroes.Add(name, new DotaHero
                                                           {
                                                               Name = name,
                                                               Link = link,
                                                               ImgLink = heroesPage.GetHeroImageLink(name)
                                                           });
                                  });
        }

        private void button_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var comboItems = heroes.Select(hero => hero.Value.Name as object).ToArray();
            Controls.OfType<ComboBox>().ToList()
                .ForEach(comboBox => comboBox.Items.AddRange(comboItems));
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = (ComboBox) sender;
            
            foreach (var button in Controls.OfType<Button>().ToList())
            {
                if (button.Tag == comboBox.Tag)
                    button.Image = HtmlHelper.GetImage(heroes[comboBox.Text].ImgLink);
//                    foreach (var dotaHero in heroes.Where(dotaHero => dotaHero.Value.Name == comboBox.Text).ToList())
//                        button.Image = HtmlHelper.GetImage(dotaHero.Value.ImgLink);
            }

            foreach (var label in Controls.OfType<Label>())
            {
                
            }

//            var tag = button.Tag.ToString();
//            foreach (var b in Controls.OfType<Button>().Where(b => int.Parse(b.Tag.ToString()) == int.Parse(tag) + 5))
//                b.Text = string.Join("/", dotaHero.GetUltimateValues());
        }
    }
}
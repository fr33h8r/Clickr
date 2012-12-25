using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ClickrAPI;

namespace DotaClickr
{
    public partial class Dota2Clickr : Form
    {
        private readonly List<DotaHero> heroes;
        private readonly HeroesPage heroesPage;
        public Dota2Clickr()
        {
            InitializeComponent();
            heroesPage = new HeroesPage();
            heroes = new List<DotaHero>();
        }

        private void button_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var heroLinks = heroesPage.GetListHeroesLinks().ToList();
            heroLinks.Sort();

            foreach (var comboBox in Controls.OfType<ComboBox>())
            {
                heroLinks.ForEach(link =>
                                       {
                                           var name = HtmlHelper.GetNameFromLink(link);
                                           heroes.Add(new DotaHero
                                                          {
                                                              Name = name,
                                                              Link = link,
                                                              ImgLink = heroesPage.GetHeroImageLink(name)
                                                          });
                                           comboBox.Items.Add(name);
                                       });
            }
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = (ComboBox) sender;

            var comboName = comboBox.Name[comboBox.Name.Length - 1];

            foreach (var button in Controls.OfType<Button>().Where(button => button.Name.Contains(comboName.ToString())).ToList())
            {
                foreach (var dotaHero in heroes.Where(dotaHero => dotaHero.Name == comboBox.Text).ToList())
                {
                    button.Image = HtmlHelper.GetImage(dotaHero.ImgLink);
                }
            }
        }
    }
}
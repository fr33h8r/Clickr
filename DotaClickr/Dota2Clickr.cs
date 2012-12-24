using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using ClickrAPI;

namespace DotaClickr
{
    public partial class Dota2Clickr : Form
    {
        private readonly List<DotaHero> heroes;
        public Dota2Clickr()
        {
            InitializeComponent();
            heroes = new List<DotaHero>();
        }

        private void button_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var heroesPage = new HeroesPage();
            var heroLinks = heroesPage.GetListHeroesLinks().ToList();

            foreach (var comboBox in Controls.OfType<ComboBox>())
            {
                heroLinks.ForEach(link =>
                                       {
                                           var name = HtmlHelper.GetNameFromLink(link);
                                           heroes.Add(new DotaHero { Name = name, Link = link });
                                           comboBox.Items.Add(name);
                                       });
            }
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = (ComboBox) sender;

            var comboName = comboBox.Name[comboBox.Name.Length - 1];

            foreach (var button in Controls.OfType<Button>().Where(button => button.Name.Contains(comboName.ToString())))
            {
                foreach (var dotaHero in heroes)
                {
                    if(dotaHero.Name == comboBox.Text)
                        button.Text = dotaHero.GetImage(dotaHero.Name);
                }
            }
        }
    }
}
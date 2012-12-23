using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using ClickrAPI;

namespace DotaClickr
{
    public partial class Form1 : Form
    {
        private readonly List<DotaHero> heroes = new List<DotaHero>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var helper = new HtmlHelper();
            foreach (var comboBox in Controls.OfType<ComboBox>())
            {
                var heroNames = helper.GetHeroes().ToList();
                heroNames.ForEach(name =>
                                       {
                                           comboBox.Items.Add(name);
                                           heroes.Add(helper.GetHeroInfo(name));
                                       });
            }
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            var helper = new HtmlHelper();
            var comboBox = (ComboBox) sender;
            helper.GetHeroInfo(comboBox.Text);

            var comboName = comboBox.Name[comboBox.Name.Length - 1];

            foreach (var button in Controls.OfType<Button>().Where(button => button.Name.Contains(comboName.ToString())))
            {
                foreach (var dotaHero in heroes)
                {
                    if(dotaHero.Name == comboBox.Text)
                        button.Image = dotaHero.Img;
                }
            }
        }
    }
}
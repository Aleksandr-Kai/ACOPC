using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace ACOPC
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            DateTime buildDate = new DateTime(2000, 1, 1)
                                    .AddDays(version.Build).AddSeconds(version.Revision * 2);
            string displayableVersion = $"{version} ({buildDate})";
            //string ver = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.Text = "О программе АСОРС";
            listBox1.Items.Clear();
            listBox1.Items.Add("А втоматизированная");
            listBox1.Items.Add("С истема");
            listBox1.Items.Add("О ценки");
            listBox1.Items.Add("Р адиопрозрачности");
            listBox1.Items.Add("С амолета");
            listBox1.Items.Add("");
            listBox1.Items.Add("Версия " + displayableVersion);
            listBox1.Items.Add("");
            listBox1.Items.Add("");
            listBox1.Items.Add("Разработано ТАНТК им.Г.М.Бериева");
            listBox1.Items.Add("отделом 0380, 2019г.");
            listBox1.Items.Add("");
            listBox1.Items.Add("");
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACOPC
{
    public partial class PointsEdit : Form
    {
        public string Value { get { return textBox1.Text.Replace('.', ','); } }

        public PointsEdit()
        {
            InitializeComponent();
        }

        public void Set(string text, string units)
        {
            label1.Text = text;
            lblUnits.Text = units;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                DialogResult = DialogResult.OK;
            }
        }
    }
}

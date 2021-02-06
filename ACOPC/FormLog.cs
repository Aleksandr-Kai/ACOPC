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
    public partial class frmLog : Form
    {
        private static frmLog self = null;
        private bool Debug = false;
        public frmLog()
        {
            InitializeComponent();

            if (self == null) self = this;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        public static void Log(string _text)
        {
            if(self.Debug)
            {
                self.listBox1.Items.Add(_text);
                self.listBox1.SelectedIndex = self.listBox1.Items.Count - 1;
                self.Update();
            }
        }

        private void frmLog_Shown(object sender, EventArgs e)
        {
            Debug = true;
        }

        private void frmLog_FormClosing(object sender, FormClosingEventArgs e)
        {
            Debug = false;
        }
    }
}

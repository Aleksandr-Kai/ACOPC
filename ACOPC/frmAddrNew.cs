using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ACOPC
{
    public partial class frmAddrNew : Form
    {
        [DllImport(".\\visa_dll.dll")]
        public static extern bool Test([MarshalAs(UnmanagedType.LPStr)] string _addr, [MarshalAs(UnmanagedType.LPStr)] string _type);

        public frmAddrNew()
        {
            InitializeComponent();
        }

        private void lblCheckGen_Click(object sender, EventArgs e)
        {
            bool Connection = false;
            if (rbGenGPIB.Checked) Connection = Test(txtGenGPIB.Text, "GPIB"); else Connection = Test(txtGenIP.Text, "IP");
            if (Connection) MessageBox.Show("Подключение установлено"); else MessageBox.Show("Подключение не установлено");
        }

        private void lblCheckAn_Click(object sender, EventArgs e)
        {
            bool Connection = false;
            if (rbAnGPIB.Checked) Connection = Test(rbAnGPIB.Text, "GPIB"); else Connection = Test(txtAnIP.Text, "IP");
            if (Connection) MessageBox.Show("Подключение установлено"); else MessageBox.Show("Подключение не установлено");
        }

        private void lblCheckMon_Click(object sender, EventArgs e)
        {
            bool Connection = false;
            if (rbMonGPIB.Checked) Connection = Test(rbMonGPIB.Text, "GPIB"); else Connection = Test(txtMonIP.Text, "IP");
            if (Connection) MessageBox.Show("Подключение установлено"); else MessageBox.Show("Подключение не установлено");
        }

        private void lblCheckBonn1_Click(object sender, EventArgs e)
        {
            bool Connection = false;
            if (rbBonn0101GPIB.Checked) Connection = Test(rbBonn0101GPIB.Text, "GPIB"); else Connection = Test(txtBonn0101IP.Text, "IP");
            if (Connection) MessageBox.Show("Подключение установлено"); else MessageBox.Show("Подключение не установлено");
        }

        private void lblCheckBonn2_Click(object sender, EventArgs e)
        {
            bool Connection = false;
            if (rbBonn0125GPIB.Checked) Connection = Test(rbBonn0125GPIB.Text, "GPIB"); else Connection = Test(txtBonn0125IP.Text, "IP");
            if (Connection) MessageBox.Show("Подключение установлено"); else MessageBox.Show("Подключение не установлено");
        }

        private void lblCheckAR_Click(object sender, EventArgs e)
        {
            bool Connection = false;
            if (rbARGPIB.Checked) Connection = Test(rbARGPIB.Text, "GPIB"); else Connection = Test(txtARIP.Text, "IP");
            if (Connection) MessageBox.Show("Подключение установлено"); else MessageBox.Show("Подключение не установлено");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

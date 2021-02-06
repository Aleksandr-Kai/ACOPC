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
    public partial class FormWaitMonitor : Form
    {
        public int WaitTimer
        {
            get
            {
                if (WaitReverse)
                {
                    return pbrWait.Maximum - pbrWait.Value;
                }
                else
                {
                    return pbrWait.Value / 1000;
                }
            }
            set
            {
                if (WaitReverse)
                {
                    pbrWait.Step = -1;
                }
                else
                {
                    pbrWait.Step = 1;
                }
                pbrWait.Maximum = value;
            }
        }
        public bool WaitReverse { get; set; }
        public string Message { set { this.Text = value; } }
        
        public FormWaitMonitor()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pbrWait.PerformStep();
            if ((WaitReverse && (pbrWait.Value == 0)) || (!WaitReverse && (pbrWait.Value == pbrWait.Maximum)))
            {
                timer1.Enabled = false;
                this.Close();
            }
        }

        private void FormWaitMonitor_Shown(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
    }
}

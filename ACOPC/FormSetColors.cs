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
    public partial class FormSetColors : Form
    {
        public System.Windows.Forms.DataVisualization.Charting.ChartColorPalette Palette;
        public Color clBackColor = Color.White;
        public Color clLegendText = Color.Black;
        public Color clGrid = Color.Black;
        public Color clMin = Color.Black;
        public Color clMax = Color.Black;

        public FormSetColors()
        {
            InitializeComponent();
            cmbxSeriesPalete.Items.AddRange(Enum.GetNames(typeof(System.Windows.Forms.DataVisualization.Charting.ChartColorPalette)));
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void FormSetColors_Shown(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].BackColor = clBackColor;
            chart1.Legends[0].BackColor = clBackColor;
            chart1.Legends[0].ForeColor = clLegendText;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = clGrid;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = clGrid;
            chart1.Series[1].Color = clMin;
            chart1.Series[0].Color = clMax;
            cmbxSeriesPalete.SelectedIndex = (int)Palette;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                clBackColor = colorDialog1.Color;
                chart1.ChartAreas[0].BackColor = clBackColor;
                chart1.Legends[0].BackColor = clBackColor;
            }
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                clLegendText = colorDialog1.Color;
                chart1.Legends[0].ForeColor = clLegendText;
            }
        }

        private void btnGrid_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                clGrid = colorDialog1.Color;
                chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = clGrid;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = clGrid;
            }
        }

        private void cmbxSeriesPalete_SelectedIndexChanged(object sender, EventArgs e)
        {
            Palette = (System.Windows.Forms.DataVisualization.Charting.ChartColorPalette)cmbxSeriesPalete.SelectedIndex;
            chart1.Palette = Palette;
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                clMin = colorDialog1.Color;
                chart1.Series[1].Color = clMin;
            }
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                clMax = colorDialog1.Color;
                chart1.Series[0].Color = clMax;
            }
        }

        private void cmbxSeriesPalete_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)0;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
    }
}

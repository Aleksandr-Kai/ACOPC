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
    public partial class frmAddr : Form
    {
        private bool clbDisableItemChecked = false;
        public frmAddr()
        {
            InitializeComponent();
        }

        private void rbUseGPIB_CheckedChanged(object sender, EventArgs e)
        {
            if (rbUseGPIB.Checked)
            {
                txtGPIB.Enabled = true;
                txtIP.Enabled = false;
            }
        }

        private void rbUseIP_CheckedChanged(object sender, EventArgs e)
        {
            if (rbUseIP.Checked)
            {
                txtGPIB.Enabled = false;
                txtIP.Enabled = true;
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAddrSet_Click(object sender, EventArgs e)
        {
            if (checkedListBox.SelectedIndex < 0) return;
            String strInstr = checkedListBox.SelectedItem.ToString();
            String strType = strInstr.Split('\t')[0];
            //String strAddr = strInstr.Split('\t')[1];
            String strName = strInstr.Split('\t')[3];
            
            if (rbUseGPIB.Checked)
            {
                strInstr = strType + "\tGPIB\t" + txtGPIB.Text + "\t" + strName;
            }else
            {
                strInstr = strType + "\tIP\t" + txtIP.Text + "\t" + strName;
            }
            if (checkedListBox.Items.IndexOf(strInstr) >= 0) { MessageBox.Show("Устройство с такими параметрами уже имеется в списке."); return; }
            checkedListBox.Items[checkedListBox.SelectedIndex] = strInstr;
        }

        private void btnDelInstrument_Click(object sender, EventArgs e)
        {
            checkedListBox.Items.RemoveAt(checkedListBox.SelectedIndex);
        }

        private void frmAddr_Shown(object sender, EventArgs e)
        {
            if (cbxInstruments.SelectedIndex < 0) cbxInstruments.SelectedIndex = 0;
        }

        private void btnAddInstrument_Click(object sender, EventArgs e)
        {
            if (cbxInstruments.SelectedIndex < 0) { MessageBox.Show("Выберите тип добавляемого инструмента."); return; }
            String strInstr = cbxInstruments.SelectedItem.ToString();
            if (rbUseGPIB.Checked)
            {
                strInstr += "\tGPIB\t" + txtGPIB.Text + "\tБЕЗ ИМЕНИ";
            }
            else
            {
                strInstr += "\tIP\t" + txtIP.Text + "\tБЕЗ ИМЕНИ";
            }
            if (checkedListBox.Items.IndexOf(strInstr) >= 0) { MessageBox.Show("Устройство с такими параметрами уже имеется в списке."); return; }
            checkedListBox.Items.Add(strInstr);
        }

        private void checkedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (clbDisableItemChecked) return;
            clbDisableItemChecked = true;
            if (!checkedListBox.GetItemChecked(checkedListBox.SelectedIndex))
            {
                String strType = checkedListBox.SelectedItem.ToString().Split('\t')[0];
                for (int i = 0; i < checkedListBox.CheckedItems.Count; i++)
                {
                    if (checkedListBox.CheckedItems[i].ToString().Contains(strType) && (checkedListBox.CheckedItems[i] != checkedListBox.SelectedItem)) checkedListBox.SetItemChecked(checkedListBox.Items.IndexOf(checkedListBox.CheckedItems[i].ToString()), false);
                }
            }
            clbDisableItemChecked = false;
        }
    }
}

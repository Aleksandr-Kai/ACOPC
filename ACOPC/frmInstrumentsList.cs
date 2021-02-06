using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NationalInstruments.Visa;
//using NationalInstruments.VisaNS;
using IniParser;
using IniParser.Model;

namespace ACOPC
{
    public partial class frmInstrumentsList : Form
    {
        public struct BandItem
        {
            public string Name;
            public double Freq1;
            public string Units1;
            public double Freq2;
            public string Units2;
        }
        
        public ListView.ListViewItemCollection Instruments { get { return lvInstrList.Items; } }
        public ListViewGroupCollection Groups { get { return lvInstrList.Groups; } }
        public ListView.CheckedListViewItemCollection CheckedInstruments { get { return lvInstrList.CheckedItems; } }

        private bool LockItemCheck;

        public frmInstrumentsList()
        {
            InitializeComponent();

            LoadInstruments();
            cmbxGroupList.Items.Clear();
            foreach (ListViewGroup grp in lvInstrList.Groups)
            {
                cmbxGroupList.Items.Add(grp.Header);
            }
            cmbxGroupList.Items.Remove("Неизвестные");
            cmbxGroupList.Text = "Выберите группу";

            LockItemCheck = false;
        }

        public static BandItem ParseBandString(string str)
        {
            BandItem bi = new BandItem();
            bi.Name = "";
            bi.Freq1 = double.NaN;
            bi.Units1 = "";
            bi.Freq2 = double.NaN;
            bi.Units2 = "";
            if (str.Split(':').Count() < 4) return bi;

            if (str.Split(':').Count() == 5) bi.Name = str.Split(':')[4];
            bi.Freq1 = double.Parse(str.Split(':')[0].Replace('.', ','));
            bi.Units1 = str.Split(':')[1];
            bi.Freq2 = double.Parse(str.Split(':')[2].Replace('.', ','));
            bi.Units2 = str.Split(':')[3];

            return bi;
        }

        public void LoadInstruments()
        {
            ListViewItem lvi;
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(Application.StartupPath + "\\devices.ini");
            lvInstrList.Items.Clear();
            foreach (var section in data.Sections)
            {
                lvi = lvInstrList.Items.Add(section.SectionName);
                lvi.SubItems.Add(section.Keys["Подключение"]);
                lvi.SubItems.Add(section.Keys["Адрес"]);
                lvi.Group = lvInstrList.Groups[section.Keys["Группа"]];
                try
                {
                    lvi.Checked = bool.Parse(section.Keys["Checked"]);
                }
                catch (Exception) {; }
                
                if (lvi.Group == null)
                {
                    lvi.ImageIndex = 0;
                    lvi.Group = lvInstrList.Groups["lvgNan"];
                }
                else
                {
                    if (TestConnection(lvi.SubItems[1].Text, lvi.SubItems[2].Text))
                        lvi.ImageIndex = 2;
                    else lvi.ImageIndex = 1;
                }
                string str = section.Keys["Band1"];
                int num = 1;
                
                while ((str != "") && (str != null))
                {
                    lvi.SubItems.Add(str);
                    num++;
                    str = section.Keys["Band" + num];
                }
            }
        }

        public void SaveInstruments()
        {
            var parser = new FileIniDataParser();
            //IniData data = new IniData();
            IniData data = parser.ReadFile(Application.StartupPath + "\\devices.ini");
            foreach (ListViewItem item in lvInstrList.Items)
            {
                data.Sections.AddSection(item.Text);
                if (data.Sections[item.Text].ContainsKey("Группа"))
                    data.Sections[item.Text].RemoveKey("Группа");
                if (data.Sections[item.Text].ContainsKey("Подключение"))
                    data.Sections[item.Text].RemoveKey("Подключение");
                if (data.Sections[item.Text].ContainsKey("Адрес"))
                    data.Sections[item.Text].RemoveKey("Адрес");
                if (data.Sections[item.Text].ContainsKey("Checked"))
                    data.Sections[item.Text].RemoveKey("Checked");

                data.Sections[item.Text].AddKey("Группа", item.Group.Name);
                data.Sections[item.Text].AddKey("Подключение", item.SubItems[1].Text);
                data.Sections[item.Text].AddKey("Адрес", item.SubItems[2].Text);
                data.Sections[item.Text].AddKey("Checked", item.Checked.ToString());
            }
            parser.WriteFile(Application.StartupPath + "\\devices.ini", data);
        }

        public bool TestConnection(string type, string addres)
        {
            VisaInstrClasses.VisaInstrument instr = new VisaInstrClasses.VisaInstrument();
            if (instr.Connect(addres)) return true;
            return false;
        }

        private void frmInstrumentsList_Load(object sender, EventArgs e)
        {
            
        }

        private void lvInstrList_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (lvInstrList.SelectedItems.Count == 1)
            {
                gbxParams.Enabled = true;
                try
                {
                    cmbxGroupList.SelectedIndex = lvInstrList.Groups.IndexOf(lvInstrList.SelectedItems[0].Group);
                }
                catch (Exception) { cmbxGroupList.SelectedIndex = -1; cmbxGroupList.Text = "Выберите группу"; }
                if (lvInstrList.SelectedItems[0].SubItems[1].Text == "GPIB") rbGPIB.Checked = true; else rbIP.Checked = true;
                txtAddres.Text = lvInstrList.SelectedItems[0].SubItems[2].Text;

                try
                {
                    BandItem bi;
                    string str;
                    for(int i = 3; i < lvInstrList.SelectedItems[0].SubItems.Count; i++)
                    {
                        bi = ParseBandString(lvInstrList.SelectedItems[0].SubItems[i].Text);
                        str = bi.Freq1.ToString("F1") + bi.Units1 + " - " +
                            bi.Freq2.ToString("F1") + bi.Units2;
                        if (bi.Name != "") str = string.Format("{0}\t{1}", bi.Name, str);
                        listBox1.Items.Add(str);
                    }
                }
                catch (Exception) {; }
            }
            else
            {
                gbxParams.Enabled = false;
            }
            if (listBox1.Items.Count > 0) listBox1.Visible = true; else listBox1.Visible = false;
        }

        private bool CheckIP(string _ip)
        {
            if (_ip.Split('.').Count() != 4) return false;
            decimal n;
            foreach(string num in _ip.Split('.'))
            {
                if (!decimal.TryParse(num, out n)) return false;
                if (n > 255) return false;
            }
            return true;
        }

        private void btnSetParams_Click(object sender, EventArgs e)
        {
            if(cmbxGroupList.SelectedIndex < 0)
            {
                MessageBox.Show(this, "Выберите тип прибора из списка", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(rbIP.Checked && !CheckIP(txtAddres.Text))
            {
                MessageBox.Show(this, "Не корректный IP адрес.\rСохранить не возможно.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal n;
            if (rbGPIB.Checked && !decimal.TryParse(txtAddres.Text, out n))
            {
                MessageBox.Show(this, "Не корректный GPIB адрес.\rСохранить не возможно.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lvInstrList.SelectedItems[0].Group = lvInstrList.Groups[cmbxGroupList.SelectedIndex];
            if (rbGPIB.Checked)
                lvInstrList.SelectedItems[0].SubItems[1].Text = "GPIB";
            else
                lvInstrList.SelectedItems[0].SubItems[1].Text = "TCP/IP";
            lvInstrList.SelectedItems[0].SubItems[2].Text = txtAddres.Text;
            if (TestConnection(lvInstrList.SelectedItems[0].SubItems[1].Text, lvInstrList.SelectedItems[0].SubItems[2].Text))
                lvInstrList.SelectedItems[0].ImageIndex = 2;
            else lvInstrList.SelectedItems[0].ImageIndex = 1;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SaveInstruments();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void lvInstrList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (LockItemCheck) return;
            LockItemCheck = true;
            ListViewItem current = lvInstrList.Items[e.Index];
            foreach (ListViewItem item in lvInstrList.CheckedItems)
            {
                if ((item.Index != e.Index) && (current.Group == item.Group)) item.Checked = false;
            }
            LockItemCheck = false;
        }
    }
}

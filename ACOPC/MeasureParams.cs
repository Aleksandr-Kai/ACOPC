using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using IniParser;
using IniParser.Model;
using VisaInstrClasses;
using System.IO;

namespace ACOPC
{
    public partial class frmMeasureParams : Form
    {
        public bool UseAnalyzer { get { return cbxUseAnalyzer.Checked; } }
        public bool UseMonitor { get { return cbxUseMonitor.Checked; } }
        public bool UseGenerator { get { return cbxUseGen.Checked && (cmbxGenMode.SelectedIndex == 0); } }
        public bool UseTrackingGenerator { get { return cbxUseGen.Checked && (cmbxGenMode.SelectedIndex == 1); } }
        public bool UseAmplifire { get { return cbxUseAmpl.Checked; } }
        
        private long m_MinFreq;
        private long m_MaxFreq;

        public frmInstrumentsList FormInstruments;

        public frmMeasureParams()
        {
            InitializeComponent();

            cmbxGenMode.SelectedIndex = 0;
            cmbxMonitorChannel.SelectedIndex = 0;
            cmbxCorrectionImpedance.SelectedIndex = 0;
            UpdateTemplateList();

            m_MinFreq = -1;
            m_MaxFreq = -1;

            System.Reflection.BindingFlags bFlags = System.Reflection.BindingFlags.Instance |
                                                    System.Reflection.BindingFlags.NonPublic;
            DGV.GetType().GetProperty("DoubleBuffered", bFlags).SetValue(DGV, true, null);
        }

        private void SetMeasRange()
        {
            int iStartPoint, iStopPoint;
            if (DGV.SelectedRows.Count > 1)
            {
                iStartPoint = DGV.SelectedRows[0].Index;
                iStopPoint = DGV.SelectedRows[DGV.SelectedRows.Count - 1].Index;
                if (iStartPoint > iStopPoint)
                {
                    iStartPoint += iStopPoint;
                    iStopPoint = iStartPoint - iStopPoint;
                    iStartPoint -= iStopPoint;
                }
            }
            else
            {
                iStartPoint = 0;
                iStopPoint = MeasConfigurator.PointsCount - 1;
            }

            lblBeginEnd.Text = string.Format("[{0}] - [{1}]", iStartPoint + 1, iStopPoint + 1);

            MeasConfigurator.m_StartPoint = iStartPoint;
            MeasConfigurator.m_StopPoint = iStopPoint;
        }
        
        /// <summary>
        /// Обновление списка шаблонов (комбобокс)
        /// </summary>
        public void UpdateTemplateList()
        {
            cmbxTemplates.Items.Clear();

            DirectoryInfo dir = new DirectoryInfo(Application.StartupPath + "\\templates");
            foreach (var item in dir.GetFiles())
            {
                cmbxTemplates.Items.Add(Path.GetFileNameWithoutExtension(item.Name));
            }
        }

        private void SetDGV(DataTable _dt)
        {
            this.Cursor = Cursors.WaitCursor;
            DGV.DataSource = _dt;
            DGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Сохранение измерения в шаблон
        /// </summary>
        private void SaveTemplateBin(string _TemplateName)
        {
            string path = Application.StartupPath + "\\templates\\" + _TemplateName + ".bin";
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Create(path)))
                {
                    foreach(var point in MeasConfigurator.Points)
                    {
                        writer.Write(point.Frequency);
                        writer.Write(point.Amplitude);
                        writer.Write(point.Time_ms);
                        writer.Write((int)point.Band);
                    }
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Загрузка шаблона в таблицу
        /// </summary>
        private void LoadTemplateBin(string _TemplateName)
        {
            string path = Application.StartupPath + "\\templates\\" + _TemplateName + ".bin";
            DGV.DataSource = null;
            lblPointsCount.Text = "";
            ApplyMeasConfig();
            try
            {
                using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
                {
                    MeasConfigurator.ClearPoints();
                    while(reader.BaseStream.Length != reader.BaseStream.Position)
                    {
                        MeasConfigurator.AddPoint(reader.ReadInt64(), reader.ReadInt32(), reader.ReadInt32(),
                            (InstrBands)reader.ReadInt32());
                    }
                }
            }
            catch (Exception) { }
            MeasConfigurator.m_StartPoint = 0;
            MeasConfigurator.m_StopPoint = MeasConfigurator.PointsCount - 1;
            UpdatePointsCountLbl();
            this.Update();
            SetDGV(MeasConfigurator.GetTable());
            SetMeasRange();
        }
        
        private void UpdatePointsCountLbl()
        {
            lblPointsCount.Text = MeasConfigurator.PointsCount.ToString() +
                "     [Выбрано: " + MeasConfigurator.RangePointsCount.ToString() + "]";
        }

        private void SetMinMaxFreq()
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(Application.StartupPath + "\\devices.ini");
            long temp;
            foreach (var section in data.Sections)
            {
                if (section.Keys["Checked"] == "True")
                {
                    try
                    {
                        temp = long.Parse(section.Keys["Мин.частота"]);
                        if ((m_MinFreq < 0) || (m_MinFreq > temp)) m_MinFreq = temp;
                        temp = long.Parse(section.Keys["Макс.частота"]);
                        if ((m_MaxFreq < 0) || (m_MaxFreq < temp)) m_MaxFreq = temp;
                    }
                    catch (Exception) { }
                }
            }
        }

        /// <summary>
        /// Генерация таблицы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerateMeasure_Click(object sender, EventArgs e)
        {
            Int64 iStartFreq = 0;
            Int64 iStopFreq = 0;
            float fAmpl = 0;
            float fTime = 0;
            int iSteps = 0;//количество проходов на точку
            int iPointsCount = (int)nudPointsCount.Value;
            float fDecCount;//количество декад
            double tmpDouble;
            Int64 iTempFreq;

            try
            {
                iStartFreq = Int64.Parse(txtStartFreq.Text) * (Int64)Math.Pow(1000, cmbxStartFreqUnits.SelectedIndex);// / 1000000;
                iStopFreq = Int64.Parse(txtStopFreq.Text) * (Int64)Math.Pow(1000, cmbxStopFreqUnits.SelectedIndex);// / 1000000;
                fAmpl = float.Parse(txtAmplitude.Text);
                fTime = float.Parse(txtTime.Text) * (float)Math.Pow(1000, cmbxTimeUnits.SelectedIndex);
                iSteps = (int)nudPointsCount.Value;
            }
            catch
            {
                MessageBox.Show(this, "Ошибка при вводе параметров.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (fAmpl > 0) { MessageBox.Show(this, "Заданная мощность сигнала превышает 0dBm!\nПри использовании усилителя превышение излучаемой\nмощности 0dBm запрещено!"); }


            SetMinMaxFreq();
            if (iStartFreq > m_MaxFreq - 1) { MessageBox.Show(this, "Введено слишком большое значение начальной частоты"); return; }
            if (iStartFreq < m_MinFreq) { MessageBox.Show(this, "Введено слишком маленькое значение начальной частоты"); return; }
            if (iStopFreq > m_MaxFreq) { MessageBox.Show(this, "Введено слишком большое значение конечной частоты"); return; }
            if (iStopFreq < m_MinFreq + 1) { MessageBox.Show(this, "Введено слишком маленькое значение конечной частоты"); return; }
            
            if (iStartFreq >= iStopFreq) { MessageBox.Show(this, "Конечное значение частоты должно быть больше начального"); return; }
            if (fTime < 0.001) { MessageBox.Show(this, "Введено слишком маленькое значение времени"); return; }
            if (fTime > 4000000) { MessageBox.Show(this, "Введено слишком большое значение времени"); return; }
            if (fAmpl > 20) { MessageBox.Show(this, "Введено слишком большое значение амплитуды сигнала"); return; }
            if (fAmpl < -40) { MessageBox.Show(this, "Введено слишком маленькое значение амплитуды сигнала"); return; }

            fDecCount = (float)Math.Ceiling(Math.Log10(iStopFreq / iStartFreq));

            MeasConfigurator.ClearPoints();
            int time_ms = 0;
            if (cmbxTimeUnits.SelectedIndex > 0)
                time_ms = int.Parse(txtTime.Text) * 1000;
            else
                time_ms = int.Parse(txtTime.Text);
            int amplitude = 0;
            int.TryParse(txtAmplitude.Text, out amplitude);

            ApplyMeasConfig();

            if (rbFixedPoints.Checked) //Фиксированное количество точек на декаду
            {
                MeasConfigurator.AddPoint(iStartFreq, amplitude, time_ms, InstrBands.NAN);
                for (int i = 0; i < fDecCount; i++)
                {
                    iTempFreq = iStartFreq * (Int64)Math.Pow(10, i);
                    for (int j = 1; j < iPointsCount; j++)
                    {
                        if (rbStepLog.Checked)
                        {
                            tmpDouble = (Math.Ceiling(iTempFreq * Math.Pow(10, ((double)j / (iPointsCount - 1)))));
                        }
                        else
                        {
                            tmpDouble = iTempFreq + iTempFreq * 9 * j / (iPointsCount - 1);
                        }
                        MeasConfigurator.AddPoint((long)tmpDouble, amplitude, time_ms, InstrBands.NAN);
                    }
                }
            }
            else if (rbMoreThenPoints.Checked)//Количество точек не меньше заданного
            {
                int i = 0;
                double step = (double)(10 * iStartFreq - iStartFreq) / (iPointsCount - 1);

                tmpDouble = iStartFreq;

                while (tmpDouble < iStopFreq)
                {
                    tmpDouble = iStartFreq + step * i;

                    MeasConfigurator.AddPoint((long)tmpDouble, amplitude, time_ms, InstrBands.NAN);
                    i++;
                }
                MeasConfigurator.EditPoint(MeasConfigurator.PointsCount - 1, iStopFreq, amplitude, time_ms, InstrBands.NAN);
            }
            else//Количество точек на диапазон
            {
                for (int i = 0; i < iPointsCount; i++)
                {
                    if (rbStepLog.Checked)
                        tmpDouble = (iStartFreq * Math.Pow(10, i * Math.Log10(iStopFreq / iStartFreq) /
                            (iPointsCount - 1)));
                    else
                        tmpDouble = (iStartFreq + (iStopFreq - iStartFreq) * i / (iPointsCount - 1));
                    
                    MeasConfigurator.AddPoint((long)tmpDouble, amplitude, time_ms, InstrBands.NAN);
                }
            }

            UpdatePointsCountLbl();
            this.Update();

            SetDGV(MeasConfigurator.GetTable());
            SetMeasRange();
            UpdatePointsCountLbl();
        }
        
        private void frmMeasureParams_Load(object sender, EventArgs e)
        {
            int iTemp = 0;
            txtStartFreq.Text = ConfigRW.Read<int>("MeasParams", "StartFreq").ToString();
            cmbxStartFreqUnits.SelectedIndex = ConfigRW.Read<int>("MeasParams", "StartFreqUnits");
            txtStopFreq.Text = ConfigRW.Read<int>("MeasParams", "StopFreq").ToString();
            cmbxStopFreqUnits.SelectedIndex = ConfigRW.Read<int>("MeasParams", "StopFreqUnits");
            txtAmplitude.Text = ConfigRW.Read<int>("MeasParams", "Amplitude").ToString();
            cmbxAnalyzerUnits.SelectedIndex = ConfigRW.Read<int>("MeasParams", "AnalyzerUnits");
            txtTime.Text = ConfigRW.Read<int>("MeasParams", "Time", 1).ToString();
            cmbxTimeUnits.SelectedIndex = ConfigRW.Read<int>("MeasParams", "TimeUnits", 1);
            nudPointsCount.Value = ConfigRW.Read<int>("MeasParams", "PointsCount");
            cmbxMonitorChannel.SelectedIndex = ConfigRW.Read<int>("MeasParams", "MonitorChannel");
            cmbxGenMode.SelectedIndex = ConfigRW.Read<int>("MeasParams", "GenMode");
            cmbxCorrectionImpedance.SelectedIndex = ConfigRW.Read<int>("MeasParams", "CorrectionImpedance");
            cmbxInputCoupling.SelectedIndex = ConfigRW.Read<int>("MeasParams", "InputCoupling");
            cmbxBandWidth.SelectedIndex = ConfigRW.Read<int>("MeasParams", "BandWidth");

            iTemp = ConfigRW.Read<int>("MeasParams", "Span");
            if (iTemp < nudSpan.Minimum)
                nudSpan.Value = nudSpan.Minimum;
            else if (iTemp > nudSpan.Maximum)
                nudSpan.Value = nudSpan.Maximum;
            else nudSpan.Value = iTemp;

            iTemp = ConfigRW.Read<int>("MeasParams", "SweepCount");
            if (iTemp < nudSweepCount.Minimum)
                nudSweepCount.Value = nudSweepCount.Minimum;
            else if (iTemp > nudSweepCount.Maximum)
                nudSweepCount.Value = nudSweepCount.Maximum;
            else nudSweepCount.Value = iTemp;

            cbxUseAnalyzer.Checked = ConfigRW.Read<bool>("MeasParams", "UseAnalyzer");
            cbxUseGen.Checked = ConfigRW.Read<bool>("MeasParams", "UseGen");
            cbxUseAmpl.Checked = ConfigRW.Read<bool>("MeasParams", "UseAmpl");
            cbxUseMonitor.Checked = ConfigRW.Read<bool>("MeasParams", "UseMonitor");
        }

        private void frmMeasureParams_FormClosing(object sender, FormClosingEventArgs e)
        {
            int iTemp = 0;
            int.TryParse(txtStartFreq.Text, out iTemp);
            if (iTemp <= 0) return;
            ConfigRW.Write<int>("MeasParams", "StartFreq", iTemp);
            ConfigRW.Write<int>("MeasParams", "StartFreqUnits", cmbxStartFreqUnits.SelectedIndex);
            int.TryParse(txtStopFreq.Text, out iTemp);
            if (iTemp <= 0) return;
            ConfigRW.Write<int>("MeasParams", "StopFreq", iTemp);
            ConfigRW.Write<int>("MeasParams", "StopFreqUnits", cmbxStopFreqUnits.SelectedIndex);
            int.TryParse(txtAmplitude.Text, out iTemp);
            ConfigRW.Write<int>("MeasParams", "Amplitude", iTemp);
            ConfigRW.Write<int>("MeasParams", "AnalyzerUnits", cmbxAnalyzerUnits.SelectedIndex);
            int.TryParse(txtTime.Text, out iTemp);
            ConfigRW.Write<int>("MeasParams", "Time", iTemp);
            ConfigRW.Write<int>("MeasParams", "TimeUnits", cmbxTimeUnits.SelectedIndex);
            ConfigRW.Write<int>("MeasParams", "PointsCount", (int)nudPointsCount.Value);

            ConfigRW.Write<int>("MeasParams", "MonitorChannel", cmbxMonitorChannel.SelectedIndex);
            ConfigRW.Write<int>("MeasParams", "GenMode", cmbxGenMode.SelectedIndex);
            ConfigRW.Write<int>("MeasParams", "CorrectionImpedance", cmbxCorrectionImpedance.SelectedIndex);
            ConfigRW.Write<int>("MeasParams", "InputCoupling", cmbxInputCoupling.SelectedIndex);
            ConfigRW.Write<int>("MeasParams", "BandWidth", cmbxBandWidth.SelectedIndex);
            ConfigRW.Write<int>("MeasParams", "Span", (int)nudSpan.Value);
            ConfigRW.Write<int>("MeasParams", "SweepCount", (int)nudSweepCount.Value);
            ConfigRW.Write<bool>("MeasParams", "UseAnalyzer", cbxUseAnalyzer.Checked);
            ConfigRW.Write<bool>("MeasParams", "UseGen", cbxUseGen.Checked);
            ConfigRW.Write<bool>("MeasParams", "UseAmpl", cbxUseAmpl.Checked);
            ConfigRW.Write<bool>("MeasParams", "UseMonitor", cbxUseMonitor.Checked);
        }

        private void ApplyMeasConfig()
        {
            MeasConfigurator.Name = txtLineName.Text;
            MeasConfigurator.SweepCount = (int)nudSweepCount.Value;
            MeasConfigurator.NewMeas = !cbxAddMeasure.Checked;
            MeasConfigurator.ShowFS = cbxShowFS.Checked;
            MeasConfigurator.InputCoupling = (InputCoupling)cmbxInputCoupling.SelectedIndex;
            try
            {
                MeasConfigurator.BandWidth = int.Parse(cmbxBandWidth.Text.Split(' ')[0]);
            }
            catch (Exception) { MeasConfigurator.BandWidth = 9; }
            MeasConfigurator.SpanPercentage = (int)nudSpan.Value;
            MeasConfigurator.MonitorChannel = (VisaMonitor.MonitorChannels)cmbxMonitorChannel.SelectedIndex;
            MeasConfigurator.CorrectionImpedance = (Impedance)cmbxCorrectionImpedance.SelectedIndex;
            MeasConfigurator.TrackingGenerator = cmbxGenMode.SelectedIndex == 1;
            MeasConfigurator.UnitsAnalyzer = Converter.ToUnits<UnitsAmplitude>(cmbxAnalyzerUnits.Text);
            MeasConfigurator.Amplifier = null;
            MeasConfigurator.Analyzer = null;
            MeasConfigurator.Generator = null;
            MeasConfigurator.Monitor = null;
        }

        private struct AmplBand
        {
            public string Name;
            public InstrBands Band { get { return Converter.StrToBand(Name); } }
            public long MinFreq;
            public long MaxFreq;
        }

        private bool GetAmplBands(out AmplBand[] _Bands)
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(Application.StartupPath + "\\devices.ini");
            
            var section = data.Sections.First(x => (x.Keys["Группа"] == "lvgAmplifires") && (x.Keys["Checked"] == "True"));
            int bandcnt = section.Keys.Where(x => x.KeyName.Contains("Band")).Count();
            if (section != null)
            {
                List<AmplBand> lst = new List<AmplBand>();
                AmplBand bnd;
                string f, u;
                foreach(var band in section.Keys.Where(x => x.KeyName.Contains("Band")))
                {
                    bnd.Name = band.Value.Split(':')[4];
                    f = band.Value.Split(':')[0].Replace('.', ',');
                    u = band.Value.Split(':')[1];
                    bnd.MinFreq = (long)Converter.Transform(double.Parse(f), 
                        Converter.ToUnits<UnitsFrequency>(u), UnitsFrequency.Hz);
                    f = band.Value.Split(':')[2].Replace('.', ',');
                    u = band.Value.Split(':')[3];
                    bnd.MaxFreq = (long)Converter.Transform(double.Parse(f),
                        Converter.ToUnits<UnitsFrequency>(u), UnitsFrequency.Hz);
                    lst.Add(bnd);
                }
                _Bands = lst.ToArray();
                return true;
            }
            _Bands = null;
            return false;
        }

        private void SetBands()
        {
            AmplBand[] abBandList;
            if(GetAmplBands(out abBandList))
            {
                DGV.DataSource = null;
                long f;

                for(int i = 0; i < MeasConfigurator.PointsCount; i++)
                {
                    f = MeasConfigurator.Points[i].Frequency;
                    foreach (var band in abBandList)
                    {
                        if ((f >= band.MinFreq) && (f <= band.MaxFreq))
                        {
                            MeasConfigurator.EditPoint(i, long.MaxValue, int.MaxValue, int.MaxValue, band.Band);
                            break;
                        }
                    }
                }
                
                SetDGV(MeasConfigurator.GetTable());
            }
        }

        private void rbMoreThenPoints_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMoreThenPoints.Checked)
            {
                rbStepLine.Checked = true;
                rbStepLog.Enabled = false;
            }
            else rbStepLog.Enabled = true;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadTemplateBin(cmbxTemplates.Text);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if ((cmbxTemplates.Text == "") || (MeasConfigurator.PointsCount == 0)) return;
            SaveTemplateBin(cmbxTemplates.Text);
            MessageBox.Show(this, "Шаблон '" + cmbxTemplates.Text + "' сохранен");
            UpdateTemplateList();
            cmbxTemplates.SelectedIndex = cmbxTemplates.Items.IndexOf(cmbxTemplates.Text);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if ((cmbxTemplates.Text == "") || (cmbxTemplates.SelectedIndex < 0)) return;
            string SectionName = cmbxTemplates.Text;
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(Application.StartupPath + "\\templates.ini");
            data.Sections.RemoveSection(SectionName);
            parser.WriteFile(Application.StartupPath + "\\templates.ini", data);
            cmbxTemplates.Text = "";
            UpdateTemplateList();
            MessageBox.Show(this, "Шаблон '" + SectionName + "' удален");
        }

        private void txtTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtAmplitude_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if(UseAmplifire && !(UseGenerator || UseTrackingGenerator))
            {
                MessageBox.Show(this, "Не допускается использование усилителя без генератора!",
                    "Не возможно применить!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MeasConfigurator.RangePointsCount > 0) DialogResult = DialogResult.OK; else DialogResult = DialogResult.Cancel;
        }

        private void ToolStripMenuItem_SetNewAmpl_Click(object sender, EventArgs e)
        {
            PointsEdit frmPointEdit = new PointsEdit();
            frmPointEdit.Set("Амплитуда", "dBm");
            int ampl = 0;
            if(frmPointEdit.ShowDialog() == DialogResult.OK)
            {
                int.TryParse(frmPointEdit.Value, out ampl);
                foreach (DataGridViewRow row in DGV.SelectedRows)
                {
                    MeasConfigurator.EditPoint(row.Index, long.MaxValue, ampl, int.MaxValue, InstrBands.Null);
                }
            }

            SetDGV(MeasConfigurator.GetTable());
        }

        private void ToolStripMenuItem_SetNewTime_Click(object sender, EventArgs e)
        {
            PointsEdit frmPointEdit = new PointsEdit();
            frmPointEdit.Set("Время измерения", "с");
            int val = 1;
            float fval = 1;
            if (frmPointEdit.ShowDialog() == DialogResult.OK)
            {
                float.TryParse(frmPointEdit.Value, out fval);
                val = (int)(fval * 1000);
                foreach (var index in GetSelectedRows())
                {
                    MeasConfigurator.EditPoint(index, long.MaxValue, int.MaxValue, val, InstrBands.Null);
                }
            }
            SetDGV(MeasConfigurator.GetTable());
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            grpAnParams.Visible = false;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            grpAnParams.Location = grpInstr.Location;
            if (cmbxBandWidth.SelectedIndex < 0) cmbxBandWidth.SelectedIndex = 0;
            if (cmbxInputCoupling.SelectedIndex < 0) cmbxInputCoupling.SelectedIndex = 0;
            grpAnParams.Visible = true;
        }

        private void cbxUseAmpl_CheckStateChanged(object sender, EventArgs e)
        {
            if (cbxUseAmpl.Checked)
            {
                SetBands();
            }
        }

        private void btnInstrs_Click(object sender, EventArgs e)
        {
            FormInstruments.ShowDialog();
        }

        private void mmSetBands_Click(object sender, EventArgs e)
        {
            SetBands();
        }

        private int[] GetSelectedRows()
        {
            List<int> SelectedIndexes = new List<int>();
            try
            {
                foreach (DataGridViewRow row in DGV.SelectedRows)
                    SelectedIndexes.Add(row.Index);
            }
            catch (Exception) { }
            return SelectedIndexes.ToArray();
        }

        private void RemoveSelectedRows()
        {
            MeasConfigurator.DeletePoints(GetSelectedRows());

            UpdatePointsCountLbl();

            SetDGV(MeasConfigurator.GetTable());
            SetMeasRange();
        }

        private void mmRemoveRows_Click(object sender, EventArgs e)
        {
            RemoveSelectedRows();
        }

        private void DGV_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) RemoveSelectedRows();
        }

        private void tmsiFreq_Click(object sender, EventArgs e)
        {
            PointsEdit frmPointsEdit = new PointsEdit();
            frmPointsEdit.Set("Частота", "МГц");
            float freq = 0;
            if (frmPointsEdit.ShowDialog() == DialogResult.OK)
            {
                float.TryParse(frmPointsEdit.Value, out freq);
                MeasConfigurator.EditPoint(DGV.SelectedRows[0].Index, 
                    (long)Converter.Transform(freq, UnitsFrequency.MHz, UnitsFrequency.Hz), int.MaxValue, 
                    int.MaxValue, InstrBands.Null);
            }

            SetDGV(MeasConfigurator.GetTable());
        }

        private void mmImportTemplates_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Файлы Конфигурации(*.ini)|options.ini";
            ofd.DefaultExt = "ini";
            ofd.InitialDirectory = Properties.Settings.Default.OpenPath;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filename = ofd.FileName;
                var parser = new FileIniDataParser();
                IniData data = parser.ReadFile(filename, Encoding.Default);
                List<MeasConfigurator.MeasPoint> ImportPoints = new List<MeasConfigurator.MeasPoint>();
                MeasConfigurator.MeasPoint NewPoint;
                UnitsFrequency FreqUnits = UnitsFrequency.Hz;
                string sTemp;
                string SectionName = "";
                foreach(var section in data.Sections)
                {
                    ImportPoints.Clear();
                    SectionName = section.SectionName;
                    try
                    {
                        foreach (var key in section.Keys.ToList().FindAll(k => k.KeyName.Contains("Строка")))
                        {
                            if (key.KeyName == "Строка0") // Читаем заголовок таблицы
                            {
                                sTemp = key.Value.Split('#')[2].Split(' ')[1];
                                FreqUnits = Converter.ToUnits<UnitsFrequency>(sTemp);
                            }
                            else
                            {
                                NewPoint = new MeasConfigurator.MeasPoint(0);
                                sTemp = key.Value.Split('#')[2];
                                NewPoint.Frequency = (long)Converter.Transform(double.Parse(sTemp), FreqUnits, UnitsFrequency.Hz);
                                sTemp = key.Value.Split('#')[4];
                                NewPoint.Amplitude = int.Parse(sTemp);
                                sTemp = key.Value.Split('#')[6];
                                NewPoint.Time_ms = int.Parse(sTemp) * 1000;
                                ImportPoints.Add(NewPoint);
                            }
                        }
                        if(ImportPoints.Count > 0)
                        {
                            string path = Application.StartupPath + "\\templates\\" + SectionName + ".bin";
                            using (BinaryWriter writer = new BinaryWriter(File.Create(path)))
                            {
                                foreach (var Point in ImportPoints)
                                {
                                    writer.Write(Point.Frequency);
                                    writer.Write(Point.Amplitude);
                                    writer.Write(Point.Time_ms);
                                    writer.Write((int)Point.Band);
                                }
                            }
                        }
                    }
                    catch (Exception) { }
                }
                UpdateTemplateList();
            }
        }

        private void btnSetMeasRange_Click(object sender, EventArgs e)
        {
            SetMeasRange();
            UpdatePointsCountLbl();
        }

        private void cmbxTimeUnits_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)0;
        }
    }
}

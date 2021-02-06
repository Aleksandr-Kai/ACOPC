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
using System.Runtime.InteropServices;
using VisaInstrClasses;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Windows.Forms.DataVisualization.Charting;

namespace ACOPC
{
    public partial class frmMain : Form
    {
        [DllImport(".\\visa_dll.dll")]
        public static extern int Connect([MarshalAs(UnmanagedType.LPStr)] string _addr, [MarshalAs(UnmanagedType.LPStr)] string _type);
        [DllImport(".\\visa_dll.dll")]
        public static extern void DisConnect(int _index);
        [DllImport(".\\visa_dll.dll")]
        public static extern void Send(int _index, [MarshalAs(UnmanagedType.LPStr)] string _cmd);
        [DllImport(".\\visa_dll.dll")]
        public static extern void Query(int _index, [MarshalAs(UnmanagedType.LPStr)] string _cmd);

        Random rnd;      // DEBUG
        int SeriesIndex = 0;   // Индекс текущей линии
        private MeasConfigurator.MeasPoint m_CurrPoint;

        private frmMeasureParams FormMeasParam;
        public frmInstrumentsList FormInstruments;

        private bool m_LockChart = false;
        private int m_MeasTime;    // Счетчик времени измерения
        private bool m_NotSaved;

        private MeasList m_Measures;

        private bool m_ctZoom = false;

        private Mutex m_mutexObj;

        private MeasConfigurator m_MeasConfig = new MeasConfigurator();
        private string m_ErrorMsg = "";

#if DEBUG
        public frmLog Log;
#endif

        //***********************************************************************
        //                  Конструктор формы
        //***********************************************************************
        public frmMain()
        {
            InitializeComponent();

            FormMeasParam = new frmMeasureParams();
            FormInstruments = new frmInstrumentsList();
            FormMeasParam.FormInstruments = FormInstruments;

            FormMeasParam.Owner = this;
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.MouseWheel += chart1_MouseWheel;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;

            chartMon.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chartMon.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chartMon.MouseWheel += chart1_MouseWheel;
            chartMon.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;

            m_Measures = new MeasList();

            m_NotSaved = false;

            FormReset(true);

            System.Reflection.BindingFlags bFlags = System.Reflection.BindingFlags.Instance |
                                                    System.Reflection.BindingFlags.NonPublic;
            chart1.GetType().GetProperty("DoubleBuffered", bFlags).SetValue(chart1, true, null);
            chartMon.GetType().GetProperty("DoubleBuffered", bFlags).SetValue(chartMon, true, null);

            toolTip1.SetToolTip(btnSave, "Сохранить измерения");
            toolTip1.SetToolTip(btnOpen, "Загрузить измерения из файла");
            toolTip1.SetToolTip(btnClear, "Очистить графики");
            toolTip1.SetToolTip(btnStart, "Начать измерение");
            toolTip1.SetToolTip(btnPause, "Приостановить измерение");
            toolTip1.SetToolTip(btnStop, "Прервать измерение");
            toolTip1.SetToolTip(btnParams, "Параметры измерения");


#if DEBUG
            Log = new frmLog();
            Log.Show();
#endif

            ConfigRW.SetPath(Application.StartupPath + "\\config.ini");
        }
        //***********************************************************************
        //                  Сброс всех элементов отображения
        //***********************************************************************
        private void FormReset(bool ClearCharts = false)
        {
            UnZoomCharts();
            if (ClearCharts)
            {
                CheckToSaved();
                DeleteAllLines();
                m_Measures.Clear();
                SetNotSaved(false);
            }
            txtARFwdPow.Text = "";
            txtARRefPow.Text = "";
            txtBonnFwdPow.Text = "";
            txtBonnRefPow.Text = "";
            pbrBonnFwdPow.Value = 0;
            pbrBonnRefPow.Value = 0;
            lblCurrentPoint.Text = "";
            lblCurrentFreq.Text = "";
            lblCurrentFreqTxt.Text = "Текущая частота, ";
            lblResInPointNum.Text = "Результат в точке: ";
            lblResFreqTxt.Text = "Частота, ";
            lblResFreq.Text = "";
            lblResAmpl.Text = "";
            lblResAmplTxt.Text = "Значение, ";
            lblResAmpl_2.Text = "";
            lblResAmplTxt_2.Text = "Значение, ";
            lblFieldStrength.Text = "";
            mmMinMax.Checked = false;
        }

        //***********************************************************************
        //                  Сохранение в Excel
        //***********************************************************************
        public bool SaveMeasures()
        {
            if (!MeasConfigurator.ReadyToStart) return false;
            bool Result = false;

            UnZoomCharts();
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Файлы Excel(*.xls;*.xlsx)|*.xls;*.xlsx";
            sfd.DefaultExt = "xls";
            sfd.InitialDirectory = Properties.Settings.Default.SavePath;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string filename = sfd.FileName;
                Properties.Settings.Default.SavePath = System.IO.Path.GetDirectoryName(filename);
                Properties.Settings.Default.Save();

                ExcelService excel = new ExcelService();

                pnlExcelWait.Visible = true;

                if (excel.Save(filename, m_Measures))
                {
                    pnlExcelWait.Visible = false;
                    //MessageBox.Show(this, "Сохранено в \r\r" + filename,
                    //    "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Result = true;
                }
                else
                {
                    pnlExcelWait.Visible = false;
                    MessageBox.Show(this, "Не удалось сохранить измерения в файл\r\r" + filename,
                                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return Result;
        }
        
        private DialogResult CheckToSaved(bool _cancel = false)
        {
            if (m_NotSaved)
            {
                DialogResult dr;
                if (_cancel)
                {
                    dr = MessageBox.Show(this, "Текущие измерения не сохранены\rCохранить сейчас?",
                        "Внимание!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                }
                else
                {
                    dr = MessageBox.Show(this, "Текущие измерения не сохранены\rCохранить сейчас?",
                        "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                }
                if (dr == DialogResult.Yes)
                {
                    SetNotSaved(SaveMeasures());
                }
                return dr;
            }
            return DialogResult.None;
        }

        private void SetNotSaved(bool _value)
        {
            m_NotSaved = _value;
            lblNotSaved.Visible = _value;
        }

        public void LoadMeasures()
        {
            //CheckToSaved();
            UnZoomCharts();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Файлы Excel(*.xls;*.xlsx)|*.xls;*.xlsx";
            ofd.DefaultExt = "xls";
            ofd.InitialDirectory = Properties.Settings.Default.OpenPath;

            if ((chart1.Series.Count > 2) || (chartMon.Series.Count > 2))
            {
                DialogResult dr = MessageBox.Show(this, "Графики не пусты.\r\rОчистить перед загрузкой?",
                    "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    FormReset(true);
                }
            }

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filename = ofd.FileName;
                string sTemp;
                Properties.Settings.Default.OpenPath = System.IO.Path.GetDirectoryName(filename);
                Properties.Settings.Default.Save();

                ExcelService excel = new ExcelService();
                MeasList MeasFromFile;

                pnlExcelWait.Visible = true;
                sTemp = "[" + System.IO.Path.GetFileNameWithoutExtension(filename) + "].";
                excel.Load(filename, sTemp, out MeasFromFile);
                
                Series CurrentMeas;
                if (m_Measures.Merge(MeasFromFile))  // Если можно добавить
                {
                    mmMinMax.Checked = false;
                    mmSelectCurrent.Checked = false;
                    mmUnselectAll.Checked = false;

                    chart1.ChartAreas[0].AxisY.Title = "Амплитуда, " + Converter.ToString<UnitsAmplitude>(MeasFromFile.AmplitudeUnits, true);

                    try
                    {
                        if (!MeasFromFile.AnIsEmpty)  // Если есть измерения анализатором
                        {
                            chart1.Visible = true;
                            foreach (var meas in MeasFromFile)
                            {
                                AddLineToMenu(meas.Name);
                                CurrentMeas = chart1.Series.Add(meas.Name);
                                CurrentMeas.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                                for (int i = 0; i < MeasFromFile.PointsCount; i++)
                                {
                                    CurrentMeas.Points.AddXY(Converter.Transform(MeasFromFile.FrequencyList[i], UnitsFrequency.Hz,
                                        UnitsFrequency.MHz), meas.AnYValues[i]);
                                }
                            }

                            chart1.ChartAreas[0].AxisX.Minimum = chart1.Series[2].Points[0].XValue;
                            chart1.ChartAreas[0].AxisX.Maximum = chart1.Series[2].Points[chart1.Series[2].Points.Count - 1].XValue;
                            SetAxisY(chart1);
                        }
                        else chart1.Visible = false;

                        if (!MeasFromFile.MonIsEmpty)  // Если есть измерения монитором
                        {
                            chartMon.Visible = true;
                            if (chart1.Visible)
                            {
                                chartMon.Dock = DockStyle.Bottom;
                            }
                            else
                            {
                                chartMon.Dock = DockStyle.Fill;
                            }

                            foreach (var meas in MeasFromFile)
                            {
                                AddLineToMenu(meas.Name);
                                CurrentMeas = chartMon.Series.Add(meas.Name);
                                CurrentMeas.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                                for (int i = 0; i < MeasFromFile.PointsCount; i++)
                                {
                                    CurrentMeas.Points.AddXY(Converter.Transform(MeasFromFile.FrequencyList[i], UnitsFrequency.Hz,
                                        UnitsFrequency.MHz), meas.MonYValues[i]);
                                }
                                chartMon.ChartAreas[0].AxisX.Minimum = CurrentMeas.Points[0].XValue;
                                chartMon.ChartAreas[0].AxisX.Maximum = CurrentMeas.Points[CurrentMeas.Points.Count - 1].XValue;
                            }
                            chartMon.ChartAreas[0].AxisX.Minimum = chartMon.Series[2].Points[0].XValue;
                            chartMon.ChartAreas[0].AxisX.Maximum = chartMon.Series[2].Points[chartMon.Series[2].Points.Count - 1].XValue;
                            SetAxisY(chartMon);
                        }
                        else chartMon.Visible = false;
                        mmLoaded.Visible = true;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(this, "Ошика при закрузке измерений из файла.\nВозможно выбранный файл уже загружен.",
                        "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(this, "Загружаемые измерения не возможно добавить к существующим.",
                        "Не возможно открыть", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                pnlExcelWait.Visible = false;
            }
        }
        
        /// <summary>
        /// Меню Старт Измерения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mmMeasureStart_Click(object sender, EventArgs e)
        {
            StartMeas();
        }

        /// <summary>
        /// Меню Пауза
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mmMeasureParams_Click(object sender, EventArgs e)
        {
            FormMeasParam.ShowDialog();
        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            Color back, grid, legend, min, max;
            ChartColorPalette palette;

            back = ConfigRW.Read<Color>("MainForm", "ChartBackColor", Color.White);
            grid = ConfigRW.Read<Color>("MainForm", "GridColor", Color.Black);
            legend = ConfigRW.Read<Color>("MainForm", "LegendTextColor", Color.Black);
            min = ConfigRW.Read<Color>("MainForm", "LineMinColor", Color.Black);
            max = ConfigRW.Read<Color>("MainForm", "LineMaxColor", Color.Black);

            palette = (ChartColorPalette)ConfigRW.Read<int>("MainForm", "ChartPalete", (int)chart1.Palette);

            SetChartColors(chart1, palette, back, legend, grid, min, max);
            SetChartColors(chartMon, palette, back, legend, grid, min, max);


            WindowState = (FormWindowState)ConfigRW.Read<int>("MainForm", "WindowState", (int)FormWindowState.Maximized);
            mmCursorMouseMove.Checked = ConfigRW.Read<bool>("MainForm", "CursorMouseMove", false);
            
        }

        /// <summary>
        /// Сохранение формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = CheckToSaved(true) == DialogResult.Cancel;

            ConfigRW.Write<Color>("MainForm", "ChartBackColor", chart1.ChartAreas[0].BackColor);
            ConfigRW.Write<Color>("MainForm", "GridColor", chart1.ChartAreas[0].AxisX.MajorGrid.LineColor);
            ConfigRW.Write<Color>("MainForm", "LegendTextColor", chart1.Legends[0].ForeColor);
            ConfigRW.Write<Color>("MainForm", "LineMinColor", chart1.Series[1].Color);
            ConfigRW.Write<Color>("MainForm", "LineMaxColor", chart1.Series[0].Color);
            ConfigRW.Write<int>("MainForm", "ChartPalete", (int)chart1.Palette);


            ConfigRW.Write<int>("MainForm", "WindowState", (int)WindowState);
            ConfigRW.Write<bool>("MainForm", "CursorMouseMove", mmCursorMouseMove.Checked);
        }

        /// <summary>
        /// Меню Показать список приборов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mmInstrumentsList_Click(object sender, EventArgs e)
        {
            DialogResult dr = FormInstruments.ShowDialog();
        }

        /// <summary>
        /// Старт нового/следующего измерения
        /// </summary>
        private void StartMeas()
        {
#if DEBUG
            rnd = new Random();
#endif
            UnZoomCharts();
            lblRefLevel.Visible = false;
            if (!timer1.Enabled && m_LockChart) // Если измерение на паузе, продолжаем работу
            {
                timer1.Enabled = true;
                btnStart.FlatStyle = FlatStyle.Standard;
                mmMeasureStart.Enabled = false;
                btnStart.Enabled = false;

                mmMeasurePause.Enabled = true;
                btnPause.Enabled = true;
                btnPause.FlatStyle = FlatStyle.Flat;
                return;
            }

            if (!MeasConfigurator.ReadyToStart) // Если таблица не сгенерирована
            {
                if (FormMeasParam.ShowDialog() == DialogResult.Cancel) return;  // Предлагаем это сделать
                if (!MeasConfigurator.ReadyToStart) return;  // Если и теперь нет данных, ничего не делаем
            }

            if (!FormMeasParam.UseAnalyzer && !FormMeasParam.UseMonitor)
            {
                MessageBox.Show(this, "Для запуска измерения необходимо выбрать прибор(ы) для измерения",
                    "Не возможно запустить измерение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            SeriesCollection ChartMesures = null;
            SeriesCollection sca = chart1.Series;
            SeriesCollection scm = chartMon.Series;

            if (FormMeasParam.UseAnalyzer)
            {
                ChartMesures = chart1.Series;                 // Если выбран анализатор, берем коллекцию линий анализатора
            }
            else if (FormMeasParam.UseMonitor)          // иначе если выбран монитор, берем коллецию линий монитора
            {
                ChartMesures = chartMon.Series;
            }                                       // третий вариант исключен проверкой выше
            // Проверяем можно ли добавить измерение
            if (!MeasConfigurator.NewMeas && !m_Measures.IsEmpty)
            {
                bool err;
                // выбраны оба прибора, но чарты имеют разное количество графиков
                err = FormMeasParam.UseAnalyzer && FormMeasParam.UseMonitor && (m_Measures[0].AnYValues.Count != m_Measures[0].MonYValues.Count);
                // на чарте выбранного прибора количество точек отличается от заданного
                err |= FormMeasParam.UseAnalyzer && (m_Measures[0].AnYValues.Count != MeasConfigurator.RangePointsCount);
                err |= FormMeasParam.UseMonitor && (m_Measures[0].MonYValues.Count != MeasConfigurator.RangePointsCount);
                // первая точка чарта отличается от заданной
                err |= m_Measures.MinFrequency != MeasConfigurator.GetStartFreq(UnitsFrequency.Hz);
                // последняя точка чарта отличается от заданной
                err |= m_Measures.MaxFrequency != MeasConfigurator.GetEndFreq(UnitsFrequency.Hz);
                // последняя точка чарта отличается от заданной
                err |= m_Measures.AmplitudeUnits != MeasConfigurator.UnitsAnalyzer;

                if (err)
                {
                    if (MessageBox.Show(this, "Добавить измерение с выбранными параметрами не возможно.\r\rНачать новое?",
                            "Не возможно добавить!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        MeasConfigurator.NewMeas = true;
                    }
                    else return;
                }

                if(chart1.Visible && chartMon.Visible && (!FormMeasParam.UseAnalyzer || !FormMeasParam.UseMonitor))
                {
                    if (MessageBox.Show(this, "Вы пытаетесь добавить измерение одним прибором\rк имеющимся измерениям двумя приборами.\r" +
                        "При добавлении измерения второго прибора будут удалены.\r\rЖелаете продолжить?",
                            "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        return;
                    }
                }
            }
            else
            {
                if (m_NotSaved)
                {
                    DialogResult dresult = MessageBox.Show(this, "Вы запускаете измерение без добавления!\r\r" +
                        "Продолжение приведет к потере несохраненных измерений.\r\rХотите продолжить?",
                        "Старт без добавления!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dresult == DialogResult.No) return; else SetNotSaved(false);
                }
            }
            //-----------------------------------------------------------------------
            //                  Подготовка приборов
            //-----------------------------------------------------------------------
            if (FormMeasParam.UseAmplifire)     // Усилители
            {
                MeasConfigurator.Amplifier = null;
                foreach (ListViewItem instr in FormInstruments.CheckedInstruments)   // поиск выбранных анализаторов
                {
                    if (instr.Group.Name == "lvgAmplifires")    // берем первый в списке
                    {
                        MeasConfigurator.Amplifier = new VisaAmplifier();
                        if (MeasConfigurator.Amplifier.Connect(instr.SubItems[2].Text))     // пытаемся подключиться
                        {
                            MeasConfigurator.Amplifier.Reset();
                            break;
                        }
                        else
                        {
                            MeasConfigurator.Amplifier = null;
                            InstrumentError("Не удалось подключить усилитель\r\r" + instr.Text);
                            return; // Не удалось подключиться
                        }
                    }
                }
                if (MeasConfigurator.Amplifier == null)   // Если не выбран
                {
                    InstrumentError("Не выбран ни один усилитель");
                    return;
                }
            }else MeasConfigurator.Amplifier = null;
            //---------------------------------------------------------------------------------
            if (FormMeasParam.UseAnalyzer)
            {
                MeasConfigurator.Analyzer = null;
                foreach (ListViewItem instr in FormInstruments.CheckedInstruments)   // поиск выбранных анализаторов
                {
                    if (instr.Group.Name == "lvgAnalyzers")    // берем первый в списке
                    {
                        MeasConfigurator.Analyzer = new VisaAnalyzer();
                        if (MeasConfigurator.Analyzer.Connect(instr.SubItems[2].Text))     // пытаемся подключиться
                        {
                            if (MeasConfigurator.Analyzer.Name.Contains("FSP-30"))
                            {
                                lblRefLevel.Text = "Ref.Level: " + MeasConfigurator.Analyzer.Get_RefLevel();
                                lblRefLevel.Visible = true;
                            }                                            
                            MeasConfigurator.Analyzer.Set_INPut_COUPling(MeasConfigurator.InputCoupling);
                            MeasConfigurator.Analyzer.Set_CORRection_IMPedance(MeasConfigurator.CorrectionImpedance);
                            MeasConfigurator.Analyzer.Reset();
                            break;
                        }
                        else
                        {
                            MeasConfigurator.Analyzer = null;
                            InstrumentError("Не удалось подключить анализатор\r\r" + instr.Text);
                            return; // Не удалось подключиться
                        }
                    }
                }
                if (MeasConfigurator.Analyzer == null)   // Если не выбран
                {
                    InstrumentError("Не выбран ни один анализатор");
                    return;
                }
            }else MeasConfigurator.Analyzer = null;
            //---------------------------------------------------------------------------------
            if (FormMeasParam.UseGenerator)
            {
                MeasConfigurator.Generator = null;
                foreach (ListViewItem instr in FormInstruments.CheckedInstruments)   // поиск выбранных генераторов
                {
                    if (instr.Group.Name == "lvgGenerators")    // берем первый в списке
                    {
                        MeasConfigurator.Generator = new VisaGenerator();
                        if (MeasConfigurator.Generator.Connect(instr.SubItems[2].Text))     // пытаемся подключиться
                        {
                            MeasConfigurator.Generator.Reset();
                            break;
                        }
                        else
                        {
                            MeasConfigurator.Generator = null;
                            InstrumentError("Не удалось подключить генератор\r\r" + instr.Text);
                            return; // Не удалось подключиться
                        }
                    }
                }
                if (MeasConfigurator.Generator == null)   // Если не выбран
                {
                    InstrumentError("Не выбран ни один генератор");
                    return;
                }
            }else MeasConfigurator.Generator = null;
            //---------------------------------------------------------------------------------
            if (FormMeasParam.UseMonitor || MeasConfigurator.ShowFS)
            {
                MeasConfigurator.Monitor = null;
                pnlFS.Visible = MeasConfigurator.ShowFS;
                foreach (ListViewItem instr in FormInstruments.CheckedInstruments)   // поиск выбранных монитор
                {
                    if (instr.Group.Name == "lvgMonitors")    // берем первый в списке
                    {
                        MeasConfigurator.Monitor = new VisaMonitor();
                        if (MeasConfigurator.Monitor.Connect(instr.SubItems[2].Text))     // пытаемся подключиться
                        {
                            MeasConfigurator.Monitor.Reset();
                            MeasConfigurator.Monitor.SetChannel(MeasConfigurator.MonitorChannel);
                            if (!MeasConfigurator.Monitor.ChannelReady())
                            {
                                //Запуск ожидания датчика
                                FormWaitMonitor frm = new FormWaitMonitor();
                                frm.WaitReverse = false;
                                frm.WaitTimer = 10; // ожидание 10 секунд
                                frm.Text = "Ожидание датчика монитора поля";
                                frm.ShowDialog();
                                if (!MeasConfigurator.Monitor.ChannelReady())
                                {
                                    // сообщение об ошибке датчика
                                    InstrumentError("Не удалось установить соединение с датчиком поля.");
                                    return;
                                }
                            }
                            break;
                        }
                        else
                        {
                            InstrumentError("Не удалось подключить монитор\r\r" + instr.Text);
                            return; // Не удалось подключиться
                        }
                    }
                }
                if (MeasConfigurator.Monitor == null)   // Если не выбран
                {
                    InstrumentError("Не выбран ни один монитор");
                    return;
                }
            }else MeasConfigurator.Monitor = null;
            //-----------------------------------------------------------------------

            //-----------------------------------------------------------------------
            //                      Создание мьютекса
            //-----------------------------------------------------------------------
            bool existed;
            string guid = Marshal.GetTypeLibGuidForAssembly(Assembly.GetExecutingAssembly()).ToString();
            m_mutexObj = new Mutex(true, guid, out existed);
            if (!existed)
            {
                MessageBox.Show(this, "Измерение уже запущено в другом экземпляре приложения.",
                    "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                m_mutexObj.Dispose();
                return;
            }
            //-----------------------------------------------------------------------
            //          Дальше return не допускается
            //-----------------------------------------------------------------------
            chart1.Visible = FormMeasParam.UseAnalyzer;  // Включаем/Выключаем график анализатора
            chartMon.Visible = FormMeasParam.UseMonitor; // Включаем/Выключаем график монитора
            if (!chart1.Visible)       // Корректируем отображение (положение) графиков
            {
                chartMon.Dock = DockStyle.Fill;
            }
            else
            {
                chartMon.Dock = DockStyle.Bottom;
            }

            MeasConfigurator.ResetMeas();
            lblResAmplTxt_2.Visible = FormMeasParam.UseAnalyzer && FormMeasParam.UseMonitor;
            lblResAmpl_2.Visible = FormMeasParam.UseAnalyzer && FormMeasParam.UseMonitor;
            mmLinkCursors.Visible = FormMeasParam.UseAnalyzer && FormMeasParam.UseMonitor;
            if (!mmLinkCursors.Visible) mmLinkCursors.Checked = false;

            if (MeasConfigurator.NewMeas)  // Если не добавляем, очищаем график
            {
                FormReset(true);
            }
            if (m_Measures.IsEmpty)  // Масштабируем ось Х для первого измерения
            {
                m_Measures.SetFrequency(MeasConfigurator.FrequencyList);
                chart1.ChartAreas[0].AxisX.Minimum = MeasConfigurator.GetStartFreq(UnitsFrequency.MHz);
                chart1.ChartAreas[0].AxisX.Maximum = MeasConfigurator.GetEndFreq(UnitsFrequency.MHz);
                chartMon.ChartAreas[0].AxisX.Minimum = MeasConfigurator.GetStartFreq(UnitsFrequency.MHz);
                chartMon.ChartAreas[0].AxisX.Maximum = MeasConfigurator.GetEndFreq(UnitsFrequency.MHz);
                //chart1.ChartAreas[0].AxisX.Interval = (chart1.ChartAreas[0].AxisX.Maximum - chart1.ChartAreas[0].AxisX.Minimum) / 20;
                //chartMon.ChartAreas[0].AxisX.Interval = (chartMon.ChartAreas[0].AxisX.Maximum - chartMon.ChartAreas[0].AxisX.Minimum) / 20;
            }

            m_MeasTime = 0;
            lblResFreq.Text = "";
            lblResAmpl.Text = "";
            if (!lblResAmplTxt_2.Visible && FormMeasParam.UseMonitor)
                lblResAmplTxt.Text = "Значение, В/м";
            else
            {
                lblResAmplTxt.Text = "Значение, " + Converter.ToString<UnitsAmplitude>(MeasConfigurator.UnitsAnalyzer, true);
                lblResAmplTxt_2.Text = "Значение, В/м";
            }

            m_Measures.AmplitudeUnits = MeasConfigurator.UnitsAnalyzer;
            chart1.ChartAreas[0].AxisY.Title = "Амплитуда, " + Converter.ToString<UnitsAmplitude>(MeasConfigurator.UnitsAnalyzer, true);

            lblResInPointNum.Text = "Результат в точке: ";
            sca["LineMax"].Points.Clear();
            sca["LineMin"].Points.Clear();
            scm["LineMax"].Points.Clear();
            scm["LineMin"].Points.Clear();

            rnd = new Random(); // Для отладки, случайные значения измерения
            string LineName = m_Measures.AddMeasure(MeasConfigurator.Name);

            // Создаем линии на графиках
            if (MeasConfigurator.Analyzer != null)
            {
                sca.Add(LineName);
                sca[sca.Count - 1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            }
            if (FormMeasParam.UseMonitor)
            {
                scm.Add(LineName);
                scm[scm.Count - 1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            }
            SeriesIndex = ChartMesures.Count - 1;  // Запоминаем индекс текущей линии
            if ((mmSelectCurrent.Checked) && (SeriesIndex > 2))  // Выделяем текущую линию, если задано
            {
                if (FormMeasParam.UseAnalyzer)
                {
                    UnSelectLines(sca);
                    SelectLine(sca, LineName);
                }
                if (FormMeasParam.UseMonitor)
                {
                    UnSelectLines(scm);
                    SelectLine(scm, LineName);
                }
            }
            //----------------------------------------------------------------------------------------

            if (chart1.ChartAreas[0].AxisY.ScaleView.IsZoomed)
            {
                chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset();
            }
            if(chart1.ChartAreas[0].AxisX.ScaleView.IsZoomed)
            {
                chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset();
            }
            if (chartMon.ChartAreas[0].AxisY.ScaleView.IsZoomed)
            {
                chartMon.ChartAreas[0].AxisY.ScaleView.ZoomReset();
            }
            if (chartMon.ChartAreas[0].AxisX.ScaleView.IsZoomed)
            {
                chartMon.ChartAreas[0].AxisX.ScaleView.ZoomReset();
            }

            progressBar1.Maximum = MeasConfigurator.RangePointsCount;
            progressBar1.Value = 0;

            progressBar1.Visible = true;
            m_LockChart = true;
            UpdateLegendText(chart1, false);
            UpdateLegendText(chartMon, false);

            btnStart.FlatStyle = FlatStyle.Standard;
            mmMeasureStart.Enabled = false;
            btnStart.Enabled = false;

            mmMeasurePause.Enabled = true;
            mmMeasureStop.Enabled = true;
            btnPause.Enabled = true;
            btnStop.Enabled = true;

            tmrTimeCounter.Enabled = true;

            mmMeasureParams.Enabled = false;
            if (!SetMeas())
            {
                Task.Run(() => PlaySound("Err"));
            }
        }

        void PlaySound(string msg)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            player.SoundLocation = Application.StartupPath + "\\sounds\\" + msg + ".wav";
            player.Load();
            if (player.IsLoadCompleted) player.Play();
        }

        /// <summary>
        /// Настройка измерения для текущей точки
        /// </summary>
        /// <returns></returns>
        private bool SetMeas()
        {
            if (timer1.Enabled) return false;
            
            m_CurrPoint = MeasConfigurator.NextPoint();

            if(m_CurrPoint == null) // Точки закончились
            {
                StopMeas();
                Task.Run(() => PlaySound("Ok"));
                return true; // Ошибки не было
            }

            long freq = m_CurrPoint.Frequency;
            int ampl = m_CurrPoint.Amplitude;

            // Отображаем номер текущей точки
            int currpoint = MeasConfigurator.CurrentIndex - MeasConfigurator.m_StartPoint + 1;
            int countpoint = MeasConfigurator.m_StopPoint - MeasConfigurator.m_StartPoint + 1;
            lblCurrentPoint.Text = currpoint.ToString() + " из " + countpoint.ToString();
            
            // Отображаем частоту для текущей точки
            double tempfreq;
            UnitsFrequency frequnits;
            
            if (freq < 100000) frequnits = UnitsFrequency.kHz; else frequnits = UnitsFrequency.MHz;
            tempfreq = Converter.Transform(freq, UnitsFrequency.Hz, frequnits);

            lblCurrentFreqTxt.Text = "Текущая частота, " + Converter.ToString<UnitsFrequency>(frequnits, true);
            lblCurrentFreq.Text = string.Format("{0:0.###}", tempfreq); //tempfreq.ToString("F3");

            if (FormMeasParam.UseGenerator)  // Настраиваем генератор
            {
                if (MeasConfigurator.TrackingGenerator)
                {
                    //Analyzer.Set_FREQuency(Converter.DoubleToLong(freq, FormMeasParam.FreqUnits),
                    //    (FrequencyUnits)System.Enum.Parse(typeof(FrequencyUnits), FormMeasParam.FreqUnits));
                    MeasConfigurator.Analyzer.Set_SOURce_POWer(ampl);
                    MeasConfigurator.Analyzer.Start();
                }
                else
                {
                    MeasConfigurator.Generator.Set_FREQuency(freq, UnitsFrequency.Hz);
                    MeasConfigurator.Generator.Set_SOURce_POWer(ampl);
                    MeasConfigurator.Generator.FREQuency_MODE(Modes.FIX);
                    MeasConfigurator.Generator.Start();
                }
            }

            if (FormMeasParam.UseAmplifire)
            {
                string name = MeasConfigurator.Amplifier.Name;
                InstrBands band = m_CurrPoint.Band;
                InstrBands instrband = MeasConfigurator.Amplifier.GetBand();

                // Проверяем диапазон усилителя
#if !DEBUG
                if ((band != InstrBands.NAN) && (instrband != band))
                {
                    MeasConfigurator.Amplifier.StopEmission();
                    bool band_ok = MeasConfigurator.Amplifier.SetBand(band);
                    if (band_ok)
                    {
                        int ht = MeasConfigurator.Amplifier.HeaterTimer();
                        if (ht > 0)
                        {
                            // ожидание нагрева
                            FormWaitMonitor fwm = new FormWaitMonitor();
                            fwm.WaitReverse = true;
                            fwm.WaitTimer = ht + 1;
                            fwm.Message = "Идет нагрев...";
                            fwm.ShowDialog();
                            band_ok = MeasConfigurator.Amplifier.HeaterTimer() > 0;
                        }
                    }
                    if (!band_ok)
                    {
                        MessageBox.Show(this, "Не удалось переключить диапазон усилителя",
                                    "Не возможно продолжить", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    MeasConfigurator.Amplifier.StartEmission();
                }
#endif

#if DEBUG
                lblAmpl.Text = name + "  [" + Enum.GetName(typeof(InstrBands), band) + "]";
#else
                lblAmpl.Text = name + "  [" + Enum.GetName(typeof(InstrBands), MeasConfigurator.Amplifier.GetBand()) + "]";
#endif

                MeasConfigurator.Amplifier.StartEmission();
            }

            if (FormMeasParam.UseMonitor)   // Включаем монитор поля
            {
                MeasConfigurator.Monitor.Start();
            }

            if (FormMeasParam.UseAnalyzer)  // Настраиваем анализатор
            {
                if (lblRefLevel.Visible) lblRefLevel.Text = "Ref.Level: " + MeasConfigurator.Analyzer.Get_RefLevel();

                MeasConfigurator.Analyzer.Set_UNIT_POWer(MeasConfigurator.UnitsAnalyzer);
                MeasConfigurator.Analyzer.Set_FREQuency_CENTer(freq);
                MeasConfigurator.Analyzer.Set_FREQuency_SPAN(MeasConfigurator.GetSpan());
                MeasConfigurator.Analyzer.Set_SWEep_COUNt(MeasConfigurator.SweepCount);
                MeasConfigurator.Analyzer.StartSinglSweep();
            }

            timer1.Interval = m_CurrPoint.Time_ms;
            timer1.Enabled = true;
            progressBar1.Value++;
            CheckErrors();
            
            return true;
        }

        private void CheckErrors()
        {
            string msg = "";
            bool newerr = false;

#if DEBUG
            double e = rnd.NextDouble();
                if ((e > 0.2) && (e < 0.3))
                    msg = string.Format("Debug Error Message {0:0.##}", e);
                else
                    msg = "Debug Error Message";
#endif
            if ((MeasConfigurator.Generator != null) && MeasConfigurator.Generator.GetError(out msg))
            {
                if(!m_ErrorMsg.Contains("[Генератор]\t" + msg + "\n"))
                {
                    newerr = true;
                    m_ErrorMsg += "[Генератор]\t" + msg + "\n";
                }
            }

            if ((MeasConfigurator.Amplifier != null) && MeasConfigurator.Amplifier.GetError(out msg))
            {
                if (!m_ErrorMsg.Contains("[Усилитель]\t" + msg + "\n"))
                {
                    newerr = true;
                    m_ErrorMsg += "[Усилитель]\t" + msg + "\n";
                }
            }

            if ((MeasConfigurator.Monitor != null) && MeasConfigurator.Monitor.GetError(out msg))
            {
                if (!m_ErrorMsg.Contains("[Монитор]\t" + msg + "\n"))
                {
                    newerr = true;
                    m_ErrorMsg += "[Монитор]\t" + msg + "\n";
                }
            }

            if ((MeasConfigurator.Analyzer != null) && MeasConfigurator.Analyzer.GetError(out msg))
            {
                if (!m_ErrorMsg.Contains("[Анализатор]\t" + msg + "\n"))
                {
                    newerr = true;
                    m_ErrorMsg += "[Анализатор]\t" + msg + "\n";
                }
            }

            if(newerr) Task.Run(() => PlaySound("Tuk"));
            lblInstrError.Visible = m_ErrorMsg.Length > 0;
        }

        //***********************************************************************
        //                  Обновление огибающих
        //***********************************************************************
        private void UpdateMinMax(SeriesCollection sc, bool _ForAll = false)
        {
            sc["LineMax"].Points.Clear();
            sc["LineMin"].Points.Clear();
            double min, max;
            
            int index = sc.Count - 1;
            for (int pointIndex = 0; pointIndex < sc[index].Points.Count; pointIndex++)
            {
                min = int.MaxValue;
                max = int.MinValue;
                for (int seriesIndex = 2; seriesIndex < sc.Count; seriesIndex++)
                {
                    if (!sc[seriesIndex].Enabled) continue;
                    if (min > sc[seriesIndex].Points[pointIndex].YValues[0]) min = sc[seriesIndex].Points[pointIndex].YValues[0];
                    if (max < sc[seriesIndex].Points[pointIndex].YValues[0]) max = sc[seriesIndex].Points[pointIndex].YValues[0];
                }
                sc["LineMax"].Points.AddXY(sc[index].Points[pointIndex].XValue, max);
                sc["LineMin"].Points.AddXY(sc[index].Points[pointIndex].XValue, min);
            }
        }
        //***********************************************************************
        //                  Таймер измерения точки
        //***********************************************************************
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            double px, py, pyy;

            CheckErrors();

            px = m_CurrPoint.Frequency; // Hz
            px /= 1000000;            // MHz

            // Отображаем напряженность поля если доступно
            if (MeasConfigurator.ShowFS && (MeasConfigurator.Monitor != null) && MeasConfigurator.Monitor.Connected)
            {
                lblFieldStrength.Text = MeasConfigurator.Monitor.GetFS().ToString("F1");
            }

            // Если меряем с усилителем, отображаем прямую и обратную мощности
            if (MeasConfigurator.Amplifier != null)
            {
                double pow = MeasConfigurator.Amplifier.GetFwdPower();
                if (MeasConfigurator.Amplifier.AmpID == VisaAmplifier.AmpIDs.AR) txtARFwdPow.Text = pow.ToString("F0") + "dBm";
                else
                {
                    txtBonnFwdPow.Text = pow.ToString("F0");
                    if (pow >= 0) pbrBonnFwdPow.Value = (int)pow; else pbrBonnFwdPow.Value = 0;
                }
                pow = MeasConfigurator.Amplifier.GetRefPower();
                if (MeasConfigurator.Amplifier.AmpID == VisaAmplifier.AmpIDs.AR) txtARRefPow.Text = pow.ToString("F0") + "dBm";
                else
                {
                    txtBonnRefPow.Text = pow.ToString("F0");
                    if (pow >= 0) pbrBonnRefPow.Value = (int)pow; else pbrBonnRefPow.Value = 0;
                }
            }

            // Добавляем на график
            if (FormMeasParam.UseAnalyzer)
            {
                py = MeasConfigurator.Analyzer.MARKer_MAXimum().Y;
#if DEBUG
                py = rnd.NextDouble();
#endif
                chart1.Series[SeriesIndex].Points.AddXY(px, py);
                lblResAmpl.Text = string.Format("{0:0.###}", py); //py.ToString("F2");// результат анализатора

                m_Measures.AddToAnalyzer(py);
                
                SetAxisY(chart1);
            }
            if (FormMeasParam.UseMonitor)
            {
                pyy = MeasConfigurator.Monitor.GetValue();
#if DEBUG
                pyy = rnd.NextDouble();
#endif
                chartMon.Series[SeriesIndex].Points.AddXY(px, pyy);
                if (FormMeasParam.UseAnalyzer)
                {
                    lblResAmpl_2.Text = string.Format("{0:0.###}", pyy); //pyy.ToString("F2");// результат монитора
                }
                else
                {
                    lblResAmpl.Text = string.Format("{0:0.###}", pyy); //pyy.ToString("F2");// результат монитора
                }
                m_Measures.AddToMonitor(pyy);

                SetAxisY(chartMon);
            }
            // Отображаем на панели справа
            UnitsFrequency frequnits;
            double tempfreq;
            if (m_CurrPoint.Frequency < 100000) frequnits = UnitsFrequency.kHz; else frequnits = UnitsFrequency.MHz;
            tempfreq = Converter.Transform(m_CurrPoint.Frequency, UnitsFrequency.Hz, frequnits);

            lblResFreqTxt.Text = "Частота, " + Converter.ToString<UnitsFrequency>(frequnits, true);
            lblResFreq.Text = string.Format("{0:0.###}", tempfreq); //tempfreq.ToString("F3");
            lblResInPointNum.Text = "Результат в точке: " + (m_CurrPoint.Index + 1).ToString();
            
            bool ok = SetMeas();
            if (!ok)            // Если возникла ошибка при настройке следующейточки
            {
                StopMeas(true); // Стоп с удалением последнего измерения
                Task.Run(() => PlaySound("Err"));
            }
        }

        /// <summary>
        /// Установка min/max для оси Y указанного чарта
        /// </summary>
        /// <param name="_ct"></param>
        private void SetAxisY(Chart _ct)
        {
            double min = double.MaxValue;
            double max = double.MinValue;
            foreach (var s in _ct.Series)
            {
                foreach (var p in s.Points)
                {
                    if (p.YValues[0] > max) max = p.YValues[0];
                    if (p.YValues[0] < min) min = p.YValues[0];
                }
            }
            if(min < max)
            {
                _ct.ChartAreas[0].AxisY.Interval = 0;
                _ct.ChartAreas[0].AxisY.Minimum = min - (max - min) * 0.1; // +10% сверху и снизу
                _ct.ChartAreas[0].AxisY.Maximum = max + (max - min) * 0.1;
            }
            else
            {
                _ct.ChartAreas[0].AxisY.Interval = 1;
                _ct.ChartAreas[0].AxisY.Minimum = min - 1; 
                _ct.ChartAreas[0].AxisY.Maximum = max + 1;
            }
        }

        //***********************************************************************
        //                  Меню Вкл/Выкл Огибающих
        //***********************************************************************
        private void mmMinMax_CheckedChanged(object sender, EventArgs e)
        {
            chart1.Series["LineMax"].Enabled = mmMinMax.Checked;
            chart1.Series["LineMin"].Enabled = mmMinMax.Checked;
            chartMon.Series["LineMax"].Enabled = mmMinMax.Checked;
            chartMon.Series["LineMin"].Enabled = mmMinMax.Checked;

            UnZoomCharts();
            if (chart1.Visible) UpdateMinMax(chart1.Series);
            if (chartMon.Visible) UpdateMinMax(chartMon.Series);
        }
        //***********************************************************************
        //                  Меню выделение линии
        //***********************************************************************
        private void mmLinesListSubItem_Click(object sender, EventArgs e)
        {
            if (chart1.Visible) SelectLine(chart1.Series, ((ToolStripMenuItem)sender).Text);
            if (chartMon.Visible) SelectLine(chartMon.Series, ((ToolStripMenuItem)sender).Text);
            foreach (ToolStripMenuItem mm in mmLinesList.DropDownItems) mm.Checked = false;
            ((ToolStripMenuItem)sender).Checked = true;
            foreach (ToolStripMenuItem o in mmLinesList.DropDownItems)
            {
                if (o.Name != ((ToolStripMenuItem)sender).Name) o.Checked = false;
            }
        }
        //***********************************************************************
        //                  Выделение линии по имени
        //***********************************************************************
        private void SelectLine(SeriesCollection sc, string name)
        {
            for (int i = 2; i < sc.Count; i++)
            {
                sc[i].BorderWidth = 1;
                sc[i].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
                sc[i].ShadowOffset = 0;
            }
            sc[name].BorderWidth = 2;
            sc[name].ShadowOffset = 2;
            sc[name].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
        }
        //***********************************************************************
        //                  Откючить выделение всех линий
        //***********************************************************************
        private void UnSelectLines(SeriesCollection sc)
        {
            for (int i = 2; i < sc.Count; i++)
            {
                sc[i].BorderWidth = 1;
                sc[i].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
                sc[i].ShadowOffset = 0;
            }
            mmUnselectAll.Checked = true;
            foreach (ToolStripMenuItem o in mmLinesList.DropDownItems)
            {
                if (o.Name != "mmUnselectAll") o.Checked = false;
            }
        }
        //***********************************************************************
        //                  Меню отключить выделение линий
        //***********************************************************************
        private void mmLinesLight(object sender, EventArgs e)
        {
            UnSelectLines(chart1.Series);
            UnSelectLines(chartMon.Series);
        }
        //***********************************************************************
        //                  Зуммирование графика колесом мыши
        //***********************************************************************
        private int FZoomLevel = 0;
        private int CZoomScale = 2;
        private void chart1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (m_LockChart) return;
            var chart = (sender as Chart);

            try
            {
                Axis xAxis = chart1.ChartAreas[0].AxisX;
                double xMin = xAxis.ScaleView.ViewMinimum;
                double xMax = xAxis.ScaleView.ViewMaximum;
                double xPixelPos = xAxis.PixelPositionToValue(e.Location.X);

                if (e.Delta < 0 && FZoomLevel > 0)
                {
                    // Scrolled down, meaning zoom out
                    if (--FZoomLevel <= 0)
                    {
                        FZoomLevel = 0;
                        xAxis.ScaleView.ZoomReset();
                    }
                    else
                    {
                        double xStartPos = Math.Max(xPixelPos - (xPixelPos - xMin) * CZoomScale, 0);
                        double xEndPos = Math.Min(xStartPos + (xMax - xMin) * CZoomScale, xAxis.Maximum);
                        xAxis.ScaleView.Zoom(xStartPos, xEndPos);
                    }
                }
                else if (e.Delta > 0)
                {
                    // Scrolled up, meaning zoom in
                    double xStartPos = Math.Max(xPixelPos - (xPixelPos - xMin) / CZoomScale, 0);
                    double xEndPos = Math.Min(xStartPos + (xMax - xMin) / CZoomScale, xAxis.Maximum);
                    xAxis.ScaleView.Zoom(xStartPos, xEndPos);
                    FZoomLevel++;
                }
            }
            catch { }

            /*
            var xAxis = chart.ChartAreas[0].AxisX;

            try
            {
                if (e.Delta < 0) // Scrolled down.
                {
                    if (xAxis.ScaleView.IsZoomed)
                    {
                        var xMin = xAxis.ScaleView.ViewMinimum;
                        var xMax = xAxis.ScaleView.ViewMaximum;

                        var posXStart = xAxis.PixelPositionToValue(e.Location.X) - (xMax - xMin);
                        var posXFinish = xAxis.PixelPositionToValue(e.Location.X) + (xMax - xMin);

                        frmLog.Log(string.Format("min = {0}   max = {1}  posXFinish = {2}  <> {3}  ScaleView = {4}",
                            xMin, xMax, posXFinish, xAxis.Maximum - xAxis.Maximum * 0.05, xAxis.ScaleView.));
                        xAxis.ScaleView.Zoom(posXStart, posXFinish);
                        if (posXFinish > (xAxis.Maximum - xAxis.Maximum * 0.05)) xAxis.ScaleView.ZoomReset();
                    }
                }
                else if (e.Delta > 0) // Scrolled up.
                {
                    var xMin = xAxis.ScaleView.ViewMinimum;
                    var xMax = xAxis.ScaleView.ViewMaximum;

                    var posXStart = xAxis.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 3;
                    var posXFinish = xAxis.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 3;

                    xAxis.ScaleView.Zoom(posXStart, posXFinish);
                }
            }
            catch { }*/
        }
        //***********************************************************************
        //                  Меню Пауза
        //***********************************************************************
        private void mmMeasurePause_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            btnStart.FlatStyle = FlatStyle.Flat;
            mmMeasureStart.Enabled = true;
            btnStart.Enabled = true;

            mmMeasurePause.Enabled = false;
            btnPause.Enabled = false;
            btnPause.FlatStyle = FlatStyle.Standard;
        }
        //***********************************************************************
        //                  Меню Стоп
        //***********************************************************************
        private void mmMeasureStop_Click(object sender, EventArgs e)
        {
            StopMeas(true);
        }
        //***********************************************************************
        //                  Сообщение об ошибке подключения
        //***********************************************************************
        private void InstrumentError(string msg)
        {
            MessageBox.Show(this, msg, "Ошибка соединиеня с прибором", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        //***********************************************************************
        //                  Добавление линий в меню выделения
        //***********************************************************************
        private void AddLineToMenu(string LineName)
        {
            foreach(ToolStripMenuItem mm in mmLinesList.DropDownItems)
            {
                if (mm.Text == LineName) return;
            }
            mmLinesList.Visible = true;   // Включаем список линий в меню (для выбора какую выделить)
            ToolStripMenuItem item = new ToolStripMenuItem(LineName);
            item.Click += mmLinesListSubItem_Click;
            mmLinesList.DropDownItems.Add(item);
        }
        //***********************************************************************
        //                  Остановить все приборы
        //***********************************************************************
        private void StopAllInstruments()
        {
            if (FormMeasParam.UseAmplifire) MeasConfigurator.Amplifier.StopEmission();
            if (FormMeasParam.UseAnalyzer) MeasConfigurator.Analyzer.Stop();
            if (FormMeasParam.UseGenerator) MeasConfigurator.Generator.Stop();
            if (FormMeasParam.UseMonitor) MeasConfigurator.Monitor.Stop();
        }
        //***********************************************************************
        //                  Стоп измерения
        //***********************************************************************
        private void StopMeas(bool RemoveLast = false)
        {
            StopAllInstruments();
            mmMeasurePause.Enabled = false;
            mmMeasureStop.Enabled = false;
            btnPause.Enabled = false;
            btnStop.Enabled = false;
            timer1.Enabled = false;
            tmrTimeCounter.Enabled = false;
            progressBar1.Visible = false;
            btnStart.FlatStyle = FlatStyle.Flat;
            mmMeasureStart.Enabled = true;
            btnStart.Enabled = true;
            if (RemoveLast)
            {
                if (FormMeasParam.UseAnalyzer) chart1.Series.RemoveAt(SeriesIndex);
                if (FormMeasParam.UseMonitor) chartMon.Series.RemoveAt(SeriesIndex);

                m_Measures.RemoveLast();

                if (mmSelectCurrent.Checked)
                {
                    if (FormMeasParam.UseAnalyzer) SelectLine(chart1.Series, chart1.Series[chart1.Series.Count - 1].Name);
                    if (FormMeasParam.UseMonitor) SelectLine(chartMon.Series, chartMon.Series[chartMon.Series.Count - 1].Name);
                }
            }
            else
            {
                if (FormMeasParam.UseAnalyzer) AddLineToMenu(chart1.Series[SeriesIndex].Name);
                else if (FormMeasParam.UseMonitor) AddLineToMenu(chartMon.Series[SeriesIndex].Name);
                SetNotSaved(true);
                MeasConfigurator.NewMeas = false;
            }
            if (FormMeasParam.UseAnalyzer) UpdateMinMax(chart1.Series);
            if (FormMeasParam.UseMonitor) UpdateMinMax(chartMon.Series);
            mmMeasureParams.Enabled = true;
            m_LockChart = false;
            lblAmpl.Text = "";

            m_mutexObj.ReleaseMutex();
            m_mutexObj.Dispose();
            this.TopMost = true;
            this.TopMost = false;
        }
        //***********************************************************************
        //                  Отображение значений в точке на легенде графика
        //***********************************************************************
        private void UpdateLegendText(Chart ct, bool ShowXY = true)
        {
            double xpos = ct.ChartAreas[0].CursorX.Position;
            foreach (var Line in ct.Series)
            {
                if ((Line.Name != "LineMax") && (Line.Name != "LineMin"))
                {
                    if (ShowXY)
                    {
                        var PrevPoint = Line.Points[0];
                        foreach (var NextPoint in Line.Points)
                        {
                            if ((PrevPoint.XValue <= xpos) && (NextPoint.XValue >= xpos))
                            {
                                double vx, vy;
                                if (xpos - PrevPoint.XValue < NextPoint.XValue - xpos)
                                {
                                    vx = PrevPoint.XValue;
                                    vy = PrevPoint.YValues[0];
                                }
                                else
                                {
                                    vx = NextPoint.XValue;
                                    vy = NextPoint.YValues[0];
                                }
                                Line.LegendText = Line.Name + "\t\n[" + vx.ToString("F1") + " : " + vy.ToString("F1") + "]";

                                break;
                            }
                            PrevPoint = NextPoint;
                        }
                    }
                    else
                    {
                        Line.LegendText = Line.Name;
                    }
                }
            }
        }
        //***********************************************************************
        //                  Перемещение мыши по графику
        //***********************************************************************
        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            Chart ct1 = (sender as Chart);
            Chart ct2;
            if (!ct1.Visible || m_LockChart || pnlExcelWait.Visible) return;

            try
            {
                if (ct1.Name == "chart1") ct2 = chartMon; else ct2 = chart1;

                if ((ct1.Series.Count <= 2) || m_LockChart) return;
                double xpos = ct1.ChartAreas[0].AxisX.PixelPositionToValue(e.X);
                if (mmCursorMouseMove.Checked)
                {
                    ct1.ChartAreas[0].CursorX.Position = xpos;
                    UpdateLegendText(ct1);
                    if (mmLinkCursors.Checked)
                    {
                        ct2.ChartAreas[0].CursorX.Position = xpos;
                        UpdateLegendText(ct2);
                    }
                }
                else
                {
                    chart1.ChartAreas[0].CursorX.Position = 0;
                    chartMon.ChartAreas[0].CursorX.Position = 0;
                }
            }
            catch (Exception) {; }
        }
        //***********************************************************************
        //                  Меню Выделить текущюю линию
        //***********************************************************************
        private void mmSelectCurrent_CheckedChanged(object sender, EventArgs e)
        {
            UnSelectLines(chart1.Series);
            UnSelectLines(chartMon.Series);
            if (mmSelectCurrent.Checked)
            {
                if (FormMeasParam.UseAnalyzer) SelectLine(chart1.Series, chart1.Series[chart1.Series.Count - 1].Name);
                if (FormMeasParam.UseMonitor) SelectLine(chartMon.Series, chartMon.Series[chartMon.Series.Count - 1].Name);
            }
        }
        //***********************************************************************
        //                  Меню Логарифмическая ось X
        //***********************************************************************
        private void mmLogX_CheckedChanged(object sender, EventArgs e)
        {
            CheckLogAxis();
            if (mmLogX.Checked)
            {
                btnLogX.FlatStyle = FlatStyle.Standard;
            }
            else
            {
                btnLogX.FlatStyle = FlatStyle.Flat;
            }
        }
        //***********************************************************************
        //                  Меню Показать/Скрыть легенду
        //***********************************************************************
        private void mmShowLegend_CheckedChanged(object sender, EventArgs e)
        {
            chart1.Legends[0].Enabled = (sender as ToolStripMenuItem).Checked;
            chartMon.Legends[0].Enabled = (sender as ToolStripMenuItem).Checked;
        }
        //***********************************************************************
        //                  Обновление позиции курсора на графике
        //***********************************************************************
        private void chart1_CursorPositionChanged(object sender, System.Windows.Forms.DataVisualization.Charting.CursorEventArgs e)
        {
            if (mmCursorMouseMove.Checked)
            {
                UpdateLegendText((sender as Chart));
            }
        }
        //***********************************************************************
        //                  Меню Автокурсор
        //***********************************************************************
        private void mmCursorMouseMove_CheckedChanged(object sender, EventArgs e)
        {
            if (!mmCursorMouseMove.Checked)
            {
                UpdateLegendText(chart1, false);
                UpdateLegendText(chartMon, false);
                chart1.ChartAreas[0].CursorX.Position = 0;
                chartMon.ChartAreas[0].CursorX.Position = 0;
            }
            mmLinkCursors.Visible = mmCursorMouseMove.Checked && chart1.Visible && chartMon.Visible;
        }
        //***********************************************************************
        //                  Меню Закрыть
        //***********************************************************************
        private void mmFileClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //***********************************************************************
        //                  Меню О программе
        //***********************************************************************
        private void mmHelpAbout_Click(object sender, EventArgs e)
        {
            (new FormAbout()).ShowDialog();
        }
        //***********************************************************************
        //                  Таймер Счетчик времени измерения
        //***********************************************************************
        private void tmrTimeCounter_Tick(object sender, EventArgs e)
        {
            m_MeasTime++;
            lblMeasTime.Text = (m_MeasTime / 60).ToString("F0") + "мин." +
                (m_MeasTime - (int)(m_MeasTime / 60) * 60).ToString("F0") + "сек.";
        }
        
        //***********************************************************************
        //                  Подгонка размеров графика монитора
        //***********************************************************************
        private void frmMain_Resize(object sender, EventArgs e)
        {
            chartMon.Height = panel1.Height / 2;
            pnlExcelWait.Location = new Point(chart1.Width / 2 - pnlExcelWait.Width / 2,
                panel1.Height / 2 + pnlExcelWait.Height / 6);
        }
        //***********************************************************************
        //                  Клик на RefLevel
        //***********************************************************************
        private void lblRefLevel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(MeasConfigurator.Analyzer != null)
            {
                txtRefLevel.Text = MeasConfigurator.Analyzer.Get_RefLevel();
                pnlSetRefLevel.Visible = true;
            }
        }
        //***********************************************************************
        //                  Принять изменение RefLevel
        //***********************************************************************
        private void btnOk_Click(object sender, EventArgs e)
        {
            MeasConfigurator.Analyzer.Set_RefLevel(txtRefLevel.Text);
            pnlSetRefLevel.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mmLogX.Checked = false;
            FormReset(true);
            mmLoaded.Visible = false;
        }

        private void DeleteAllLines()
        {
            while (chart1.Series.Count > 2) chart1.Series.RemoveAt(chart1.Series.Count - 1);
            while (chartMon.Series.Count > 2) chartMon.Series.RemoveAt(chartMon.Series.Count - 1);
            while (mmLinesList.DropDownItems.Count > 1)
            {
                mmLinesList.DropDownItems.RemoveAt(mmLinesList.DropDownItems.Count - 1);
            }
        }

        private void UnZoomCharts()
        {
            if (m_ctZoom)
            {
                m_ctZoom = false;
                chart1.Visible = true;
                chartMon.Visible = true;
                chartMon.Dock = DockStyle.Bottom;
            }
        }

        private void chart1_DoubleClick(object sender, EventArgs e)
        {
            if (chart1.Visible && chartMon.Visible && !m_LockChart)
            {
                m_ctZoom = true;
                if (((Chart)sender) == chart1)
                {
                    chartMon.Visible = false;
                }
                else
                {
                    chart1.Visible = false;
                    chartMon.Dock = DockStyle.Fill;
                }
            }
            else UnZoomCharts();
        }

        private void mmFileSave_Click(object sender, EventArgs e)
        {
            SetNotSaved(SaveMeasures());
        }

        private void mmFileOpen_Click(object sender, EventArgs e)
        {
            LoadMeasures();
        }

        private void lblInstrError_Click(object sender, EventArgs e)
        {
            MessageBox.Show(m_ErrorMsg, "Ошибки приборов", MessageBoxButtons.OK, MessageBoxIcon.Error);
            m_ErrorMsg = "";
            lblInstrError.Visible = false;
        }

        private void CheckLogAxis()
        {
            if ((chart1.Series[chart1.Series.Count - 1].Points.Count == 0) &&
                (chartMon.Series[chartMon.Series.Count - 1].Points.Count == 0))
            {
                mmLogX.Checked = false;
            }
            if (m_Measures.IsEmpty) return;

            if (mmLogX.Checked)
            {
                chart1.ChartAreas[0].AxisX.IsLogarithmic = true;

                chartMon.ChartAreas[0].AxisX.IsLogarithmic = true;
            }
            else
            {
                chart1.ChartAreas[0].AxisX.IsLogarithmic = false;
                chartMon.ChartAreas[0].AxisX.IsLogarithmic = false;

                chart1.ChartAreas[0].AxisX.Minimum = Converter.Transform(m_Measures.MinFrequency, UnitsFrequency.Hz, UnitsFrequency.MHz);
                chart1.ChartAreas[0].AxisX.Maximum = Converter.Transform(m_Measures.MaxFrequency, UnitsFrequency.Hz, UnitsFrequency.MHz);
                chartMon.ChartAreas[0].AxisX.Minimum = Converter.Transform(m_Measures.MinFrequency, UnitsFrequency.Hz, UnitsFrequency.MHz);
                chartMon.ChartAreas[0].AxisX.Maximum = Converter.Transform(m_Measures.MaxFrequency, UnitsFrequency.Hz, UnitsFrequency.MHz);
            }
            
        }

        private void btnLogX_Click(object sender, EventArgs e)
        {
            mmLogX.Checked = !mmLogX.Checked;
        }

        private void SetChartColors(Chart _ct, ChartColorPalette _palette, Color _backcolor, Color _legendtext, Color _grid, Color _min, Color _max)
        {
            _ct.ChartAreas[0].BackColor = _backcolor;
            _ct.Legends[0].BackColor = _backcolor;
            _ct.Legends[0].ForeColor = _legendtext;
            _ct.ChartAreas[0].AxisX.MajorGrid.LineColor = _grid;
            _ct.ChartAreas[0].AxisY.MajorGrid.LineColor = _grid;
            _ct.Series[1].Color = _min;
            _ct.Series[0].Color = _max;
            _ct.Palette = _palette;
        }

        private void mmSetColors_Click(object sender, EventArgs e)
        {
            FormSetColors frmColors = new FormSetColors();
            frmColors.clBackColor = chart1.ChartAreas[0].BackColor;
            frmColors.clGrid = chart1.ChartAreas[0].AxisX.MajorGrid.LineColor;
            frmColors.clLegendText = chart1.Legends[0].ForeColor;
            frmColors.clMin = chart1.Series[1].Color;
            frmColors.clMax = chart1.Series[0].Color;
            frmColors.Palette = chart1.Palette;

            frmColors.ShowDialog();
            if (frmColors.DialogResult == DialogResult.OK)
            {
                SetChartColors(chart1, frmColors.Palette, frmColors.clBackColor, frmColors.clLegendText, frmColors.clGrid, frmColors.clMin, frmColors.clMax);
                SetChartColors(chartMon, frmColors.Palette, frmColors.clBackColor, frmColors.clLegendText, frmColors.clGrid, frmColors.clMin, frmColors.clMax);
            }
        }

        private void mmHideLoadedLines_Click(object sender, EventArgs e)
        {
            foreach(var meas in m_Measures)
            {
                if (meas.FromFile)
                {
                    if (chart1.Visible)
                    {
                        chart1.Series[meas.Name].Enabled = !mmHideLoadedLines.Checked;
                        UpdateMinMax(chart1.Series, true);
                    }
                    if (chartMon.Visible)
                    {
                        chartMon.Series[meas.Name].Enabled = !mmHideLoadedLines.Checked;
                        UpdateMinMax(chartMon.Series, true);
                    }
                }
            }
            
        }

        private void mmRemoveLoadedLines_Click(object sender, EventArgs e)
        {
            foreach (var meas in m_Measures)
            {
                if (meas.FromFile)
                {
                    if (chart1.Visible)
                    {
                        chart1.Series.RemoveAt(chart1.Series.IndexOf(meas.Name));
                        UpdateMinMax(chart1.Series, true);
                    }
                    if (chartMon.Visible)
                    {
                        chart1.Series.RemoveAt(chart1.Series.IndexOf(meas.Name));
                        UpdateMinMax(chartMon.Series, true);
                    }
                }
            }
            for(int i = 0; i < m_Measures.Count; i++)
            {
                if (m_Measures[i].FromFile)
                {
                    RemoveFromMenu(m_Measures[i].Name);
                    m_Measures.Remove(i);
                    i--;
                }
            }
            
            mmLoaded.Visible = false;
        }

        private void RemoveFromMenu(string _NameOfLine)
        {
            for(int i = 0; i < mmLinesList.DropDownItems.Count; i++)
            {
                if(mmLinesList.DropDownItems[i].Text == _NameOfLine)
                {
                    mmLinesList.DropDownItems.RemoveAt(i);
                    return;
                }
            }
        }

        private void lblNotSaved_Click(object sender, EventArgs e)
        {
            SaveMeasures();
        }
    }
}

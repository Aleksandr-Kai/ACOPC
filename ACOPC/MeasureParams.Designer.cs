namespace ACOPC
{
    partial class frmMeasureParams
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAmpl = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTime = new System.Windows.Forms.ToolStripMenuItem();
            this.mmSetBands = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mmRemoveRows = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbStepLog = new System.Windows.Forms.RadioButton();
            this.rbStepLine = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.cmbxTimeUnits = new System.Windows.Forms.ComboBox();
            this.cmbxStopFreqUnits = new System.Windows.Forms.ComboBox();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.cmbxStartFreqUnits = new System.Windows.Forms.ComboBox();
            this.txtAmplitude = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtStopFreq = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStartFreq = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.nudPointsCount = new System.Windows.Forms.NumericUpDown();
            this.rbCountOnRangePoints = new System.Windows.Forms.RadioButton();
            this.rbMoreThenPoints = new System.Windows.Forms.RadioButton();
            this.rbFixedPoints = new System.Windows.Forms.RadioButton();
            this.grpInstr = new System.Windows.Forms.GroupBox();
            this.btnInstrs = new System.Windows.Forms.Button();
            this.cbxShowFS = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbxMonitorChannel = new System.Windows.Forms.ComboBox();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.cmbxGenMode = new System.Windows.Forms.ComboBox();
            this.cbxAddMeasure = new System.Windows.Forms.CheckBox();
            this.cbxUseAmpl = new System.Windows.Forms.CheckBox();
            this.cbxUseGen = new System.Windows.Forms.CheckBox();
            this.cbxUseMonitor = new System.Windows.Forms.CheckBox();
            this.cbxUseAnalyzer = new System.Windows.Forms.CheckBox();
            this.btnGenerateMeasure = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.cmbxTemplates = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLineName = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblBeginEnd = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.grpAnParams = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cmbxAnalyzerUnits = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.nudSweepCount = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbxCorrectionImpedance = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbxInputCoupling = new System.Windows.Forms.ComboBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.nudSpan = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbxBandWidth = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.lblPointsCount = new System.Windows.Forms.Label();
            this.DGV = new System.Windows.Forms.DataGridView();
            this.mmImportTemplates = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tmsiFreq = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSetMeasRange = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPointsCount)).BeginInit();
            this.grpInstr.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.grpAnParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSweepCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmsiFreq,
            this.tsmiAmpl,
            this.tsmiTime,
            this.mmSetBands,
            this.toolStripSeparator1,
            this.mmRemoveRows});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(200, 120);
            // 
            // tsmiAmpl
            // 
            this.tsmiAmpl.Name = "tsmiAmpl";
            this.tsmiAmpl.Size = new System.Drawing.Size(199, 22);
            this.tsmiAmpl.Text = "Изменить Амплитуду";
            this.tsmiAmpl.Click += new System.EventHandler(this.ToolStripMenuItem_SetNewAmpl_Click);
            // 
            // tsmiTime
            // 
            this.tsmiTime.Name = "tsmiTime";
            this.tsmiTime.Size = new System.Drawing.Size(199, 22);
            this.tsmiTime.Text = "Изменить Время";
            this.tsmiTime.Click += new System.EventHandler(this.ToolStripMenuItem_SetNewTime_Click);
            // 
            // mmSetBands
            // 
            this.mmSetBands.Name = "mmSetBands";
            this.mmSetBands.Size = new System.Drawing.Size(199, 22);
            this.mmSetBands.Text = "Установить диапазоны";
            this.mmSetBands.Click += new System.EventHandler(this.mmSetBands_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(196, 6);
            // 
            // mmRemoveRows
            // 
            this.mmRemoveRows.Name = "mmRemoveRows";
            this.mmRemoveRows.Size = new System.Drawing.Size(199, 22);
            this.mmRemoveRows.Text = "Удалить строки";
            this.mmRemoveRows.Click += new System.EventHandler(this.mmRemoveRows_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbStepLog);
            this.groupBox1.Controls.Add(this.rbStepLine);
            this.groupBox1.Location = new System.Drawing.Point(12, 159);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 71);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Изменение частоты";
            // 
            // rbStepLog
            // 
            this.rbStepLog.AutoSize = true;
            this.rbStepLog.Location = new System.Drawing.Point(15, 42);
            this.rbStepLog.Name = "rbStepLog";
            this.rbStepLog.Size = new System.Drawing.Size(119, 17);
            this.rbStepLog.TabIndex = 8;
            this.rbStepLog.Text = "Логарифмическое";
            this.rbStepLog.UseVisualStyleBackColor = true;
            // 
            // rbStepLine
            // 
            this.rbStepLine.AutoSize = true;
            this.rbStepLine.Checked = true;
            this.rbStepLine.Location = new System.Drawing.Point(15, 19);
            this.rbStepLine.Name = "rbStepLine";
            this.rbStepLine.Size = new System.Drawing.Size(75, 17);
            this.rbStepLine.TabIndex = 7;
            this.rbStepLine.TabStop = true;
            this.rbStepLine.Text = "Линейное";
            this.rbStepLine.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.cmbxTimeUnits);
            this.groupBox2.Controls.Add(this.cmbxStopFreqUnits);
            this.groupBox2.Controls.Add(this.txtTime);
            this.groupBox2.Controls.Add(this.cmbxStartFreqUnits);
            this.groupBox2.Controls.Add(this.txtAmplitude);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtStopFreq);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtStartFreq);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(274, 141);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Параметры";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(192, 79);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(28, 13);
            this.label16.TabIndex = 23;
            this.label16.Text = "dBm";
            // 
            // cmbxTimeUnits
            // 
            this.cmbxTimeUnits.DisplayMember = "0";
            this.cmbxTimeUnits.FormattingEnabled = true;
            this.cmbxTimeUnits.Items.AddRange(new object[] {
            "мс",
            "с"});
            this.cmbxTimeUnits.Location = new System.Drawing.Point(195, 102);
            this.cmbxTimeUnits.Name = "cmbxTimeUnits";
            this.cmbxTimeUnits.Size = new System.Drawing.Size(62, 21);
            this.cmbxTimeUnits.TabIndex = 6;
            this.cmbxTimeUnits.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbxTimeUnits_KeyPress);
            // 
            // cmbxStopFreqUnits
            // 
            this.cmbxStopFreqUnits.DisplayMember = "0";
            this.cmbxStopFreqUnits.FormattingEnabled = true;
            this.cmbxStopFreqUnits.Items.AddRange(new object[] {
            "Гц",
            "кГц",
            "МГц",
            "ГГц"});
            this.cmbxStopFreqUnits.Location = new System.Drawing.Point(195, 49);
            this.cmbxStopFreqUnits.Name = "cmbxStopFreqUnits";
            this.cmbxStopFreqUnits.Size = new System.Drawing.Size(62, 21);
            this.cmbxStopFreqUnits.TabIndex = 3;
            this.cmbxStopFreqUnits.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbxTimeUnits_KeyPress);
            // 
            // txtTime
            // 
            this.txtTime.Location = new System.Drawing.Point(123, 102);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(66, 20);
            this.txtTime.TabIndex = 5;
            this.txtTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTime_KeyPress);
            // 
            // cmbxStartFreqUnits
            // 
            this.cmbxStartFreqUnits.DisplayMember = "0";
            this.cmbxStartFreqUnits.FormattingEnabled = true;
            this.cmbxStartFreqUnits.Items.AddRange(new object[] {
            "Гц",
            "кГц",
            "МГц",
            "ГГц"});
            this.cmbxStartFreqUnits.Location = new System.Drawing.Point(195, 23);
            this.cmbxStartFreqUnits.Name = "cmbxStartFreqUnits";
            this.cmbxStartFreqUnits.Size = new System.Drawing.Size(62, 21);
            this.cmbxStartFreqUnits.TabIndex = 1;
            this.cmbxStartFreqUnits.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbxTimeUnits_KeyPress);
            // 
            // txtAmplitude
            // 
            this.txtAmplitude.Location = new System.Drawing.Point(123, 76);
            this.txtAmplitude.Name = "txtAmplitude";
            this.txtAmplitude.Size = new System.Drawing.Size(66, 20);
            this.txtAmplitude.TabIndex = 4;
            this.txtAmplitude.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmplitude_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Время измерений";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Амплитуда";
            // 
            // txtStopFreq
            // 
            this.txtStopFreq.Location = new System.Drawing.Point(123, 49);
            this.txtStopFreq.Name = "txtStopFreq";
            this.txtStopFreq.Size = new System.Drawing.Size(66, 20);
            this.txtStopFreq.TabIndex = 2;
            this.txtStopFreq.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTime_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Конечная частота";
            // 
            // txtStartFreq
            // 
            this.txtStartFreq.Location = new System.Drawing.Point(123, 23);
            this.txtStartFreq.Name = "txtStartFreq";
            this.txtStartFreq.Size = new System.Drawing.Size(66, 20);
            this.txtStartFreq.TabIndex = 0;
            this.txtStartFreq.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTime_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Начальная частота";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.nudPointsCount);
            this.groupBox3.Controls.Add(this.rbCountOnRangePoints);
            this.groupBox3.Controls.Add(this.rbMoreThenPoints);
            this.groupBox3.Controls.Add(this.rbFixedPoints);
            this.groupBox3.Location = new System.Drawing.Point(12, 236);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(274, 125);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Распределение точек";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Количество точек";
            // 
            // nudPointsCount
            // 
            this.nudPointsCount.Location = new System.Drawing.Point(125, 24);
            this.nudPointsCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudPointsCount.Name = "nudPointsCount";
            this.nudPointsCount.Size = new System.Drawing.Size(64, 20);
            this.nudPointsCount.TabIndex = 9;
            this.nudPointsCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudPointsCount.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // rbCountOnRangePoints
            // 
            this.rbCountOnRangePoints.AutoSize = true;
            this.rbCountOnRangePoints.Location = new System.Drawing.Point(15, 99);
            this.rbCountOnRangePoints.Name = "rbCountOnRangePoints";
            this.rbCountOnRangePoints.Size = new System.Drawing.Size(181, 17);
            this.rbCountOnRangePoints.TabIndex = 12;
            this.rbCountOnRangePoints.Text = "Количество точек на диапазон";
            this.rbCountOnRangePoints.UseVisualStyleBackColor = true;
            // 
            // rbMoreThenPoints
            // 
            this.rbMoreThenPoints.AutoSize = true;
            this.rbMoreThenPoints.Location = new System.Drawing.Point(15, 76);
            this.rbMoreThenPoints.Name = "rbMoreThenPoints";
            this.rbMoreThenPoints.Size = new System.Drawing.Size(229, 17);
            this.rbMoreThenPoints.TabIndex = 11;
            this.rbMoreThenPoints.Text = "Количество точек не меньше заданного";
            this.rbMoreThenPoints.UseVisualStyleBackColor = true;
            this.rbMoreThenPoints.CheckedChanged += new System.EventHandler(this.rbMoreThenPoints_CheckedChanged);
            // 
            // rbFixedPoints
            // 
            this.rbFixedPoints.AutoSize = true;
            this.rbFixedPoints.Checked = true;
            this.rbFixedPoints.Location = new System.Drawing.Point(15, 53);
            this.rbFixedPoints.Name = "rbFixedPoints";
            this.rbFixedPoints.Size = new System.Drawing.Size(253, 17);
            this.rbFixedPoints.TabIndex = 10;
            this.rbFixedPoints.TabStop = true;
            this.rbFixedPoints.Text = "Фиксированное количество точек на декаду";
            this.rbFixedPoints.UseVisualStyleBackColor = true;
            // 
            // grpInstr
            // 
            this.grpInstr.Controls.Add(this.btnInstrs);
            this.grpInstr.Controls.Add(this.cbxShowFS);
            this.grpInstr.Controls.Add(this.label13);
            this.grpInstr.Controls.Add(this.cmbxMonitorChannel);
            this.grpInstr.Controls.Add(this.linkLabel2);
            this.grpInstr.Controls.Add(this.cmbxGenMode);
            this.grpInstr.Controls.Add(this.cbxAddMeasure);
            this.grpInstr.Controls.Add(this.cbxUseAmpl);
            this.grpInstr.Controls.Add(this.cbxUseGen);
            this.grpInstr.Controls.Add(this.cbxUseMonitor);
            this.grpInstr.Controls.Add(this.cbxUseAnalyzer);
            this.grpInstr.Location = new System.Drawing.Point(12, 490);
            this.grpInstr.Name = "grpInstr";
            this.grpInstr.Size = new System.Drawing.Size(274, 167);
            this.grpInstr.TabIndex = 9;
            this.grpInstr.TabStop = false;
            this.grpInstr.Text = "Проведение измерения";
            // 
            // btnInstrs
            // 
            this.btnInstrs.Location = new System.Drawing.Point(193, 138);
            this.btnInstrs.Name = "btnInstrs";
            this.btnInstrs.Size = new System.Drawing.Size(75, 23);
            this.btnInstrs.TabIndex = 21;
            this.btnInstrs.Text = "Приборы";
            this.btnInstrs.UseVisualStyleBackColor = true;
            this.btnInstrs.Click += new System.EventHandler(this.btnInstrs_Click);
            // 
            // cbxShowFS
            // 
            this.cbxShowFS.AutoSize = true;
            this.cbxShowFS.Location = new System.Drawing.Point(15, 83);
            this.cbxShowFS.Name = "cbxShowFS";
            this.cbxShowFS.Size = new System.Drawing.Size(198, 17);
            this.cbxShowFS.TabIndex = 18;
            this.cbxShowFS.Text = "Показывать напряженность поля";
            this.cbxShowFS.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(178, 61);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(38, 13);
            this.label13.TabIndex = 16;
            this.label13.Text = "Канал";
            // 
            // cmbxMonitorChannel
            // 
            this.cmbxMonitorChannel.FormattingEnabled = true;
            this.cmbxMonitorChannel.Items.AddRange(new object[] {
            "Default",
            "1",
            "2",
            "3",
            "4"});
            this.cmbxMonitorChannel.Location = new System.Drawing.Point(222, 58);
            this.cmbxMonitorChannel.Name = "cmbxMonitorChannel";
            this.cmbxMonitorChannel.Size = new System.Drawing.Size(45, 21);
            this.cmbxMonitorChannel.TabIndex = 17;
            this.cmbxMonitorChannel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTime_KeyPress);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(151, 38);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(87, 13);
            this.linkLabel2.TabIndex = 14;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Дополнительно";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // cmbxGenMode
            // 
            this.cmbxGenMode.FormattingEnabled = true;
            this.cmbxGenMode.Items.AddRange(new object[] {
            "Обычный",
            "Следящий"});
            this.cmbxGenMode.Location = new System.Drawing.Point(146, 104);
            this.cmbxGenMode.Name = "cmbxGenMode";
            this.cmbxGenMode.Size = new System.Drawing.Size(121, 21);
            this.cmbxGenMode.TabIndex = 20;
            this.cmbxGenMode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTime_KeyPress);
            // 
            // cbxAddMeasure
            // 
            this.cbxAddMeasure.AutoSize = true;
            this.cbxAddMeasure.Checked = true;
            this.cbxAddMeasure.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxAddMeasure.Location = new System.Drawing.Point(136, 14);
            this.cbxAddMeasure.Name = "cbxAddMeasure";
            this.cbxAddMeasure.Size = new System.Drawing.Size(131, 17);
            this.cbxAddMeasure.TabIndex = 14;
            this.cbxAddMeasure.Text = "Добавить на график";
            this.cbxAddMeasure.UseVisualStyleBackColor = true;
            // 
            // cbxUseAmpl
            // 
            this.cbxUseAmpl.AutoSize = true;
            this.cbxUseAmpl.Location = new System.Drawing.Point(15, 129);
            this.cbxUseAmpl.Name = "cbxUseAmpl";
            this.cbxUseAmpl.Size = new System.Drawing.Size(81, 17);
            this.cbxUseAmpl.TabIndex = 21;
            this.cbxUseAmpl.Text = "Усилитель";
            this.cbxUseAmpl.UseVisualStyleBackColor = true;
            this.cbxUseAmpl.CheckStateChanged += new System.EventHandler(this.cbxUseAmpl_CheckStateChanged);
            // 
            // cbxUseGen
            // 
            this.cbxUseGen.AutoSize = true;
            this.cbxUseGen.Location = new System.Drawing.Point(15, 106);
            this.cbxUseGen.Name = "cbxUseGen";
            this.cbxUseGen.Size = new System.Drawing.Size(79, 17);
            this.cbxUseGen.TabIndex = 19;
            this.cbxUseGen.Text = "Генератор";
            this.cbxUseGen.UseVisualStyleBackColor = true;
            // 
            // cbxUseMonitor
            // 
            this.cbxUseMonitor.AutoSize = true;
            this.cbxUseMonitor.Location = new System.Drawing.Point(15, 60);
            this.cbxUseMonitor.Name = "cbxUseMonitor";
            this.cbxUseMonitor.Size = new System.Drawing.Size(97, 17);
            this.cbxUseMonitor.TabIndex = 16;
            this.cbxUseMonitor.Text = "Монитор поля";
            this.cbxUseMonitor.UseVisualStyleBackColor = true;
            // 
            // cbxUseAnalyzer
            // 
            this.cbxUseAnalyzer.AutoSize = true;
            this.cbxUseAnalyzer.Checked = true;
            this.cbxUseAnalyzer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxUseAnalyzer.Location = new System.Drawing.Point(15, 37);
            this.cbxUseAnalyzer.Name = "cbxUseAnalyzer";
            this.cbxUseAnalyzer.Size = new System.Drawing.Size(130, 17);
            this.cbxUseAnalyzer.TabIndex = 15;
            this.cbxUseAnalyzer.Text = "Анализатор спектра";
            this.cbxUseAnalyzer.UseVisualStyleBackColor = true;
            // 
            // btnGenerateMeasure
            // 
            this.btnGenerateMeasure.Location = new System.Drawing.Point(12, 366);
            this.btnGenerateMeasure.Margin = new System.Windows.Forms.Padding(2);
            this.btnGenerateMeasure.Name = "btnGenerateMeasure";
            this.btnGenerateMeasure.Size = new System.Drawing.Size(128, 33);
            this.btnGenerateMeasure.TabIndex = 13;
            this.btnGenerateMeasure.Text = "Заполнить таблицу";
            this.btnGenerateMeasure.UseVisualStyleBackColor = true;
            this.btnGenerateMeasure.Click += new System.EventHandler(this.btnGenerateMeasure_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnDelete);
            this.groupBox5.Controls.Add(this.btnSave);
            this.groupBox5.Controls.Add(this.btnLoad);
            this.groupBox5.Controls.Add(this.cmbxTemplates);
            this.groupBox5.Location = new System.Drawing.Point(12, 405);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(274, 79);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Шаблон";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(181, 46);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.TabStop = false;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(100, 46);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.TabStop = false;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.ContextMenuStrip = this.contextMenuStrip2;
            this.btnLoad.Location = new System.Drawing.Point(19, 46);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.TabStop = false;
            this.btnLoad.Text = "Загрузить";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // cmbxTemplates
            // 
            this.cmbxTemplates.FormattingEnabled = true;
            this.cmbxTemplates.Location = new System.Drawing.Point(7, 19);
            this.cmbxTemplates.Name = "cmbxTemplates";
            this.cmbxTemplates.Size = new System.Drawing.Size(261, 21);
            this.cmbxTemplates.TabIndex = 0;
            this.cmbxTemplates.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 664);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Имя измерения";
            // 
            // txtLineName
            // 
            this.txtLineName.Location = new System.Drawing.Point(12, 680);
            this.txtLineName.Name = "txtLineName";
            this.txtLineName.Size = new System.Drawing.Size(274, 20);
            this.txtLineName.TabIndex = 22;
            this.txtLineName.Text = "Измерение";
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(731, 678);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(114, 23);
            this.btnStart.TabIndex = 23;
            this.btnStart.Text = "Применить";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblBeginEnd
            // 
            this.lblBeginEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBeginEnd.AutoSize = true;
            this.lblBeginEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblBeginEnd.Location = new System.Drawing.Point(598, 680);
            this.lblBeginEnd.Name = "lblBeginEnd";
            this.lblBeginEnd.Size = new System.Drawing.Size(0, 16);
            this.lblBeginEnd.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(422, 680);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(170, 16);
            this.label8.TabIndex = 21;
            this.label8.Text = "Диапазон измерения:";
            // 
            // grpAnParams
            // 
            this.grpAnParams.Controls.Add(this.label15);
            this.grpAnParams.Controls.Add(this.cmbxAnalyzerUnits);
            this.grpAnParams.Controls.Add(this.label5);
            this.grpAnParams.Controls.Add(this.nudSweepCount);
            this.grpAnParams.Controls.Add(this.label14);
            this.grpAnParams.Controls.Add(this.cmbxCorrectionImpedance);
            this.grpAnParams.Controls.Add(this.label12);
            this.grpAnParams.Controls.Add(this.cmbxInputCoupling);
            this.grpAnParams.Controls.Add(this.linkLabel1);
            this.grpAnParams.Controls.Add(this.label11);
            this.grpAnParams.Controls.Add(this.label10);
            this.grpAnParams.Controls.Add(this.nudSpan);
            this.grpAnParams.Controls.Add(this.label9);
            this.grpAnParams.Controls.Add(this.cmbxBandWidth);
            this.grpAnParams.Location = new System.Drawing.Point(366, 404);
            this.grpAnParams.Name = "grpAnParams";
            this.grpAnParams.Size = new System.Drawing.Size(274, 211);
            this.grpAnParams.TabIndex = 22;
            this.grpAnParams.TabStop = false;
            this.grpAnParams.Text = "Параметры анализатора";
            this.grpAnParams.Visible = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(32, 26);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(111, 13);
            this.label15.TabIndex = 22;
            this.label15.Text = "Единицы измерения";
            // 
            // cmbxAnalyzerUnits
            // 
            this.cmbxAnalyzerUnits.DisplayMember = "0";
            this.cmbxAnalyzerUnits.FormattingEnabled = true;
            this.cmbxAnalyzerUnits.Items.AddRange(new object[] {
            "дБм",
            "дБмВ",
            "дБмкВ",
            "В",
            "Вт"});
            this.cmbxAnalyzerUnits.Location = new System.Drawing.Point(149, 23);
            this.cmbxAnalyzerUnits.Name = "cmbxAnalyzerUnits";
            this.cmbxAnalyzerUnits.Size = new System.Drawing.Size(78, 21);
            this.cmbxAnalyzerUnits.TabIndex = 21;
            this.cmbxAnalyzerUnits.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTime_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(52, 159);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Кол-во проходов";
            // 
            // nudSweepCount
            // 
            this.nudSweepCount.Location = new System.Drawing.Point(149, 157);
            this.nudSweepCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSweepCount.Name = "nudSweepCount";
            this.nudSweepCount.Size = new System.Drawing.Size(37, 20);
            this.nudSweepCount.TabIndex = 18;
            this.nudSweepCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(32, 53);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(111, 13);
            this.label14.TabIndex = 17;
            this.label14.Text = "Correction Impedance";
            // 
            // cmbxCorrectionImpedance
            // 
            this.cmbxCorrectionImpedance.FormattingEnabled = true;
            this.cmbxCorrectionImpedance.Items.AddRange(new object[] {
            "50 Ом",
            "75 Ом"});
            this.cmbxCorrectionImpedance.Location = new System.Drawing.Point(149, 50);
            this.cmbxCorrectionImpedance.Name = "cmbxCorrectionImpedance";
            this.cmbxCorrectionImpedance.Size = new System.Drawing.Size(78, 21);
            this.cmbxCorrectionImpedance.TabIndex = 16;
            this.cmbxCorrectionImpedance.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTime_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(71, 80);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 13);
            this.label12.TabIndex = 15;
            this.label12.Text = "InputCoupling";
            // 
            // cmbxInputCoupling
            // 
            this.cmbxInputCoupling.FormattingEnabled = true;
            this.cmbxInputCoupling.Items.AddRange(new object[] {
            "AC",
            "DC",
            "ACDC"});
            this.cmbxInputCoupling.Location = new System.Drawing.Point(149, 77);
            this.cmbxInputCoupling.Name = "cmbxInputCoupling";
            this.cmbxInputCoupling.Size = new System.Drawing.Size(78, 21);
            this.cmbxInputCoupling.TabIndex = 14;
            this.cmbxInputCoupling.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTime_KeyPress);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(218, 184);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(45, 13);
            this.linkLabel1.TabIndex = 13;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Скрыть";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(210, 133);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(15, 13);
            this.label11.TabIndex = 12;
            this.label11.Text = "%";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(85, 133);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Диапазон";
            // 
            // nudSpan
            // 
            this.nudSpan.Location = new System.Drawing.Point(149, 131);
            this.nudSpan.Name = "nudSpan";
            this.nudSpan.Size = new System.Drawing.Size(55, 20);
            this.nudSpan.TabIndex = 10;
            this.nudSpan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudSpan.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(30, 107);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(113, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Полоса пропускания";
            // 
            // cmbxBandWidth
            // 
            this.cmbxBandWidth.FormattingEnabled = true;
            this.cmbxBandWidth.Items.AddRange(new object[] {
            "9 кГц",
            "120 кГц"});
            this.cmbxBandWidth.Location = new System.Drawing.Point(149, 104);
            this.cmbxBandWidth.Name = "cmbxBandWidth";
            this.cmbxBandWidth.Size = new System.Drawing.Size(78, 21);
            this.cmbxBandWidth.TabIndex = 3;
            this.cmbxBandWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTime_KeyPress);
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label17.Location = new System.Drawing.Point(422, 660);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(117, 13);
            this.label17.TabIndex = 23;
            this.label17.Text = "Количество точек:";
            // 
            // lblPointsCount
            // 
            this.lblPointsCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPointsCount.AutoSize = true;
            this.lblPointsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPointsCount.Location = new System.Drawing.Point(544, 660);
            this.lblPointsCount.Name = "lblPointsCount";
            this.lblPointsCount.Size = new System.Drawing.Size(14, 13);
            this.lblPointsCount.TabIndex = 24;
            this.lblPointsCount.Text = "0";
            // 
            // DGV
            // 
            this.DGV.AllowUserToAddRows = false;
            this.DGV.AllowUserToDeleteRows = false;
            this.DGV.AllowUserToResizeRows = false;
            this.DGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGV.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGV.DefaultCellStyle = dataGridViewCellStyle1;
            this.DGV.Location = new System.Drawing.Point(292, 12);
            this.DGV.Name = "DGV";
            this.DGV.ReadOnly = true;
            this.DGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGV.Size = new System.Drawing.Size(634, 645);
            this.DGV.TabIndex = 25;
            // 
            // mmImportTemplates
            // 
            this.mmImportTemplates.Name = "mmImportTemplates";
            this.mmImportTemplates.Size = new System.Drawing.Size(179, 22);
            this.mmImportTemplates.Text = "Импорт шаблонов";
            this.mmImportTemplates.Click += new System.EventHandler(this.mmImportTemplates_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mmImportTemplates});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(180, 26);
            // 
            // tmsiFreq
            // 
            this.tmsiFreq.Name = "tmsiFreq";
            this.tmsiFreq.Size = new System.Drawing.Size(199, 22);
            this.tmsiFreq.Text = "Изменить Частоту";
            this.tmsiFreq.Click += new System.EventHandler(this.tmsiFreq_Click);
            // 
            // btnSetMeasRange
            // 
            this.btnSetMeasRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSetMeasRange.ContextMenuStrip = this.contextMenuStrip2;
            this.btnSetMeasRange.Location = new System.Drawing.Point(305, 677);
            this.btnSetMeasRange.Name = "btnSetMeasRange";
            this.btnSetMeasRange.Size = new System.Drawing.Size(111, 23);
            this.btnSetMeasRange.TabIndex = 27;
            this.btnSetMeasRange.TabStop = false;
            this.btnSetMeasRange.Text = "Выбрать диапазон";
            this.btnSetMeasRange.UseVisualStyleBackColor = true;
            this.btnSetMeasRange.Click += new System.EventHandler(this.btnSetMeasRange_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Image = global::ACOPC.Properties.Resources.exit2_16;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.Location = new System.Drawing.Point(851, 678);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 17;
            this.btnClose.TabStop = false;
            this.btnClose.Text = "Закрыть";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // frmMeasureParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(938, 712);
            this.Controls.Add(this.btnSetMeasRange);
            this.Controls.Add(this.lblPointsCount);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.grpAnParams);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblBeginEnd);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtLineName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.btnGenerateMeasure);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.grpInstr);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.DGV);
            this.MinimumSize = new System.Drawing.Size(874, 751);
            this.Name = "frmMeasureParams";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Параметры измерения";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMeasureParams_FormClosing);
            this.Load += new System.EventHandler(this.frmMeasureParams_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPointsCount)).EndInit();
            this.grpInstr.ResumeLayout(false);
            this.grpInstr.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.grpAnParams.ResumeLayout(false);
            this.grpAnParams.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSweepCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbStepLog;
        private System.Windows.Forms.RadioButton rbStepLine;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbxTimeUnits;
        private System.Windows.Forms.ComboBox cmbxStopFreqUnits;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.ComboBox cmbxStartFreqUnits;
        private System.Windows.Forms.TextBox txtAmplitude;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtStopFreq;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtStartFreq;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudPointsCount;
        private System.Windows.Forms.RadioButton rbCountOnRangePoints;
        private System.Windows.Forms.RadioButton rbMoreThenPoints;
        private System.Windows.Forms.RadioButton rbFixedPoints;
        private System.Windows.Forms.GroupBox grpInstr;
        private System.Windows.Forms.CheckBox cbxAddMeasure;
        private System.Windows.Forms.CheckBox cbxUseAmpl;
        private System.Windows.Forms.CheckBox cbxUseGen;
        private System.Windows.Forms.CheckBox cbxUseMonitor;
        private System.Windows.Forms.CheckBox cbxUseAnalyzer;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button btnGenerateMeasure;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.ComboBox cmbxTemplates;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLineName;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblBeginEnd;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolStripMenuItem tsmiAmpl;
        private System.Windows.Forms.ToolStripMenuItem tsmiTime;
        private System.Windows.Forms.ComboBox cmbxGenMode;
        private System.Windows.Forms.GroupBox grpAnParams;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nudSpan;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbxBandWidth;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.ComboBox cmbxInputCoupling;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbxMonitorChannel;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmbxCorrectionImpedance;
        private System.Windows.Forms.CheckBox cbxShowFS;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudSweepCount;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cmbxAnalyzerUnits;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnInstrs;
        private System.Windows.Forms.ToolStripMenuItem mmSetBands;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mmRemoveRows;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblPointsCount;
        private System.Windows.Forms.DataGridView DGV;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem mmImportTemplates;
        private System.Windows.Forms.ToolStripMenuItem tmsiFreq;
        private System.Windows.Forms.Button btnSetMeasRange;
    }
}
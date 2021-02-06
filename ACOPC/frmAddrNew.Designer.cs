namespace ACOPC
{
    partial class frmAddrNew
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
            this.gbxGen = new System.Windows.Forms.GroupBox();
            this.lblCheckGen = new System.Windows.Forms.Label();
            this.cbxGenTracking = new System.Windows.Forms.CheckBox();
            this.rbGenIP = new System.Windows.Forms.RadioButton();
            this.rbGenGPIB = new System.Windows.Forms.RadioButton();
            this.txtGenIP = new System.Windows.Forms.TextBox();
            this.txtGenGPIB = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCheckAn = new System.Windows.Forms.Label();
            this.rbAnIP = new System.Windows.Forms.RadioButton();
            this.rbAnGPIB = new System.Windows.Forms.RadioButton();
            this.txtAnIP = new System.Windows.Forms.TextBox();
            this.txtAnGPIB = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblCheckMon = new System.Windows.Forms.Label();
            this.rbMonIP = new System.Windows.Forms.RadioButton();
            this.rbMonGPIB = new System.Windows.Forms.RadioButton();
            this.txtMonIP = new System.Windows.Forms.TextBox();
            this.txtMonGPIB = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblCheckBonn1 = new System.Windows.Forms.Label();
            this.rbBonn0101IP = new System.Windows.Forms.RadioButton();
            this.rbBonn0101GPIB = new System.Windows.Forms.RadioButton();
            this.txtBonn0101IP = new System.Windows.Forms.TextBox();
            this.txtBonn0101GPIB = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblCheckBonn2 = new System.Windows.Forms.Label();
            this.rbBonn0125IP = new System.Windows.Forms.RadioButton();
            this.rbBonn0125GPIB = new System.Windows.Forms.RadioButton();
            this.txtBonn0125IP = new System.Windows.Forms.TextBox();
            this.txtBonn0125GPIB = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblCheckAR = new System.Windows.Forms.Label();
            this.rbARIP = new System.Windows.Forms.RadioButton();
            this.rbARGPIB = new System.Windows.Forms.RadioButton();
            this.txtARIP = new System.Windows.Forms.TextBox();
            this.txtARGPIB = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.gbxGen.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxGen
            // 
            this.gbxGen.Controls.Add(this.lblCheckGen);
            this.gbxGen.Controls.Add(this.cbxGenTracking);
            this.gbxGen.Controls.Add(this.rbGenIP);
            this.gbxGen.Controls.Add(this.rbGenGPIB);
            this.gbxGen.Controls.Add(this.txtGenIP);
            this.gbxGen.Controls.Add(this.txtGenGPIB);
            this.gbxGen.Location = new System.Drawing.Point(9, 10);
            this.gbxGen.Margin = new System.Windows.Forms.Padding(2);
            this.gbxGen.Name = "gbxGen";
            this.gbxGen.Padding = new System.Windows.Forms.Padding(2);
            this.gbxGen.Size = new System.Drawing.Size(249, 65);
            this.gbxGen.TabIndex = 0;
            this.gbxGen.TabStop = false;
            this.gbxGen.Text = "Генератор";
            // 
            // lblCheckGen
            // 
            this.lblCheckGen.AutoSize = true;
            this.lblCheckGen.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCheckGen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCheckGen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCheckGen.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCheckGen.Location = new System.Drawing.Point(172, 42);
            this.lblCheckGen.Name = "lblCheckGen";
            this.lblCheckGen.Size = new System.Drawing.Size(73, 15);
            this.lblCheckGen.TabIndex = 4;
            this.lblCheckGen.Text = "Проверить";
            this.lblCheckGen.Click += new System.EventHandler(this.lblCheckGen_Click);
            // 
            // cbxGenTracking
            // 
            this.cbxGenTracking.AutoSize = true;
            this.cbxGenTracking.Location = new System.Drawing.Point(166, 17);
            this.cbxGenTracking.Margin = new System.Windows.Forms.Padding(2);
            this.cbxGenTracking.Name = "cbxGenTracking";
            this.cbxGenTracking.Size = new System.Drawing.Size(78, 17);
            this.cbxGenTracking.TabIndex = 3;
            this.cbxGenTracking.Text = "Следящий";
            this.cbxGenTracking.UseVisualStyleBackColor = true;
            // 
            // rbGenIP
            // 
            this.rbGenIP.AutoSize = true;
            this.rbGenIP.Location = new System.Drawing.Point(4, 40);
            this.rbGenIP.Margin = new System.Windows.Forms.Padding(2);
            this.rbGenIP.Name = "rbGenIP";
            this.rbGenIP.Size = new System.Drawing.Size(61, 17);
            this.rbGenIP.TabIndex = 2;
            this.rbGenIP.Text = "TCP/IP";
            this.rbGenIP.UseVisualStyleBackColor = true;
            // 
            // rbGenGPIB
            // 
            this.rbGenGPIB.AutoSize = true;
            this.rbGenGPIB.Checked = true;
            this.rbGenGPIB.Location = new System.Drawing.Point(4, 17);
            this.rbGenGPIB.Margin = new System.Windows.Forms.Padding(2);
            this.rbGenGPIB.Name = "rbGenGPIB";
            this.rbGenGPIB.Size = new System.Drawing.Size(50, 17);
            this.rbGenGPIB.TabIndex = 2;
            this.rbGenGPIB.TabStop = true;
            this.rbGenGPIB.Text = "GPIB";
            this.rbGenGPIB.UseVisualStyleBackColor = true;
            // 
            // txtGenIP
            // 
            this.txtGenIP.Location = new System.Drawing.Point(71, 39);
            this.txtGenIP.Margin = new System.Windows.Forms.Padding(2);
            this.txtGenIP.Name = "txtGenIP";
            this.txtGenIP.Size = new System.Drawing.Size(83, 20);
            this.txtGenIP.TabIndex = 1;
            this.txtGenIP.Text = "192.168.0.20";
            // 
            // txtGenGPIB
            // 
            this.txtGenGPIB.Location = new System.Drawing.Point(71, 17);
            this.txtGenGPIB.Margin = new System.Windows.Forms.Padding(2);
            this.txtGenGPIB.Name = "txtGenGPIB";
            this.txtGenGPIB.Size = new System.Drawing.Size(38, 20);
            this.txtGenGPIB.TabIndex = 1;
            this.txtGenGPIB.Text = "20";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblCheckAn);
            this.groupBox1.Controls.Add(this.rbAnIP);
            this.groupBox1.Controls.Add(this.rbAnGPIB);
            this.groupBox1.Controls.Add(this.txtAnIP);
            this.groupBox1.Controls.Add(this.txtAnGPIB);
            this.groupBox1.Location = new System.Drawing.Point(9, 80);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(249, 65);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Анализатор спектра";
            // 
            // lblCheckAn
            // 
            this.lblCheckAn.AutoSize = true;
            this.lblCheckAn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCheckAn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCheckAn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCheckAn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCheckAn.Location = new System.Drawing.Point(172, 42);
            this.lblCheckAn.Name = "lblCheckAn";
            this.lblCheckAn.Size = new System.Drawing.Size(73, 15);
            this.lblCheckAn.TabIndex = 5;
            this.lblCheckAn.Text = "Проверить";
            this.lblCheckAn.Click += new System.EventHandler(this.lblCheckAn_Click);
            // 
            // rbAnIP
            // 
            this.rbAnIP.AutoSize = true;
            this.rbAnIP.Location = new System.Drawing.Point(4, 40);
            this.rbAnIP.Margin = new System.Windows.Forms.Padding(2);
            this.rbAnIP.Name = "rbAnIP";
            this.rbAnIP.Size = new System.Drawing.Size(61, 17);
            this.rbAnIP.TabIndex = 2;
            this.rbAnIP.Text = "TCP/IP";
            this.rbAnIP.UseVisualStyleBackColor = true;
            // 
            // rbAnGPIB
            // 
            this.rbAnGPIB.AutoSize = true;
            this.rbAnGPIB.Checked = true;
            this.rbAnGPIB.Location = new System.Drawing.Point(4, 17);
            this.rbAnGPIB.Margin = new System.Windows.Forms.Padding(2);
            this.rbAnGPIB.Name = "rbAnGPIB";
            this.rbAnGPIB.Size = new System.Drawing.Size(50, 17);
            this.rbAnGPIB.TabIndex = 2;
            this.rbAnGPIB.TabStop = true;
            this.rbAnGPIB.Text = "GPIB";
            this.rbAnGPIB.UseVisualStyleBackColor = true;
            // 
            // txtAnIP
            // 
            this.txtAnIP.Location = new System.Drawing.Point(71, 39);
            this.txtAnIP.Margin = new System.Windows.Forms.Padding(2);
            this.txtAnIP.Name = "txtAnIP";
            this.txtAnIP.Size = new System.Drawing.Size(83, 20);
            this.txtAnIP.TabIndex = 1;
            this.txtAnIP.Text = "192.168.0.20";
            // 
            // txtAnGPIB
            // 
            this.txtAnGPIB.Location = new System.Drawing.Point(71, 17);
            this.txtAnGPIB.Margin = new System.Windows.Forms.Padding(2);
            this.txtAnGPIB.Name = "txtAnGPIB";
            this.txtAnGPIB.Size = new System.Drawing.Size(38, 20);
            this.txtAnGPIB.TabIndex = 1;
            this.txtAnGPIB.Text = "20";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblCheckMon);
            this.groupBox2.Controls.Add(this.rbMonIP);
            this.groupBox2.Controls.Add(this.rbMonGPIB);
            this.groupBox2.Controls.Add(this.txtMonIP);
            this.groupBox2.Controls.Add(this.txtMonGPIB);
            this.groupBox2.Location = new System.Drawing.Point(9, 150);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(249, 65);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Монитор поля";
            // 
            // lblCheckMon
            // 
            this.lblCheckMon.AutoSize = true;
            this.lblCheckMon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCheckMon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCheckMon.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCheckMon.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCheckMon.Location = new System.Drawing.Point(172, 42);
            this.lblCheckMon.Name = "lblCheckMon";
            this.lblCheckMon.Size = new System.Drawing.Size(73, 15);
            this.lblCheckMon.TabIndex = 5;
            this.lblCheckMon.Text = "Проверить";
            this.lblCheckMon.Click += new System.EventHandler(this.lblCheckMon_Click);
            // 
            // rbMonIP
            // 
            this.rbMonIP.AutoSize = true;
            this.rbMonIP.Location = new System.Drawing.Point(4, 40);
            this.rbMonIP.Margin = new System.Windows.Forms.Padding(2);
            this.rbMonIP.Name = "rbMonIP";
            this.rbMonIP.Size = new System.Drawing.Size(61, 17);
            this.rbMonIP.TabIndex = 2;
            this.rbMonIP.Text = "TCP/IP";
            this.rbMonIP.UseVisualStyleBackColor = true;
            // 
            // rbMonGPIB
            // 
            this.rbMonGPIB.AutoSize = true;
            this.rbMonGPIB.Checked = true;
            this.rbMonGPIB.Location = new System.Drawing.Point(4, 17);
            this.rbMonGPIB.Margin = new System.Windows.Forms.Padding(2);
            this.rbMonGPIB.Name = "rbMonGPIB";
            this.rbMonGPIB.Size = new System.Drawing.Size(50, 17);
            this.rbMonGPIB.TabIndex = 2;
            this.rbMonGPIB.TabStop = true;
            this.rbMonGPIB.Text = "GPIB";
            this.rbMonGPIB.UseVisualStyleBackColor = true;
            // 
            // txtMonIP
            // 
            this.txtMonIP.Location = new System.Drawing.Point(71, 39);
            this.txtMonIP.Margin = new System.Windows.Forms.Padding(2);
            this.txtMonIP.Name = "txtMonIP";
            this.txtMonIP.Size = new System.Drawing.Size(83, 20);
            this.txtMonIP.TabIndex = 1;
            this.txtMonIP.Text = "192.168.0.20";
            // 
            // txtMonGPIB
            // 
            this.txtMonGPIB.Location = new System.Drawing.Point(71, 17);
            this.txtMonGPIB.Margin = new System.Windows.Forms.Padding(2);
            this.txtMonGPIB.Name = "txtMonGPIB";
            this.txtMonGPIB.Size = new System.Drawing.Size(38, 20);
            this.txtMonGPIB.TabIndex = 1;
            this.txtMonGPIB.Text = "20";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblCheckBonn1);
            this.groupBox3.Controls.Add(this.rbBonn0101IP);
            this.groupBox3.Controls.Add(this.rbBonn0101GPIB);
            this.groupBox3.Controls.Add(this.txtBonn0101IP);
            this.groupBox3.Controls.Add(this.txtBonn0101GPIB);
            this.groupBox3.Location = new System.Drawing.Point(262, 10);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(249, 65);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Усилитель Bonn 0101";
            // 
            // lblCheckBonn1
            // 
            this.lblCheckBonn1.AutoSize = true;
            this.lblCheckBonn1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCheckBonn1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCheckBonn1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCheckBonn1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCheckBonn1.Location = new System.Drawing.Point(171, 42);
            this.lblCheckBonn1.Name = "lblCheckBonn1";
            this.lblCheckBonn1.Size = new System.Drawing.Size(73, 15);
            this.lblCheckBonn1.TabIndex = 5;
            this.lblCheckBonn1.Text = "Проверить";
            this.lblCheckBonn1.Click += new System.EventHandler(this.lblCheckBonn1_Click);
            // 
            // rbBonn0101IP
            // 
            this.rbBonn0101IP.AutoSize = true;
            this.rbBonn0101IP.Enabled = false;
            this.rbBonn0101IP.Location = new System.Drawing.Point(4, 40);
            this.rbBonn0101IP.Margin = new System.Windows.Forms.Padding(2);
            this.rbBonn0101IP.Name = "rbBonn0101IP";
            this.rbBonn0101IP.Size = new System.Drawing.Size(61, 17);
            this.rbBonn0101IP.TabIndex = 2;
            this.rbBonn0101IP.Text = "TCP/IP";
            this.rbBonn0101IP.UseVisualStyleBackColor = true;
            // 
            // rbBonn0101GPIB
            // 
            this.rbBonn0101GPIB.AutoSize = true;
            this.rbBonn0101GPIB.Checked = true;
            this.rbBonn0101GPIB.Location = new System.Drawing.Point(4, 17);
            this.rbBonn0101GPIB.Margin = new System.Windows.Forms.Padding(2);
            this.rbBonn0101GPIB.Name = "rbBonn0101GPIB";
            this.rbBonn0101GPIB.Size = new System.Drawing.Size(50, 17);
            this.rbBonn0101GPIB.TabIndex = 2;
            this.rbBonn0101GPIB.TabStop = true;
            this.rbBonn0101GPIB.Text = "GPIB";
            this.rbBonn0101GPIB.UseVisualStyleBackColor = true;
            // 
            // txtBonn0101IP
            // 
            this.txtBonn0101IP.Enabled = false;
            this.txtBonn0101IP.Location = new System.Drawing.Point(70, 39);
            this.txtBonn0101IP.Margin = new System.Windows.Forms.Padding(2);
            this.txtBonn0101IP.Name = "txtBonn0101IP";
            this.txtBonn0101IP.Size = new System.Drawing.Size(83, 20);
            this.txtBonn0101IP.TabIndex = 1;
            // 
            // txtBonn0101GPIB
            // 
            this.txtBonn0101GPIB.Location = new System.Drawing.Point(70, 17);
            this.txtBonn0101GPIB.Margin = new System.Windows.Forms.Padding(2);
            this.txtBonn0101GPIB.Name = "txtBonn0101GPIB";
            this.txtBonn0101GPIB.Size = new System.Drawing.Size(38, 20);
            this.txtBonn0101GPIB.TabIndex = 1;
            this.txtBonn0101GPIB.Text = "20";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblCheckBonn2);
            this.groupBox4.Controls.Add(this.rbBonn0125IP);
            this.groupBox4.Controls.Add(this.rbBonn0125GPIB);
            this.groupBox4.Controls.Add(this.txtBonn0125IP);
            this.groupBox4.Controls.Add(this.txtBonn0125GPIB);
            this.groupBox4.Location = new System.Drawing.Point(262, 80);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(249, 65);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Усилитель Bonn 0125";
            // 
            // lblCheckBonn2
            // 
            this.lblCheckBonn2.AutoSize = true;
            this.lblCheckBonn2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCheckBonn2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCheckBonn2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCheckBonn2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCheckBonn2.Location = new System.Drawing.Point(171, 42);
            this.lblCheckBonn2.Name = "lblCheckBonn2";
            this.lblCheckBonn2.Size = new System.Drawing.Size(73, 15);
            this.lblCheckBonn2.TabIndex = 5;
            this.lblCheckBonn2.Text = "Проверить";
            this.lblCheckBonn2.Click += new System.EventHandler(this.lblCheckBonn2_Click);
            // 
            // rbBonn0125IP
            // 
            this.rbBonn0125IP.AutoSize = true;
            this.rbBonn0125IP.Enabled = false;
            this.rbBonn0125IP.Location = new System.Drawing.Point(4, 40);
            this.rbBonn0125IP.Margin = new System.Windows.Forms.Padding(2);
            this.rbBonn0125IP.Name = "rbBonn0125IP";
            this.rbBonn0125IP.Size = new System.Drawing.Size(61, 17);
            this.rbBonn0125IP.TabIndex = 2;
            this.rbBonn0125IP.Text = "TCP/IP";
            this.rbBonn0125IP.UseVisualStyleBackColor = true;
            // 
            // rbBonn0125GPIB
            // 
            this.rbBonn0125GPIB.AutoSize = true;
            this.rbBonn0125GPIB.Checked = true;
            this.rbBonn0125GPIB.Location = new System.Drawing.Point(4, 17);
            this.rbBonn0125GPIB.Margin = new System.Windows.Forms.Padding(2);
            this.rbBonn0125GPIB.Name = "rbBonn0125GPIB";
            this.rbBonn0125GPIB.Size = new System.Drawing.Size(50, 17);
            this.rbBonn0125GPIB.TabIndex = 2;
            this.rbBonn0125GPIB.TabStop = true;
            this.rbBonn0125GPIB.Text = "GPIB";
            this.rbBonn0125GPIB.UseVisualStyleBackColor = true;
            // 
            // txtBonn0125IP
            // 
            this.txtBonn0125IP.Enabled = false;
            this.txtBonn0125IP.Location = new System.Drawing.Point(70, 39);
            this.txtBonn0125IP.Margin = new System.Windows.Forms.Padding(2);
            this.txtBonn0125IP.Name = "txtBonn0125IP";
            this.txtBonn0125IP.Size = new System.Drawing.Size(83, 20);
            this.txtBonn0125IP.TabIndex = 1;
            // 
            // txtBonn0125GPIB
            // 
            this.txtBonn0125GPIB.Location = new System.Drawing.Point(70, 17);
            this.txtBonn0125GPIB.Margin = new System.Windows.Forms.Padding(2);
            this.txtBonn0125GPIB.Name = "txtBonn0125GPIB";
            this.txtBonn0125GPIB.Size = new System.Drawing.Size(38, 20);
            this.txtBonn0125GPIB.TabIndex = 1;
            this.txtBonn0125GPIB.Text = "20";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblCheckAR);
            this.groupBox5.Controls.Add(this.rbARIP);
            this.groupBox5.Controls.Add(this.rbARGPIB);
            this.groupBox5.Controls.Add(this.txtARIP);
            this.groupBox5.Controls.Add(this.txtARGPIB);
            this.groupBox5.Location = new System.Drawing.Point(262, 150);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox5.Size = new System.Drawing.Size(249, 65);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Усилитель AR 20ST1G18";
            // 
            // lblCheckAR
            // 
            this.lblCheckAR.AutoSize = true;
            this.lblCheckAR.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCheckAR.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCheckAR.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCheckAR.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCheckAR.Location = new System.Drawing.Point(171, 42);
            this.lblCheckAR.Name = "lblCheckAR";
            this.lblCheckAR.Size = new System.Drawing.Size(73, 15);
            this.lblCheckAR.TabIndex = 6;
            this.lblCheckAR.Text = "Проверить";
            this.lblCheckAR.Click += new System.EventHandler(this.lblCheckAR_Click);
            // 
            // rbARIP
            // 
            this.rbARIP.AutoSize = true;
            this.rbARIP.Enabled = false;
            this.rbARIP.Location = new System.Drawing.Point(4, 40);
            this.rbARIP.Margin = new System.Windows.Forms.Padding(2);
            this.rbARIP.Name = "rbARIP";
            this.rbARIP.Size = new System.Drawing.Size(61, 17);
            this.rbARIP.TabIndex = 2;
            this.rbARIP.Text = "TCP/IP";
            this.rbARIP.UseVisualStyleBackColor = true;
            // 
            // rbARGPIB
            // 
            this.rbARGPIB.AutoSize = true;
            this.rbARGPIB.Checked = true;
            this.rbARGPIB.Location = new System.Drawing.Point(4, 17);
            this.rbARGPIB.Margin = new System.Windows.Forms.Padding(2);
            this.rbARGPIB.Name = "rbARGPIB";
            this.rbARGPIB.Size = new System.Drawing.Size(50, 17);
            this.rbARGPIB.TabIndex = 2;
            this.rbARGPIB.TabStop = true;
            this.rbARGPIB.Text = "GPIB";
            this.rbARGPIB.UseVisualStyleBackColor = true;
            // 
            // txtARIP
            // 
            this.txtARIP.Enabled = false;
            this.txtARIP.Location = new System.Drawing.Point(70, 39);
            this.txtARIP.Margin = new System.Windows.Forms.Padding(2);
            this.txtARIP.Name = "txtARIP";
            this.txtARIP.Size = new System.Drawing.Size(83, 20);
            this.txtARIP.TabIndex = 1;
            // 
            // txtARGPIB
            // 
            this.txtARGPIB.Location = new System.Drawing.Point(70, 17);
            this.txtARGPIB.Margin = new System.Windows.Forms.Padding(2);
            this.txtARGPIB.Name = "txtARGPIB";
            this.txtARGPIB.Size = new System.Drawing.Size(38, 20);
            this.txtARGPIB.TabIndex = 1;
            this.txtARGPIB.Text = "20";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(431, 220);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(350, 220);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 3;
            this.btnAccept.Text = "Принять";
            this.btnAccept.UseVisualStyleBackColor = true;
            // 
            // frmAddrNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 246);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gbxGen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmAddrNew";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Адреса устройств";
            this.gbxGen.ResumeLayout(false);
            this.gbxGen.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxGen;
        private System.Windows.Forms.RadioButton rbGenGPIB;
        private System.Windows.Forms.TextBox txtGenGPIB;
        private System.Windows.Forms.RadioButton rbGenIP;
        private System.Windows.Forms.TextBox txtGenIP;
        private System.Windows.Forms.CheckBox cbxGenTracking;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbAnIP;
        private System.Windows.Forms.RadioButton rbAnGPIB;
        private System.Windows.Forms.TextBox txtAnIP;
        private System.Windows.Forms.TextBox txtAnGPIB;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbMonIP;
        private System.Windows.Forms.RadioButton rbMonGPIB;
        private System.Windows.Forms.TextBox txtMonIP;
        private System.Windows.Forms.TextBox txtMonGPIB;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbBonn0101IP;
        private System.Windows.Forms.RadioButton rbBonn0101GPIB;
        private System.Windows.Forms.TextBox txtBonn0101IP;
        private System.Windows.Forms.TextBox txtBonn0101GPIB;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbBonn0125IP;
        private System.Windows.Forms.RadioButton rbBonn0125GPIB;
        private System.Windows.Forms.TextBox txtBonn0125IP;
        private System.Windows.Forms.TextBox txtBonn0125GPIB;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rbARIP;
        private System.Windows.Forms.RadioButton rbARGPIB;
        private System.Windows.Forms.TextBox txtARIP;
        private System.Windows.Forms.TextBox txtARGPIB;
        private System.Windows.Forms.Label lblCheckGen;
        private System.Windows.Forms.Label lblCheckAn;
        private System.Windows.Forms.Label lblCheckMon;
        private System.Windows.Forms.Label lblCheckBonn1;
        private System.Windows.Forms.Label lblCheckBonn2;
        private System.Windows.Forms.Label lblCheckAR;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAccept;
    }
}
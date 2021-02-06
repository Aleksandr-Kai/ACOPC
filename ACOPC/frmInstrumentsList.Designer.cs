namespace ACOPC
{
    partial class frmInstrumentsList
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Анализаторы", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Генераторы", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Монитор поля", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Усилители", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("Неизвестные", System.Windows.Forms.HorizontalAlignment.Left);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInstrumentsList));
            this.lvInstrList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.gbxParams = new System.Windows.Forms.GroupBox();
            this.btnSetParams = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAddres = new System.Windows.Forms.TextBox();
            this.rbIP = new System.Windows.Forms.RadioButton();
            this.rbGPIB = new System.Windows.Forms.RadioButton();
            this.cmbxGroupList = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.gbxParams.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvInstrList
            // 
            this.lvInstrList.CheckBoxes = true;
            this.lvInstrList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvInstrList.FullRowSelect = true;
            this.lvInstrList.GridLines = true;
            listViewGroup1.Header = "Анализаторы";
            listViewGroup1.Name = "lvgAnalyzers";
            listViewGroup2.Header = "Генераторы";
            listViewGroup2.Name = "lvgGenerators";
            listViewGroup3.Header = "Монитор поля";
            listViewGroup3.Name = "lvgMonitors";
            listViewGroup4.Header = "Усилители";
            listViewGroup4.Name = "lvgAmplifires";
            listViewGroup5.Header = "Неизвестные";
            listViewGroup5.Name = "lvgNan";
            this.lvInstrList.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3,
            listViewGroup4,
            listViewGroup5});
            this.lvInstrList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvInstrList.HideSelection = false;
            this.lvInstrList.LargeImageList = this.imageList1;
            this.lvInstrList.Location = new System.Drawing.Point(12, 12);
            this.lvInstrList.Name = "lvInstrList";
            this.lvInstrList.ShowItemToolTips = true;
            this.lvInstrList.Size = new System.Drawing.Size(398, 320);
            this.lvInstrList.SmallImageList = this.imageList1;
            this.lvInstrList.TabIndex = 2;
            this.lvInstrList.UseCompatibleStateImageBehavior = false;
            this.lvInstrList.View = System.Windows.Forms.View.Details;
            this.lvInstrList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvInstrList_ItemCheck);
            this.lvInstrList.SelectedIndexChanged += new System.EventHandler(this.lvInstrList_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Имя";
            this.columnHeader1.Width = 186;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Подключение";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 84;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Адрес";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 102;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Help_32.png");
            this.imageList1.Images.SetKeyName(1, "light_red.png");
            this.imageList1.Images.SetKeyName(2, "light_green.png");
            // 
            // gbxParams
            // 
            this.gbxParams.Controls.Add(this.btnSetParams);
            this.gbxParams.Controls.Add(this.label3);
            this.gbxParams.Controls.Add(this.label2);
            this.gbxParams.Controls.Add(this.label1);
            this.gbxParams.Controls.Add(this.txtAddres);
            this.gbxParams.Controls.Add(this.rbIP);
            this.gbxParams.Controls.Add(this.rbGPIB);
            this.gbxParams.Controls.Add(this.cmbxGroupList);
            this.gbxParams.Enabled = false;
            this.gbxParams.Location = new System.Drawing.Point(416, 12);
            this.gbxParams.Name = "gbxParams";
            this.gbxParams.Size = new System.Drawing.Size(250, 137);
            this.gbxParams.TabIndex = 8;
            this.gbxParams.TabStop = false;
            this.gbxParams.Text = "Параметры";
            // 
            // btnSetParams
            // 
            this.btnSetParams.Location = new System.Drawing.Point(163, 102);
            this.btnSetParams.Name = "btnSetParams";
            this.btnSetParams.Size = new System.Drawing.Size(75, 23);
            this.btnSetParams.TabIndex = 15;
            this.btnSetParams.Text = "Сохранить";
            this.btnSetParams.UseVisualStyleBackColor = true;
            this.btnSetParams.Click += new System.EventHandler(this.btnSetParams_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(64, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Адрес";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Тип подключения";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Тип прибора";
            // 
            // txtAddres
            // 
            this.txtAddres.Location = new System.Drawing.Point(108, 67);
            this.txtAddres.Name = "txtAddres";
            this.txtAddres.Size = new System.Drawing.Size(130, 20);
            this.txtAddres.TabIndex = 11;
            // 
            // rbIP
            // 
            this.rbIP.AutoSize = true;
            this.rbIP.Location = new System.Drawing.Point(177, 44);
            this.rbIP.Name = "rbIP";
            this.rbIP.Size = new System.Drawing.Size(61, 17);
            this.rbIP.TabIndex = 10;
            this.rbIP.TabStop = true;
            this.rbIP.Text = "TCP/IP";
            this.rbIP.UseVisualStyleBackColor = true;
            // 
            // rbGPIB
            // 
            this.rbGPIB.AutoSize = true;
            this.rbGPIB.Location = new System.Drawing.Point(108, 44);
            this.rbGPIB.Name = "rbGPIB";
            this.rbGPIB.Size = new System.Drawing.Size(50, 17);
            this.rbGPIB.TabIndex = 9;
            this.rbGPIB.TabStop = true;
            this.rbGPIB.Text = "GPIB";
            this.rbGPIB.UseVisualStyleBackColor = true;
            // 
            // cmbxGroupList
            // 
            this.cmbxGroupList.FormattingEnabled = true;
            this.cmbxGroupList.Location = new System.Drawing.Point(108, 17);
            this.cmbxGroupList.Name = "cmbxGroupList";
            this.cmbxGroupList.Size = new System.Drawing.Size(130, 21);
            this.cmbxGroupList.TabIndex = 8;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(591, 309);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(510, 309);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 10;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // listBox1
            // 
            this.listBox1.Enabled = false;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(416, 155);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(250, 134);
            this.listBox1.TabIndex = 11;
            this.listBox1.Visible = false;
            // 
            // frmInstrumentsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(681, 344);
            this.ControlBox = false;
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gbxParams);
            this.Controls.Add(this.lvInstrList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmInstrumentsList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Приборы";
            this.Load += new System.EventHandler(this.frmInstrumentsList_Load);
            this.gbxParams.ResumeLayout(false);
            this.gbxParams.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView lvInstrList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.GroupBox gbxParams;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAddres;
        private System.Windows.Forms.RadioButton rbIP;
        private System.Windows.Forms.RadioButton rbGPIB;
        private System.Windows.Forms.ComboBox cmbxGroupList;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnSetParams;
        private System.Windows.Forms.ListBox listBox1;
    }
}
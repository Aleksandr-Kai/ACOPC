namespace ACOPC
{
    partial class frmAddr
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
            this.cbxInstruments = new System.Windows.Forms.ComboBox();
            this.btnAddInstrument = new System.Windows.Forms.Button();
            this.checkedListBox = new System.Windows.Forms.CheckedListBox();
            this.btnDelInstrument = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbUseIP = new System.Windows.Forms.RadioButton();
            this.rbUseGPIB = new System.Windows.Forms.RadioButton();
            this.btnAddrSet = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtGPIB = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxInstruments
            // 
            this.cbxInstruments.FormattingEnabled = true;
            this.cbxInstruments.Items.AddRange(new object[] {
            "Генератор",
            "Анализатор",
            "Усилитель",
            "Монитор поля"});
            this.cbxInstruments.Location = new System.Drawing.Point(354, 12);
            this.cbxInstruments.Name = "cbxInstruments";
            this.cbxInstruments.Size = new System.Drawing.Size(107, 21);
            this.cbxInstruments.TabIndex = 0;
            // 
            // btnAddInstrument
            // 
            this.btnAddInstrument.Location = new System.Drawing.Point(354, 39);
            this.btnAddInstrument.Name = "btnAddInstrument";
            this.btnAddInstrument.Size = new System.Drawing.Size(107, 23);
            this.btnAddInstrument.TabIndex = 1;
            this.btnAddInstrument.Text = "Добавить";
            this.btnAddInstrument.UseVisualStyleBackColor = true;
            this.btnAddInstrument.Click += new System.EventHandler(this.btnAddInstrument_Click);
            // 
            // checkedListBox
            // 
            this.checkedListBox.CheckOnClick = true;
            this.checkedListBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.checkedListBox.FormattingEnabled = true;
            this.checkedListBox.Items.AddRange(new object[] {
            "Анализатор\tGPIB\t18\tFSP30",
            "Генератор\tIP\t10.48.45.20\tHMF2525"});
            this.checkedListBox.Location = new System.Drawing.Point(12, 9);
            this.checkedListBox.Name = "checkedListBox";
            this.checkedListBox.Size = new System.Drawing.Size(336, 274);
            this.checkedListBox.Sorted = true;
            this.checkedListBox.TabIndex = 2;
            this.checkedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox_ItemCheck);
            this.checkedListBox.SelectedIndexChanged += new System.EventHandler(this.frmAddr_Shown);
            // 
            // btnDelInstrument
            // 
            this.btnDelInstrument.Location = new System.Drawing.Point(354, 68);
            this.btnDelInstrument.Name = "btnDelInstrument";
            this.btnDelInstrument.Size = new System.Drawing.Size(107, 23);
            this.btnDelInstrument.TabIndex = 9;
            this.btnDelInstrument.Text = "Удалить";
            this.btnDelInstrument.UseVisualStyleBackColor = true;
            this.btnDelInstrument.Click += new System.EventHandler(this.btnDelInstrument_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbUseIP);
            this.groupBox1.Controls.Add(this.rbUseGPIB);
            this.groupBox1.Controls.Add(this.btnAddrSet);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtIP);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtGPIB);
            this.groupBox1.Location = new System.Drawing.Point(354, 109);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(107, 147);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // rbUseIP
            // 
            this.rbUseIP.AutoSize = true;
            this.rbUseIP.Location = new System.Drawing.Point(8, 67);
            this.rbUseIP.Name = "rbUseIP";
            this.rbUseIP.Size = new System.Drawing.Size(14, 13);
            this.rbUseIP.TabIndex = 14;
            this.rbUseIP.UseVisualStyleBackColor = true;
            this.rbUseIP.CheckedChanged += new System.EventHandler(this.rbUseIP_CheckedChanged);
            // 
            // rbUseGPIB
            // 
            this.rbUseGPIB.AutoSize = true;
            this.rbUseGPIB.Checked = true;
            this.rbUseGPIB.Location = new System.Drawing.Point(8, 17);
            this.rbUseGPIB.Name = "rbUseGPIB";
            this.rbUseGPIB.Size = new System.Drawing.Size(14, 13);
            this.rbUseGPIB.TabIndex = 15;
            this.rbUseGPIB.TabStop = true;
            this.rbUseGPIB.UseVisualStyleBackColor = true;
            this.rbUseGPIB.CheckedChanged += new System.EventHandler(this.rbUseGPIB_CheckedChanged);
            // 
            // btnAddrSet
            // 
            this.btnAddrSet.Location = new System.Drawing.Point(7, 117);
            this.btnAddrSet.Name = "btnAddrSet";
            this.btnAddrSet.Size = new System.Drawing.Size(91, 23);
            this.btnAddrSet.TabIndex = 13;
            this.btnAddrSet.Text = "Применить";
            this.btnAddrSet.UseVisualStyleBackColor = true;
            this.btnAddrSet.Click += new System.EventHandler(this.btnAddrSet_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "IP адрес";
            // 
            // txtIP
            // 
            this.txtIP.Enabled = false;
            this.txtIP.Location = new System.Drawing.Point(7, 86);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(91, 20);
            this.txtIP.TabIndex = 11;
            this.txtIP.Text = "192.168.100.100";
            this.txtIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "GPIB адрес";
            // 
            // txtGPIB
            // 
            this.txtGPIB.Location = new System.Drawing.Point(7, 36);
            this.txtGPIB.Name = "txtGPIB";
            this.txtGPIB.Size = new System.Drawing.Size(58, 20);
            this.txtGPIB.TabIndex = 9;
            this.txtGPIB.Text = "20";
            this.txtGPIB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // frmAddr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 309);
            this.Controls.Add(this.btnDelInstrument);
            this.Controls.Add(this.checkedListBox);
            this.Controls.Add(this.btnAddInstrument);
            this.Controls.Add(this.cbxInstruments);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmAddr";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Адреса устройств";
            this.Shown += new System.EventHandler(this.frmAddr_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxInstruments;
        private System.Windows.Forms.Button btnAddInstrument;
        private System.Windows.Forms.CheckedListBox checkedListBox;
        private System.Windows.Forms.Button btnDelInstrument;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbUseIP;
        private System.Windows.Forms.RadioButton rbUseGPIB;
        private System.Windows.Forms.Button btnAddrSet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtGPIB;
    }
}
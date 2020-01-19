namespace CheckingProgram
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGV = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab1 = new System.Windows.Forms.TabPage();
            this.tab2 = new System.Windows.Forms.TabPage();
            this.cProject = new System.Windows.Forms.ComboBox();
            this.lProject = new System.Windows.Forms.Label();
            this.lCDriver = new System.Windows.Forms.Label();
            this.cDriver = new System.Windows.Forms.ComboBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.cCheckItem = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cDateFilter = new System.Windows.Forms.CheckBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.tNowLayer = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGV)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tab1.SuspendLayout();
            this.tab2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGV
            // 
            this.dataGV.AllowUserToOrderColumns = true;
            this.dataGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGV.Location = new System.Drawing.Point(5, 145);
            this.dataGV.Name = "dataGV";
            this.dataGV.RowTemplate.Height = 23;
            this.dataGV.Size = new System.Drawing.Size(791, 280);
            this.dataGV.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab1);
            this.tabControl1.Controls.Add(this.tab2);
            this.tabControl1.Location = new System.Drawing.Point(5, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(791, 134);
            this.tabControl1.TabIndex = 9;
            // 
            // tab1
            // 
            this.tab1.Controls.Add(this.dateTimePicker1);
            this.tab1.Controls.Add(this.cDateFilter);
            this.tab1.Controls.Add(this.btnStart);
            this.tab1.Controls.Add(this.cCheckItem);
            this.tab1.Controls.Add(this.label1);
            this.tab1.Location = new System.Drawing.Point(4, 22);
            this.tab1.Name = "tab1";
            this.tab1.Padding = new System.Windows.Forms.Padding(3);
            this.tab1.Size = new System.Drawing.Size(783, 108);
            this.tab1.TabIndex = 0;
            this.tab1.Text = "Normal";
            this.tab1.UseVisualStyleBackColor = true;
            // 
            // tab2
            // 
            this.tab2.Controls.Add(this.cProject);
            this.tab2.Controls.Add(this.lProject);
            this.tab2.Controls.Add(this.lCDriver);
            this.tab2.Controls.Add(this.cDriver);
            this.tab2.Location = new System.Drawing.Point(4, 22);
            this.tab2.Name = "tab2";
            this.tab2.Padding = new System.Windows.Forms.Padding(3);
            this.tab2.Size = new System.Drawing.Size(783, 108);
            this.tab2.TabIndex = 1;
            this.tab2.Text = "Seting";
            this.tab2.UseVisualStyleBackColor = true;
            // 
            // cProject
            // 
            this.cProject.FormattingEnabled = true;
            this.cProject.Location = new System.Drawing.Point(106, 6);
            this.cProject.Name = "cProject";
            this.cProject.Size = new System.Drawing.Size(61, 20);
            this.cProject.TabIndex = 9;
            this.cProject.SelectedIndexChanged += new System.EventHandler(this.CProject_SelectedIndexChanged);
            // 
            // lProject
            // 
            this.lProject.AutoSize = true;
            this.lProject.Location = new System.Drawing.Point(11, 9);
            this.lProject.Name = "lProject";
            this.lProject.Size = new System.Drawing.Size(89, 12);
            this.lProject.TabIndex = 8;
            this.lProject.Text = "Select project";
            // 
            // lCDriver
            // 
            this.lCDriver.AutoSize = true;
            this.lCDriver.Location = new System.Drawing.Point(184, 9);
            this.lCDriver.Name = "lCDriver";
            this.lCDriver.Size = new System.Drawing.Size(77, 12);
            this.lCDriver.TabIndex = 7;
            this.lCDriver.Text = "Select drive";
            // 
            // cDriver
            // 
            this.cDriver.FormattingEnabled = true;
            this.cDriver.Location = new System.Drawing.Point(267, 6);
            this.cDriver.Name = "cDriver";
            this.cDriver.Size = new System.Drawing.Size(121, 20);
            this.cDriver.TabIndex = 6;
            this.cDriver.SelectedIndexChanged += new System.EventHandler(this.CDriver_SelectedIndexChanged);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(627, 6);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 49);
            this.btnStart.TabIndex = 11;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // cCheckItem
            // 
            this.cCheckItem.FormattingEnabled = true;
            this.cCheckItem.Location = new System.Drawing.Point(121, 8);
            this.cCheckItem.Name = "cCheckItem";
            this.cCheckItem.Size = new System.Drawing.Size(500, 20);
            this.cCheckItem.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "Select check item";
            // 
            // cDateFilter
            // 
            this.cDateFilter.AutoSize = true;
            this.cDateFilter.Location = new System.Drawing.Point(10, 39);
            this.cDateFilter.Name = "cDateFilter";
            this.cDateFilter.Size = new System.Drawing.Size(90, 16);
            this.cDateFilter.TabIndex = 12;
            this.cDateFilter.Text = "Date Filter";
            this.cDateFilter.UseVisualStyleBackColor = true;
            this.cDateFilter.CheckedChanged += new System.EventHandler(this.CDateFilter_CheckedChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(106, 34);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(120, 21);
            this.dateTimePicker1.TabIndex = 13;
            // 
            // tNowLayer
            // 
            this.tNowLayer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tNowLayer.Enabled = false;
            this.tNowLayer.Location = new System.Drawing.Point(696, 429);
            this.tNowLayer.Name = "tNowLayer";
            this.tNowLayer.Size = new System.Drawing.Size(100, 14);
            this.tNowLayer.TabIndex = 10;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tNowLayer);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.dataGV);
            this.Name = "FormMain";
            this.Text = "Checking Program";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGV)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.tab2.ResumeLayout(false);
            this.tab2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGV;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.CheckBox cDateFilter;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ComboBox cCheckItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tab2;
        private System.Windows.Forms.ComboBox cProject;
        private System.Windows.Forms.Label lProject;
        private System.Windows.Forms.Label lCDriver;
        private System.Windows.Forms.ComboBox cDriver;
        private System.Windows.Forms.TextBox tNowLayer;
    }
}


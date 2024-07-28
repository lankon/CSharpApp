
namespace Mapping
{
    partial class F_OneToOne
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
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.panel2 = new System.Windows.Forms.Panel();
            this.TxtBx_FilePath2 = new System.Windows.Forms.TextBox();
            this.Btn_LoadFile2 = new System.Windows.Forms.Button();
            this.TxtBx_FilePath1 = new System.Windows.Forms.TextBox();
            this.Btn_LoadFile1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Chart_Difference = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.Cmbx_TestItem = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Cmbx_ScaleSetting = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtBx_LowLimit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtBx_UpLimit = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Cmbx_UsePercentage = new System.Windows.Forms.ComboBox();
            this.Btn_Compare = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Chart_Difference)).BeginInit();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.TxtBx_FilePath2);
            this.panel2.Controls.Add(this.Btn_LoadFile2);
            this.panel2.Controls.Add(this.TxtBx_FilePath1);
            this.panel2.Controls.Add(this.Btn_LoadFile1);
            this.panel2.Location = new System.Drawing.Point(13, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(997, 75);
            this.panel2.TabIndex = 6;
            // 
            // TxtBx_FilePath2
            // 
            this.TxtBx_FilePath2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TxtBx_FilePath2.Location = new System.Drawing.Point(121, 41);
            this.TxtBx_FilePath2.Name = "TxtBx_FilePath2";
            this.TxtBx_FilePath2.Size = new System.Drawing.Size(870, 29);
            this.TxtBx_FilePath2.TabIndex = 9;
            // 
            // Btn_LoadFile2
            // 
            this.Btn_LoadFile2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_LoadFile2.Location = new System.Drawing.Point(3, 41);
            this.Btn_LoadFile2.Name = "Btn_LoadFile2";
            this.Btn_LoadFile2.Size = new System.Drawing.Size(102, 30);
            this.Btn_LoadFile2.TabIndex = 8;
            this.Btn_LoadFile2.Text = "Load File 2";
            this.Btn_LoadFile2.UseVisualStyleBackColor = true;
            this.Btn_LoadFile2.Click += new System.EventHandler(this.Btn_LoadFile2_Click);
            // 
            // TxtBx_FilePath1
            // 
            this.TxtBx_FilePath1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TxtBx_FilePath1.Location = new System.Drawing.Point(121, 5);
            this.TxtBx_FilePath1.Name = "TxtBx_FilePath1";
            this.TxtBx_FilePath1.Size = new System.Drawing.Size(870, 29);
            this.TxtBx_FilePath1.TabIndex = 7;
            // 
            // Btn_LoadFile1
            // 
            this.Btn_LoadFile1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_LoadFile1.Location = new System.Drawing.Point(3, 5);
            this.Btn_LoadFile1.Name = "Btn_LoadFile1";
            this.Btn_LoadFile1.Size = new System.Drawing.Size(102, 30);
            this.Btn_LoadFile1.TabIndex = 6;
            this.Btn_LoadFile1.Text = "Load File 1";
            this.Btn_LoadFile1.UseVisualStyleBackColor = true;
            this.Btn_LoadFile1.Click += new System.EventHandler(this.Btn_LoadFile1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Chart_Difference);
            this.panel1.Location = new System.Drawing.Point(13, 85);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(743, 457);
            this.panel1.TabIndex = 7;
            // 
            // Chart_Difference
            // 
            this.Chart_Difference.Location = new System.Drawing.Point(3, 3);
            this.Chart_Difference.Name = "Chart_Difference";
            this.Chart_Difference.Size = new System.Drawing.Size(737, 451);
            this.Chart_Difference.TabIndex = 0;
            this.Chart_Difference.Text = "chart1";
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.Name = "Title1";
            title1.Text = "File1-File2";
            this.Chart_Difference.Titles.Add(title1);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.panel3.Controls.Add(this.tableLayoutPanel1);
            this.panel3.Controls.Add(this.Btn_Compare);
            this.panel3.Location = new System.Drawing.Point(759, 85);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(251, 325);
            this.panel3.TabIndex = 9;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Cmbx_TestItem, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Cmbx_ScaleSetting, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.TxtBx_LowLimit, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.TxtBx_UpLimit, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.Cmbx_UsePercentage, 1, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 68);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(242, 189);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(6, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 34);
            this.label4.TabIndex = 7;
            this.label4.Text = "Test Item";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Cmbx_TestItem
            // 
            this.Cmbx_TestItem.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cmbx_TestItem.FormattingEnabled = true;
            this.Cmbx_TestItem.Location = new System.Drawing.Point(125, 6);
            this.Cmbx_TestItem.Name = "Cmbx_TestItem";
            this.Cmbx_TestItem.Size = new System.Drawing.Size(111, 28);
            this.Cmbx_TestItem.TabIndex = 8;
            this.Cmbx_TestItem.SelectedIndexChanged += new System.EventHandler(this.Cmbx_TestItem_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(6, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 34);
            this.label3.TabIndex = 5;
            this.label3.Text = "Scale Setting";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Cmbx_ScaleSetting
            // 
            this.Cmbx_ScaleSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Cmbx_ScaleSetting.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cmbx_ScaleSetting.FormattingEnabled = true;
            this.Cmbx_ScaleSetting.Items.AddRange(new object[] {
            "Define",
            "Auto"});
            this.Cmbx_ScaleSetting.Location = new System.Drawing.Point(125, 43);
            this.Cmbx_ScaleSetting.Name = "Cmbx_ScaleSetting";
            this.Cmbx_ScaleSetting.Size = new System.Drawing.Size(111, 28);
            this.Cmbx_ScaleSetting.TabIndex = 9;
            this.Cmbx_ScaleSetting.Text = "Auto";
            this.Cmbx_ScaleSetting.SelectedIndexChanged += new System.EventHandler(this.Cmbx_ScaleSetting_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(6, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 35);
            this.label2.TabIndex = 2;
            this.label2.Text = "Low Limit";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtBx_LowLimit
            // 
            this.TxtBx_LowLimit.BackColor = System.Drawing.Color.White;
            this.TxtBx_LowLimit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtBx_LowLimit.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TxtBx_LowLimit.Location = new System.Drawing.Point(125, 118);
            this.TxtBx_LowLimit.Name = "TxtBx_LowLimit";
            this.TxtBx_LowLimit.Size = new System.Drawing.Size(111, 29);
            this.TxtBx_LowLimit.TabIndex = 4;
            this.TxtBx_LowLimit.Text = "-0.005";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(6, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "Up Limit";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtBx_UpLimit
            // 
            this.TxtBx_UpLimit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtBx_UpLimit.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TxtBx_UpLimit.Location = new System.Drawing.Point(125, 80);
            this.TxtBx_UpLimit.Name = "TxtBx_UpLimit";
            this.TxtBx_UpLimit.Size = new System.Drawing.Size(111, 29);
            this.TxtBx_UpLimit.TabIndex = 3;
            this.TxtBx_UpLimit.Text = "0.005";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(6, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 33);
            this.label5.TabIndex = 10;
            this.label5.Text = "Percentage";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Cmbx_UsePercentage
            // 
            this.Cmbx_UsePercentage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Cmbx_UsePercentage.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cmbx_UsePercentage.FormattingEnabled = true;
            this.Cmbx_UsePercentage.Items.AddRange(new object[] {
            "No",
            "Yes"});
            this.Cmbx_UsePercentage.Location = new System.Drawing.Point(125, 156);
            this.Cmbx_UsePercentage.Name = "Cmbx_UsePercentage";
            this.Cmbx_UsePercentage.Size = new System.Drawing.Size(111, 28);
            this.Cmbx_UsePercentage.TabIndex = 11;
            // 
            // Btn_Compare
            // 
            this.Btn_Compare.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Compare.Location = new System.Drawing.Point(3, 0);
            this.Btn_Compare.Name = "Btn_Compare";
            this.Btn_Compare.Size = new System.Drawing.Size(242, 61);
            this.Btn_Compare.TabIndex = 5;
            this.Btn_Compare.Text = "Compare";
            this.Btn_Compare.UseVisualStyleBackColor = true;
            this.Btn_Compare.Click += new System.EventHandler(this.Btn_Compare_Click);
            // 
            // F_OneToOne
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.ClientSize = new System.Drawing.Size(1022, 554);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "F_OneToOne";
            this.Text = "F_OneToOne";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Chart_Difference)).EndInit();
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox TxtBx_FilePath2;
        private System.Windows.Forms.Button Btn_LoadFile2;
        private System.Windows.Forms.TextBox TxtBx_FilePath1;
        private System.Windows.Forms.Button Btn_LoadFile1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtBx_LowLimit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtBx_UpLimit;
        private System.Windows.Forms.ComboBox Cmbx_TestItem;
        private System.Windows.Forms.Button Btn_Compare;
        private System.Windows.Forms.DataVisualization.Charting.Chart Chart_Difference;
        private System.Windows.Forms.ComboBox Cmbx_ScaleSetting;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox Cmbx_UsePercentage;
    }
}
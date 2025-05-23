﻿
namespace InstrumentTest
{
    partial class F_TC_Setting
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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.Cmbx_Parity = new System.Windows.Forms.ComboBox();
            this.Cmbx_BaudRate = new System.Windows.Forms.ComboBox();
            this.Cmbx_TC_Type = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.Cmbx_Comport = new System.Windows.Forms.ComboBox();
            this.Panel_ShowFormName = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Cmbx_ReadTempAvg = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble;
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel5.Controls.Add(this.label1, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.Cmbx_Parity, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.Cmbx_BaudRate, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.Cmbx_TC_Type, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.label8, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.label9, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.Cmbx_Comport, 1, 3);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(12, 7);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 4;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(350, 156);
            this.tableLayoutPanel5.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(6, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 36);
            this.label1.TabIndex = 11;
            this.label1.Text = "Comport";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Cmbx_Parity
            // 
            this.Cmbx_Parity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Cmbx_Parity.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cmbx_Parity.FormattingEnabled = true;
            this.Cmbx_Parity.Items.AddRange(new object[] {
            "Odd",
            "Even",
            "None"});
            this.Cmbx_Parity.Location = new System.Drawing.Point(213, 82);
            this.Cmbx_Parity.Name = "Cmbx_Parity";
            this.Cmbx_Parity.Size = new System.Drawing.Size(131, 28);
            this.Cmbx_Parity.TabIndex = 10;
            // 
            // Cmbx_BaudRate
            // 
            this.Cmbx_BaudRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Cmbx_BaudRate.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cmbx_BaudRate.FormattingEnabled = true;
            this.Cmbx_BaudRate.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "38400"});
            this.Cmbx_BaudRate.Location = new System.Drawing.Point(213, 44);
            this.Cmbx_BaudRate.Name = "Cmbx_BaudRate";
            this.Cmbx_BaudRate.Size = new System.Drawing.Size(131, 28);
            this.Cmbx_BaudRate.TabIndex = 9;
            // 
            // Cmbx_TC_Type
            // 
            this.Cmbx_TC_Type.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Cmbx_TC_Type.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cmbx_TC_Type.FormattingEnabled = true;
            this.Cmbx_TC_Type.Items.AddRange(new object[] {
            "TPT8000"});
            this.Cmbx_TC_Type.Location = new System.Drawing.Point(213, 6);
            this.Cmbx_TC_Type.Name = "Cmbx_TC_Type";
            this.Cmbx_TC_Type.Size = new System.Drawing.Size(131, 28);
            this.Cmbx_TC_Type.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.Location = new System.Drawing.Point(6, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(198, 35);
            this.label7.TabIndex = 6;
            this.label7.Text = "TC Type";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label8.Location = new System.Drawing.Point(6, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(198, 35);
            this.label8.TabIndex = 7;
            this.label8.Text = "Baud Rate";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label9.Location = new System.Drawing.Point(6, 79);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(198, 35);
            this.label9.TabIndex = 8;
            this.label9.Text = "Parity";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Cmbx_Comport
            // 
            this.Cmbx_Comport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Cmbx_Comport.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cmbx_Comport.FormattingEnabled = true;
            this.Cmbx_Comport.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10"});
            this.Cmbx_Comport.Location = new System.Drawing.Point(213, 120);
            this.Cmbx_Comport.Name = "Cmbx_Comport";
            this.Cmbx_Comport.Size = new System.Drawing.Size(131, 28);
            this.Cmbx_Comport.TabIndex = 12;
            // 
            // Panel_ShowFormName
            // 
            this.Panel_ShowFormName.Location = new System.Drawing.Point(968, 517);
            this.Panel_ShowFormName.Name = "Panel_ShowFormName";
            this.Panel_ShowFormName.Size = new System.Drawing.Size(52, 35);
            this.Panel_ShowFormName.TabIndex = 6;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Controls.Add(this.Cmbx_ReadTempAvg, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(377, 7);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(350, 143);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // Cmbx_ReadTempAvg
            // 
            this.Cmbx_ReadTempAvg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Cmbx_ReadTempAvg.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cmbx_ReadTempAvg.FormattingEnabled = true;
            this.Cmbx_ReadTempAvg.Items.AddRange(new object[] {
            "NO USE",
            "USE"});
            this.Cmbx_ReadTempAvg.Location = new System.Drawing.Point(213, 6);
            this.Cmbx_ReadTempAvg.Name = "Cmbx_ReadTempAvg";
            this.Cmbx_ReadTempAvg.Size = new System.Drawing.Size(131, 28);
            this.Cmbx_ReadTempAvg.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(6, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(198, 32);
            this.label3.TabIndex = 6;
            this.label3.Text = "Read Temp Avg";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // F_TC_Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.ClientSize = new System.Drawing.Size(1022, 554);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.Panel_ShowFormName);
            this.Controls.Add(this.tableLayoutPanel5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "F_TC_Setting";
            this.Text = "F_TC_Setting";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.F_TC_Setting_FormClosed);
            this.VisibleChanged += new System.EventHandler(this.F_TC_Setting_VisibleChanged);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox Cmbx_Parity;
        private System.Windows.Forms.ComboBox Cmbx_BaudRate;
        private System.Windows.Forms.ComboBox Cmbx_TC_Type;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox Cmbx_Comport;
        private System.Windows.Forms.Panel Panel_ShowFormName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox Cmbx_ReadTempAvg;
        private System.Windows.Forms.Label label3;
    }
}
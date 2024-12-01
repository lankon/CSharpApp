
namespace InstrumentTest
{
    partial class F_TemperatureController
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_TemperatureController));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Labl_PV = new System.Windows.Forms.Label();
            this.Labl_SV = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Btn_StartAll = new System.Windows.Forms.Button();
            this.Btn_Stop = new System.Windows.Forms.Button();
            this.Btn_Start = new System.Windows.Forms.Button();
            this.Btn_Connect = new System.Windows.Forms.Button();
            this.Btn_DisConnect = new System.Windows.Forms.Button();
            this.Btn_StopAll = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Panel_ShowFormName = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.TxtBx_CT_Time = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.TxtBx_TC_Target = new System.Windows.Forms.TextBox();
            this.TxtBx_CT_T2 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.Cmbx_CycleTest = new System.Windows.Forms.ComboBox();
            this.TxtBx_CT_T1 = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.Labl_PV, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.Labl_SV, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 13);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.18058F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.81941F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(432, 200);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // Labl_PV
            // 
            this.Labl_PV.AutoSize = true;
            this.Labl_PV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Labl_PV.Font = new System.Drawing.Font("微軟正黑體", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Labl_PV.Location = new System.Drawing.Point(220, 36);
            this.Labl_PV.Name = "Labl_PV";
            this.Labl_PV.Size = new System.Drawing.Size(206, 161);
            this.Labl_PV.TabIndex = 4;
            this.Labl_PV.Text = "_._";
            this.Labl_PV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Labl_SV
            // 
            this.Labl_SV.AutoSize = true;
            this.Labl_SV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Labl_SV.Font = new System.Drawing.Font("微軟正黑體", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Labl_SV.Location = new System.Drawing.Point(6, 36);
            this.Labl_SV.Name = "Labl_SV";
            this.Labl_SV.Size = new System.Drawing.Size(205, 161);
            this.Labl_SV.TabIndex = 3;
            this.Labl_SV.Text = "_._";
            this.Labl_SV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(6, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(205, 30);
            this.label2.TabIndex = 1;
            this.label2.Text = "SV";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(220, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "PV";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.Btn_StartAll);
            this.panel1.Controls.Add(this.Btn_Stop);
            this.panel1.Controls.Add(this.Btn_Start);
            this.panel1.Controls.Add(this.Btn_Connect);
            this.panel1.Controls.Add(this.Btn_DisConnect);
            this.panel1.Controls.Add(this.Btn_StopAll);
            this.panel1.Location = new System.Drawing.Point(12, 494);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(998, 93);
            this.panel1.TabIndex = 4;
            // 
            // Btn_StartAll
            // 
            this.Btn_StartAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_StartAll.BackgroundImage")));
            this.Btn_StartAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_StartAll.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_StartAll.Location = new System.Drawing.Point(274, 4);
            this.Btn_StartAll.Name = "Btn_StartAll";
            this.Btn_StartAll.Size = new System.Drawing.Size(85, 86);
            this.Btn_StartAll.TabIndex = 9;
            this.Btn_StartAll.UseVisualStyleBackColor = true;
            this.Btn_StartAll.Click += new System.EventHandler(this.Btn_StartAll_Click);
            // 
            // Btn_Stop
            // 
            this.Btn_Stop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_Stop.BackgroundImage")));
            this.Btn_Stop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_Stop.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Stop.Location = new System.Drawing.Point(184, 4);
            this.Btn_Stop.Name = "Btn_Stop";
            this.Btn_Stop.Size = new System.Drawing.Size(85, 86);
            this.Btn_Stop.TabIndex = 8;
            this.Btn_Stop.UseVisualStyleBackColor = true;
            this.Btn_Stop.Click += new System.EventHandler(this.Btn_Stop_Click);
            // 
            // Btn_Start
            // 
            this.Btn_Start.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_Start.BackgroundImage")));
            this.Btn_Start.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_Start.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Start.Location = new System.Drawing.Point(94, 4);
            this.Btn_Start.Name = "Btn_Start";
            this.Btn_Start.Size = new System.Drawing.Size(85, 86);
            this.Btn_Start.TabIndex = 4;
            this.Btn_Start.UseVisualStyleBackColor = true;
            this.Btn_Start.Click += new System.EventHandler(this.Btn_Start_Click);
            // 
            // Btn_Connect
            // 
            this.Btn_Connect.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_Connect.BackgroundImage")));
            this.Btn_Connect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_Connect.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Connect.Location = new System.Drawing.Point(3, 4);
            this.Btn_Connect.Name = "Btn_Connect";
            this.Btn_Connect.Size = new System.Drawing.Size(85, 86);
            this.Btn_Connect.TabIndex = 6;
            this.Btn_Connect.UseVisualStyleBackColor = true;
            this.Btn_Connect.Click += new System.EventHandler(this.Btn_Connect_Click);
            // 
            // Btn_DisConnect
            // 
            this.Btn_DisConnect.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_DisConnect.BackgroundImage")));
            this.Btn_DisConnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_DisConnect.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_DisConnect.Location = new System.Drawing.Point(4, 4);
            this.Btn_DisConnect.Name = "Btn_DisConnect";
            this.Btn_DisConnect.Size = new System.Drawing.Size(85, 86);
            this.Btn_DisConnect.TabIndex = 7;
            this.Btn_DisConnect.UseVisualStyleBackColor = true;
            this.Btn_DisConnect.Visible = false;
            this.Btn_DisConnect.Click += new System.EventHandler(this.Btn_DisConnect_Click);
            // 
            // Btn_StopAll
            // 
            this.Btn_StopAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_StopAll.BackgroundImage")));
            this.Btn_StopAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_StopAll.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_StopAll.Location = new System.Drawing.Point(274, 4);
            this.Btn_StopAll.Name = "Btn_StopAll";
            this.Btn_StopAll.Size = new System.Drawing.Size(85, 86);
            this.Btn_StopAll.TabIndex = 10;
            this.Btn_StopAll.UseVisualStyleBackColor = true;
            this.Btn_StopAll.Visible = false;
            this.Btn_StopAll.Click += new System.EventHandler(this.Btn_StopAll_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.chart1);
            this.panel2.Location = new System.Drawing.Point(12, 220);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(998, 267);
            this.panel2.TabIndex = 5;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Location = new System.Drawing.Point(2, 3);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(991, 245);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // Panel_ShowFormName
            // 
            this.Panel_ShowFormName.Location = new System.Drawing.Point(966, 559);
            this.Panel_ShowFormName.Name = "Panel_ShowFormName";
            this.Panel_ShowFormName.Size = new System.Drawing.Size(52, 38);
            this.Panel_ShowFormName.TabIndex = 15;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.TxtBx_CT_Time, 1, 4);
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label9, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label10, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.label11, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.TxtBx_TC_Target, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.TxtBx_CT_T2, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.label12, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.Cmbx_CycleTest, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.TxtBx_CT_T1, 1, 2);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(450, 12);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 5;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(286, 200);
            this.tableLayoutPanel3.TabIndex = 16;
            // 
            // TxtBx_CT_Time
            // 
            this.TxtBx_CT_Time.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtBx_CT_Time.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TxtBx_CT_Time.Location = new System.Drawing.Point(147, 162);
            this.TxtBx_CT_Time.Name = "TxtBx_CT_Time";
            this.TxtBx_CT_Time.Size = new System.Drawing.Size(133, 29);
            this.TxtBx_CT_Time.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(6, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 36);
            this.label3.TabIndex = 0;
            this.label3.Text = "Target Temp.";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label9.Location = new System.Drawing.Point(6, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(132, 36);
            this.label9.TabIndex = 1;
            this.label9.Text = "Cycle Test";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label10.Location = new System.Drawing.Point(6, 81);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(132, 36);
            this.label10.TabIndex = 2;
            this.label10.Text = "CT Temp.1";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label11.Location = new System.Drawing.Point(6, 120);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(132, 36);
            this.label11.TabIndex = 3;
            this.label11.Text = "CT Temp.2";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TxtBx_TC_Target
            // 
            this.TxtBx_TC_Target.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtBx_TC_Target.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TxtBx_TC_Target.Location = new System.Drawing.Point(147, 6);
            this.TxtBx_TC_Target.Name = "TxtBx_TC_Target";
            this.TxtBx_TC_Target.Size = new System.Drawing.Size(133, 29);
            this.TxtBx_TC_Target.TabIndex = 4;
            this.TxtBx_TC_Target.TextChanged += new System.EventHandler(this.TxtBx_Target_TextChanged);
            // 
            // TxtBx_CT_T2
            // 
            this.TxtBx_CT_T2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtBx_CT_T2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TxtBx_CT_T2.Location = new System.Drawing.Point(147, 123);
            this.TxtBx_CT_T2.Name = "TxtBx_CT_T2";
            this.TxtBx_CT_T2.Size = new System.Drawing.Size(133, 29);
            this.TxtBx_CT_T2.TabIndex = 6;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label12.Location = new System.Drawing.Point(6, 159);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(132, 38);
            this.label12.TabIndex = 7;
            this.label12.Text = "CT Time(min)";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Cmbx_CycleTest
            // 
            this.Cmbx_CycleTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Cmbx_CycleTest.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cmbx_CycleTest.FormattingEnabled = true;
            this.Cmbx_CycleTest.Items.AddRange(new object[] {
            "No Use",
            "Use"});
            this.Cmbx_CycleTest.Location = new System.Drawing.Point(147, 45);
            this.Cmbx_CycleTest.Name = "Cmbx_CycleTest";
            this.Cmbx_CycleTest.Size = new System.Drawing.Size(133, 28);
            this.Cmbx_CycleTest.TabIndex = 8;
            // 
            // TxtBx_CT_T1
            // 
            this.TxtBx_CT_T1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtBx_CT_T1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBx_CT_T1.Location = new System.Drawing.Point(147, 84);
            this.TxtBx_CT_T1.Name = "TxtBx_CT_T1";
            this.TxtBx_CT_T1.Size = new System.Drawing.Size(133, 29);
            this.TxtBx_CT_T1.TabIndex = 10;
            // 
            // F_TemperatureController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.ClientSize = new System.Drawing.Size(1022, 600);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.Panel_ShowFormName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "F_TemperatureController";
            this.Text = "F_TemperatureController";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label Labl_PV;
        private System.Windows.Forms.Label Labl_SV;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Btn_Start;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button Btn_Connect;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button Btn_DisConnect;
        private System.Windows.Forms.Button Btn_Stop;
        private System.Windows.Forms.Button Btn_StartAll;
        private System.Windows.Forms.Button Btn_StopAll;
        private System.Windows.Forms.Panel Panel_ShowFormName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TextBox TxtBx_CT_Time;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox TxtBx_TC_Target;
        private System.Windows.Forms.TextBox TxtBx_CT_T2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox Cmbx_CycleTest;
        private System.Windows.Forms.TextBox TxtBx_CT_T1;
    }
}
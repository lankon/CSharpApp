
namespace InstrumentTest
{
    partial class F_LoadCell
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.Btn_ZeroCalibration = new System.Windows.Forms.Button();
            this.Pnl_Open = new System.Windows.Forms.Panel();
            this.Btn_Close = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtBx_Target = new System.Windows.Forms.TextBox();
            this.Cmbx_Type = new System.Windows.Forms.ComboBox();
            this.Btn_Open = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Cmbx_Com1_DeviceNum = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.Cmbx_Com1_Comport = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Pnl_Com1_Touch3 = new System.Windows.Forms.Panel();
            this.TxtBx_Com1_Gram3 = new System.Windows.Forms.TextBox();
            this.TxtBx_Com1_Gram2 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtBx_Com1_Gram1 = new System.Windows.Forms.TextBox();
            this.Pnl_Com1_Touch1 = new System.Windows.Forms.Panel();
            this.Pnl_Com1_Touch2 = new System.Windows.Forms.Panel();
            this.TxtBx_Com1_Station3 = new System.Windows.Forms.TextBox();
            this.TxtBx_Com1_Station2 = new System.Windows.Forms.TextBox();
            this.TxtBx_Com1_Station1 = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Btn_ZeroCalibration);
            this.panel1.Controls.Add(this.Pnl_Open);
            this.panel1.Controls.Add(this.Btn_Close);
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Controls.Add(this.Btn_Open);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 204);
            this.panel1.TabIndex = 9;
            // 
            // Btn_ZeroCalibration
            // 
            this.Btn_ZeroCalibration.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_ZeroCalibration.Location = new System.Drawing.Point(4, 62);
            this.Btn_ZeroCalibration.Name = "Btn_ZeroCalibration";
            this.Btn_ZeroCalibration.Size = new System.Drawing.Size(242, 51);
            this.Btn_ZeroCalibration.TabIndex = 12;
            this.Btn_ZeroCalibration.Text = "Zero Calibration";
            this.Btn_ZeroCalibration.UseVisualStyleBackColor = true;
            this.Btn_ZeroCalibration.Click += new System.EventHandler(this.Btn_ZeroCalibration_Click);
            // 
            // Pnl_Open
            // 
            this.Pnl_Open.BackColor = System.Drawing.Color.Red;
            this.Pnl_Open.Location = new System.Drawing.Point(5, 44);
            this.Pnl_Open.Name = "Pnl_Open";
            this.Pnl_Open.Size = new System.Drawing.Size(112, 10);
            this.Pnl_Open.TabIndex = 11;
            // 
            // Btn_Close
            // 
            this.Btn_Close.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Close.Location = new System.Drawing.Point(130, 3);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(117, 51);
            this.Btn_Close.TabIndex = 11;
            this.Btn_Close.Text = "Close";
            this.Btn_Close.UseVisualStyleBackColor = true;
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.TxtBx_Target, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.Cmbx_Type, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(5, 123);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(242, 75);
            this.tableLayoutPanel2.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(6, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 33);
            this.label4.TabIndex = 7;
            this.label4.Text = "Type";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(6, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 33);
            this.label2.TabIndex = 0;
            this.label2.Text = "Target(g)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtBx_Target
            // 
            this.TxtBx_Target.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtBx_Target.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TxtBx_Target.Location = new System.Drawing.Point(125, 42);
            this.TxtBx_Target.Name = "TxtBx_Target";
            this.TxtBx_Target.Size = new System.Drawing.Size(111, 29);
            this.TxtBx_Target.TabIndex = 3;
            this.TxtBx_Target.Text = "5";
            // 
            // Cmbx_Type
            // 
            this.Cmbx_Type.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cmbx_Type.FormattingEnabled = true;
            this.Cmbx_Type.Items.AddRange(new object[] {
            "Delta"});
            this.Cmbx_Type.Location = new System.Drawing.Point(125, 6);
            this.Cmbx_Type.Name = "Cmbx_Type";
            this.Cmbx_Type.Size = new System.Drawing.Size(111, 28);
            this.Cmbx_Type.TabIndex = 8;
            // 
            // Btn_Open
            // 
            this.Btn_Open.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Open.Location = new System.Drawing.Point(3, 3);
            this.Btn_Open.Name = "Btn_Open";
            this.Btn_Open.Size = new System.Drawing.Size(116, 51);
            this.Btn_Open.TabIndex = 9;
            this.Btn_Open.Text = "Open";
            this.Btn_Open.UseVisualStyleBackColor = true;
            this.Btn_Open.Click += new System.EventHandler(this.Btn_Open_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.groupBox1.Controls.Add(this.Cmbx_Com1_DeviceNum);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.Cmbx_Com1_Comport);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(275, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(735, 242);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // Cmbx_Com1_DeviceNum
            // 
            this.Cmbx_Com1_DeviceNum.FormattingEnabled = true;
            this.Cmbx_Com1_DeviceNum.Items.AddRange(new object[] {
            "None",
            "1",
            "2",
            "3",
            "4"});
            this.Cmbx_Com1_DeviceNum.Location = new System.Drawing.Point(321, 205);
            this.Cmbx_Com1_DeviceNum.Name = "Cmbx_Com1_DeviceNum";
            this.Cmbx_Com1_DeviceNum.Size = new System.Drawing.Size(101, 28);
            this.Cmbx_Com1_DeviceNum.TabIndex = 15;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(218, 208);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(97, 20);
            this.label10.TabIndex = 14;
            this.label10.Text = "DeviceNum";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Cmbx_Com1_Comport
            // 
            this.Cmbx_Com1_Comport.FormattingEnabled = true;
            this.Cmbx_Com1_Comport.Items.AddRange(new object[] {
            "NONE",
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5"});
            this.Cmbx_Com1_Comport.Location = new System.Drawing.Point(92, 205);
            this.Cmbx_Com1_Comport.Name = "Cmbx_Com1_Comport";
            this.Cmbx_Com1_Comport.Size = new System.Drawing.Size(101, 28);
            this.Cmbx_Com1_Comport.TabIndex = 13;
            this.Cmbx_Com1_Comport.Text = "None";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 208);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 20);
            this.label9.TabIndex = 12;
            this.label9.Text = "Comport";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.00001F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.TxtBx_Com1_Station1, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.TxtBx_Com1_Station2, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.TxtBx_Com1_Station3, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.Pnl_Com1_Touch3, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.TxtBx_Com1_Gram3, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.TxtBx_Com1_Gram2, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label8, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label7, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.TxtBx_Com1_Gram1, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.Pnl_Com1_Touch1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.Pnl_Com1_Touch2, 2, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 11);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.01891F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.01892F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.01892F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.96886F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.97439F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(723, 182);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // Pnl_Com1_Touch3
            // 
            this.Pnl_Com1_Touch3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pnl_Com1_Touch3.Location = new System.Drawing.Point(436, 41);
            this.Pnl_Com1_Touch3.Name = "Pnl_Com1_Touch3";
            this.Pnl_Com1_Touch3.Size = new System.Drawing.Size(134, 26);
            this.Pnl_Com1_Touch3.TabIndex = 20;
            // 
            // TxtBx_Com1_Gram3
            // 
            this.TxtBx_Com1_Gram3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtBx_Com1_Gram3.Location = new System.Drawing.Point(436, 111);
            this.TxtBx_Com1_Gram3.Name = "TxtBx_Com1_Gram3";
            this.TxtBx_Com1_Gram3.ReadOnly = true;
            this.TxtBx_Com1_Gram3.Size = new System.Drawing.Size(134, 29);
            this.TxtBx_Com1_Gram3.TabIndex = 19;
            this.TxtBx_Com1_Gram3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TxtBx_Com1_Gram2
            // 
            this.TxtBx_Com1_Gram2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtBx_Com1_Gram2.Location = new System.Drawing.Point(293, 111);
            this.TxtBx_Com1_Gram2.Name = "TxtBx_Com1_Gram2";
            this.TxtBx_Com1_Gram2.ReadOnly = true;
            this.TxtBx_Com1_Gram2.Size = new System.Drawing.Size(134, 29);
            this.TxtBx_Com1_Gram2.TabIndex = 16;
            this.TxtBx_Com1_Gram2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label12.Location = new System.Drawing.Point(6, 143);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(135, 36);
            this.label12.TabIndex = 14;
            this.label12.Text = "Station";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label11.Location = new System.Drawing.Point(6, 73);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(135, 32);
            this.label11.TabIndex = 13;
            this.label11.Text = "Gain";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(6, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 32);
            this.label1.TabIndex = 12;
            this.label1.Text = "Gram(g)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(579, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(138, 32);
            this.label8.TabIndex = 11;
            this.label8.Text = "AES-4";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(436, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(134, 32);
            this.label7.TabIndex = 10;
            this.label7.Text = "AES-3";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(293, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 32);
            this.label6.TabIndex = 9;
            this.label6.Text = "AES-2";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(6, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 32);
            this.label3.TabIndex = 0;
            this.label3.Text = "Touch";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(150, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 32);
            this.label5.TabIndex = 8;
            this.label5.Text = "AES-1";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtBx_Com1_Gram1
            // 
            this.TxtBx_Com1_Gram1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtBx_Com1_Gram1.Location = new System.Drawing.Point(150, 111);
            this.TxtBx_Com1_Gram1.Name = "TxtBx_Com1_Gram1";
            this.TxtBx_Com1_Gram1.ReadOnly = true;
            this.TxtBx_Com1_Gram1.Size = new System.Drawing.Size(134, 29);
            this.TxtBx_Com1_Gram1.TabIndex = 15;
            this.TxtBx_Com1_Gram1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Pnl_Com1_Touch1
            // 
            this.Pnl_Com1_Touch1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pnl_Com1_Touch1.Location = new System.Drawing.Point(150, 41);
            this.Pnl_Com1_Touch1.Name = "Pnl_Com1_Touch1";
            this.Pnl_Com1_Touch1.Size = new System.Drawing.Size(134, 26);
            this.Pnl_Com1_Touch1.TabIndex = 17;
            // 
            // Pnl_Com1_Touch2
            // 
            this.Pnl_Com1_Touch2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pnl_Com1_Touch2.Location = new System.Drawing.Point(293, 41);
            this.Pnl_Com1_Touch2.Name = "Pnl_Com1_Touch2";
            this.Pnl_Com1_Touch2.Size = new System.Drawing.Size(134, 26);
            this.Pnl_Com1_Touch2.TabIndex = 18;
            // 
            // TxtBx_Com1_Station3
            // 
            this.TxtBx_Com1_Station3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtBx_Com1_Station3.Location = new System.Drawing.Point(436, 146);
            this.TxtBx_Com1_Station3.Name = "TxtBx_Com1_Station3";
            this.TxtBx_Com1_Station3.Size = new System.Drawing.Size(134, 29);
            this.TxtBx_Com1_Station3.TabIndex = 21;
            this.TxtBx_Com1_Station3.Text = "0";
            this.TxtBx_Com1_Station3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TxtBx_Com1_Station2
            // 
            this.TxtBx_Com1_Station2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtBx_Com1_Station2.Location = new System.Drawing.Point(293, 146);
            this.TxtBx_Com1_Station2.Name = "TxtBx_Com1_Station2";
            this.TxtBx_Com1_Station2.Size = new System.Drawing.Size(134, 29);
            this.TxtBx_Com1_Station2.TabIndex = 22;
            this.TxtBx_Com1_Station2.Text = "0";
            this.TxtBx_Com1_Station2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TxtBx_Com1_Station1
            // 
            this.TxtBx_Com1_Station1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtBx_Com1_Station1.Location = new System.Drawing.Point(150, 146);
            this.TxtBx_Com1_Station1.Name = "TxtBx_Com1_Station1";
            this.TxtBx_Com1_Station1.Size = new System.Drawing.Size(134, 29);
            this.TxtBx_Com1_Station1.TabIndex = 23;
            this.TxtBx_Com1_Station1.Text = "0";
            this.TxtBx_Com1_Station1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // F_LoadCell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.ClientSize = new System.Drawing.Size(1022, 554);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(13, 77);
            this.Name = "F_LoadCell";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "F_LoadCell";
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Btn_Close;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtBx_Target;
        private System.Windows.Forms.ComboBox Cmbx_Type;
        private System.Windows.Forms.Button Btn_Open;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel Pnl_Open;
        private System.Windows.Forms.ComboBox Cmbx_Com1_DeviceNum;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox Cmbx_Com1_Comport;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox TxtBx_Com1_Gram2;
        private System.Windows.Forms.TextBox TxtBx_Com1_Gram1;
        private System.Windows.Forms.Panel Pnl_Com1_Touch1;
        private System.Windows.Forms.Panel Pnl_Com1_Touch2;
        private System.Windows.Forms.TextBox TxtBx_Com1_Gram3;
        private System.Windows.Forms.Panel Pnl_Com1_Touch3;
        private System.Windows.Forms.Button Btn_ZeroCalibration;
        private System.Windows.Forms.TextBox TxtBx_Com1_Station1;
        private System.Windows.Forms.TextBox TxtBx_Com1_Station2;
        private System.Windows.Forms.TextBox TxtBx_Com1_Station3;
    }
}
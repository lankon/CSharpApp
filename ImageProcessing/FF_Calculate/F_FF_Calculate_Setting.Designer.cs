
namespace ImageProcessing.FF_Calculate
{
    partial class F_FF_Calculate_Setting
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.TxtBx_BatchPath = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label16 = new System.Windows.Forms.Label();
            this.Cmbx_ServerMode = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Title_IO = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Title_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title_Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title_CardType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title_CardNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title_IO_Num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title_Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Btn_Add = new System.Windows.Forms.Button();
            this.Btn_Remove = new System.Windows.Forms.Button();
            this.Btn_RowUp = new System.Windows.Forms.Button();
            this.Btn_RowDown = new System.Windows.Forms.Button();
            this.Btn_Save = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.Cmbx_CalValley = new System.Windows.Forms.ComboBox();
            this.Cmbx_CalEyeSafe = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtBx_TestHeight = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.TxtBx_TeachPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtBx_PixelSize = new System.Windows.Forms.TextBox();
            this.Cmbx_CalAngle = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // TxtBx_BatchPath
            // 
            this.TxtBx_BatchPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtBx_BatchPath.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TxtBx_BatchPath.Location = new System.Drawing.Point(338, 120);
            this.TxtBx_BatchPath.Name = "TxtBx_BatchPath";
            this.TxtBx_BatchPath.Size = new System.Drawing.Size(140, 29);
            this.TxtBx_BatchPath.TabIndex = 14;
            this.TxtBx_BatchPath.Click += new System.EventHandler(this.TxtBx_BatchPath_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.43867F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.56133F));
            this.tableLayoutPanel2.Controls.Add(this.label16, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.Cmbx_ServerMode, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(526, 12);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(484, 114);
            this.tableLayoutPanel2.TabIndex = 11;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label16.Location = new System.Drawing.Point(6, 3);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(323, 34);
            this.label16.TabIndex = 7;
            this.label16.Text = "Server Mode";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Cmbx_ServerMode
            // 
            this.Cmbx_ServerMode.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cmbx_ServerMode.FormattingEnabled = true;
            this.Cmbx_ServerMode.Items.AddRange(new object[] {
            "No Use",
            "Use"});
            this.Cmbx_ServerMode.Location = new System.Drawing.Point(338, 6);
            this.Cmbx_ServerMode.Name = "Cmbx_ServerMode";
            this.Cmbx_ServerMode.Size = new System.Drawing.Size(140, 28);
            this.Cmbx_ServerMode.TabIndex = 8;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Title_IO,
            this.Title_Name,
            this.Title_Description,
            this.Title_CardType,
            this.Title_CardNum,
            this.Title_IO_Num,
            this.Title_Status});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Location = new System.Drawing.Point(12, 372);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(783, 170);
            this.dataGridView1.TabIndex = 12;
            // 
            // Title_IO
            // 
            this.Title_IO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Title_IO.DefaultCellStyle = dataGridViewCellStyle2;
            this.Title_IO.HeaderText = "I/O";
            this.Title_IO.Items.AddRange(new object[] {
            "None",
            "Input",
            "Output"});
            this.Title_IO.Name = "Title_IO";
            this.Title_IO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Title_IO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Title_Name
            // 
            this.Title_Name.HeaderText = "Name";
            this.Title_Name.Name = "Title_Name";
            // 
            // Title_Description
            // 
            this.Title_Description.HeaderText = "Description";
            this.Title_Description.Name = "Title_Description";
            this.Title_Description.Width = 120;
            // 
            // Title_CardType
            // 
            this.Title_CardType.HeaderText = "Card Type";
            this.Title_CardType.Name = "Title_CardType";
            this.Title_CardType.Width = 120;
            // 
            // Title_CardNum
            // 
            this.Title_CardNum.HeaderText = "Card No.";
            this.Title_CardNum.Name = "Title_CardNum";
            this.Title_CardNum.Width = 120;
            // 
            // Title_IO_Num
            // 
            this.Title_IO_Num.HeaderText = "I/O No.";
            this.Title_IO_Num.Name = "Title_IO_Num";
            this.Title_IO_Num.Width = 120;
            // 
            // Title_Status
            // 
            this.Title_Status.HeaderText = "Status";
            this.Title_Status.Name = "Title_Status";
            // 
            // Btn_Add
            // 
            this.Btn_Add.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Add.Location = new System.Drawing.Point(801, 371);
            this.Btn_Add.Name = "Btn_Add";
            this.Btn_Add.Size = new System.Drawing.Size(123, 43);
            this.Btn_Add.TabIndex = 13;
            this.Btn_Add.Text = "Add";
            this.Btn_Add.UseVisualStyleBackColor = true;
            this.Btn_Add.Click += new System.EventHandler(this.Btn_Add_Click);
            // 
            // Btn_Remove
            // 
            this.Btn_Remove.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Remove.Location = new System.Drawing.Point(801, 414);
            this.Btn_Remove.Name = "Btn_Remove";
            this.Btn_Remove.Size = new System.Drawing.Size(123, 43);
            this.Btn_Remove.TabIndex = 14;
            this.Btn_Remove.Text = "Remove";
            this.Btn_Remove.UseVisualStyleBackColor = true;
            this.Btn_Remove.Click += new System.EventHandler(this.Btn_Remove_Click);
            // 
            // Btn_RowUp
            // 
            this.Btn_RowUp.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_RowUp.Location = new System.Drawing.Point(801, 456);
            this.Btn_RowUp.Name = "Btn_RowUp";
            this.Btn_RowUp.Size = new System.Drawing.Size(123, 43);
            this.Btn_RowUp.TabIndex = 15;
            this.Btn_RowUp.Text = "Row Up";
            this.Btn_RowUp.UseVisualStyleBackColor = true;
            this.Btn_RowUp.Click += new System.EventHandler(this.Btn_RowUp_Click);
            // 
            // Btn_RowDown
            // 
            this.Btn_RowDown.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_RowDown.Location = new System.Drawing.Point(801, 499);
            this.Btn_RowDown.Name = "Btn_RowDown";
            this.Btn_RowDown.Size = new System.Drawing.Size(123, 43);
            this.Btn_RowDown.TabIndex = 16;
            this.Btn_RowDown.Text = "Row Down";
            this.Btn_RowDown.UseVisualStyleBackColor = true;
            this.Btn_RowDown.Click += new System.EventHandler(this.Btn_RowDown_Click);
            // 
            // Btn_Save
            // 
            this.Btn_Save.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Save.Location = new System.Drawing.Point(801, 326);
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.Size = new System.Drawing.Size(123, 43);
            this.Btn_Save.TabIndex = 17;
            this.Btn_Save.Text = "Save";
            this.Btn_Save.UseVisualStyleBackColor = true;
            this.Btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.43867F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.56133F));
            this.tableLayoutPanel3.Controls.Add(this.Cmbx_CalValley, 1, 5);
            this.tableLayoutPanel3.Controls.Add(this.Cmbx_CalEyeSafe, 1, 4);
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 5);
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.TxtBx_TestHeight, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.label9, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label8, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.TxtBx_TeachPath, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.TxtBx_PixelSize, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.Cmbx_CalAngle, 1, 3);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 8;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(484, 326);
            this.tableLayoutPanel3.TabIndex = 18;
            // 
            // Cmbx_CalValley
            // 
            this.Cmbx_CalValley.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Cmbx_CalValley.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cmbx_CalValley.FormattingEnabled = true;
            this.Cmbx_CalValley.Items.AddRange(new object[] {
            "No Use",
            "Use"});
            this.Cmbx_CalValley.Location = new System.Drawing.Point(338, 196);
            this.Cmbx_CalValley.Name = "Cmbx_CalValley";
            this.Cmbx_CalValley.Size = new System.Drawing.Size(140, 28);
            this.Cmbx_CalValley.TabIndex = 22;
            // 
            // Cmbx_CalEyeSafe
            // 
            this.Cmbx_CalEyeSafe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Cmbx_CalEyeSafe.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cmbx_CalEyeSafe.FormattingEnabled = true;
            this.Cmbx_CalEyeSafe.Items.AddRange(new object[] {
            "No Use",
            "Use"});
            this.Cmbx_CalEyeSafe.Location = new System.Drawing.Point(338, 158);
            this.Cmbx_CalEyeSafe.Name = "Cmbx_CalEyeSafe";
            this.Cmbx_CalEyeSafe.Size = new System.Drawing.Size(140, 28);
            this.Cmbx_CalEyeSafe.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(6, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(323, 35);
            this.label3.TabIndex = 18;
            this.label3.Text = "Calculate Valley";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(6, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(323, 35);
            this.label2.TabIndex = 17;
            this.label2.Text = "Calculate Angle";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TxtBx_TestHeight
            // 
            this.TxtBx_TestHeight.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TxtBx_TestHeight.Location = new System.Drawing.Point(338, 44);
            this.TxtBx_TestHeight.Name = "TxtBx_TestHeight";
            this.TxtBx_TestHeight.Size = new System.Drawing.Size(140, 29);
            this.TxtBx_TestHeight.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label9.Location = new System.Drawing.Point(6, 79);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(323, 35);
            this.label9.TabIndex = 10;
            this.label9.Text = "Teach File Path";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.Location = new System.Drawing.Point(6, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(323, 35);
            this.label7.TabIndex = 7;
            this.label7.Text = "Pixel Size(um/pixel)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label8.Location = new System.Drawing.Point(6, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(323, 35);
            this.label8.TabIndex = 9;
            this.label8.Text = "Test Height(mm)";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TxtBx_TeachPath
            // 
            this.TxtBx_TeachPath.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TxtBx_TeachPath.Location = new System.Drawing.Point(338, 82);
            this.TxtBx_TeachPath.Name = "TxtBx_TeachPath";
            this.TxtBx_TeachPath.Size = new System.Drawing.Size(140, 29);
            this.TxtBx_TeachPath.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(6, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(323, 35);
            this.label1.TabIndex = 16;
            this.label1.Text = "Calculate Eye Safe";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TxtBx_PixelSize
            // 
            this.TxtBx_PixelSize.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TxtBx_PixelSize.Location = new System.Drawing.Point(338, 6);
            this.TxtBx_PixelSize.Name = "TxtBx_PixelSize";
            this.TxtBx_PixelSize.Size = new System.Drawing.Size(140, 29);
            this.TxtBx_PixelSize.TabIndex = 19;
            // 
            // Cmbx_CalAngle
            // 
            this.Cmbx_CalAngle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Cmbx_CalAngle.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Cmbx_CalAngle.FormattingEnabled = true;
            this.Cmbx_CalAngle.Items.AddRange(new object[] {
            "No Use",
            "Use"});
            this.Cmbx_CalAngle.Location = new System.Drawing.Point(338, 120);
            this.Cmbx_CalAngle.Name = "Cmbx_CalAngle";
            this.Cmbx_CalAngle.Size = new System.Drawing.Size(140, 28);
            this.Cmbx_CalAngle.TabIndex = 20;
            // 
            // F_FF_Calculate_Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.ClientSize = new System.Drawing.Size(1022, 554);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.Btn_Save);
            this.Controls.Add(this.Btn_RowDown);
            this.Controls.Add(this.Btn_RowUp);
            this.Controls.Add(this.Btn_Remove);
            this.Controls.Add(this.Btn_Add);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.tableLayoutPanel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "F_FF_Calculate_Setting";
            this.Text = "F_FF_Calculate_Setting";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.F_Wafer_Align_Angle_Setting_FormClosed);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox TxtBx_BatchPath;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox Cmbx_ServerMode;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button Btn_Add;
        private System.Windows.Forms.DataGridViewComboBoxColumn Title_IO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title_Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title_CardType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title_CardNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title_IO_Num;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title_Status;
        private System.Windows.Forms.Button Btn_Remove;
        private System.Windows.Forms.Button Btn_RowUp;
        private System.Windows.Forms.Button Btn_RowDown;
        private System.Windows.Forms.Button Btn_Save;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtBx_TestHeight;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TxtBx_TeachPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtBx_PixelSize;
        private System.Windows.Forms.ComboBox Cmbx_CalValley;
        private System.Windows.Forms.ComboBox Cmbx_CalEyeSafe;
        private System.Windows.Forms.ComboBox Cmbx_CalAngle;
    }
}
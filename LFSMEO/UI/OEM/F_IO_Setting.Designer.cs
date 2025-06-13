﻿
namespace LFSMEO.UI
{
    partial class F_IO_Setting
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DGV_IO = new System.Windows.Forms.DataGridView();
            this.Title_IO = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Title_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title_Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title_CardType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Title_IO_Num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title_Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title_Inverse = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Title_CardNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title_LineNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title_DevNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Btn_Load = new System.Windows.Forms.Button();
            this.Btn_Save = new System.Windows.Forms.Button();
            this.Btn_RowDown = new System.Windows.Forms.Button();
            this.Btn_RowUp = new System.Windows.Forms.Button();
            this.Btn_Remove = new System.Windows.Forms.Button();
            this.Btn_Add = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_IO)).BeginInit();
            this.SuspendLayout();
            // 
            // DGV_IO
            // 
            this.DGV_IO.AllowUserToAddRows = false;
            this.DGV_IO.AllowUserToDeleteRows = false;
            this.DGV_IO.AllowUserToResizeColumns = false;
            this.DGV_IO.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV_IO.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.DGV_IO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_IO.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Title_IO,
            this.Title_Name,
            this.Title_Description,
            this.Title_CardType,
            this.Title_IO_Num,
            this.Title_Status,
            this.Title_Inverse,
            this.Title_CardNum,
            this.Title_LineNum,
            this.Title_DevNum});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGV_IO.DefaultCellStyle = dataGridViewCellStyle6;
            this.DGV_IO.Location = new System.Drawing.Point(2, 1);
            this.DGV_IO.Name = "DGV_IO";
            this.DGV_IO.RowHeadersVisible = false;
            this.DGV_IO.RowTemplate.Height = 24;
            this.DGV_IO.Size = new System.Drawing.Size(1187, 660);
            this.DGV_IO.TabIndex = 14;
            // 
            // Title_IO
            // 
            this.Title_IO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Title_IO.DefaultCellStyle = dataGridViewCellStyle5;
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
            this.Title_Name.Width = 162;
            // 
            // Title_Description
            // 
            this.Title_Description.HeaderText = "Description";
            this.Title_Description.Name = "Title_Description";
            this.Title_Description.Width = 162;
            // 
            // Title_CardType
            // 
            this.Title_CardType.HeaderText = "Card Type";
            this.Title_CardType.Items.AddRange(new object[] {
            "None",
            "MN200",
            "PCI_9111"});
            this.Title_CardType.Name = "Title_CardType";
            this.Title_CardType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Title_CardType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Title_CardType.Width = 120;
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
            // Title_Inverse
            // 
            this.Title_Inverse.HeaderText = "Inverse";
            this.Title_Inverse.Name = "Title_Inverse";
            this.Title_Inverse.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Title_Inverse.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Title_CardNum
            // 
            this.Title_CardNum.HeaderText = "Card No.";
            this.Title_CardNum.Name = "Title_CardNum";
            this.Title_CardNum.Width = 120;
            // 
            // Title_LineNum
            // 
            this.Title_LineNum.HeaderText = "Line No.";
            this.Title_LineNum.Name = "Title_LineNum";
            // 
            // Title_DevNum
            // 
            this.Title_DevNum.HeaderText = "Dev No.";
            this.Title_DevNum.Name = "Title_DevNum";
            // 
            // Btn_Load
            // 
            this.Btn_Load.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Load.Location = new System.Drawing.Point(1196, 51);
            this.Btn_Load.Name = "Btn_Load";
            this.Btn_Load.Size = new System.Drawing.Size(123, 47);
            this.Btn_Load.TabIndex = 30;
            this.Btn_Load.Text = "Load";
            this.Btn_Load.UseVisualStyleBackColor = true;
            // 
            // Btn_Save
            // 
            this.Btn_Save.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Save.Location = new System.Drawing.Point(1196, 1);
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.Size = new System.Drawing.Size(123, 47);
            this.Btn_Save.TabIndex = 29;
            this.Btn_Save.Text = "Save";
            this.Btn_Save.UseVisualStyleBackColor = true;
            // 
            // Btn_RowDown
            // 
            this.Btn_RowDown.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_RowDown.Location = new System.Drawing.Point(1196, 251);
            this.Btn_RowDown.Name = "Btn_RowDown";
            this.Btn_RowDown.Size = new System.Drawing.Size(123, 47);
            this.Btn_RowDown.TabIndex = 28;
            this.Btn_RowDown.Text = "Row Down";
            this.Btn_RowDown.UseVisualStyleBackColor = true;
            // 
            // Btn_RowUp
            // 
            this.Btn_RowUp.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_RowUp.Location = new System.Drawing.Point(1196, 201);
            this.Btn_RowUp.Name = "Btn_RowUp";
            this.Btn_RowUp.Size = new System.Drawing.Size(123, 47);
            this.Btn_RowUp.TabIndex = 27;
            this.Btn_RowUp.Text = "Row Up";
            this.Btn_RowUp.UseVisualStyleBackColor = true;
            // 
            // Btn_Remove
            // 
            this.Btn_Remove.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Remove.Location = new System.Drawing.Point(1196, 151);
            this.Btn_Remove.Name = "Btn_Remove";
            this.Btn_Remove.Size = new System.Drawing.Size(123, 47);
            this.Btn_Remove.TabIndex = 26;
            this.Btn_Remove.Text = "Remove";
            this.Btn_Remove.UseVisualStyleBackColor = true;
            // 
            // Btn_Add
            // 
            this.Btn_Add.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Add.Location = new System.Drawing.Point(1196, 101);
            this.Btn_Add.Name = "Btn_Add";
            this.Btn_Add.Size = new System.Drawing.Size(123, 47);
            this.Btn_Add.TabIndex = 25;
            this.Btn_Add.Text = "Add";
            this.Btn_Add.UseVisualStyleBackColor = true;
            this.Btn_Add.Click += new System.EventHandler(this.Btn_Add_Click);
            // 
            // F_IO_Setting
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.ClientSize = new System.Drawing.Size(1326, 661);
            this.Controls.Add(this.Btn_Load);
            this.Controls.Add(this.Btn_Save);
            this.Controls.Add(this.Btn_RowDown);
            this.Controls.Add(this.Btn_RowUp);
            this.Controls.Add(this.Btn_Remove);
            this.Controls.Add(this.Btn_Add);
            this.Controls.Add(this.DGV_IO);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "F_IO_Setting";
            this.Text = "F_Template";
            ((System.ComponentModel.ISupportInitialize)(this.DGV_IO)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DGV_IO;
        private System.Windows.Forms.DataGridViewComboBoxColumn Title_IO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title_Description;
        private System.Windows.Forms.DataGridViewComboBoxColumn Title_CardType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title_IO_Num;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title_Status;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Title_Inverse;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title_CardNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title_LineNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title_DevNum;
        private System.Windows.Forms.Button Btn_Load;
        private System.Windows.Forms.Button Btn_Save;
        private System.Windows.Forms.Button Btn_RowDown;
        private System.Windows.Forms.Button Btn_RowUp;
        private System.Windows.Forms.Button Btn_Remove;
        private System.Windows.Forms.Button Btn_Add;
    }
}
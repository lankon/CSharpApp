
namespace Mapping
{
    partial class F_Mapping
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.Pnl_Mapping = new System.Windows.Forms.Panel();
            this.Labl_ShowCellValue = new System.Windows.Forms.Label();
            this.TxtBx_ShowItem = new System.Windows.Forms.TextBox();
            this.PicBx_Mapping = new System.Windows.Forms.PictureBox();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtBx_Step = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtBx_End = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtBx_Start = new System.Windows.Forms.TextBox();
            this.Cmbx_TestItem = new System.Windows.Forms.ComboBox();
            this.Btn_DrawMap = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.TxtBx_FilePath = new System.Windows.Forms.TextBox();
            this.Btn_LoadFile = new System.Windows.Forms.Button();
            this.Pnl_Colorbar = new System.Windows.Forms.Panel();
            this.PicBx_Colorbar = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Pnl_FormHint = new System.Windows.Forms.Panel();
            this.Pnl_Mapping.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBx_Mapping)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.Pnl_Colorbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBx_Colorbar)).BeginInit();
            this.SuspendLayout();
            // 
            // Pnl_Mapping
            // 
            this.Pnl_Mapping.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Pnl_Mapping.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Mapping.Controls.Add(this.Labl_ShowCellValue);
            this.Pnl_Mapping.Controls.Add(this.TxtBx_ShowItem);
            this.Pnl_Mapping.Controls.Add(this.PicBx_Mapping);
            this.Pnl_Mapping.Location = new System.Drawing.Point(245, 48);
            this.Pnl_Mapping.Name = "Pnl_Mapping";
            this.Pnl_Mapping.Size = new System.Drawing.Size(500, 500);
            this.Pnl_Mapping.TabIndex = 1;
            // 
            // Labl_ShowCellValue
            // 
            this.Labl_ShowCellValue.AutoSize = true;
            this.Labl_ShowCellValue.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Labl_ShowCellValue.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Labl_ShowCellValue.Location = new System.Drawing.Point(458, 40);
            this.Labl_ShowCellValue.Name = "Labl_ShowCellValue";
            this.Labl_ShowCellValue.Size = new System.Drawing.Size(37, 20);
            this.Labl_ShowCellValue.TabIndex = 2;
            this.Labl_ShowCellValue.Text = "X, Y";
            // 
            // TxtBx_ShowItem
            // 
            this.TxtBx_ShowItem.BackColor = System.Drawing.Color.Cornsilk;
            this.TxtBx_ShowItem.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TxtBx_ShowItem.Location = new System.Drawing.Point(390, 3);
            this.TxtBx_ShowItem.Name = "TxtBx_ShowItem";
            this.TxtBx_ShowItem.ReadOnly = true;
            this.TxtBx_ShowItem.Size = new System.Drawing.Size(105, 29);
            this.TxtBx_ShowItem.TabIndex = 1;
            this.TxtBx_ShowItem.Text = "TestItem";
            this.TxtBx_ShowItem.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PicBx_Mapping
            // 
            this.PicBx_Mapping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PicBx_Mapping.Location = new System.Drawing.Point(0, 0);
            this.PicBx_Mapping.Name = "PicBx_Mapping";
            this.PicBx_Mapping.Size = new System.Drawing.Size(498, 498);
            this.PicBx_Mapping.TabIndex = 0;
            this.PicBx_Mapping.TabStop = false;
            this.PicBx_Mapping.Paint += new System.Windows.Forms.PaintEventHandler(this.PicBx_Mapping_Paint);
            this.PicBx_Mapping.DoubleClick += new System.EventHandler(this.PicBx_Mapping_DoubleClick);
            this.PicBx_Mapping.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PicBx_Mapping_MouseClick);
            this.PicBx_Mapping.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PicBx_Mapping_MouseDown);
            this.PicBx_Mapping.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PicBx_Mapping_MouseMove);
            this.PicBx_Mapping.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PicBx_Mapping_MouseUp);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Controls.Add(this.Btn_DrawMap);
            this.panel1.Location = new System.Drawing.Point(758, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(251, 229);
            this.panel1.TabIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.TxtBx_Step, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.TxtBx_End, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.TxtBx_Start, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.Cmbx_TestItem, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 67);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.99813F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(242, 150);
            this.tableLayoutPanel1.TabIndex = 6;
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
            this.label4.Text = "Test Item";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtBx_Step
            // 
            this.TxtBx_Step.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtBx_Step.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TxtBx_Step.Location = new System.Drawing.Point(125, 114);
            this.TxtBx_Step.Name = "TxtBx_Step";
            this.TxtBx_Step.Size = new System.Drawing.Size(111, 29);
            this.TxtBx_Step.TabIndex = 6;
            this.TxtBx_Step.Text = "0.1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(6, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 36);
            this.label3.TabIndex = 5;
            this.label3.Text = "Step";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtBx_End
            // 
            this.TxtBx_End.BackColor = System.Drawing.Color.White;
            this.TxtBx_End.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtBx_End.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TxtBx_End.Location = new System.Drawing.Point(125, 78);
            this.TxtBx_End.Name = "TxtBx_End";
            this.TxtBx_End.Size = new System.Drawing.Size(111, 29);
            this.TxtBx_End.TabIndex = 4;
            this.TxtBx_End.Text = "7";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(6, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "Start";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(6, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 33);
            this.label2.TabIndex = 2;
            this.label2.Text = "End";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtBx_Start
            // 
            this.TxtBx_Start.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtBx_Start.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TxtBx_Start.Location = new System.Drawing.Point(125, 42);
            this.TxtBx_Start.Name = "TxtBx_Start";
            this.TxtBx_Start.Size = new System.Drawing.Size(111, 29);
            this.TxtBx_Start.TabIndex = 3;
            this.TxtBx_Start.Text = "5";
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
            // Btn_DrawMap
            // 
            this.Btn_DrawMap.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_DrawMap.Location = new System.Drawing.Point(3, 0);
            this.Btn_DrawMap.Name = "Btn_DrawMap";
            this.Btn_DrawMap.Size = new System.Drawing.Size(242, 61);
            this.Btn_DrawMap.TabIndex = 5;
            this.Btn_DrawMap.Text = "Draw Mapping";
            this.Btn_DrawMap.UseVisualStyleBackColor = true;
            this.Btn_DrawMap.Click += new System.EventHandler(this.Btn_DrawMap_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.TxtBx_FilePath);
            this.panel2.Controls.Add(this.Btn_LoadFile);
            this.panel2.Location = new System.Drawing.Point(12, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(997, 40);
            this.panel2.TabIndex = 5;
            // 
            // TxtBx_FilePath
            // 
            this.TxtBx_FilePath.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TxtBx_FilePath.Location = new System.Drawing.Point(102, 5);
            this.TxtBx_FilePath.Name = "TxtBx_FilePath";
            this.TxtBx_FilePath.Size = new System.Drawing.Size(889, 29);
            this.TxtBx_FilePath.TabIndex = 7;
            // 
            // Btn_LoadFile
            // 
            this.Btn_LoadFile.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_LoadFile.Location = new System.Drawing.Point(3, 5);
            this.Btn_LoadFile.Name = "Btn_LoadFile";
            this.Btn_LoadFile.Size = new System.Drawing.Size(93, 30);
            this.Btn_LoadFile.TabIndex = 6;
            this.Btn_LoadFile.Text = "Load File";
            this.Btn_LoadFile.UseVisualStyleBackColor = true;
            this.Btn_LoadFile.Click += new System.EventHandler(this.Btn_LoadFile_Click);
            // 
            // Pnl_Colorbar
            // 
            this.Pnl_Colorbar.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Pnl_Colorbar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pnl_Colorbar.Controls.Add(this.PicBx_Colorbar);
            this.Pnl_Colorbar.Location = new System.Drawing.Point(16, 48);
            this.Pnl_Colorbar.Name = "Pnl_Colorbar";
            this.Pnl_Colorbar.Size = new System.Drawing.Size(228, 500);
            this.Pnl_Colorbar.TabIndex = 6;
            // 
            // PicBx_Colorbar
            // 
            this.PicBx_Colorbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PicBx_Colorbar.Location = new System.Drawing.Point(0, 0);
            this.PicBx_Colorbar.Name = "PicBx_Colorbar";
            this.PicBx_Colorbar.Size = new System.Drawing.Size(226, 498);
            this.PicBx_Colorbar.TabIndex = 0;
            this.PicBx_Colorbar.TabStop = false;
            // 
            // Pnl_FormHint
            // 
            this.Pnl_FormHint.Location = new System.Drawing.Point(971, 520);
            this.Pnl_FormHint.Name = "Pnl_FormHint";
            this.Pnl_FormHint.Size = new System.Drawing.Size(51, 35);
            this.Pnl_FormHint.TabIndex = 40;
            // 
            // F_Mapping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.ClientSize = new System.Drawing.Size(1022, 554);
            this.Controls.Add(this.Pnl_Colorbar);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.Pnl_FormHint);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Pnl_Mapping);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "F_Mapping";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "F_Mapping";
            this.Load += new System.EventHandler(this.F_Mapping_Load);
            this.Pnl_Mapping.ResumeLayout(false);
            this.Pnl_Mapping.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBx_Mapping)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.Pnl_Colorbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PicBx_Colorbar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel Pnl_Mapping;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Btn_DrawMap;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TxtBx_Step;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtBx_End;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtBx_Start;
        private System.Windows.Forms.ComboBox Cmbx_TestItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button Btn_LoadFile;
        private System.Windows.Forms.TextBox TxtBx_FilePath;
        private System.Windows.Forms.Panel Pnl_Colorbar;
        private System.Windows.Forms.PictureBox PicBx_Colorbar;
        private System.Windows.Forms.PictureBox PicBx_Mapping;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox TxtBx_ShowItem;
        private System.Windows.Forms.Label Labl_ShowCellValue;
        private System.Windows.Forms.Panel Pnl_FormHint;
    }
}
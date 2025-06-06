
namespace ImageProcessing.FF_Calculate
{
    partial class F_FF_Calculate
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
            this.Pnl_Picture = new System.Windows.Forms.Panel();
            this.PicBx_Picture = new System.Windows.Forms.PictureBox();
            this.Btn_LoadImage = new System.Windows.Forms.Button();
            this.Btn_Next = new System.Windows.Forms.Button();
            this.ToTip_Image = new System.Windows.Forms.ToolTip(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.Btn_ServeTest = new System.Windows.Forms.Button();
            this.Btn_ClientTest = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtBx_RltAngle = new System.Windows.Forms.TextBox();
            this.TxtBx_RltEyeSafe = new System.Windows.Forms.TextBox();
            this.TxtBx_RltValley = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtBx_RltTestTime = new System.Windows.Forms.TextBox();
            this.Pnl_Picture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBx_Picture)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pnl_Picture
            // 
            this.Pnl_Picture.Controls.Add(this.PicBx_Picture);
            this.Pnl_Picture.Location = new System.Drawing.Point(12, 12);
            this.Pnl_Picture.Name = "Pnl_Picture";
            this.Pnl_Picture.Size = new System.Drawing.Size(700, 525);
            this.Pnl_Picture.TabIndex = 4;
            // 
            // PicBx_Picture
            // 
            this.PicBx_Picture.BackColor = System.Drawing.Color.DimGray;
            this.PicBx_Picture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PicBx_Picture.Location = new System.Drawing.Point(0, 0);
            this.PicBx_Picture.Name = "PicBx_Picture";
            this.PicBx_Picture.Size = new System.Drawing.Size(700, 525);
            this.PicBx_Picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PicBx_Picture.TabIndex = 0;
            this.PicBx_Picture.TabStop = false;
            this.PicBx_Picture.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PicBx_Picture_MouseClick);
            // 
            // Btn_LoadImage
            // 
            this.Btn_LoadImage.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_LoadImage.Location = new System.Drawing.Point(728, 12);
            this.Btn_LoadImage.Name = "Btn_LoadImage";
            this.Btn_LoadImage.Size = new System.Drawing.Size(282, 58);
            this.Btn_LoadImage.TabIndex = 5;
            this.Btn_LoadImage.Text = "Load Image";
            this.Btn_LoadImage.UseVisualStyleBackColor = true;
            this.Btn_LoadImage.Click += new System.EventHandler(this.Btn_LoadImage_Click);
            // 
            // Btn_Next
            // 
            this.Btn_Next.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Next.Location = new System.Drawing.Point(728, 76);
            this.Btn_Next.Name = "Btn_Next";
            this.Btn_Next.Size = new System.Drawing.Size(282, 58);
            this.Btn_Next.TabIndex = 6;
            this.Btn_Next.Text = "Next";
            this.Btn_Next.UseVisualStyleBackColor = true;
            this.Btn_Next.Click += new System.EventHandler(this.Btn_Next_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(368, 509);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 43);
            this.button1.TabIndex = 7;
            this.button1.Text = "Test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Btn_ServeTest
            // 
            this.Btn_ServeTest.Location = new System.Drawing.Point(499, 502);
            this.Btn_ServeTest.Name = "Btn_ServeTest";
            this.Btn_ServeTest.Size = new System.Drawing.Size(125, 50);
            this.Btn_ServeTest.TabIndex = 8;
            this.Btn_ServeTest.Text = "Server Test";
            this.Btn_ServeTest.UseVisualStyleBackColor = true;
            this.Btn_ServeTest.Visible = false;
            this.Btn_ServeTest.Click += new System.EventHandler(this.Btn_ServeTest_Click);
            // 
            // Btn_ClientTest
            // 
            this.Btn_ClientTest.Location = new System.Drawing.Point(237, 502);
            this.Btn_ClientTest.Name = "Btn_ClientTest";
            this.Btn_ClientTest.Size = new System.Drawing.Size(125, 50);
            this.Btn_ClientTest.TabIndex = 9;
            this.Btn_ClientTest.Text = "Client Test";
            this.Btn_ClientTest.UseVisualStyleBackColor = true;
            this.Btn_ClientTest.Visible = false;
            this.Btn_ClientTest.Click += new System.EventHandler(this.Btn_ClientTest_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Silver;
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(728, 291);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(282, 246);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Result";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.TxtBx_RltAngle, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.TxtBx_RltEyeSafe, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.TxtBx_RltValley, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.TxtBx_RltTestTime, 1, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 28);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 11F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(270, 187);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(6, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 35);
            this.label2.TabIndex = 18;
            this.label2.Text = "Angle";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(6, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 35);
            this.label1.TabIndex = 17;
            this.label1.Text = "Eye Safe";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(6, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 35);
            this.label3.TabIndex = 19;
            this.label3.Text = "Valley";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TxtBx_RltAngle
            // 
            this.TxtBx_RltAngle.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TxtBx_RltAngle.Location = new System.Drawing.Point(139, 6);
            this.TxtBx_RltAngle.Name = "TxtBx_RltAngle";
            this.TxtBx_RltAngle.Size = new System.Drawing.Size(125, 29);
            this.TxtBx_RltAngle.TabIndex = 21;
            // 
            // TxtBx_RltEyeSafe
            // 
            this.TxtBx_RltEyeSafe.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TxtBx_RltEyeSafe.Location = new System.Drawing.Point(139, 44);
            this.TxtBx_RltEyeSafe.Name = "TxtBx_RltEyeSafe";
            this.TxtBx_RltEyeSafe.Size = new System.Drawing.Size(125, 29);
            this.TxtBx_RltEyeSafe.TabIndex = 22;
            // 
            // TxtBx_RltValley
            // 
            this.TxtBx_RltValley.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TxtBx_RltValley.Location = new System.Drawing.Point(139, 82);
            this.TxtBx_RltValley.Name = "TxtBx_RltValley";
            this.TxtBx_RltValley.Size = new System.Drawing.Size(125, 29);
            this.TxtBx_RltValley.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(6, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 34);
            this.label4.TabIndex = 23;
            this.label4.Text = "Test Time(ms)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TxtBx_RltTestTime
            // 
            this.TxtBx_RltTestTime.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TxtBx_RltTestTime.Location = new System.Drawing.Point(139, 120);
            this.TxtBx_RltTestTime.Name = "TxtBx_RltTestTime";
            this.TxtBx_RltTestTime.Size = new System.Drawing.Size(125, 29);
            this.TxtBx_RltTestTime.TabIndex = 24;
            // 
            // F_FF_Calculate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.ClientSize = new System.Drawing.Size(1022, 554);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Btn_ClientTest);
            this.Controls.Add(this.Btn_ServeTest);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Btn_Next);
            this.Controls.Add(this.Btn_LoadImage);
            this.Controls.Add(this.Pnl_Picture);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "F_FF_Calculate";
            this.Text = "F_FF_Calculate";
            this.Pnl_Picture.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PicBx_Picture)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Pnl_Picture;
        private System.Windows.Forms.PictureBox PicBx_Picture;
        private System.Windows.Forms.Button Btn_LoadImage;
        private System.Windows.Forms.Button Btn_Next;
        private System.Windows.Forms.ToolTip ToTip_Image;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Btn_ServeTest;
        private System.Windows.Forms.Button Btn_ClientTest;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtBx_RltAngle;
        private System.Windows.Forms.TextBox TxtBx_RltEyeSafe;
        private System.Windows.Forms.TextBox TxtBx_RltValley;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TxtBx_RltTestTime;
    }
}
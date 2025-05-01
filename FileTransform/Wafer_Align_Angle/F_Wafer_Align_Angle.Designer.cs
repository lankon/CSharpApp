
namespace FileTransform.Wafer_Align_Angle
{
    partial class F_Wafer_Align_Angle
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
            this.Btn_BatchCalculate = new System.Windows.Forms.Button();
            this.Labl_OCR_Result = new System.Windows.Forms.Label();
            this.Pnl_Picture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBx_Picture)).BeginInit();
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
            this.PicBx_Picture.BackColor = System.Drawing.Color.Black;
            this.PicBx_Picture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PicBx_Picture.Location = new System.Drawing.Point(0, 0);
            this.PicBx_Picture.Name = "PicBx_Picture";
            this.PicBx_Picture.Size = new System.Drawing.Size(700, 525);
            this.PicBx_Picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PicBx_Picture.TabIndex = 0;
            this.PicBx_Picture.TabStop = false;
            this.PicBx_Picture.Paint += new System.Windows.Forms.PaintEventHandler(this.PicBx_Picture_Paint);
            this.PicBx_Picture.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PicBx_Picture_MouseClick);
            this.PicBx_Picture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PicBx_Picture_MouseDown);
            this.PicBx_Picture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PicBx_Picture_MouseMove);
            this.PicBx_Picture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PicBx_Picture_MouseUp);
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
            this.button1.Location = new System.Drawing.Point(870, 438);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 43);
            this.button1.TabIndex = 7;
            this.button1.Text = "Test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Btn_ServeTest
            // 
            this.Btn_ServeTest.Location = new System.Drawing.Point(859, 487);
            this.Btn_ServeTest.Name = "Btn_ServeTest";
            this.Btn_ServeTest.Size = new System.Drawing.Size(125, 50);
            this.Btn_ServeTest.TabIndex = 8;
            this.Btn_ServeTest.Text = "Server Test";
            this.Btn_ServeTest.UseVisualStyleBackColor = true;
            this.Btn_ServeTest.Click += new System.EventHandler(this.Btn_ServeTest_Click);
            // 
            // Btn_ClientTest
            // 
            this.Btn_ClientTest.Location = new System.Drawing.Point(728, 487);
            this.Btn_ClientTest.Name = "Btn_ClientTest";
            this.Btn_ClientTest.Size = new System.Drawing.Size(125, 50);
            this.Btn_ClientTest.TabIndex = 9;
            this.Btn_ClientTest.Text = "Client Test";
            this.Btn_ClientTest.UseVisualStyleBackColor = true;
            this.Btn_ClientTest.Click += new System.EventHandler(this.Btn_ClientTest_Click);
            // 
            // Btn_BatchCalculate
            // 
            this.Btn_BatchCalculate.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_BatchCalculate.Location = new System.Drawing.Point(728, 140);
            this.Btn_BatchCalculate.Name = "Btn_BatchCalculate";
            this.Btn_BatchCalculate.Size = new System.Drawing.Size(282, 71);
            this.Btn_BatchCalculate.TabIndex = 10;
            this.Btn_BatchCalculate.Text = "Batch Calculate";
            this.Btn_BatchCalculate.UseVisualStyleBackColor = true;
            this.Btn_BatchCalculate.Click += new System.EventHandler(this.Btn_BatchCalculate_Click);
            // 
            // Labl_OCR_Result
            // 
            this.Labl_OCR_Result.BackColor = System.Drawing.Color.Yellow;
            this.Labl_OCR_Result.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Labl_OCR_Result.Location = new System.Drawing.Point(724, 283);
            this.Labl_OCR_Result.Name = "Labl_OCR_Result";
            this.Labl_OCR_Result.Size = new System.Drawing.Size(286, 56);
            this.Labl_OCR_Result.TabIndex = 11;
            this.Labl_OCR_Result.Text = "OCR Result";
            this.Labl_OCR_Result.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // F_Wafer_Align_Angle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.ClientSize = new System.Drawing.Size(1022, 554);
            this.Controls.Add(this.Labl_OCR_Result);
            this.Controls.Add(this.Btn_BatchCalculate);
            this.Controls.Add(this.Btn_ClientTest);
            this.Controls.Add(this.Btn_ServeTest);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Btn_Next);
            this.Controls.Add(this.Btn_LoadImage);
            this.Controls.Add(this.Pnl_Picture);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "F_Wafer_Align_Angle";
            this.Text = "F_Wafer_Align_Angle";
            this.Pnl_Picture.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PicBx_Picture)).EndInit();
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
        private System.Windows.Forms.Button Btn_BatchCalculate;
        private System.Windows.Forms.Label Labl_OCR_Result;
    }
}
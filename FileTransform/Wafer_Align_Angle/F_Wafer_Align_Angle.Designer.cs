﻿
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
            this.button1.Location = new System.Drawing.Point(728, 297);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 43);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // F_Wafer_Align_Angle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.ClientSize = new System.Drawing.Size(1022, 554);
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
    }
}
﻿
namespace FileTransform
{
    partial class F_Recursion_ButtonGroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_Recursion_ButtonGroup));
            this.Btn_Setting = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // Btn_Setting
            // 
            this.Btn_Setting.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_Setting.BackgroundImage")));
            this.Btn_Setting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_Setting.Location = new System.Drawing.Point(817, 5);
            this.Btn_Setting.Name = "Btn_Setting";
            this.Btn_Setting.Size = new System.Drawing.Size(60, 60);
            this.Btn_Setting.TabIndex = 31;
            this.Btn_Setting.UseVisualStyleBackColor = true;
            this.Btn_Setting.Click += new System.EventHandler(this.Btn_Setting_Click);
            // 
            // F_Recursion_ButtonGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.ClientSize = new System.Drawing.Size(883, 68);
            this.Controls.Add(this.Btn_Setting);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "F_Recursion_ButtonGroup";
            this.Text = "F_Recursion_ButtonGroup";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Btn_Setting;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
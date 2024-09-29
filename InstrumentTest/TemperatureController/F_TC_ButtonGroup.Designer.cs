
namespace InstrumentTest
{
    partial class F_TC_ButtonGroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_TC_ButtonGroup));
            this.Btn_Setting = new System.Windows.Forms.Button();
            this.Btn_Back = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Btn_Setting_TPT8000 = new System.Windows.Forms.Button();
            this.Btn_Show_PV = new System.Windows.Forms.Button();
            this.Pnl_FormHint = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // Btn_Setting
            // 
            this.Btn_Setting.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_Setting.BackgroundImage")));
            this.Btn_Setting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_Setting.Location = new System.Drawing.Point(817, 5);
            this.Btn_Setting.Name = "Btn_Setting";
            this.Btn_Setting.Size = new System.Drawing.Size(60, 60);
            this.Btn_Setting.TabIndex = 29;
            this.Btn_Setting.UseVisualStyleBackColor = true;
            this.Btn_Setting.Click += new System.EventHandler(this.Btn_Setting_Click);
            // 
            // Btn_Back
            // 
            this.Btn_Back.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_Back.BackgroundImage")));
            this.Btn_Back.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_Back.Location = new System.Drawing.Point(4, 5);
            this.Btn_Back.Name = "Btn_Back";
            this.Btn_Back.Size = new System.Drawing.Size(60, 60);
            this.Btn_Back.TabIndex = 30;
            this.Btn_Back.UseVisualStyleBackColor = true;
            this.Btn_Back.Click += new System.EventHandler(this.Btn_Back_Click);
            // 
            // Btn_Setting_TPT8000
            // 
            this.Btn_Setting_TPT8000.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_Setting_TPT8000.BackgroundImage")));
            this.Btn_Setting_TPT8000.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_Setting_TPT8000.Location = new System.Drawing.Point(751, 5);
            this.Btn_Setting_TPT8000.Name = "Btn_Setting_TPT8000";
            this.Btn_Setting_TPT8000.Size = new System.Drawing.Size(60, 60);
            this.Btn_Setting_TPT8000.TabIndex = 31;
            this.Btn_Setting_TPT8000.UseVisualStyleBackColor = true;
            this.Btn_Setting_TPT8000.Click += new System.EventHandler(this.Btn_Setting_TPT8000_Click);
            // 
            // Btn_Show_PV
            // 
            this.Btn_Show_PV.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_Show_PV.BackgroundImage")));
            this.Btn_Show_PV.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_Show_PV.Location = new System.Drawing.Point(70, 5);
            this.Btn_Show_PV.Name = "Btn_Show_PV";
            this.Btn_Show_PV.Size = new System.Drawing.Size(60, 60);
            this.Btn_Show_PV.TabIndex = 32;
            this.Btn_Show_PV.UseVisualStyleBackColor = true;
            this.Btn_Show_PV.Click += new System.EventHandler(this.Btn_Show_PV_Click);
            // 
            // Pnl_FormHint
            // 
            this.Pnl_FormHint.Location = new System.Drawing.Point(832, -2);
            this.Pnl_FormHint.Name = "Pnl_FormHint";
            this.Pnl_FormHint.Size = new System.Drawing.Size(51, 70);
            this.Pnl_FormHint.TabIndex = 40;
            // 
            // F_TC_ButtonGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.ClientSize = new System.Drawing.Size(883, 68);
            this.Controls.Add(this.Btn_Show_PV);
            this.Controls.Add(this.Btn_Setting_TPT8000);
            this.Controls.Add(this.Btn_Back);
            this.Controls.Add(this.Btn_Setting);
            this.Controls.Add(this.Pnl_FormHint);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "F_TC_ButtonGroup";
            this.Text = "F_ButtonGroup";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Btn_Setting;
        private System.Windows.Forms.Button Btn_Back;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button Btn_Setting_TPT8000;
        private System.Windows.Forms.Button Btn_Show_PV;
        private System.Windows.Forms.Panel Pnl_FormHint;
    }
}
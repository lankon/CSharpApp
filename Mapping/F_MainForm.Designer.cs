
namespace Mapping
{
    partial class F_MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_MainForm));
            this.Pnl_Function = new System.Windows.Forms.Panel();
            this.Btn_OneToOne = new System.Windows.Forms.Button();
            this.Btn_Save = new System.Windows.Forms.Button();
            this.Btn_Home = new System.Windows.Forms.Button();
            this.Btn_Setting = new System.Windows.Forms.Button();
            this.Btn_CloseApp = new System.Windows.Forms.Button();
            this.Pnl_Group = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Pnl_Function.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pnl_Function
            // 
            this.Pnl_Function.Controls.Add(this.Btn_OneToOne);
            this.Pnl_Function.Controls.Add(this.Btn_Save);
            this.Pnl_Function.Controls.Add(this.Btn_Home);
            this.Pnl_Function.Controls.Add(this.Btn_Setting);
            this.Pnl_Function.Controls.Add(this.Btn_CloseApp);
            this.Pnl_Function.Location = new System.Drawing.Point(13, 3);
            this.Pnl_Function.Name = "Pnl_Function";
            this.Pnl_Function.Size = new System.Drawing.Size(1021, 68);
            this.Pnl_Function.TabIndex = 27;
            this.Pnl_Function.Paint += new System.Windows.Forms.PaintEventHandler(this.Pnl_Function_Paint);
            // 
            // Btn_OneToOne
            // 
            this.Btn_OneToOne.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_OneToOne.BackgroundImage")));
            this.Btn_OneToOne.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_OneToOne.Location = new System.Drawing.Point(135, 5);
            this.Btn_OneToOne.Name = "Btn_OneToOne";
            this.Btn_OneToOne.Size = new System.Drawing.Size(60, 60);
            this.Btn_OneToOne.TabIndex = 32;
            this.Btn_OneToOne.UseVisualStyleBackColor = true;
            this.Btn_OneToOne.Click += new System.EventHandler(this.Btn_OneToOne_Click);
            // 
            // Btn_Save
            // 
            this.Btn_Save.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_Save.BackgroundImage")));
            this.Btn_Save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_Save.Location = new System.Drawing.Point(69, 5);
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.Size = new System.Drawing.Size(60, 60);
            this.Btn_Save.TabIndex = 31;
            this.Btn_Save.UseVisualStyleBackColor = true;
            this.Btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // Btn_Home
            // 
            this.Btn_Home.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_Home.BackgroundImage")));
            this.Btn_Home.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_Home.Location = new System.Drawing.Point(3, 5);
            this.Btn_Home.Name = "Btn_Home";
            this.Btn_Home.Size = new System.Drawing.Size(60, 60);
            this.Btn_Home.TabIndex = 29;
            this.Btn_Home.UseVisualStyleBackColor = true;
            this.Btn_Home.Click += new System.EventHandler(this.Btn_Home_Click);
            // 
            // Btn_Setting
            // 
            this.Btn_Setting.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_Setting.BackgroundImage")));
            this.Btn_Setting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_Setting.Location = new System.Drawing.Point(892, 5);
            this.Btn_Setting.Name = "Btn_Setting";
            this.Btn_Setting.Size = new System.Drawing.Size(60, 60);
            this.Btn_Setting.TabIndex = 28;
            this.Btn_Setting.UseVisualStyleBackColor = true;
            this.Btn_Setting.Click += new System.EventHandler(this.Btn_Setting_Click);
            // 
            // Btn_CloseApp
            // 
            this.Btn_CloseApp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_CloseApp.BackgroundImage")));
            this.Btn_CloseApp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_CloseApp.Location = new System.Drawing.Point(958, 5);
            this.Btn_CloseApp.Name = "Btn_CloseApp";
            this.Btn_CloseApp.Size = new System.Drawing.Size(60, 60);
            this.Btn_CloseApp.TabIndex = 27;
            this.Btn_CloseApp.UseVisualStyleBackColor = true;
            this.Btn_CloseApp.Click += new System.EventHandler(this.Btn_CloseApp_Click);
            // 
            // Pnl_Group
            // 
            this.Pnl_Group.Location = new System.Drawing.Point(13, 77);
            this.Pnl_Group.Name = "Pnl_Group";
            this.Pnl_Group.Size = new System.Drawing.Size(1022, 554);
            this.Pnl_Group.TabIndex = 28;
            // 
            // F_MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.ClientSize = new System.Drawing.Size(1046, 641);
            this.Controls.Add(this.Pnl_Group);
            this.Controls.Add(this.Pnl_Function);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "F_MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "F_MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.F_MainForm_FormClosed);
            this.Pnl_Function.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Pnl_Function;
        private System.Windows.Forms.Button Btn_Setting;
        private System.Windows.Forms.Button Btn_CloseApp;
        private System.Windows.Forms.Panel Pnl_Group;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button Btn_Home;
        private System.Windows.Forms.Button Btn_Save;
        private System.Windows.Forms.Button Btn_OneToOne;
    }
}
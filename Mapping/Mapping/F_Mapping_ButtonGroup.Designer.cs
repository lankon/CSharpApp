
namespace Mapping
{
    partial class F_Mapping_ButtonGroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_Mapping_ButtonGroup));
            this.Btn_BinTable = new System.Windows.Forms.Button();
            this.Btn_OneToOne = new System.Windows.Forms.Button();
            this.Btn_Save = new System.Windows.Forms.Button();
            this.Btn_Setting = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Pnl_FormHint = new System.Windows.Forms.Panel();
            this.Btn_BinMapping = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_BinTable
            // 
            this.Btn_BinTable.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_BinTable.Location = new System.Drawing.Point(267, 5);
            this.Btn_BinTable.Name = "Btn_BinTable";
            this.Btn_BinTable.Size = new System.Drawing.Size(60, 60);
            this.Btn_BinTable.TabIndex = 37;
            this.Btn_BinTable.UseVisualStyleBackColor = true;
            this.Btn_BinTable.Visible = false;
            // 
            // Btn_OneToOne
            // 
            this.Btn_OneToOne.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_OneToOne.BackgroundImage")));
            this.Btn_OneToOne.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_OneToOne.Location = new System.Drawing.Point(65, 5);
            this.Btn_OneToOne.Name = "Btn_OneToOne";
            this.Btn_OneToOne.Size = new System.Drawing.Size(60, 60);
            this.Btn_OneToOne.TabIndex = 36;
            this.Btn_OneToOne.UseVisualStyleBackColor = true;
            this.Btn_OneToOne.Click += new System.EventHandler(this.Btn_OneToOne_Click);
            // 
            // Btn_Save
            // 
            this.Btn_Save.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_Save.BackgroundImage")));
            this.Btn_Save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_Save.Location = new System.Drawing.Point(0, 5);
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.Size = new System.Drawing.Size(60, 60);
            this.Btn_Save.TabIndex = 35;
            this.Btn_Save.UseVisualStyleBackColor = true;
            this.Btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // Btn_Setting
            // 
            this.Btn_Setting.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_Setting.BackgroundImage")));
            this.Btn_Setting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_Setting.Location = new System.Drawing.Point(817, 5);
            this.Btn_Setting.Name = "Btn_Setting";
            this.Btn_Setting.Size = new System.Drawing.Size(60, 60);
            this.Btn_Setting.TabIndex = 34;
            this.Btn_Setting.UseVisualStyleBackColor = true;
            this.Btn_Setting.Click += new System.EventHandler(this.Btn_Setting_Click);
            // 
            // Pnl_FormHint
            // 
            this.Pnl_FormHint.Location = new System.Drawing.Point(832, 0);
            this.Pnl_FormHint.Name = "Pnl_FormHint";
            this.Pnl_FormHint.Size = new System.Drawing.Size(51, 70);
            this.Pnl_FormHint.TabIndex = 39;
            // 
            // Btn_BinMapping
            // 
            this.Btn_BinMapping.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_BinMapping.BackgroundImage")));
            this.Btn_BinMapping.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_BinMapping.Location = new System.Drawing.Point(131, 5);
            this.Btn_BinMapping.Name = "Btn_BinMapping";
            this.Btn_BinMapping.Size = new System.Drawing.Size(60, 60);
            this.Btn_BinMapping.TabIndex = 40;
            this.Btn_BinMapping.UseVisualStyleBackColor = true;
            this.Btn_BinMapping.Click += new System.EventHandler(this.Btn_BinMapping_Click);
            // 
            // F_Mapping_ButtonGroup
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.ClientSize = new System.Drawing.Size(883, 68);
            this.Controls.Add(this.Btn_BinMapping);
            this.Controls.Add(this.Btn_BinTable);
            this.Controls.Add(this.Btn_OneToOne);
            this.Controls.Add(this.Btn_Save);
            this.Controls.Add(this.Btn_Setting);
            this.Controls.Add(this.Pnl_FormHint);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "F_Mapping_ButtonGroup";
            this.Text = "F_Mapping_ButtonGroup";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Btn_BinTable;
        private System.Windows.Forms.Button Btn_OneToOne;
        private System.Windows.Forms.Button Btn_Save;
        private System.Windows.Forms.Button Btn_Setting;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel Pnl_FormHint;
        private System.Windows.Forms.Button Btn_BinMapping;
    }
}

namespace CSharpApp
{
    partial class F_TransformFile
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
            this.Btn_Start = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TabCtrl_Function = new System.Windows.Forms.TabControl();
            this.TabPg_Final = new System.Windows.Forms.TabPage();
            this.TxtBx_NewName = new System.Windows.Forms.TextBox();
            this.TxtBx_OldName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Btn_Preview = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.GpBx_Function = new System.Windows.Forms.GroupBox();
            this.LstBx_Function = new System.Windows.Forms.ListBox();
            this.Btn_LoadFile = new System.Windows.Forms.Button();
            this.TxtBx_FilePath = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.TabCtrl_Function.SuspendLayout();
            this.TabPg_Final.SuspendLayout();
            this.GpBx_Function.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn_Start
            // 
            this.Btn_Start.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Start.Location = new System.Drawing.Point(62, 51);
            this.Btn_Start.Name = "Btn_Start";
            this.Btn_Start.Size = new System.Drawing.Size(102, 38);
            this.Btn_Start.TabIndex = 2;
            this.Btn_Start.Text = "Start";
            this.Btn_Start.UseVisualStyleBackColor = true;
            this.Btn_Start.Click += new System.EventHandler(this.Btn_Start_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TabCtrl_Function);
            this.groupBox1.Controls.Add(this.Btn_Preview);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.GpBx_Function);
            this.groupBox1.Controls.Add(this.Btn_LoadFile);
            this.groupBox1.Controls.Add(this.TxtBx_FilePath);
            this.groupBox1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(42, 114);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(617, 256);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File Name Modify";
            // 
            // TabCtrl_Function
            // 
            this.TabCtrl_Function.Controls.Add(this.TabPg_Final);
            this.TabCtrl_Function.Location = new System.Drawing.Point(6, 77);
            this.TabCtrl_Function.Name = "TabCtrl_Function";
            this.TabCtrl_Function.SelectedIndex = 0;
            this.TabCtrl_Function.Size = new System.Drawing.Size(318, 130);
            this.TabCtrl_Function.TabIndex = 17;
            // 
            // TabPg_Final
            // 
            this.TabPg_Final.Controls.Add(this.TxtBx_NewName);
            this.TabPg_Final.Controls.Add(this.TxtBx_OldName);
            this.TabPg_Final.Controls.Add(this.label4);
            this.TabPg_Final.Controls.Add(this.label3);
            this.TabPg_Final.Location = new System.Drawing.Point(4, 29);
            this.TabPg_Final.Name = "TabPg_Final";
            this.TabPg_Final.Size = new System.Drawing.Size(310, 97);
            this.TabPg_Final.TabIndex = 0;
            this.TabPg_Final.Text = "Final";
            this.TabPg_Final.UseVisualStyleBackColor = true;
            // 
            // TxtBx_NewName
            // 
            this.TxtBx_NewName.Location = new System.Drawing.Point(132, 59);
            this.TxtBx_NewName.Name = "TxtBx_NewName";
            this.TxtBx_NewName.Size = new System.Drawing.Size(152, 29);
            this.TxtBx_NewName.TabIndex = 16;
            // 
            // TxtBx_OldName
            // 
            this.TxtBx_OldName.Location = new System.Drawing.Point(132, 17);
            this.TxtBx_OldName.Name = "TxtBx_OldName";
            this.TxtBx_OldName.Size = new System.Drawing.Size(152, 29);
            this.TxtBx_OldName.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AllowDrop = true;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 17);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 20);
            this.label4.TabIndex = 14;
            this.label4.Text = "Old Name";
            // 
            // label3
            // 
            this.label3.AllowDrop = true;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 59);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 20);
            this.label3.TabIndex = 13;
            this.label3.Text = "New Name";
            // 
            // Btn_Preview
            // 
            this.Btn_Preview.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Preview.Location = new System.Drawing.Point(411, 214);
            this.Btn_Preview.Name = "Btn_Preview";
            this.Btn_Preview.Size = new System.Drawing.Size(93, 30);
            this.Btn_Preview.TabIndex = 14;
            this.Btn_Preview.Text = "Preview";
            this.Btn_Preview.UseVisualStyleBackColor = true;
            this.Btn_Preview.Click += new System.EventHandler(this.Btn_Preview_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button2.Location = new System.Drawing.Point(513, 214);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(93, 30);
            this.button2.TabIndex = 13;
            this.button2.Text = "Modify";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // GpBx_Function
            // 
            this.GpBx_Function.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.GpBx_Function.Controls.Add(this.LstBx_Function);
            this.GpBx_Function.Location = new System.Drawing.Point(330, 77);
            this.GpBx_Function.Name = "GpBx_Function";
            this.GpBx_Function.Size = new System.Drawing.Size(276, 131);
            this.GpBx_Function.TabIndex = 10;
            this.GpBx_Function.TabStop = false;
            this.GpBx_Function.Text = "Function";
            // 
            // LstBx_Function
            // 
            this.LstBx_Function.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LstBx_Function.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.LstBx_Function.FormattingEnabled = true;
            this.LstBx_Function.ItemHeight = 20;
            this.LstBx_Function.Items.AddRange(new object[] {
            "Coordinate Mirror XY",
            "Coordinate Transform",
            "Insert Text to File Name",
            "Delete Last Condition",
            "Clear All Condition"});
            this.LstBx_Function.Location = new System.Drawing.Point(6, 28);
            this.LstBx_Function.Name = "LstBx_Function";
            this.LstBx_Function.ScrollAlwaysVisible = true;
            this.LstBx_Function.Size = new System.Drawing.Size(274, 102);
            this.LstBx_Function.TabIndex = 1;
            this.LstBx_Function.Click += new System.EventHandler(this.LstBx_Function_Click);
            this.LstBx_Function.SelectedIndexChanged += new System.EventHandler(this.LstBx_Function_SelectedIndexChanged);
            this.LstBx_Function.DoubleClick += new System.EventHandler(this.LstBx_Function_DoubleClick);
            // 
            // Btn_LoadFile
            // 
            this.Btn_LoadFile.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_LoadFile.Location = new System.Drawing.Point(6, 41);
            this.Btn_LoadFile.Name = "Btn_LoadFile";
            this.Btn_LoadFile.Size = new System.Drawing.Size(93, 30);
            this.Btn_LoadFile.TabIndex = 9;
            this.Btn_LoadFile.Text = "Browse";
            this.Btn_LoadFile.UseVisualStyleBackColor = true;
            this.Btn_LoadFile.Click += new System.EventHandler(this.Btn_LoadFile_Click);
            // 
            // TxtBx_FilePath
            // 
            this.TxtBx_FilePath.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TxtBx_FilePath.Location = new System.Drawing.Point(105, 41);
            this.TxtBx_FilePath.Name = "TxtBx_FilePath";
            this.TxtBx_FilePath.Size = new System.Drawing.Size(501, 29);
            this.TxtBx_FilePath.TabIndex = 8;
            // 
            // F_TransformFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Btn_Start);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "F_TransformFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TransformFile";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.TabCtrl_Function.ResumeLayout(false);
            this.TabPg_Final.ResumeLayout(false);
            this.TabPg_Final.PerformLayout();
            this.GpBx_Function.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button Btn_Start;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox TxtBx_FilePath;
        private System.Windows.Forms.Button Btn_LoadFile;
        private System.Windows.Forms.GroupBox GpBx_Function;
        private System.Windows.Forms.ListBox LstBx_Function;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button Btn_Preview;
        private System.Windows.Forms.TabControl TabCtrl_Function;
        private System.Windows.Forms.TabPage TabPg_Final;
        private System.Windows.Forms.TextBox TxtBx_NewName;
        private System.Windows.Forms.TextBox TxtBx_OldName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}
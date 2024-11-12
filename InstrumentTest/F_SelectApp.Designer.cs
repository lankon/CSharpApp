
namespace InstrumentTest
{
    partial class F_SelectApp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_SelectApp));
            this.label3 = new System.Windows.Forms.Label();
            this.Btn_TemperatureControl = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Btn_DeltaLoadCell = new System.Windows.Forms.Button();
            this.Btn_Communication = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(118, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 28);
            this.label3.TabIndex = 12;
            this.label3.Text = "TC";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Btn_TemperatureControl
            // 
            this.Btn_TemperatureControl.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_TemperatureControl.BackgroundImage")));
            this.Btn_TemperatureControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_TemperatureControl.Location = new System.Drawing.Point(132, 12);
            this.Btn_TemperatureControl.Name = "Btn_TemperatureControl";
            this.Btn_TemperatureControl.Size = new System.Drawing.Size(60, 60);
            this.Btn_TemperatureControl.TabIndex = 11;
            this.Btn_TemperatureControl.UseVisualStyleBackColor = true;
            this.Btn_TemperatureControl.Click += new System.EventHandler(this.Btn_TemperatureControl_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(12, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 28);
            this.label1.TabIndex = 10;
            this.label1.Text = "LoadCell";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Btn_DeltaLoadCell
            // 
            this.Btn_DeltaLoadCell.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_DeltaLoadCell.BackgroundImage")));
            this.Btn_DeltaLoadCell.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_DeltaLoadCell.Location = new System.Drawing.Point(23, 12);
            this.Btn_DeltaLoadCell.Name = "Btn_DeltaLoadCell";
            this.Btn_DeltaLoadCell.Size = new System.Drawing.Size(60, 60);
            this.Btn_DeltaLoadCell.TabIndex = 9;
            this.Btn_DeltaLoadCell.UseVisualStyleBackColor = true;
            this.Btn_DeltaLoadCell.Click += new System.EventHandler(this.Btn_DeltaLoadCell_Click);
            // 
            // Btn_Communication
            // 
            this.Btn_Communication.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_Communication.BackgroundImage")));
            this.Btn_Communication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_Communication.Location = new System.Drawing.Point(239, 12);
            this.Btn_Communication.Name = "Btn_Communication";
            this.Btn_Communication.Size = new System.Drawing.Size(60, 60);
            this.Btn_Communication.TabIndex = 13;
            this.Btn_Communication.UseVisualStyleBackColor = true;
            this.Btn_Communication.Click += new System.EventHandler(this.Btn_Communication_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(225, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 28);
            this.label2.TabIndex = 14;
            this.label2.Text = "Comms.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // F_SelectApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.ClientSize = new System.Drawing.Size(1022, 554);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Btn_Communication);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Btn_TemperatureControl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Btn_DeltaLoadCell);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "F_SelectApp";
            this.Text = "F_SelectApp";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Btn_TemperatureControl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Btn_DeltaLoadCell;
        private System.Windows.Forms.Button Btn_Communication;
        private System.Windows.Forms.Label label2;
    }
}

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
            this.label2 = new System.Windows.Forms.Label();
            this.Btn_PISODIO = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Btn_DeltaLoadCell = new System.Windows.Forms.Button();
            this.Btn_TemperatureController = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(114, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 28);
            this.label2.TabIndex = 10;
            this.label2.Text = "P32C32";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Btn_PISODIO
            // 
            this.Btn_PISODIO.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_PISODIO.BackgroundImage")));
            this.Btn_PISODIO.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_PISODIO.Location = new System.Drawing.Point(127, 12);
            this.Btn_PISODIO.Name = "Btn_PISODIO";
            this.Btn_PISODIO.Size = new System.Drawing.Size(60, 60);
            this.Btn_PISODIO.TabIndex = 9;
            this.Btn_PISODIO.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(11, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 28);
            this.label1.TabIndex = 8;
            this.label1.Text = "LoadCell";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Btn_DeltaLoadCell
            // 
            this.Btn_DeltaLoadCell.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_DeltaLoadCell.BackgroundImage")));
            this.Btn_DeltaLoadCell.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_DeltaLoadCell.Location = new System.Drawing.Point(24, 12);
            this.Btn_DeltaLoadCell.Name = "Btn_DeltaLoadCell";
            this.Btn_DeltaLoadCell.Size = new System.Drawing.Size(60, 60);
            this.Btn_DeltaLoadCell.TabIndex = 7;
            this.Btn_DeltaLoadCell.UseVisualStyleBackColor = true;
            this.Btn_DeltaLoadCell.Click += new System.EventHandler(this.Btn_DeltaLoadCell_Click);
            // 
            // Btn_TemperatureController
            // 
            this.Btn_TemperatureController.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_TemperatureController.BackgroundImage")));
            this.Btn_TemperatureController.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_TemperatureController.Location = new System.Drawing.Point(226, 12);
            this.Btn_TemperatureController.Name = "Btn_TemperatureController";
            this.Btn_TemperatureController.Size = new System.Drawing.Size(60, 60);
            this.Btn_TemperatureController.TabIndex = 11;
            this.Btn_TemperatureController.UseVisualStyleBackColor = true;
            this.Btn_TemperatureController.Click += new System.EventHandler(this.Btn_TemperatureController_Click);
            // 
            // F_SelectApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.ClientSize = new System.Drawing.Size(1022, 554);
            this.Controls.Add(this.Btn_TemperatureController);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Btn_PISODIO);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Btn_DeltaLoadCell);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "F_SelectApp";
            this.Text = "F_SelectApp";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Btn_PISODIO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Btn_DeltaLoadCell;
        private System.Windows.Forms.Button Btn_TemperatureController;
    }
}

namespace LFSMEO.UI
{
    partial class F_OEM_Setting
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
            this.Btn_IO_Form = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Btn_IO_Form
            // 
            this.Btn_IO_Form.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_IO_Form.Location = new System.Drawing.Point(44, 12);
            this.Btn_IO_Form.Name = "Btn_IO_Form";
            this.Btn_IO_Form.Size = new System.Drawing.Size(60, 60);
            this.Btn_IO_Form.TabIndex = 32;
            this.Btn_IO_Form.UseVisualStyleBackColor = true;
            this.Btn_IO_Form.Click += new System.EventHandler(this.Btn_IO_Form_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(44, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 23);
            this.label1.TabIndex = 33;
            this.label1.Text = "IO";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // F_OEM_Setting
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.ClientSize = new System.Drawing.Size(1326, 661);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Btn_IO_Form);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "F_OEM_Setting";
            this.Text = "F_Template";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Btn_IO_Form;
        private System.Windows.Forms.Label label1;
    }
}
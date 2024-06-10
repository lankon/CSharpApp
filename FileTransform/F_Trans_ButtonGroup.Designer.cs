
namespace FileTransform
{
    partial class F_Trans_ButtonGroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_Trans_ButtonGroup));
            this.Btn_Transform = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_Transform
            // 
            this.Btn_Transform.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_Transform.BackgroundImage")));
            this.Btn_Transform.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_Transform.Location = new System.Drawing.Point(4, 5);
            this.Btn_Transform.Name = "Btn_Transform";
            this.Btn_Transform.Size = new System.Drawing.Size(60, 60);
            this.Btn_Transform.TabIndex = 17;
            this.Btn_Transform.UseVisualStyleBackColor = true;
            // 
            // F_Trans_ButtonGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.ClientSize = new System.Drawing.Size(883, 68);
            this.Controls.Add(this.Btn_Transform);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "F_Trans_ButtonGroup";
            this.Text = "F_Trans_ButtonGroup";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Btn_Transform;
    }
}
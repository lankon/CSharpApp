
namespace CSharpApp
{
    partial class F_Coordinate_MirrorXY
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
            this.CkBx_MirrorX = new System.Windows.Forms.CheckBox();
            this.CkBx_MirrorY = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // CkBx_MirrorX
            // 
            this.CkBx_MirrorX.AutoSize = true;
            this.CkBx_MirrorX.Location = new System.Drawing.Point(12, 12);
            this.CkBx_MirrorX.Name = "CkBx_MirrorX";
            this.CkBx_MirrorX.Size = new System.Drawing.Size(89, 24);
            this.CkBx_MirrorX.TabIndex = 0;
            this.CkBx_MirrorX.Text = "Mirror X";
            this.CkBx_MirrorX.UseVisualStyleBackColor = true;
            // 
            // CkBx_MirrorY
            // 
            this.CkBx_MirrorY.AutoSize = true;
            this.CkBx_MirrorY.Location = new System.Drawing.Point(12, 51);
            this.CkBx_MirrorY.Name = "CkBx_MirrorY";
            this.CkBx_MirrorY.Size = new System.Drawing.Size(89, 24);
            this.CkBx_MirrorY.TabIndex = 1;
            this.CkBx_MirrorY.Text = "Mirror Y";
            this.CkBx_MirrorY.UseVisualStyleBackColor = true;
            // 
            // F_Coordinate_MirrorXY
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 98);
            this.Controls.Add(this.CkBx_MirrorY);
            this.Controls.Add(this.CkBx_MirrorX);
            this.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "F_Coordinate_MirrorXY";
            this.Text = "F_Coordinate_MirrorXY";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox CkBx_MirrorX;
        private System.Windows.Forms.CheckBox CkBx_MirrorY;
    }
}
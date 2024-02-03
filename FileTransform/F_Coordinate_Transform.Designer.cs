
namespace FileTransform
{
    partial class F_Coordinate_Transform
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
            this.TxtBx_ShiftY = new System.Windows.Forms.TextBox();
            this.TxtBx_ShiftX = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TxtBx_ShiftY
            // 
            this.TxtBx_ShiftY.Location = new System.Drawing.Point(81, 54);
            this.TxtBx_ShiftY.Margin = new System.Windows.Forms.Padding(5);
            this.TxtBx_ShiftY.Name = "TxtBx_ShiftY";
            this.TxtBx_ShiftY.Size = new System.Drawing.Size(191, 29);
            this.TxtBx_ShiftY.TabIndex = 7;
            // 
            // TxtBx_ShiftX
            // 
            this.TxtBx_ShiftX.Location = new System.Drawing.Point(81, 15);
            this.TxtBx_ShiftX.Margin = new System.Windows.Forms.Padding(5);
            this.TxtBx_ShiftX.Name = "TxtBx_ShiftX";
            this.TxtBx_ShiftX.Size = new System.Drawing.Size(191, 29);
            this.TxtBx_ShiftX.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(14, 57);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Shift Y";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(14, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Shift X";
            // 
            // F_Coordinate_Transform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 98);
            this.Controls.Add(this.TxtBx_ShiftY);
            this.Controls.Add(this.TxtBx_ShiftX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "F_Coordinate_Transform";
            this.Text = "F_Coordinate_Transform";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtBx_ShiftY;
        private System.Windows.Forms.TextBox TxtBx_ShiftX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

namespace CSharpApp
{
    partial class UserCtrl_Coordinate_Transform
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtBx_ShiftX = new System.Windows.Forms.TextBox();
            this.TxtBx_ShiftY = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(14, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Shift X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(14, 47);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Shift Y";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // TxtBx_ShiftX
            // 
            this.TxtBx_ShiftX.Location = new System.Drawing.Point(81, 5);
            this.TxtBx_ShiftX.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.TxtBx_ShiftX.Name = "TxtBx_ShiftX";
            this.TxtBx_ShiftX.Size = new System.Drawing.Size(191, 29);
            this.TxtBx_ShiftX.TabIndex = 2;
            // 
            // TxtBx_ShiftY
            // 
            this.TxtBx_ShiftY.Location = new System.Drawing.Point(81, 44);
            this.TxtBx_ShiftY.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.TxtBx_ShiftY.Name = "TxtBx_ShiftY";
            this.TxtBx_ShiftY.Size = new System.Drawing.Size(191, 29);
            this.TxtBx_ShiftY.TabIndex = 3;
            // 
            // UserCtrl_Coordinate_Transform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TxtBx_ShiftY);
            this.Controls.Add(this.TxtBx_ShiftX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "UserCtrl_Coordinate_Transform";
            this.Size = new System.Drawing.Size(310, 98);
            this.Load += new System.EventHandler(this.UserCtrl_Coordinate_Transform_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtBx_ShiftX;
        private System.Windows.Forms.TextBox TxtBx_ShiftY;
    }
}

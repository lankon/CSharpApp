
namespace CSharpApp
{
    partial class F_LogIn
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

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_LogIn));
            this.label1 = new System.Windows.Forms.Label();
            this.TxtBx_Account = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtBx_Password = new System.Windows.Forms.TextBox();
            this.Btn_LogIn = new System.Windows.Forms.Button();
            this.Labl_CreateAccount = new System.Windows.Forms.Label();
            this.Cmbx_LogIn = new System.Windows.Forms.CheckBox();
            this.PBx_ViewPassword = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PBx_ViewPassword)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Account";
            // 
            // TxtBx_Account
            // 
            this.TxtBx_Account.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBx_Account.Location = new System.Drawing.Point(13, 53);
            this.TxtBx_Account.Margin = new System.Windows.Forms.Padding(4);
            this.TxtBx_Account.Name = "TxtBx_Account";
            this.TxtBx_Account.Size = new System.Drawing.Size(295, 22);
            this.TxtBx_Account.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 85);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password";
            // 
            // TxtBx_Password
            // 
            this.TxtBx_Password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBx_Password.Location = new System.Drawing.Point(13, 111);
            this.TxtBx_Password.Margin = new System.Windows.Forms.Padding(4);
            this.TxtBx_Password.Name = "TxtBx_Password";
            this.TxtBx_Password.PasswordChar = '*';
            this.TxtBx_Password.Size = new System.Drawing.Size(295, 22);
            this.TxtBx_Password.TabIndex = 3;
            // 
            // Btn_LogIn
            // 
            this.Btn_LogIn.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_LogIn.Location = new System.Drawing.Point(13, 154);
            this.Btn_LogIn.Margin = new System.Windows.Forms.Padding(4);
            this.Btn_LogIn.Name = "Btn_LogIn";
            this.Btn_LogIn.Size = new System.Drawing.Size(88, 36);
            this.Btn_LogIn.TabIndex = 4;
            this.Btn_LogIn.Text = "Loged In";
            this.Btn_LogIn.UseVisualStyleBackColor = true;
            this.Btn_LogIn.Click += new System.EventHandler(this.Btn_LogIn_Click);
            // 
            // Labl_CreateAccount
            // 
            this.Labl_CreateAccount.AutoSize = true;
            this.Labl_CreateAccount.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Labl_CreateAccount.ForeColor = System.Drawing.SystemColors.Desktop;
            this.Labl_CreateAccount.Location = new System.Drawing.Point(194, 228);
            this.Labl_CreateAccount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Labl_CreateAccount.Name = "Labl_CreateAccount";
            this.Labl_CreateAccount.Size = new System.Drawing.Size(114, 19);
            this.Labl_CreateAccount.TabIndex = 5;
            this.Labl_CreateAccount.Text = "Create Account";
            this.Labl_CreateAccount.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Labl_CreateAccount_MouseDown);
            this.Labl_CreateAccount.MouseLeave += new System.EventHandler(this.Labl_CreateAccount_MouseLeave);
            this.Labl_CreateAccount.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Labl_CreateAccount_MouseMove);
            // 
            // Cmbx_LogIn
            // 
            this.Cmbx_LogIn.AutoSize = true;
            this.Cmbx_LogIn.Location = new System.Drawing.Point(211, 166);
            this.Cmbx_LogIn.Name = "Cmbx_LogIn";
            this.Cmbx_LogIn.Size = new System.Drawing.Size(97, 18);
            this.Cmbx_LogIn.TabIndex = 6;
            this.Cmbx_LogIn.Text = "Keep loged in";
            this.Cmbx_LogIn.UseVisualStyleBackColor = true;
            // 
            // PBx_ViewPassword
            // 
            this.PBx_ViewPassword.Image = ((System.Drawing.Image)(resources.GetObject("PBx_ViewPassword.Image")));
            this.PBx_ViewPassword.Location = new System.Drawing.Point(285, 112);
            this.PBx_ViewPassword.Name = "PBx_ViewPassword";
            this.PBx_ViewPassword.Size = new System.Drawing.Size(20, 20);
            this.PBx_ViewPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PBx_ViewPassword.TabIndex = 7;
            this.PBx_ViewPassword.TabStop = false;
            this.PBx_ViewPassword.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PBx_ViewPassword_MouseDown);
            this.PBx_ViewPassword.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PBx_ViewPassword_MouseUp);
            // 
            // F_LogIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.ClientSize = new System.Drawing.Size(325, 262);
            this.Controls.Add(this.PBx_ViewPassword);
            this.Controls.Add(this.Cmbx_LogIn);
            this.Controls.Add(this.Labl_CreateAccount);
            this.Controls.Add(this.Btn_LogIn);
            this.Controls.Add(this.TxtBx_Password);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TxtBx_Account);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "F_LogIn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Log In";
            this.Load += new System.EventHandler(this.F_LogIn_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.F_LogIn_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.F_LogIn_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.F_LogIn_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.PBx_ViewPassword)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtBx_Account;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtBx_Password;
        private System.Windows.Forms.Button Btn_LogIn;
        private System.Windows.Forms.Label Labl_CreateAccount;
        private System.Windows.Forms.CheckBox Cmbx_LogIn;
        private System.Windows.Forms.PictureBox PBx_ViewPassword;
    }
}


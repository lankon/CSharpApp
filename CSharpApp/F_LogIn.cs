using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.Runtime.InteropServices;



using System.Security.Cryptography;


namespace CSharpApp
{   
    public partial class F_LogIn : Form
    {
        enum BOOL_SETTING
        {
            Cmbx_LogIn,
        }

        enum STRING_SETTING
        {
            TxtBx_Account,
            TxtBx_Password,
            Cmbx_LogIn,
        }

        Tool Tool = new Tool();

        public F_LogIn()
        {
            InitializeComponent();
        }

        #region 視窗拖曳功能
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        bool beginMove = false;//初始化滑鼠位置
        int currentXPosition;
        int currentYPosition;

        private void F_LogIn_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                beginMove = true;
                currentXPosition = MousePosition.X;//滑鼠的x座標為當前窗體左上角x座標
                currentYPosition = MousePosition.Y;//滑鼠的y座標為當前窗體左上角y座標
            }
        }

        private void F_LogIn_MouseMove(object sender, MouseEventArgs e)
        {
            if (beginMove)
            {
                this.Left += MousePosition.X - currentXPosition;//根據滑鼠x座標確定窗體的左邊座標x
                this.Top += MousePosition.Y - currentYPosition;//根據滑鼠的y座標窗體的頂部，即Y座標
                currentXPosition = MousePosition.X;
                currentYPosition = MousePosition.Y;
            }
        }

        private void F_LogIn_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                currentXPosition = 0; //設定初始狀態
                currentYPosition = 0;
                beginMove = false;
            }
        }
        #endregion

        private void Labl_CreateAccount_MouseDown(object sender, MouseEventArgs e)
        {          
            if(TxtBx_Account.Text == "" || TxtBx_Password.Text == "")
            {
                MessageBox.Show("Account Create Fail");
                return;
            }
            
            StreamWriter File;
            String Msg;

            Msg = TxtBx_Account.Text + "," + Tool.FixPassword(TxtBx_Password.Text);

            File = Tool.CreateFile("Account&Password", ".dat", true);
            Tool.WriteFile(File, Msg);
            Tool.CloseFile(File);

            MessageBox.Show("Account Create Success");
        }

        private void Labl_CreateAccount_MouseMove(object sender, MouseEventArgs e)
        {
            Labl_CreateAccount.ForeColor = SystemColors.HotTrack;
        }

        private void Labl_CreateAccount_MouseLeave(object sender, EventArgs e)
        {
            Labl_CreateAccount.ForeColor = SystemColors.Desktop;
        }

        

        private void F_LogIn_Load(object sender, EventArgs e)
        {
            
        }

       

        private void Btn_LogIn_Click(object sender, EventArgs e)
        {
            //Tool.AddUpdateAppSettings("TxtBx_Password", "lankon850321");
            //Tool.AddUpdateAppSettings("TxtBx_Account", "lankon");
            ReadSetting();


        }

        

        public void ReadSetting()
        {            
            List<STRING_SETTING> enumTypeList = Tool.EnumToStringList<STRING_SETTING>();
           
            Control[] controls;

            for (int i = 0; i < enumTypeList.Count; i++)
            {               
                controls = Controls.Find(enumTypeList[i].ToString(), false);

                if (controls.Count() == 1) // 0 means not found, more - there are several controls with the same name
                {
                    TextBox control = controls[0] as TextBox;
                    if (control != null)
                    {
                        control.Text = Tool.ReadSetting(enumTypeList[i].ToString());
                    }
                }
            }
        }

        

        private void PBx_ViewPassword_MouseDown(object sender, MouseEventArgs e)
        {
            TxtBx_Password.PasswordChar = '\0';
        }

        private void PBx_ViewPassword_MouseUp(object sender, MouseEventArgs e)
        {
            TxtBx_Password.PasswordChar = '*';
        }
    }
}

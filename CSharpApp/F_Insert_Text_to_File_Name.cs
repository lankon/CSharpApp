using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpApp
{
    public partial class F_Insert_Text_to_File_Name : Form
    {
        public F_Insert_Text_to_File_Name()
        {
            InitializeComponent();
        }


        public void SetF_Insert_Text_to_File_Name(TabControl TbCtrl, TabPage[] tabpage, string tabpage_name, int count, F_Insert_Text_to_File_Name user_control)
        {
            tabpage[count] = new TabPage(tabpage_name);

            // 可以设置 UserControl 的大小和其他属性
            user_control.Dock = DockStyle.Fill;
            user_control.Visible = true;
            user_control.TopLevel = false;
            user_control.Top = 0;
            user_control.Left = 0;

            // 将 UserControl 添加到 TabPage
            tabpage[count].Controls.Add(user_control);
            TbCtrl.TabPages.Add(tabpage[count]);
        }
        public string Preview_Insert_Text_to_File_Name(string FileName)
        {
            if(CkBx_BeginningText.Checked == true)
            {
                FileName = TxtBx_BeginningInsertText.Text + FileName;
            }

            if(CkBx_InsertTextBeforKeyword.Checked == true)
            {
                string Keyword = TxtBx_BeforKeywordKeyword.Text;
                string Addword = TxtBx_BeforKeywordInsertText.Text;
                string Final = Addword + Keyword;

                FileName = FileName.Replace(Keyword, Final);
            }

            return FileName;
        }
        

    }
}

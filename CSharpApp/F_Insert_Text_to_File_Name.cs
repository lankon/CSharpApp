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

        public bool GetCkBx_BeginningText()
        {
            bool IsUse = CkBx_BeginningText.Checked;
            return IsUse;
        }

        public bool GetCkBx_InsertTextBeforKeyword()
        {
            bool IsUse = CkBx_InsertTextBeforKeyword.Checked;
            return IsUse;
        }

        public string GetTxtBx_BeginningInsertText()
        {
            string Txt = TxtBx_BeginningInsertText.Text;
            return Txt;
        }


        

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CommonFunction;

namespace ImageProcessing.FF_Calculate
{
    public partial class F_FF_Calculate_ButtonGroup : Form
    {
        #region parameter define
        private string RecipeSavePath = "FF_Calculate.exe.Config";  //參數儲存檔名
        #endregion

        #region private function
        private void InitialApplication()
        {
            SetHint();

            ApplicationSetting.ReadAllRecipe<FormItem>(RecipeSavePath);
            ApplicationSetting.UpdataRecipeToForm<FormItem>(this, RecipeSavePath);
        }
        private void SetHint()
        {
            toolTip1.SetToolTip(Btn_Setting, "Setting");
        }
        private void CloseFormOnPanel(Panel pnl)
        {
            foreach (Control control in pnl.Controls)
            {
                if (control is Form && control.Visible == true)
                {
                    ((Form)control).Close();
                    ((Form)control).Dispose();
                    break;
                }
            }
        }
        #endregion

        #region public function
        public void SetF_FF_Calculate_ButtonGroup(Panel pnl, F_FF_Calculate_ButtonGroup form)
        {
            form.Dock = DockStyle.Fill;
            form.Visible = false;
            form.TopLevel = false;
            form.Top = 0;
            form.Left = 0;
            form.TopMost = true;

            pnl.Controls.Add(form);

            form.Hide();
        }
        #endregion



        public F_FF_Calculate_ButtonGroup()
        {
            InitializeComponent();

            InitialApplication();
        }

        private void Btn_Setting_Click(object sender, EventArgs e)
        {
            CloseFormOnPanel(Scope.MyStaticPanel);

            F_FF_Calculate_Setting f_FF_Calculate_Setting = new F_FF_Calculate_Setting();
            f_FF_Calculate_Setting.SetF_FF_Calculate_Setting(Scope.MyStaticPanel, f_FF_Calculate_Setting);
            f_FF_Calculate_Setting.Show();
        }
    }
}

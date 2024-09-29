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
using CommonFunction;

namespace InstrumentTest
{
    public partial class F_TC_Setting : Form
    {
        #region parameter define


        #endregion

        #region private function
        private void InitialApplication()
        {
            ShowHint();
            
            ApplicationSetting.ReadAllRecipe<eFormAppSet>();
            ApplicationSetting.UpdataRecipeToForm<eFormAppSet>(this);
        }
        private void ShowHint()
        {
            toolTip1.SetToolTip(Panel_ShowFormName, "F_TC_Setting");
        }
        #endregion

        #region public function
        public void SetF_TC_Setting(Panel pnl, F_TC_Setting form)
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

        public F_TC_Setting()
        {
            InitializeComponent();

            InitialApplication();
        }

        private void F_TC_Setting_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                ApplicationSetting.SaveAllRecipe(this);
                ApplicationSetting.ReadAllRecipe<eFormAppSet>();
            }
        }

        private void F_TC_Setting_FormClosed(object sender, FormClosedEventArgs e)
        {
            ApplicationSetting.SaveAllRecipe(this);
            ApplicationSetting.ReadAllRecipe<eFormAppSet>();

            this.Dispose(); // 显式调用 Dispose 以释放资源

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        
    }
}

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

namespace LFSMEO.Base.Template
{
    public partial class F_Template : Form
    {
        #region parameter define
        #endregion

        #region private function
        void InitialApplication()
        {
            ApplicationSetting.ReadAllRecipe<FormItem>();
            ApplicationSetting.UpdataRecipeToForm<FormItem>(this);

            ShowHint();
        }
        void ShowHint()
        {

        }
        #endregion

        #region public function
        public void SetForm(Panel pnl, Form form)
        {
            form.Dock = DockStyle.Fill;
            form.Visible = false;
            form.TopLevel = false;
            form.Top = 0;
            form.Left = 0;

            pnl.Controls.Add(form);
        }
        #endregion

        public F_Template()
        {
            InitializeComponent();
        }
    }
}

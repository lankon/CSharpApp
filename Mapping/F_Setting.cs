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

namespace Mapping
{
    public partial class F_Setting : Form
    {
        #region parameter define
        bool bCustomerShow = false; //用於判別是否秀出Customer名稱
        #endregion


        public F_Setting()
        {
            InitializeComponent();

            ApplicationSetting.ReadAllRecipe<FormItem>();
            ApplicationSetting.UpdataRecipeToForm<FormItem>(this);
        }

        public void SetF_Setting(Panel pnl, F_Setting form)
        {
            form.Dock = DockStyle.Fill;
            form.Visible = true;
            form.TopLevel = false;
            form.Top = 0;
            form.Left = 0;

            pnl.Controls.Add(form);

            form.Hide();
        }

        public int[] Get_XY_Direc()
        {
            int[] xy_direc = new int[2];

            xy_direc[0] = 1;
            xy_direc[1] = 1;

            if (Cmbx_X_Direc.SelectedIndex == 2)
                xy_direc[0] = -1;

            if (Cmbx_Y_Direc.SelectedIndex == 2)
                xy_direc[1] = -1;

            return xy_direc;
        }

        private void F_Setting_FormClosed(object sender, FormClosedEventArgs e)
        {
            ApplicationSetting.SaveAllRecipe(this);
            ApplicationSetting.ReadAllRecipe<FormItem>();

            this.Dispose(); // 显式调用 Dispose 以释放资源

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }


        private void F_Setting_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                ApplicationSetting.SaveAllRecipe(this);
                ApplicationSetting.ReadAllRecipe<FormItem>();
            }
        }

        private void label6_DoubleClick(object sender, EventArgs e)
        {

            int index = Cmbx_Customer.SelectedIndex;

            Cmbx_Customer.Items.Clear();

            string[] customer_item = new string[2];

            if (bCustomerShow == false)
            {
                customer_item[0] = "Defult";
                customer_item[1] = "AMIDA";
                bCustomerShow = true;
            }
            else
            {
                customer_item[0] = "Default";
                customer_item[1] = "Customer1";
                bCustomerShow = false;
            }
            
            for (int i = 0; i < customer_item.Length; i++)
            {
                Cmbx_Customer.Items.Add(customer_item[i]);
            }

            Cmbx_Customer.SelectedIndex = index;
        }
    }
}

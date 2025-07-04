using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

using CommonFunction;

namespace InstrumentTest.Motion_IO_Card
{
    public partial class F_Motion_Card : Form
    {

        #region private function
        Function_Motion_Card DML = new Function_Motion_Card();

        void InitialForm()
        {
            ApplicationSetting.ReadAllRecipe<eFormAppSet>();
            ApplicationSetting.UpdataRecipeToForm<eFormAppSet>(this);

            ShowHint();
        }
        void ShowHint()
        {

        }
        #endregion

        public F_Motion_Card()
        {
            InitializeComponent();

            InitialForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DML.Initial_All_Motion();

            Function_Motion_Card.MOTION_INFO MF = new Function_Motion_Card.MOTION_INFO();

            MF.NAME = "AMP_204C";
            MF.LINE_NO = 0;
            MF.DEV_NO = 0;
            MF.AXIS_NO = 0;
            MF.ORIGIN_POS = 0;

            DML.SetAxis(MF);

            MF.NAME = "AMP_204C";
            MF.LINE_NO = 0;
            MF.DEV_NO = 1;
            MF.AXIS_NO = 1;
            MF.ORIGIN_POS = 0;

            DML.SetAxis(MF);
            
            DML.BindingAxis();

            DML.SetServo(0, true);
            DML.SetServo(1, true);

            MessageBox.Show("Initial Success");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                await DML.GoHome(0);
            });

            Task.Run(async () =>
            {
                await DML.GoHome(1);
            });
        }

        private void Btn_GetPosition_Click(object sender, EventArgs e)
        {
            Btn_GetPosition.Text = DML.GetPosition(1).ToString();
        }
    }
}

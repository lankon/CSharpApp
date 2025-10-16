using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using LFSMEO.Base_LFSMEO;
using ToolFunction.Base;

namespace LFSMEO.UI
{
    public partial class F_AxisButton: Form, IF_MotionSetting
    {
        #region parameter define
        Med_MotionSetting MotionMediator;
        List<Panel> PnlPartList = new List<Panel>();
        private int curPnlPart = 0;
        private int CurBtnNum = 0;
        #endregion

        #region private function
        private void SetHint()
        {
            //toolTip1.SetToolTip(Btn_OEM_Setting, "OEM Setting");
        }
        private void InitialForm()
        {
            SetPnlPartPos(Pnl_Part1);

            SetHint();

            if (ApplicationSetting.Get_Int_Recipe<eOEMSetting>((int)eOEMSetting.Cmbx_ShowFormName) == 1)
                Tool.ShowFormName(this);

            PnlPartList.Add(Pnl_Part1);
            PnlPartList.Add(Pnl_Part2);
        }
        private int PreviousPnlPart(List<Panel> list, int index)
        {
            index = index - 1;

            if (index < 0)
                index = 0;

            SetPnlPartPos(list[index]);

            return index;
        }
        private int NextPnlPart(List<Panel> list, int index)
        {
            index = index + 1;

            if (index >= list.Count)
                index = 0;

            SetPnlPartPos(list[index]);

            return index;
        }
        private void SetPnlPartPos(Panel pnl)   //應該可以移到ToolFunction
        {
            pnl.Location = new Point(0, 0);
            pnl.BringToFront();
        }
        #endregion

        #region public function
        public int GetCurrentBtnNum()
        {
            return CurBtnNum;
        }
        public void SetMediator(Med_MotionSetting med)
        {
            MotionMediator = med;
        }
        #endregion

        public F_AxisButton()
        {
            InitializeComponent();


            InitialForm();
        }

        private void Btn_Axis0_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            CurBtnNum = Tool.StringToInt((string)btn.Tag);
            
            MotionMediator.UpdateParameter();
        }

        private void Btn_PreviousPnlPart1_Click(object sender, EventArgs e)
        {
            curPnlPart = PreviousPnlPart(PnlPartList, curPnlPart);
        }

        private void Btn_NextPnlPart1_Click(object sender, EventArgs e)
        {
            curPnlPart = NextPnlPart(PnlPartList, curPnlPart);
        }
    }
}

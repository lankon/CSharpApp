using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LFSMEO.UI
{
    public partial class F_SartForm_ButtonGroup : Form
    {
        #region parameter define
        List<Panel> PnlPartList = new List<Panel>();
        int curPnlPart = 0;
        #endregion

        #region private function
        private void SetHint()
        {
            toolTip1.SetToolTip(Btn_MFR_Setting, "Manufacturer Setting");
            toolTip1.SetToolTip(Btn_ParameterSetting, "Parameter Setting");
            toolTip1.SetToolTip(Btn_LogIn, "Log In");
        }
        private void InitialForm()
        {
            SetPnlPartPos(Pnl_Part1);

            SetHint();

            PnlPartList.Add(Pnl_Part1);
            PnlPartList.Add(Pnl_Part2);
        }
        private void SetPnlPartPos(Panel pnl)   //應該可以移到CommonFunction
        {
            pnl.Location = new Point(0, 0);
            pnl.BringToFront();
        }
        private int NextPnlPart(List<Panel> list, int index)
        {
            index = index + 1;

            if (index >= list.Count)
                index = 0;

            SetPnlPartPos(list[index]);

            return index;
        }
        private int PreviousPnlPart(List<Panel> list, int index)
        {
            index = index - 1;

            if (index < 0)
                index = 0;

            SetPnlPartPos(list[index]);

            return index;
        }

        #endregion

        public F_SartForm_ButtonGroup()
        {
            InitializeComponent();

            InitialForm();
        }

        private void Btn_PreviousPnlPart_Click(object sender, EventArgs e)
        {
            curPnlPart = PreviousPnlPart(PnlPartList, curPnlPart);
        }

        private void Btn_NextPnlPart_Click(object sender, EventArgs e)
        {
            curPnlPart = NextPnlPart(PnlPartList, curPnlPart);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GC.Collect();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InstrumentTest
{
    

    public partial class F_Show_PV_TPT8000 : Form
    {
        #region parameter define 
        private Dictionary<string, Label> PV_Labels = new Dictionary<string, Label>();
        #endregion

        #region private function
        protected override CreateParams CreateParams    //防止元件更新時畫面閃爍
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        private void InitialApplication()
        {
            Add_PV_Label();
            Add_Board_Label();
            Add_RTD_Label();

            ShowHint();

            GlobalVariable.Task_TC.GetBoardRTD += Show_PV;
        }

        private void Add_PV_Label()
        {
            string Name = "PV";

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Label PV_label = new Label();

                    PV_label.AutoSize = true;
                    PV_label.Dock = System.Windows.Forms.DockStyle.Fill;
                    PV_label.Font = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
                    PV_label.Location = new System.Drawing.Point(486, 54);
                    PV_label.Name = $"{Name}_{i}_{j}";
                    PV_label.Size = new System.Drawing.Size(87, 48);
                    PV_label.TabIndex = 0;
                    PV_label.Text = $"{Name}_{i}_{j}";
                    PV_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                    this.tableLayoutPanel1.Controls.Add(PV_label, i + 1, j + 1);

                    PV_Labels[PV_label.Name] = PV_label;
                }
            }
        }
        private void Add_Board_Label()
        {
            string Name = "Board";

            for (int i = 0; i < 9; i++)
            {
                Label label = new Label();

                label.AutoSize = true;
                label.Dock = System.Windows.Forms.DockStyle.Fill;
                label.Font = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
                label.Location = new System.Drawing.Point(486, 54);
                label.Name = $"{Name}_{i}";
                label.Size = new System.Drawing.Size(87, 48);
                label.TabIndex = 0;
                label.Text = $"Board{i + 1}";
                label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                this.tableLayoutPanel1.Controls.Add(label, i + 1, 0);
            }
        }
        private void Add_RTD_Label()
        {
            string Name = "RTD";

            for (int i = 0; i < 5; i++)
            {
                Label label = new Label();

                label.AutoSize = true;
                label.Dock = System.Windows.Forms.DockStyle.Fill;
                label.Font = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
                label.Location = new System.Drawing.Point(486, 54);
                label.Name = $"{Name}_{i}";
                label.Size = new System.Drawing.Size(87, 48);
                label.TabIndex = 0;
                label.Text = $"RTD{i + 1}";
                label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                this.tableLayoutPanel1.Controls.Add(label, 0, i + 1);
            }
        }
        private void Show_PV(string temp, int board_num)
        {
            char[] separator = new char[] { ',' };
            String[] values = temp.Split(separator);

            for (int i=0; i<5; i++)
            {
                if (PV_Labels.ContainsKey($"PV_{board_num}_{i}"))
                {
                    if (PV_Labels[$"PV_{board_num }_{i}"].InvokeRequired)
                    {
                        PV_Labels[$"PV_{board_num}_{i}"].Invoke(new Action(() => PV_Labels[$"PV_{board_num}_{i}"].Text = values[i]));
                    }
                    else
                    {
                        PV_Labels[$"PV_{board_num}_{i}"].Text = values[i];
                    }
                }                
            }
        }
        private void ShowHint()
        {
            toolTip1.SetToolTip(Panel_ShowFormName, "F_Show_PV_TPT8000");
        }
        #endregion

        #region public function
        public void SetF_Show_PV_TPT8000(Panel pnl, F_Show_PV_TPT8000 form)
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

        public F_Show_PV_TPT8000()
        {
            InitializeComponent();

            InitialApplication();
        }

        private void F_Show_PV_TPT8000_VisibleChanged(object sender, EventArgs e)
        {
            if(this.Visible == false)
            {
                GlobalVariable.Task_TC.MonitorAll(false);
            }            
        }
    }
}
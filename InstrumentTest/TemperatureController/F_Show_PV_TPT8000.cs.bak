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
        #region private function
        private void InitialApplication()
        {

            Add_PV_Label();
            Add_Board_Label();
            Add_RTD_Label();
        }

        private void Add_PV_Label()
        {
            string Name = "PV";

            for(int i=0; i<9; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Label label = new Label();

                    label.AutoSize = true;
                    label.Dock = System.Windows.Forms.DockStyle.Fill;
                    label.Font = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
                    label.Location = new System.Drawing.Point(486, 54);
                    label.Name = $"{Name}_{i}_{j}";
                    label.Size = new System.Drawing.Size(87, 48);
                    label.TabIndex = 0;
                    label.Text = "200.45";
                    label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                    this.tableLayoutPanel1.Controls.Add(label, i + 1, j + 1);
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
                label.Text = $"Board{i+1}";
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
                label.Text = $"RTD{i+1}";
                label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                this.tableLayoutPanel1.Controls.Add(label, 0, i+1);
            }
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
    }
}

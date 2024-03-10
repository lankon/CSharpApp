using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mapping
{
    public partial class F_BinTable : Form
    {
        public F_BinTable()
        {
            InitializeComponent();


            InitializeDataGridView();

        }

        public void SetF_BinTable(Panel pnl, F_BinTable form)
        {
            form.Dock = DockStyle.Fill;
            form.Visible = true;
            form.TopLevel = false;
            form.Top = 0;
            form.Left = 0;

            pnl.Controls.Add(form);

            form.Hide();
        }

        private void InitializeDataGridView()
        {
            // 設置列
            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].Name = "ID";
            dataGridView1.Columns[1].Name = "Name";
            dataGridView1.Columns[2].Name = "Age";

            // 添加行
            string[] row1 = new string[] { "1", "John Doe", "30" };
            string[] row2 = new string[] { "2", "Jane Smith", "25" };
            string[] row3 = new string[] { "3", "Mike Johnson", "27" };

            dataGridView1.Rows.Add(row1);
            dataGridView1.Rows.Add(row2);
            dataGridView1.Rows.Add(row3);

            // 使用者不能自己添加新行
            dataGridView1.AllowUserToAddRows = false;
        }
    }
}

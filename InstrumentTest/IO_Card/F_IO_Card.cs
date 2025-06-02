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
using InstrumentTest.IO_Card.Base;

namespace InstrumentTest.IO_Card
{
    public partial class F_IO_Card : Form
    {
        #region parameter
        Tool tool = new Tool();
        List<IOData> IOList = new List<IOData>();
        Function_IO_Card DIOL = new Function_IO_Card();
        private bool IO_Init = false;
        #endregion

        #region private function
        //private void Update_IO_List(DataGridView DGV, List<IOData> io_list)
        //{
        //    io_list.Clear();

        //    foreach (DataGridViewRow row in DGV.Rows)
        //    {
        //        if (row.IsNewRow) continue;

        //        var data = new IOData()
        //        {
        //            Title_IO = row.Cells["Title_IO"]?.Value?.ToString(),
        //            Title_Name = row.Cells["Title_Name"]?.Value?.ToString(),
        //            Title_Description = row.Cells["Title_Description"]?.Value?.ToString(),
        //            Title_CardType = row.Cells["Title_CardType"]?.Value?.ToString(),
        //            Title_CardNum = Convert.ToInt32(row.Cells["Title_CardNum"]?.Value ?? "-1"),
        //            Title_IO_Num = Convert.ToInt32(row.Cells["Title_IO_Num"]?.Value ?? "-1"),
        //            Title_Status = row.Cells["Title_Status"]?.Value?.ToString(),
        //        };

        //        io_list.Add(data);
        //    }
        //}

        #endregion

        #region public function
        public void SetF_IO_Card(Panel pnl, F_IO_Card form)
        {
            form.Dock = DockStyle.Fill;
            form.Visible = true;
            form.TopLevel = false;
            form.Top = 0;
            form.Left = 0;
            form.TopMost = true;

            pnl.Controls.Add(form);

            form.Hide();
        }
        #endregion

        public F_IO_Card()
        {
            InitializeComponent();


            
        }

        private void Btn_Add_Click(object sender, EventArgs e)
        {
            string[] context = new string[] { "None", "None", "None", "None", "-1", "OFF", "False", "-1", "-1", "-1" };

            tool.DataGrid_AddRow(DGV_IO, context);
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            tool.DataGrid_DataSave(DGV_IO, "IO.xml");
        }

        private void Btn_Remove_Click(object sender, EventArgs e)
        {
            tool.DataGrid_DeleteRow(DGV_IO);
        }

        private void Btn_RowUp_Click(object sender, EventArgs e)
        {
            tool.DataGrid_RowUp(DGV_IO);
        }

        private void Btn_RowDown_Click(object sender, EventArgs e)
        {
            tool.DataGrid_RowDown(DGV_IO);
        }

        private void Btn_Load_Click(object sender, EventArgs e)
        {
            tool.DataGrid_DataLoad(DGV_IO, "IO.xml");
        }

        private void DGV_IO_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (IOList.Count == 0)
                return;
            
            // 取得改變的欄位與行
            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;

            // 可以用欄位名稱來確認是你要監控的欄位
            var columnName = DGV_IO.Columns[colIndex].Name;

            if (columnName == "Title_IO_Num" && rowIndex >= 0)
            {
                object newValue = DGV_IO.Rows[rowIndex].Cells[colIndex].Value;

                Int32.TryParse((string)newValue, out int port);

                IOList[rowIndex].Title_IO_Num = port;
            }

            DIOL.Update_IO_List(DGV_IO, IOList);
        }

        private void Btn_Open_Click(object sender, EventArgs e)
        {
            if(DIOL.Initial_All_IO())
            {
                IO_Init = true;

                DIOL.Update_IO_List(DGV_IO, IOList);
            }
        }

        private void Timer_IO_Tick(object sender, EventArgs e)
        {
            if (IO_Init == false)
                return;

            for (int i = 0; i < IOList.Count; i++)
            {
                bool input_res = false;

                if (IOList[i].Title_CardType == "MN200" && IOList[i].Title_IO == "Input")
                {
                    if (DIOL.GetInputStatus(EIOCardType.MN200,
                                            (byte)IOList[i].Title_LineNum,
                                            (byte)IOList[i].Title_DevNum, 
                                            (byte)IOList[i].Title_IO_Num,i))
                    {
                        input_res = true;
                    }
                }
                else if (IOList[i].Title_CardType == "PCI_9111" && IOList[i].Title_IO == "Input")
                {
                    if (DIOL.GetInputStatus(EIOCardType.PCI_9111,
                                            (byte)IOList[i].Title_LineNum,
                                            (byte)IOList[i].Title_DevNum, 
                                            (byte)IOList[i].Title_IO_Num,i))
                    {
                        input_res = true;
                    }
                }

                if (input_res)
                {
                    DGV_IO.Rows[i].Cells["Title_Status"].Value = "ON";
                    DGV_IO.Rows[i].Cells["Title_Status"].Style.BackColor = Color.SkyBlue;
                }
                else
                {
                    DGV_IO.Rows[i].Cells["Title_Status"].Value = "OFF";
                    DGV_IO.Rows[i].Cells["Title_Status"].Style.BackColor = Color.White;
                }
            }
        }

        private void Btn_Test_Click(object sender, EventArgs e)
        {
            bool res = DIOL.GetInputStatus(EIOName.SafePos_Sensor);
        }
    }
}

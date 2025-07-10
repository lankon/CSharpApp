﻿using System;
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
using InstrumentTest.Motion_IO_Card.Base;

namespace InstrumentTest.Motion_IO_Card
{
    public partial class F_IO_Card : Form
    {
        #region parameter
        List<IOData> IOList = new List<IOData>();
        Function_IO_Card DIOL = new Function_IO_Card();
        private bool IO_Init = false;
        #endregion

        #region private function
        private bool IOCard_GetInputStatus(int i)
        {
            bool input_res = false;

            if (IOList[i].Title_IO == "Input" &&
                Enum.TryParse<EIOCardType>(IOList[i].Title_CardType, out var cardType))
            {
                input_res = DIOL.GetInputStatus(cardType,
                                    (byte)IOList[i].Title_LineNum,
                                    (byte)IOList[i].Title_DevNum,
                                    (byte)IOList[i].Title_IO_Num, i);
            }

            return input_res;
        }
        private bool IOCard_GetOutputStatus(int i)
        {
            bool output_res = false;

            if (IOList[i].Title_IO == "Output" &&
                Enum.TryParse<EIOCardType>(IOList[i].Title_CardType, out var cardType))
            {
                output_res = DIOL.GetOutputStatus(cardType,
                                    (byte)IOList[i].Title_LineNum,
                                    (byte)IOList[i].Title_DevNum,
                                    (byte)IOList[i].Title_IO_Num, i);
            }

            return output_res;
        }
        private void UpdateOutputStatus_UI()
        {
            for (int i = 0; i < IOList.Count; i++)
            {
                bool output_res = false;

                output_res = IOCard_GetOutputStatus(i);

                if (output_res && DGV_IO.Rows[i].Cells["Title_IO"].Value.ToString() == "Output")
                {
                    DGV_IO.Rows[i].Cells["Title_Status"].Value = "ON";
                    DGV_IO.Rows[i].Cells["Title_Status"].Style.BackColor = Color.SkyBlue;
                }
                else if (output_res == false && DGV_IO.Rows[i].Cells["Title_IO"].Value.ToString() == "Output")
                {
                    DGV_IO.Rows[i].Cells["Title_Status"].Value = "OFF";
                    DGV_IO.Rows[i].Cells["Title_Status"].Style.BackColor = Color.White;
                }
            }
        }
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
            string[] context = new string[] { "None", "None", "None", "None", "-1", "OFF", "False", "0", "0", "0" };

            Tool.DataGrid_AddRow(DGV_IO, context);
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            Tool.DataGrid_DataSave(DGV_IO, "IO.xml");
        }

        private void Btn_Remove_Click(object sender, EventArgs e)
        {
            Tool.DataGrid_DeleteRow(DGV_IO);

            DIOL.Update_IO_List(DGV_IO, IOList);
        }

        private void Btn_RowUp_Click(object sender, EventArgs e)
        {
            Tool.DataGrid_RowUp(DGV_IO);
        }

        private void Btn_RowDown_Click(object sender, EventArgs e)
        {
            Tool.DataGrid_RowDown(DGV_IO);
        }

        private void Btn_Load_Click(object sender, EventArgs e)
        {
            if(Tool.DataGrid_DataLoad(DGV_IO, "IO.xml"))
            {
                Btn_Open.Enabled = true;
            }
            else
            {
                Tool.SaveHistoryToFile("IO表讀取失敗");
            }
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

                UpdateOutputStatus_UI();
            }
        }

        private void Timer_IO_Tick(object sender, EventArgs e)
        {
            if (IO_Init == false)
                return;

            for (int i = 0; i < IOList.Count; i++)
            {
                bool input_res = false;

                input_res = IOCard_GetInputStatus(i);

                if (input_res && DGV_IO.Rows[i].Cells["Title_IO"].Value.ToString() != "Output")
                {
                    DGV_IO.Rows[i].Cells["Title_Status"].Value = "ON";
                    DGV_IO.Rows[i].Cells["Title_Status"].Style.BackColor = Color.SkyBlue;
                }
                else if(input_res == false && DGV_IO.Rows[i].Cells["Title_IO"].Value.ToString() != "Output")
                {
                    DGV_IO.Rows[i].Cells["Title_Status"].Value = "OFF";
                    DGV_IO.Rows[i].Cells["Title_Status"].Style.BackColor = Color.White;
                }
            }
        }

        private void Btn_Test_Click(object sender, EventArgs e)
        {
            //if (IO_Init == false)
            //    return;

            //UpdateOutputStatus_UI();
        }

        private void DGV_IO_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (IO_Init == false)
                return;

            if (e.RowIndex >= 0 && 
                DGV_IO.Columns[e.ColumnIndex].Name == "Title_Status" && 
                DGV_IO.Rows[e.RowIndex].Cells["Title_IO"].Value.ToString() == "Output")
            {
                //判斷IO卡
                EIOCardType eIOCardType = EIOCardType.None;

                if (DGV_IO.Rows[e.RowIndex].Cells["Title_CardType"].Value.ToString() == "AMP_204C")
                    eIOCardType = EIOCardType.AMP_204C;
                else if (DGV_IO.Rows[e.RowIndex].Cells["Title_CardType"].Value.ToString() == "MN200")
                    eIOCardType = EIOCardType.MN200;
                else if (DGV_IO.Rows[e.RowIndex].Cells["Title_CardType"].Value.ToString() == "P32C32")
                    eIOCardType = EIOCardType.P32C32;


                // 取得目前儲存格值
                var value = DGV_IO.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();

                // 可以根據值做切換
                if (value == "ON")
                {
                    DGV_IO.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "OFF";
                    DGV_IO.Rows[e.RowIndex].Cells["Title_Status"].Style.BackColor = Color.White;

                    DIOL.SetOutputStatus(eIOCardType,
                                            (byte)IOList[e.RowIndex].Title_CardNum,
                                            (byte)IOList[e.RowIndex].Title_LineNum,
                                            (byte)IOList[e.RowIndex].Title_DevNum,
                                            (byte)IOList[e.RowIndex].Title_IO_Num,
                                            false);
                }
                else
                {
                    DGV_IO.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "ON";
                    DGV_IO.Rows[e.RowIndex].Cells["Title_Status"].Style.BackColor = Color.SkyBlue;

                    DIOL.SetOutputStatus(eIOCardType,
                                            (byte)IOList[e.RowIndex].Title_CardNum,
                                            (byte)IOList[e.RowIndex].Title_LineNum,
                                            (byte)IOList[e.RowIndex].Title_DevNum,
                                            (byte)IOList[e.RowIndex].Title_IO_Num,
                                            true);
                }

                DGV_IO.ClearSelection();
                DGV_IO.CurrentCell = null;
            }
        }
    }
}

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

        public class IOData
        {
            public string Title_IO { get; set; }
            public string Title_Name { get; set; }
            public string Title_Description { get; set; }
            public string Title_CardType { get; set; }
            public int Title_CardNum { get; set; }
            public int Title_IO_Num { get; set; }
            public string Title_Status { get; set; }
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

        private void button1_Click(object sender, EventArgs e)
        {
            ////要依種類分別創建IO Card
            //bool UseMN200 = false;
            //List<Base_IO_Card> IO = new List<Base_IO_Card>();
            //Base_IO_Card mN200 = new MN200();
            //IO.Add(mN200);

            ////開啟所有IO卡
            //for (int i = 0; i < IO.Count; i++)
            //{
            //    if (IO[i].Open() == false)
            //        continue;

            //    if (IO[i].GetName() == "MN200")
            //        UseMN200 = true;

            //}


            ////Thread持續讀取Input訊號
            //for (int k = 0; k < IO.Count; k++)
            //{
            //    List<byte> LineNo = IO[k].GetLineNo();
            //    List<byte> DevNo = IO[k].GetDevNo();

            //    for (byte i = 0; i < LineNo.Count; i++)
            //    {
            //        IO[k].UpdateInput(LineNo[i], DevNo[i]);
            //    }
            //}

            IOList.Clear();

            //Thread持續更新UI介面
            foreach (DataGridViewRow row in DGV_IO.Rows)
            {
                if (row.IsNewRow) continue;

                

                var data = new IOData()
                {
                    Title_IO = row.Cells["Title_IO"]?.Value?.ToString(),
                    Title_Name = row.Cells["Title_Name"]?.Value?.ToString(),
                    Title_Description = row.Cells["Title_Description"]?.Value?.ToString(),
                    Title_CardType = row.Cells["Title_CardType"]?.Value?.ToString(),
                    Title_CardNum = Convert.ToInt32(row.Cells["Title_CardNum"]?.Value ?? "-1"),
                    Title_IO_Num = Convert.ToInt32(row.Cells["Title_IO_Num"]?.Value ?? "-1"),
                    Title_Status = row.Cells["Title_Status"]?.Value?.ToString(),
                };

                IOList.Add(data);
            }

            for(int i=0; i<IOList.Count; i++)
            {
                bool input_res = false;
                
                if(IOList[i].Title_CardType == "MN200" && IOList[i].Title_IO == "Input")
                {
                    if (DIOL.GetInputStatus(EIOCardType.MN200, 1, 20, (byte)IOList[i].Title_IO_Num))   //編號先寫死之後開放設定
                        input_res = true;
                }
                else if (IOList[i].Title_CardType == "9111" && IOList[i].Title_IO == "Input")
                {
                    if (DIOL.GetInputStatus(EIOCardType.PCI_9111, 1, 20, (byte)IOList[i].Title_IO_Num))   //編號先寫死之後開放設定
                        input_res = true;
                }

                if(input_res)
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

        private void Btn_Add_Click(object sender, EventArgs e)
        {
            string[] context = new string[] { "None", "None", "None", "None", "-1", "-1", "OFF" };

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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(DIOL.Initial_All_IO())
            {
                DIOL.IO_ThreadStart();

                IO_Init = true;
            }
        }

        private void Timer_IO_Tick(object sender, EventArgs e)
        {
            if (IO_Init == false)
                return;

            IOList.Clear();

            //Thread持續更新UI介面
            foreach (DataGridViewRow row in DGV_IO.Rows)
            {
                if (row.IsNewRow) continue;

                var data = new IOData()
                {
                    Title_IO = row.Cells["Title_IO"]?.Value?.ToString(),
                    Title_Name = row.Cells["Title_Name"]?.Value?.ToString(),
                    Title_Description = row.Cells["Title_Description"]?.Value?.ToString(),
                    Title_CardType = row.Cells["Title_CardType"]?.Value?.ToString(),
                    Title_CardNum = Convert.ToInt32(row.Cells["Title_CardNum"]?.Value ?? "-1"),
                    Title_IO_Num = Convert.ToInt32(row.Cells["Title_IO_Num"]?.Value ?? "-1"),
                    Title_Status = row.Cells["Title_Status"]?.Value?.ToString(),
                };

                IOList.Add(data);
            }

            for (int i = 0; i < IOList.Count; i++)
            {
                bool input_res = false;

                if (IOList[i].Title_CardType == "MN200" && IOList[i].Title_IO == "Input")
                {
                    if (DIOL.GetInputStatus(EIOCardType.MN200, 1, 20, (byte)IOList[i].Title_IO_Num))   //編號先寫死之後開放設定
                        input_res = true;
                }
                else if (IOList[i].Title_CardType == "PCI_9111" && IOList[i].Title_IO == "Input")
                {
                    if (DIOL.GetInputStatus(EIOCardType.PCI_9111, 0, 0, (byte)IOList[i].Title_IO_Num))   //編號先寫死之後開放設定
                        input_res = true;
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
    }
}

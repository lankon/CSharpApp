using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using CommonFunction;
using System.Windows.Forms;

namespace InstrumentTest.IO_Card.Base
{
    public enum EIOCardType
    {
        None = 0,
        PCI_9111,
        MN200,
        
    }
    public enum EIOName
    {
        #region Input
        SafePos_Sensor,

        #endregion

        #region Output

        #endregion
    }

    public class IOData
    {
        public string Title_IO { get; set; }
        public string Title_Name { get; set; }
        public string Title_Description { get; set; }
        public string Title_CardType { get; set; }
        public int Title_CardNum { get; set; }
        public int Title_LineNum { get; set; }
        public int Title_DevNum { get; set; }
        public int Title_IO_Num { get; set; }
        public string Title_Status { get; set; }
        public string Title_Inverse { get; set; }
    }

    public class Function_IO_Card
    {
        #region parameter define
        private List<Base_IO_Card> IO = new List<Base_IO_Card>();
        private List<IOData> IO_List = new List<IOData>();
        Tool tool = new Tool();
        private Dictionary<string, IOData> ioListDict;
        #endregion

        #region private function
        private void Process()
        {
            while(true)
            {
                //Thread持續讀取Input訊號
                for (int k = 0; k < IO.Count; k++)
                {
                    if(IO[k].GetName() == "MN200")
                    {
                        List<byte> LineNo = IO[k].GetLineNo();
                        List<byte> DevNo = IO[k].GetDevNo();

                        for (byte i = 0; i < LineNo.Count; i++)
                        {
                            IO[k].UpdateInput(lineNo: LineNo[i], devNo: DevNo[i]);
                        }
                    }
                    else if(IO[k].GetName() == "PCI_9111")
                    {
                        for (byte i = 0; i < 15; i++)
                            IO[k].UpdateInput(port: i);
                    }
                    else if (IO[k].GetName() == "AMP_204C")
                    {
                        IO[k].UpdateInput();
                    }
                }

                Thread.Sleep(1);
            }
        }
        #endregion

        #region public function
        public bool Initial_All_IO()
        {
            bool UseMN200 = false, UseP32C32 = false, UsePcisDask = false, UseAPS = false ;
            
            Base_IO_Card mN200 = new MN200();
            Base_IO_Card pcis_dask = new pcis_dask(PCIS_DASK.Param.PCI_9111DG);
            Base_IO_Card APS = new APS();

            

            IO.Add(mN200);
            IO.Add(pcis_dask);
            IO.Add(APS);

            //開啟所有IO卡
            for (int i = 0; i < IO.Count; i++)
            {
                if (IO[i].Open() == false)
                    continue;

                if (IO[i].GetName() == "MN200")
                    UseMN200 = true;
                if (IO[i].GetName() == "PCI_9111")
                    UsePcisDask = true;
            }

            if (!UseMN200 && !UseP32C32 && !UsePcisDask)    //沒有任何一張IO卡
            {
                tool.SaveHistoryToFile("IO卡Initial失敗");
                return false;
            }

            Task task = Task.Run(() => Process());

            return true;
        }
        public void Update_IO_List(DataGridView DGV, List<IOData> io_list)
        {
            io_list.Clear();
            IO_List.Clear();

            foreach (DataGridViewRow row in DGV.Rows)
            {
                if (row.IsNewRow) continue;

                var data = new IOData()
                {
                    Title_IO = row.Cells["Title_IO"]?.Value?.ToString(),
                    Title_Name = row.Cells["Title_Name"]?.Value?.ToString(),
                    Title_Description = row.Cells["Title_Description"]?.Value?.ToString(),
                    Title_CardType = row.Cells["Title_CardType"]?.Value?.ToString(),
                    Title_IO_Num = Convert.ToInt32(row.Cells["Title_IO_Num"]?.Value ?? "-1"),
                    Title_Status = row.Cells["Title_Status"]?.Value?.ToString(),
                    Title_Inverse = row.Cells["Title_Inverse"]?.Value?.ToString(),
                    Title_CardNum = Convert.ToInt32(row.Cells["Title_CardNum"]?.Value ?? "-1"),
                    Title_LineNum = Convert.ToInt32(row.Cells["Title_LineNum"]?.Value ?? "-1"),
                    Title_DevNum = Convert.ToInt32(row.Cells["Title_DevNum"]?.Value ?? "-1"),
                };

                io_list.Add(data);
                IO_List.Add(data);
            }

            ioListDict = IO_List.GroupBy(x => x.Title_Name).ToDictionary(g => g.Key, g => g.First());
        }
        public bool GetInputStatus(EIOName name)
        {
            ioListDict.TryGetValue(name.ToString(), out IOData iOData);

            byte lineNo = (byte)iOData.Title_LineNum;
            byte devNo = (byte)iOData.Title_DevNum;
            byte port = (byte)iOData.Title_IO_Num;
                    
                    for (int j = 0; j < IO.Count; j++)
                    {
                if (IO[j].GetName() != iOData.Title_CardType)
                    continue;

                if (iOData.Title_Inverse == "True" || iOData.Title_Inverse == "true")
                    return !IO[j].GetInputStatus(lineNo, devNo, port);
                else if (iOData.Title_Inverse == "False" || iOData.Title_Inverse == "false")
                    return IO[j].GetInputStatus(lineNo, devNo, port);
            }
            
            return false;
        }
        public bool GetInputStatus(EIOCardType CardType,byte lineNo, byte devNo, byte port, int iList)
        {
            for(int i=0; i<IO.Count; i++)
            {
                if (IO[i].GetName() != CardType.ToString())
                    continue;

                if(IO_List[iList].Title_Inverse == "True" || IO_List[iList].Title_Inverse == "true")
                    return !IO[i].GetInputStatus(lineNo, devNo, port);
                else if (IO_List[iList].Title_Inverse == "False" || IO_List[iList].Title_Inverse == "false")
                    return IO[i].GetInputStatus(lineNo, devNo, port);
            }

            return false;
        }
        #endregion
    }
}

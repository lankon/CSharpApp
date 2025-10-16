using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System.Xml.Linq;

using InstrumentTest.Motion_IO_Card.Base;
using ToolFunction.Base;
using System.IO;

namespace InstrumentTest.Motion_IO_Card
{
    class Axis_Para
    {

    }
    
    class Function_Motion_Card
    {
        #region parameter define
        private List<Base_Motion_IO_Card> DML = new List<Base_Motion_IO_Card>();
        private string[] AxisName = new string[100];
        enum AxisNum
        {
            Axis0,
            Axis1,
            Axis2,
            Axis3,
        }
        #endregion

        #region private function
        private void Process()
        {
            while (true)
            {
                ////Thread持續讀取Status訊號
                //for (int k = 0; k < IO.Count; k++)
                //{
                //    if (IO[k].GetName() == "MN200")
                //    {
                //        List<byte> LineNo = IO[k].GetLineNo();
                //        List<byte> DevNo = IO[k].GetDevNo();

                //        for (byte i = 0; i < LineNo.Count; i++)
                //        {
                //            IO[k].UpdateInput(lineNo: LineNo[i], devNo: DevNo[i]);
                //        }
                //    }
                //    else if (IO[k].GetName() == "PCI_9111")
                //    {
                //        for (byte i = 0; i < 15; i++)
                //            IO[k].UpdateInput(port: i);
                //    }
                //    else if (IO[k].GetName() == "AMP_204C")
                //    {
                //        IO[k].UpdateInput();
                //    }
                //}

                Thread.Sleep(100);
            }
        }
        private bool OpenMotionCard(Base_Motion_IO_Card motion, string name)
        {
            if (motion.Open() == true)
            {
                DML.Add(motion);
                return true;
            }
            else
            {
                Tool.SaveLogToFile($"{name}開卡失敗");
                return false;
            }
        }
        
        #endregion

        #region public function
        public bool Initial_All_Motion()
        {
            bool Use_MN200, Use_APS;
            
            Base_Motion_IO_Card mN200 = new MN200();
            Base_Motion_IO_Card APS = new APS();

            Use_MN200 = OpenMotionCard(mN200, "MN200");
            Use_APS = OpenMotionCard(APS, "APS");

            if (!Use_MN200 && !Use_APS)    //沒有任何一張Motion卡
            {
                Tool.SaveLogToFile("Motion卡Initial失敗");
                return false;
            }

            Task task = Task.Run(() => Process());

            return true;
        }
        //[Read&Save Axis Information]
        public void SaveAxis(string filePath, string axisName, Dictionary<string, string> parameters)
        {
            XDocument doc;

            if (File.Exists(filePath))
                doc = XDocument.Load(filePath);
            else
                doc = new XDocument(new XElement("MachineConfig"));

            XElement root = doc.Element("MachineConfig");

            // 找出 Axis 節點
            var existingAxis = root.Elements("Axis")
                                   .FirstOrDefault(x => (string)x.Attribute("name") == axisName);

            if (existingAxis != null)
            {
                // 清空舊的參數
                existingAxis.RemoveNodes();
            }
            else
            {
                existingAxis = new XElement("Axis", new XAttribute("name", axisName));
                root.Add(existingAxis);
            }

            // 新增參數
            foreach (var kvp in parameters)
            {
                existingAxis.Add(new XElement("Parameter",
                    new XAttribute("key", kvp.Key),
                    new XAttribute("value", kvp.Value)
                ));
            }

            doc.Save(filePath);
        }
        #endregion
    }
}

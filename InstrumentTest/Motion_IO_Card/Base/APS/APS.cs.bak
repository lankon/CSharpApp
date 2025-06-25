using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InstrumentTest.IO_Card.Base.APS168_W64;
using InstrumentTest.IO_Card.Base.APS_Define_W32;

namespace InstrumentTest.IO_Card.Base
{
    class APS : Base_IO_Card
    {
        #region parameter define 
        APS_Parameter APS_Param = new APS_Parameter();
        
        struct APS_Parameter
        {
            public Int32 card_name;
            public Int32 MAX_DI_NUM;
            public bool[,,] InputStatus;   //紀錄[LineNo,DevNo,Port]對應的Input訊號
        }

        #endregion

        public APS()
        {

        }
        
        public override bool GetInputStatus(byte lineNo, byte DevNo, byte port)
        {
            throw new NotImplementedException();
        }

        public override string GetName()
        {
            if (APS_Param.card_name == (Int32)APS_Define.DEVICE_NAME_AMP_20408C)
                return "AMP_204C";

            return "None";
        }

        public override bool Open()
        {
            Int32 boardID_InBits = 0;
            Int32 mode = 0;
            Int32 ret = 0;
            Int32 StartAxisID = 0;
            Int32 TotalAxisNum = 0;

            ret = APS168.APS_initial(ref boardID_InBits, mode);
            
            if (ret != 0)
                return false;

            for (int i = 0; i < 16; i++)
            {
                int temp = (boardID_InBits >> i) & 1;

                if (temp != 1)
                    continue;

                ret = APS168.APS_get_card_name(i, ref APS_Param.card_name);

                if (APS_Param.card_name == (Int32)APS_Define.DEVICE_NAME_PCI_825458 ||
                    APS_Param.card_name == (Int32)APS_Define.DEVICE_NAME_AMP_20408C)
                {
                    ret = APS168.APS_get_first_axisId(i, ref StartAxisID, ref TotalAxisNum);

                    APS_Param.MAX_DI_NUM = 24;
                    APS_Param.InputStatus = new bool[5, 5, APS_Param.MAX_DI_NUM];

                    break;
                }
            }

            return false;
        }

        public override void UpdateInput(byte cardNo = 0, byte lineNo = 0, byte devNo = 0, byte port = 0)
        {
            Int32 digital_input_value = 0;

            APS168.APS_read_d_input(lineNo, 0 , ref digital_input_value);

            for (int i = 0; i < APS_Param.MAX_DI_NUM; i++)
            {
                int check = ((digital_input_value >> i) & 1);

                if (check == 1)
                    APS_Param.InputStatus[lineNo, devNo, i] = true;
                else
                    APS_Param.InputStatus[lineNo, devNo, i] = false;
            }
        }
    }
}

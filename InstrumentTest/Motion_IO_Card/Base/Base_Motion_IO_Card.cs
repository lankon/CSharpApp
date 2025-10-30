using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentTest.Motion_IO_Card.Base
{
    enum MOTION_IO
    {
        ALM,
        PEL,
        MEL,
        ORG,
        SVON,
        INP,
        RDY,
    }

    

    public abstract class Base_Motion_IO_Card
    {
        public struct AXIS_INFO
        {
            //[Axis Configuration]
            public string AXIS_TYPE;    //軸卡名稱
            public int LINE_NO;         //軸卡線程
            public int DEV_NO;          //軸卡軸編號
            public int AXIS_USE;        //軸卡使用Y/N
            public int LIMIT_LOGIC;     //硬體極限觸發邏輯
            public int STOP_MODE;       //停止模式

            //[Software Configuration]
            public string AXIS_NANE;    //軸名稱
            public int SW_LIMIT;        //軟體極限Y/N
            public double PEL_POS;      //軟體正極限位置
            public double MEL_POS;      //軟體負極限位置
            public int REVERSE_MOVE;    //運動方向相反Y/N

            //[Home Configuration]
            public int MODE;            //歸Home模式
            public int DIRECTION;       //方向
            public int HOME_POS;        //原點位置
            public int HOME_SHIFT;      //到原點後位移距離
            public int MAX_VELOCITY;    //最大速度
            public int HOEM_FIND_ORG_VELOCITY;  //搜尋原點速度
            public int ACC;             //加速度
        }


        #region abstract
        public abstract bool Open();

        //[Motion Function]
        public abstract bool SetMotionConfig();
        public abstract short UpdateMotionStatus(byte cardNo = 0, byte lineNo = 0, byte devNo = 0);
        public abstract bool GetMotionStatus(byte cardNo = 0, byte lineNo = 0, byte devNo = 0, int state = 0);
        public abstract bool GetMotionComplete(byte cardNo = 0, byte lineNo = 0, byte devNo = 0);
        public abstract bool Servo_ONOff(byte cardNo = 0, byte lineNo = 0, byte devNo = 0, bool flag = false);
        //public abstract bool SetGoHomeParam(AXIS_INFO hOME_PARAM);
        public abstract bool GoHome(byte cardNo = 0, byte lineNo = 0, byte devNo = 0);
        public abstract double GetPosition(byte cardNo = 0, byte lineNo = 0, byte devNo = 0);
        public abstract int SetPosition(byte cardNo = 0, byte lineNo = 0, byte devNo = 0, double pos = 0);
        public abstract int AbsoluteSMove(int axis, double position, double velocity_max, double velocity_start,
                                          double Tacc, double Sacc, double Tdec, double Sdec);
        public abstract int RelativeSMove(int axis, double position, double velocity_max, double velocity_start,
                                          double Tacc, double Sacc, double Tdec, double Sdec);


        //[IO Function]
        public abstract string GetName();
        public abstract void UpdateInput(byte cardNo = 0, byte lineNo = 0, byte devNo = 0, byte port = 0);
        public abstract bool GetInputStatus(byte lineNo, byte DevNo, byte port);
        public abstract void UpdateOutput(byte cardNo = 0, byte lineNo = 0, byte devNo = 0, byte port = 0);
        public abstract bool GetOutputStatus(byte lineNo, byte DevNo, byte port);
        public abstract bool SetOutputStatus(byte cardNo = 0, byte lineNo = 0, byte devNo = 0, byte port = 0, bool truefalse = false);
        #endregion

        #region virtual
        // Motion Function
        public virtual List<byte> Get_Motion_LineNo()
        {
            List<byte> temp = new List<byte>();
            return temp;
        }
        public virtual List<byte> Get_Motion_DevNo()
        {
            List<byte> temp = new List<byte>();
            return temp;
        }

        // IO Function
        public virtual List<byte> Get_IO_LineNo()
        {
            List<byte> temp = new List<byte>();
            return temp;
        }
        public virtual List<byte> Get_IO_DevNo()
        {
            List<byte> temp = new List<byte>();
            return temp;
        }
        #endregion
    }


}

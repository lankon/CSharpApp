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
        #region abstract
        public abstract bool Open();

        //[Motion Define]
        public struct HOME_PARAM
        {
            public int MODE;            //歸Home模式
            public int DIRECTION;       //方向
            public int ACC;             //加速度
            public int MAX_VELOCITY;    //最大速度
        }

        //[Motion Function]
        public abstract bool SetMotionConfig();
        public abstract short UpdateMotionStatus(byte cardNo = 0, byte lineNo = 0, byte devNo = 0);
        public abstract bool GetMotionStatus(byte cardNo = 0, byte lineNo = 0, byte devNo = 0, int state = 0);
        public abstract bool GetMotionComplete(byte cardNo = 0, byte lineNo = 0, byte devNo = 0);
        public abstract bool Servo_ONOff(byte cardNo = 0, byte lineNo = 0, byte devNo = 0, bool flag = false);
        public abstract bool SetGoHomeParam(HOME_PARAM hOME_PARAM);
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

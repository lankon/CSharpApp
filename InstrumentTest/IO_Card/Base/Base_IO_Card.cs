using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentTest.IO_Card.Base
{
    

    public abstract class Base_IO_Card
    {
        #region abstract
        public abstract bool Open();
        public abstract string GetName();
        public abstract void UpdateInput(byte lineNo, byte devNo);
        public abstract bool GetInputStatus(byte lineNo, byte DevNo, byte port);
        #endregion

        #region virtual
        public virtual List<byte> GetLineNo()
        {
            List<byte> temp = new List<byte>();
            return temp;
        }
        public virtual List<byte> GetDevNo()
        {
            List<byte> temp = new List<byte>();
            return temp;
        }
        #endregion
    }


}

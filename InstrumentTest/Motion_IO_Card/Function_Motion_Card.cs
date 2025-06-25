using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InstrumentTest.Motion_IO_Card.Base;

namespace InstrumentTest.Motion_IO_Card
{
    class Function_Motion_Card
    {
        public bool Initial_All_Motion()
        {
            Base_Motion_IO_Card mN200 = new MN200();

            if (!mN200.Open())
                return false;


            return true;
        }
    }
}

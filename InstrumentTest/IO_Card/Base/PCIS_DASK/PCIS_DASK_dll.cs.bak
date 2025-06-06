using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentTest.IO_Card.Base.PCIS_DASK
{
    public class Param
    {
        //ADLink PCI Card Type
        public const ushort PCI_9111DG = 20;

        //Channel Count
        public const ushort P9111_CHANNEL_DI = 0;
    }
    public class Functions
    {
        
        [DllImport("PCI-Dask.dll")]
        public static extern short Register_Card(ushort CardType, ushort card_num);

        [DllImport("PCI-Dask.dll")]
        public static extern short DI_ReadLine(ushort CardNumber, ushort Port, ushort Line, out ushort State);

    }
}

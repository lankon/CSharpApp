using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentTest.Motion_IO_Card.Base.PISO_P32C32
{
    public class Functions
    {
        [DllImport("UniDAQ.dll", EntryPoint = "Ixud_DriverInit", CallingConvention = CallingConvention.Cdecl)]
        public static extern int DriverInit(out ushort cardCount);

        [DllImport("UniDAQ.dll", EntryPoint = "Ixud_DriverClose", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Ixud_DriverClose();

        [DllImport("UniDAQ.dll", EntryPoint = "Ixud_ReadDI", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Ixud_ReadDI(ushort card, ushort port, out uint cardCount);

        [DllImport("UniDAQ.dll", EntryPoint = "Ixud_WriteDOBit", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Ixud_WriteDOBit(ushort card, ushort port, ushort bit, ushort status);

    }
}

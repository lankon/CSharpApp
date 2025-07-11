using NHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentTest.Motion_IO_Card.Base.PISO_P32C32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct IXUD_DEVICE_INFO
    {
        public uint dwSize;
        public ushort wVendorID;
        public ushort wDeviceID;
        public ushort wSubVendorID;
        public ushort wSubDeviceID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public uint[] dwBAR;
        public byte BusNo;
        public byte DevNo;
        public byte IRQ;
        public byte Aux;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public uint[] dwBarVirtualAddress;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct IXUD_CARD_INFO
    {
        public uint dwSize;                 // DWORD -> uint
        public uint dwModelNo;              // DWORD -> uint

        public byte CardID;                 // UCHAR -> byte
        public byte wSingleEnded;           // UCHAR -> byte

        public ushort wAIOResolution;       // WORD -> ushort
        public ushort wAIChannels;          // WORD -> ushort
        public ushort wAOChannels;          // WORD -> ushort
        public ushort wDIPorts;             // WORD -> ushort
        public ushort wDOPorts;             // WORD -> ushort
        public ushort wDIOPorts;            // WORD -> ushort
        public ushort wDIOPortWidth;        // WORD -> ushort
        public ushort wCounterChannels;     // WORD -> ushort
        public ushort wMemorySize;          // WORD -> ushort

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public uint[] dwReserved1;          // DWORD[6] -> uint[6]
    }

    public class Param
    {
        
        
        // ID
        public const uint PISO_P16R16U = 0x18000000;    // for PISO-P16R16U
        public const uint PISO_C64 = 0x800800;    // for PISO-C64
        public const uint PISO_P64 = 0x800810;    // for PISO-P64
        public const uint PISO_A64 = 0x800850;    // for PISO-A64
        public const uint PISO_P32C32 = 0x800820;    // for PISO-P32C32 
        public const uint PISO_P32A32 = 0x800870;    // for PISO-P32A32 
        public const uint PISO_P8R8 = 0x800830;    // for PISO-P8R8
        public const uint PISO_P8SSR8AC = 0x800830;    // for PISO-P8SSR8AC
        public const uint PISO_P8SSR8DC = 0x800830;    // for PISO-P8SSR8DC
        public const uint PISO_730 = 0x800840;    // for PISO-730
        public const uint PISO_730A = 0x800880;    // for PISO-730A
    }

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

        [DllImport("UniDAQ.dll", EntryPoint = "Ixud_GetCardInfo", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int Ixud_GetCardInfo(ushort board_no, ref IXUD_DEVICE_INFO dev_info, ref IXUD_CARD_INFO card_info, StringBuilder szModelName);

        [DllImport("UniDAQ.dll", EntryPoint = "Ixud_SoftwareReadbackDO", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Ixud_SoftwareReadbackDO(ushort board_no, ushort port, out uint status);
    }
}

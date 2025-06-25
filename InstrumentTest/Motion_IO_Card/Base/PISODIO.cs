using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace InstrumentTest
{

    public class CardInfo
    {
        public const uint TotalPort = 4;

        public uint BaseAddress;
        public ushort Irq;
        public ushort SubVendor;
        public ushort SubDevice;
        public ushort SubAux;
        public ushort SlotBus;
        public ushort SlotDevice;

        public ushort[] OutputState = new ushort[TotalPort];

        public bool IsOK;
    }

    public class PISODIO
    {
        // return code
        public const uint NoError = 0;
        public const uint DriverOpenError = 1;
        public const uint DriverNoOpen = 2;
        public const uint GetDriverVersionError = 3;
        public const uint InstallIrqError = 4;
        public const uint ClearIntCountError = 5;
        public const uint GetIntCountError = 6;
        public const uint RegisterApcError = 7;
        public const uint RemoveIrqError = 8;
        public const uint FindBoardError = 9;
        public const uint ExceedBoardNumber = 10;
        public const uint ResetError = 11;

        // to trigger a interrupt when high -> low
        public const ushort ActiveLow = 0;
        // to trigger a interrupt when low -> high
        public const ushort ActiveHigh = 1;

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

        private ushort TotalCard = 0;

        CardInfo[] _CardInfo;

        public virtual bool Initialize()
        {
            ushort res = DriverInit();

            if (res != PISODIO.NoError)
            {
                return false;
            }
            
            if (PISODIO.SearchCard(out TotalCard, PISODIO.PISO_P32C32) != PISODIO.NoError)
            {
                return false;
            }

            _CardInfo = new CardInfo[TotalCard];

            for (int k = 0; k < TotalCard; k++)
            {
                _CardInfo[k] = new CardInfo();

                var card_info = _CardInfo[k];

                ushort ret_n = PISODIO.GetConfigAddressSpace(
                                  (ushort)k, out card_info.BaseAddress, out card_info.Irq,
                                  out card_info.SubVendor, out card_info.SubDevice, out card_info.SubAux,
                                  out card_info.SlotBus, out card_info.SlotDevice);

                if (ret_n == PISODIO.NoError)
                {
                    card_info.IsOK = true;

                    PISODIO.OutputByte(card_info.BaseAddress, 1);

                    int j;
                    for (j = 0; j < CardInfo.TotalPort; j++)
                    {
                        PISODIO.OutputByte((card_info.BaseAddress + 0xC0 + 0x4 * (uint)j), 0);

                        card_info.OutputState[j] = 0;
                    }
                }
                else
                {
                    card_info.IsOK = false;
                    return false;
                }
            }

            return true;
        }

        public virtual bool GetInBitState(int CardNumber, int LineNumber)
        {
            if (CardNumber < TotalCard && CardNumber >= 0
                && LineNumber >= 0 && LineNumber < (CardInfo.TotalPort * 8))
            {
                var f = _CardInfo[CardNumber];

                uint port_number = (uint)(LineNumber / 8);
                uint line_number = (uint)(LineNumber % 8);

                ushort DI_value
                    = PISODIO.InputByte(f.BaseAddress + 0xC0 + 0x4 * port_number);

                bool s =
                    (DI_value & (1 << (byte)line_number)) >= 1;

                return s;
            }
            else return false;
        }

        public virtual void SetOutBitState(int CardNumber, int LineNumber, bool s1)
        {
            bool s = s1;

            if (CardNumber < TotalCard && CardNumber >= 0
                && LineNumber >= 0 && LineNumber < (CardInfo.TotalPort * 8))
            {
                var f = _CardInfo[CardNumber];

                uint port_number = (uint)(LineNumber / 8);
                byte line_number = (byte)(LineNumber % 8);

                ushort do_state = f.OutputState[port_number];

                if (s)
                    do_state |= (ushort)(1 << (byte)line_number);
                else
                    do_state &= (ushort)(0xffff ^ (1 << (byte)line_number));

                PISODIO.OutputByte(f.BaseAddress + 0xC0 + 0x4 * port_number, do_state);

                f.OutputState[port_number] = do_state;
            }
        }

        public virtual void Close()
        {
            try
            {
                PISODIO.DriverClose();
            }
            catch { }
        }

        // Test functions
        [DllImport("PISODIO.dll", EntryPoint = "PISODIO_FloatSub")]
        internal static extern float FloatSub(float fA, float fB);
        [DllImport("PISODIO.dll", EntryPoint = "PISODIO_ShortSub")]
        internal static extern short ShortSub(short nA, short nB);
        [DllImport("PISODIO.dll", EntryPoint = "PISODIO_GetDllVersion")]
        internal static extern ushort GetDllVersion();

        // Driver functions
        [DllImport("PISODIO.dll", EntryPoint = "PISODIO_DriverInit")]
        internal static extern ushort DriverInit();
        [DllImport("PISODIO.dll", EntryPoint = "PISODIO_DriverClose")]
        internal static extern void DriverClose();
        [DllImport("PISODIO.dll", EntryPoint = "PISODIO_SearchCard")]
        internal static extern ushort SearchCard(out ushort wBoards, uint dwPIOCardID);
        [DllImport("PISODIO.dll", EntryPoint = "PISODIO_GetDriverVersion")]
        internal static extern ushort GetDriverVersion(out ushort wDriverVersion);
        [DllImport("PISODIO.dll", EntryPoint = "PISODIO_GetConfigAddressSpace")]
        internal static extern ushort GetConfigAddressSpace(ushort wBoardNo, out uint wAddrBase, out ushort wIrqNo,
                                                                                             out ushort wSubVendor, out ushort wSubDevice, out ushort wSubAux,
                                                                                             out ushort wSlotBus, out ushort wSlotDevice);
        [DllImport("PISODIO.dll", EntryPoint = "PISODIO_ActiveBoard")]
        internal static extern ushort ActiveBoard(ushort wBoardNo);
        [DllImport("PISODIO.dll", EntryPoint = "PISODIO_WhichBoardActive")]
        internal static extern ushort WhichBoardActive();

        // DIO functions
        [DllImport("PISODIO.dll", EntryPoint = "PISODIO_OutputWord")]
        internal static extern void OutputWord(uint wPortAddress, uint wOutData);
        [DllImport("PISODIO.dll", EntryPoint = "PISODIO_OutputByte")]
        internal static extern void OutputByte(uint wPortAddr, ushort bOutputValue);
        [DllImport("PISODIO.dll", EntryPoint = "PISODIO_InputWord")]
        internal static extern uint InputWord(uint wPortAddress);
        [DllImport("PISODIO.dll", EntryPoint = "PISODIO_InputByte")]
        internal static extern ushort InputByte(uint wPortAddr);

        // Interrupt functions
        [DllImport("PISODIO.dll", EntryPoint = "PISODIO_IntInstall")]
        internal static extern ushort IntInstall(ushort wBoardNo, out uint hEvent, ushort wInterruptSource, ushort wActiveMode);
        [DllImport("PISODIO.dll", EntryPoint = "PISODIO_IntRemove")]
        internal static extern ushort IntRemove();
        [DllImport("PISODIO.dll", EntryPoint = "PISODIO_IntResetCount")]
        internal static extern ushort IntResetCount();
        [DllImport("PISODIO.dll", EntryPoint = "PISODIO_IntGetCount")]
        internal static extern ushort IntGetCount(out uint dwIntCount);


    }

    //public class PISODIO_Null : PISODIO
    //{
    //    public override bool Initialize(bool DoCardInit = true)
    //    {
    //        return false;
    //    }

    //    public override bool GetInBitState(int CardNumber, int LineNumber)
    //    {
    //        return false;
    //    }

    //    public override void SetOutBitState(int CardNumber, int LineNumber, bool s1)
    //    {
             
    //    }

    //    public override void Close()
    //    {
            
    //    }
    //}
}

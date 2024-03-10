using System;
using System.IO;
using System.IO.Ports;


namespace CommonFunction
{
    class Rs485
	{
        SerialPort serialPort = new SerialPort();
        private bool IsOpen = false;

        public bool Open(string port_name, int baud_rate, int parity, int data_bit, int stop_bit)
        {
            // 設置串行端口的參數
            serialPort.PortName = port_name; // 例子中使用 COM1，實際使用時請根據你的情況修改
            serialPort.BaudRate = baud_rate; // 設置波特率
            
            if(parity == 0)
                serialPort.Parity = Parity.None; // 設置奇偶校驗位
            
            serialPort.DataBits = data_bit; // 設置數據位
            
            if(stop_bit == 1)
                serialPort.StopBits = StopBits.One; // 設置停止位

            try
            {
                serialPort.Open(); // 打開串行端口
                IsOpen = true;

                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SenMsg(byte[] msg, int offset, int data_len)
        {
            ClearBuffer();

            if (IsOpen == true)
            {
                try
                {
                    serialPort.Write(msg, offset, data_len);
                    return true;
                }
                catch
                {
                    return false;
                }
                
            }
            else
            {
                return false;
            }
        }
        public byte[] ReadMsg(byte[] msg, int offset, int data_len)
        {
            serialPort.Read(msg, offset, data_len);

            return msg;
        }
        public void ClearBuffer()
        {
            serialPort.DiscardOutBuffer();
            serialPort.DiscardInBuffer();
        }
        public bool Close()
        {
            try
            {
                if(serialPort != null)
                    serialPort.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }


	}
}



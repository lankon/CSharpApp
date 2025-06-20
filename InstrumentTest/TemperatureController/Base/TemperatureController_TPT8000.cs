﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using System.Threading;
using CommonFunction;

namespace InstrumentTest
{
    public class TemperatureController_TPT8000 : BaseTemperatureController
    {
        #region parameter define
        private static SerialPort Comport = new SerialPort();


        //private int DataReceivedLength = 0;
        //private byte[] DataReceivedBuf = new byte[11];
        String FivePointValue = "Tmp,Temp,Temp,-99,-99,-99,-99,-99"; 
        double Offset_Value;
        int err_Same_Count = 0;
        public int[] UseCtrl = new int[10]; //用來判斷老化板是否使用
        String err_SameValue;
        TC_STATE c_STATE = TC_STATE.IDLE;

        enum TC_STATE
        {
            IDLE,
            WORKING,
        }

        //double TemperatureValue1, TemperatureValue2, TemperatureValue3;
        //double TemperatureValue4, TemperatureValue5;

        //double CompensateValue1, CompensateValue2, CompensateValue3;
        //double CompensateValue4, CompensateValue5;

        //double GetTempOffsetValue1, GetTempOffsetValue2, GetTempOffsetValue3;
        //double GetTempOffsetValue4, GetTempOffsetValue5;
        #endregion

        #region Public Function       
        public override bool Open(String com, String baudrate, String parity)
        {
            Comport.PortName = com;
            Comport.BaudRate = int.Parse(baudrate);
            Comport.DataBits = int.Parse("8");
            Comport.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "One");
            Comport.Parity = (Parity)Enum.Parse(typeof(Parity), parity);
            Comport.ReadTimeout = 2000;

            if (Comport.PortName == "None")
                return false;

            if (!Comport.IsOpen)
            {
                try
                {
                    Comport.Open();
                }
                catch(Exception ex)
                {
                    Tool.SaveHistoryToFile($"TemperatureController_TPT8000:Open Fail{ex}");
                    return false;
                }
            }
            return true;
        }
        public override bool Close()
        {
            bool res = true;

            if (Comport.IsOpen)
            {
                try
                {
                    Comport.DiscardOutBuffer();
                    Comport.DiscardInBuffer();
                    Comport.Close();

                }
                catch (Exception ex)
                {
                    Tool.SaveHistoryToFile($"TemperatureController_TPT8000:Close Fail{ex}");
                    res = false;
                }
            }

            Comport.Dispose();

            return res;
        }
        public override bool AskPV()
        {
            int ctrl_box = ApplicationSetting.Get_Int_Recipe((int)eFormAppSet.Cmbx_CtrlBox);
            string ch = ApplicationSetting.Get_String_Recipe((int)eFormAppSet.TxtBx_Board_CH);

            if (ctrl_box > 0)
                ctrl_box++;
          
            bool res = false;
            String ReadTemperature_Order = $"B{ctrl_box.ToString("00")},GTEMP,{ch}\r\n";
            try
            {
                Clear();
                byte[] cmd = Encoding.Default.GetBytes(ReadTemperature_Order);
                if (cmd != null)
                {
                    Comport.Write(ReadTemperature_Order);
                    res = true;
                }
                else
                {
                    //WriteLogData($"Read Temperature Fail");
                }
            }
            catch (Exception)
            {
                //WriteLogData($"Read Temperature Success: {ex}");
            }

            return res;
        }
        public override bool AskPV(int ctrl_box, string ch)
        {
            bool res = false;
            String ReadTemperature_Order = $"B{ctrl_box.ToString("00")},GTEMP,{ch}\r\n";
            try
            {
                Clear();
                byte[] cmd = Encoding.Default.GetBytes(ReadTemperature_Order);
                if (cmd != null)
                {
                    Comport.Write(ReadTemperature_Order);
                    res = true;
                }
                else
                {
                    //WriteLogData($"Read Temperature Fail");
                }
            }
            catch (Exception)
            {
                //WriteLogData($"Read Temperature Success: {ex}");
            }

            return res;
        }
        public override String GetAns()
        {
            try
            {
                String receive_msg = "";
                String temp = "";
                receive_msg = Comport.ReadLine();
                FivePointValue = receive_msg;
                //WriteLogData($"Receive ComportString {FivePointValue}");
                temp = receive_msg;

                if (err_SameValue == receive_msg)
                {
                    err_Same_Count++;
                }
                else
                {
                    err_Same_Count = 0;
                }
                err_SameValue = receive_msg;
                if (err_Same_Count > 2000)
                    return "-999";

                if (receive_msg.Contains("\r"))
                {
                    temp = temp.Substring(12, 5);

                    double Now_Value = Convert.ToDouble(temp);
                    double CalculateOffset = 0;
                    
                    //if (ApplicationSetting.Get_Int_Recipe((int)eFormAppSet.Cmbx_CalibrationFunc) == 0)
                    //    CalculateOffset = LinearityCalibration_Ans(Now_Value);
                    //else if (ApplicationSetting.Get_Int_Recipe((int)eFormAppSet.Cmbx_CalibrationFunc) == 1)
                    //    CalculateOffset = FivePointCalibration_Ans(temp);

                    Now_Value += CalculateOffset;

                    temp = Convert.ToString(Now_Value);
                    //WriteLogData($"TPT Receive Present Value{temp}");
                    return temp;
                }
                else
                {
                    return "error";
                }
            }
            catch (Exception)
            {
                //WriteLogData($"TPT GetAns exception: {ex}");
                FivePointValue = "-99.0,-99.0,-99.0,-99.0,-99.0";
                return "error";
            }
        }       
        public override bool Start()
        {
            int ctrl_box = ApplicationSetting.Get_Int_Recipe((int)eFormAppSet.Cmbx_CtrlBox);         
            string ch = ApplicationSetting.Get_String_Recipe((int)eFormAppSet.TxtBx_Board_CH);
            
            ReadTempOffsetFile(ctrl_box, Tool.StringToInt(ch));

            if (ctrl_box >= 1)   //TPT8000韌體端沒有編號1控制箱指令
                ctrl_box++;

            double SV_Value = 25.0f;

            if(ApplicationSetting.Get_Int_Recipe((int)eFormAppSet.Cmbx_CalibrationFunc) == 0)
                SV_Value = LinearityCalibration();
            else if(ApplicationSetting.Get_Int_Recipe((int)eFormAppSet.Cmbx_CalibrationFunc) == 1)
                SV_Value = FivePointCalibration();

            bool res = false;
            String SetTemperature_Order = $"B{ctrl_box.ToString("00")},STEMP,1,{SV_Value},1,{ch}\r\n";
            Tool.SaveHistoryToFile($"B{ctrl_box.ToString("00")},STEMP,1,{SV_Value},1,{ch}");
            try
            {
                Clear();
                byte[] cmd = Encoding.Default.GetBytes(SetTemperature_Order);
                if (cmd != null)
                {
                    Comport.Write(SetTemperature_Order);
                    res = true;
                }
                else
                {
                    Tool.SaveHistoryToFile("TemperatureController_TPT8000:Start Fail");
                }
            }
            catch (Exception)
            {
                //Tool.SaveHistoryToFile($"TemperatureController_TPT8000:Start Fail{ex}");
            }
            return res;
        }
        public override bool Start(int ctrl_box, string ch, int Value)
        {
            int temp_ctrl_box = ctrl_box;

            if (temp_ctrl_box > 0)   //為了配合主程式ReadTempOffsetFile所以減1
                temp_ctrl_box--;

            ReadTempOffsetFile(temp_ctrl_box, Tool.StringToInt(ch));

            double Compensate = 0, Temperature = 0;
            //int Value = ApplicationSetting.Get_Int_Recipe((int)eFormAppSet.TxtBx_Target);

            if (Value > 180 && Value <= 200)
            {
                Temperature = ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Temp1);
                Compensate = ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Comp1);
            }
            else if (Value > 151 && Value <= 180)
            {
                Temperature = ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Temp2);
                Compensate = ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Comp2);
            }
            else if (Value >= 120 && Value <= 150)
            {
                Temperature = ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Temp3);
                Compensate = ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Comp3);
            }
            else if (Value >= 85 && Value < 120)
            {
                Temperature = ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Temp4);
                Compensate = ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Comp4);
            }
            else if (Value < 85)
            {
                Temperature = ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Temp5);
                Compensate = ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Comp5);
            }

            double CorrectValue = Compensate / (200 - Temperature);
            Offset_Value = Math.Round(CorrectValue * (Value - Temperature), 1);
            var SV_Value = Math.Round(Value + CorrectValue * (Value - Temperature), 1);

            bool res = false;
            String SetTemperature_Order = $"B{ctrl_box.ToString("00")},STEMP,1,{SV_Value},1,{ch}\r\n";
            Tool.SaveHistoryToFile($"B{ctrl_box.ToString("00")},STEMP,1,{SV_Value},1,{ch}");
            try
            {
                Clear();
                byte[] cmd = Encoding.Default.GetBytes(SetTemperature_Order);
                if (cmd != null)
                {
                    Comport.Write(SetTemperature_Order);
                    res = true;
                }
                else
                {
                    Tool.SaveHistoryToFile("TemperatureController_TPT8000:Start Fail");
                }
            }
            catch (Exception)
            {
                //Tool.SaveHistoryToFile($"TemperatureController_TPT8000:Start Fail{ex}");
            }
            return res;
        }
        public override bool Stop()
        {
            int ctrl_box = ApplicationSetting.Get_Int_Recipe((int)eFormAppSet.Cmbx_CtrlBox);

            if (ctrl_box >= 1)   //TPT8000韌體端沒有編號B01控制箱指令
                ctrl_box++;

            string ch = ApplicationSetting.Get_String_Recipe((int)eFormAppSet.TxtBx_Board_CH);

            bool res = false;
            String SetTemperature_Order = $"B{ctrl_box.ToString("00")},STEMP,0,20,1,{ch}\r\n";
            Tool.SaveHistoryToFile($"B{ctrl_box.ToString("00")},STEMP,0,20,1,{ch}");
            try
            {
                Clear();
                byte[] cmd = Encoding.Default.GetBytes(SetTemperature_Order);
                if (cmd != null)
                {
                    Comport.Write(SetTemperature_Order);
                    res = true;
                }
                else
                {
                    Tool.SaveHistoryToFile("TemperatureController_TPT8000:Stop Fail");
                }
            }
            catch (Exception ex)
            {
                Tool.SaveHistoryToFile($"TemperatureController_TPT8000:Stop Fail{ex}");
            }
            return res;
        }
        public override bool Stop(int ctrl_box, string ch)
        {
            if (ctrl_box >= 1)   //TPT8000韌體端沒有編號B01控制箱指令
                ctrl_box++;

            bool res = false;
            String SetTemperature_Order = $"B{ctrl_box.ToString("00")},STEMP,0,20,1,{ch}\r\n";
            Tool.SaveHistoryToFile($"B{ctrl_box.ToString("00")},STEMP,0,20,1,{ch}");
            try
            {
                Clear();
                byte[] cmd = Encoding.Default.GetBytes(SetTemperature_Order);
                if (cmd != null)
                {
                    Comport.Write(SetTemperature_Order);
                    res = true;
                }
                else
                {
                    Tool.SaveHistoryToFile("TemperatureController_TPT8000:Stop Fail");
                }
            }
            catch (Exception ex)
            {
                Tool.SaveHistoryToFile($"TemperatureController_TPT8000:Stop Fail{ex}");
            }
            return res;
        }
        public void ReadTempOffsetFile(int ControlNum, int ChuckNum)
        {
            String Line,Temp_Line = "";
            Tool.SaveHistoryToFile("讀取溫度校正值");

            if (ControlNum >= 1)    //TPT8000韌體端沒有編號1控制箱指令
                ControlNum++;

            String FileName = "T" + (ControlNum) + "C" + (ChuckNum);
            String FilePath = System.IO.Directory.GetCurrentDirectory();
            FilePath = FilePath + "\\TemperatureController\\" + "TPT8000_Controller" + ".txt";

            StreamReader sw = null;

            try
            {
                sw = new StreamReader(FilePath);
            }
            catch (Exception ex)
            {
                Tool.SaveHistoryToFile($"TemperatureController_TPT8000:ReadTempOffsetFile Fail{ex}");

                //讀檔失敗填入預設值
                //ApplicationSetting.SetRecipe((int)eFormAppSet.TxtBx_Board_CH, "-1");
                for (int j = 0; j < 5; j++)
                {
                    ApplicationSetting.SetRecipe((int)eFormAppSet.TxtBx_Temp1 + j, "0");
                    ApplicationSetting.SetRecipe((int)eFormAppSet.TxtBx_Comp1 + j, "0");
                    ApplicationSetting.SetRecipe((int)eFormAppSet.TxtBx_Offset1 + j, "0");
                }

                return;
            }
            int i = 0;
            char[] SplitMark = { ',', '\\' };
            bool StartRead = false;
            bool bCheckCompensate = false;

            while ((Line = sw.ReadLine()) != null)
            {
                if (Line == "#END")
                {
                    StartRead = false;
                    Temp_Line = Line;
                }
                    
                if (Temp_Line == $"#TEMPERATURE_COMPENSATE_T{ControlNum}C{ChuckNum}")
                {
                    StartRead = true;
                    bCheckCompensate = true;
                    String[] SplitStr = Line.Split(SplitMark);

                    ApplicationSetting.SetRecipe((int)eFormAppSet.TxtBx_Temp1 + i, SplitStr[0]);
                    ApplicationSetting.SetRecipe((int)eFormAppSet.TxtBx_Comp1 + i, SplitStr[1]);
                    ApplicationSetting.SetRecipe((int)eFormAppSet.TxtBx_Offset1 + i, SplitStr[2]);

                    i++;
                }

                if (Temp_Line == $"#USE_CTRL_BOX")
                {
                    StartRead = true;
                    String[] SplitStr = Line.Split(SplitMark);

                    int board_num = Int32.Parse(SplitStr[0]);
                    int use = Int32.Parse(SplitStr[1]);

                    UseCtrl[board_num] = use;
                }

                if (StartRead == false)
                    Temp_Line = Line;
                
            }

            if(bCheckCompensate == false)
            {
                Tool.SaveHistoryToFile($"TemperatureController_TPT8000:ReadTempOffsetFile Fail");

                //讀檔失敗填入預設值
                //ApplicationSetting.SetRecipe((int)eFormAppSet.TxtBx_Board_CH, "-1");
                for (int j = 0; j < 5; j++)
                {
                    ApplicationSetting.SetRecipe((int)eFormAppSet.TxtBx_Temp1 + j, "0");
                    ApplicationSetting.SetRecipe((int)eFormAppSet.TxtBx_Comp1 + j, "0");
                    ApplicationSetting.SetRecipe((int)eFormAppSet.TxtBx_Offset1 + j, "0");
                }
            }

            sw.Close();
        }
        public override String GetFivePointValue()
        {
            string value = FivePointValue;
            char[] separator = new char[] { ',' };
            String[] values = value.Split(separator);
            String value1 = "";
            if (values.Length >= 8)
            {
                for (int i = 3; i < 8; i++)
                {
                    double temp = Convert.ToDouble(values[i]);
                    temp -= Offset_Value;
                    values[i] = temp.ToString();
                    if (i == 7)
                        value1 += values[i];
                    else
                        value1 = value1 + values[i] + ",";
                }
            }
            else
            {
                value1 = "-99,-99,-99,-99,-99";
            }
            return value1;
        }

        //public string AnsPID(byte[] PID_ans, int PID_length)
        //{
        //    int num = Comport.BytesToRead;  //獲取接收緩衝區中的字串數
        //    byte[] received_buf = new byte[num];
        //    Comport.Read(received_buf, 0, num); //讀取緩衝區內的資料

        //    DataReceivedBuf = received_buf;
        //    DataReceivedLength = num;

        //    byte[] PID_temp = new byte[6];

        //    if (PID_length == 11)
        //    {
        //        PID_temp[0] = PID_ans[3];
        //        PID_temp[1] = PID_ans[4];
        //        PID_temp[2] = PID_ans[5];
        //        PID_temp[3] = PID_ans[6];
        //        PID_temp[4] = PID_ans[7];
        //        PID_temp[5] = PID_ans[8];
        //        double P_buf = (double)(((PID_temp[0] & 0xFF) << 8) | (PID_temp[1] & 0xFF));
        //        double P_Value = (P_buf / 10);
        //        int I_buf = (int)(((PID_temp[2] & 0xFF) << 8) | (PID_temp[3] & 0xFF));
        //        int D_buf = (int)(((PID_temp[4] & 0xFF) << 8) | (PID_temp[5] & 0xFF));
        //        return "P:" + Convert.ToDouble(P_Value).ToString("0.0") + "  I:" + Convert.ToString(I_buf) + "  D:" + Convert.ToString(D_buf);
        //    }
        //    else
        //    {
        //        return "error";
        //    }
        //}
        //public bool DoandConfirmHeatOperation_FLCR()
        //{
        //    bool res = false;
        //    String HeatOperation_Order = "B00,FCLR,1\r\n";
        //    try
        //    {
        //        Clear();
        //        byte[] cmd = Encoding.Default.GetBytes(HeatOperation_Order);
        //        if (cmd != null)
        //        {
        //            Comport.Write(HeatOperation_Order);
        //            res = true;
        //        }
        //        else
        //        {
        //            WriteLogData($"Confirm Heat Operation Fail");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLogData($"Confirm Heat Operation Success: {ex}");
        //    }            
        //    return res;
        //}
        //public bool SetValue(int value)
        //{
        //    try
        //    {
        //        Clear();

        //        int sv_buf = Convert.ToInt32(value);
        //        int sv_value = sv_buf * 10;

        //        byte temp_value_1 = Convert.ToByte(sv_value / 256);
        //        byte temp_value_2 = Convert.ToByte(sv_value % 256);

        //        byte[] temp = new byte[9];
        //        temp[0] = 0x01;
        //        temp[1] = 0x10;
        //        temp[2] = 0x63;
        //        temp[3] = 0x8E;
        //        temp[4] = 0x00;
        //        temp[5] = 0x01;
        //        temp[6] = 0x02;
        //        temp[7] = temp_value_1;
        //        temp[8] = temp_value_2;
        //        byte[] crc = CRC16(temp);

        //        byte[] cmd = new byte[11];
        //        for (int i = 0; i < 9; i++)
        //        {
        //            cmd[i] = temp[i];
        //        }
        //        cmd[9] = crc[0];
        //        cmd[10] = crc[1];

        //        bool res = Write(cmd, 0, cmd.Length);
        //        return res;
        //    }
        //    catch (Exception ex)
        //    {
        //        Tool.SaveHistoryToFile($"TemperatureController_TPT8000:SetValue Fail{ex}");
        //        return false;
        //    }
        //}
        //public bool CheckEndCode() 
        //{
        //    bool res = false;
        //    try
        //    {
        //        String receive_msg = "";
        //        receive_msg = Comport.ReadLine();
        //        if (receive_msg.Contains("\r"))
        //        {
        //            receive_msg += "\n";
        //            WriteLogData(receive_msg);
        //            res = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLogData($"TPT CheckEndCode exception: {ex}");
        //    }
        //    return res;
        //}
        //public bool SetPVOffset(int set_temp_value, int high_temp, int low_temp, double high_comp, double low_comp)
        //{
        //    try
        //    {
        //        Clear();

        //        var cal_value = 10 * ((set_temp_value - low_temp) * (high_comp - low_comp) / (high_temp - low_temp) + low_comp);

        //        if (high_temp == low_temp)
        //        {
        //            if (set_temp_value >= high_temp)
        //                cal_value = 10 * high_comp;
        //            if (set_temp_value < low_temp)
        //                cal_value = 10 * low_comp;
        //        }

        //        int offset_value = Convert.ToInt32(cal_value);

        //        if (offset_value < 0)
        //        {
        //            offset_value += 65536;
        //        }

        //        WriteLogData($"SetPVOffset offset_value: {offset_value}");

        //        byte temp_value_1 = Convert.ToByte(offset_value / 256);
        //        byte temp_value_2 = Convert.ToByte(offset_value % 256);

        //        byte[] temp = new byte[9];
        //        temp[0] = 0x01;
        //        temp[1] = 0x10;
        //        temp[2] = 0x57;
        //        temp[3] = 0x78;
        //        temp[4] = 0x00;
        //        temp[5] = 0x01;
        //        temp[6] = 0x02;
        //        temp[7] = temp_value_1;
        //        temp[8] = temp_value_2;
        //        byte[] crc = CRC16(temp);

        //        byte[] cmd = new byte[11];
        //        for (int i = 0; i < 9; i++)
        //        {
        //            cmd[i] = temp[i];
        //        }
        //        cmd[9] = crc[0];
        //        cmd[10] = crc[1];

        //        bool res = Write(cmd, 0, cmd.Length);
        //        return res;
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLogData($"SetPVOffset exception: {ex}");
        //        return false;
        //    }
        //}
        //public bool AskPID()
        //{
        //    bool res = Ask(ASK_ITEM.PID);
        //    return res;
        //}
        //public bool AskSV()
        //{
        //    bool res = Ask(ASK_ITEM.SV);
        //    return res;
        //}
        //public bool AskMV()
        //{
        //    bool res = Ask(ASK_ITEM.MV);
        //    return res;
        //}

        #endregion

        #region Private Function
        private double LinearityCalibration()
        {
            double TValue = ApplicationSetting.Get_Int_Recipe((int)eFormAppSet.TxtBx_Target);
            double TStart = 0, TEnd = 0;
            double DStart = 0, DEnd = 0;
            double dT;

            for (int i = 0; i < 10; i++)
            {
                if (i == 9) break;

                double Offset_T1 = ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Temp1 + i);
                double Offset_T2 = ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Temp1 + i + 1);
                double Offset_C1 = ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Comp1 + i);
                double Offset_C2 = ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Comp1 + i + 1);

                if (TValue >= Offset_T1 && TValue < Offset_T2 && i < 3)
                {
                    TStart = Offset_T1;
                    TEnd = Offset_T2;
                    DStart = Offset_C1;
                    DEnd = Offset_C2;
                    break;
                }
                else if (TValue >= Offset_T1 && TValue <= Offset_T2 && i == 3)
                {
                    TStart = Offset_T1;
                    TEnd = Offset_T2;
                    DStart = Offset_C1;
                    DEnd = Offset_C2;
                    break;
                }
                else
                {
                    //不補償
                    TStart = -300;
                    TEnd = 300;
                    DStart = 0;
                    DEnd = 0;
                }
            }

            dT = DStart + (((DEnd - DStart) * (TValue - TStart)) / (TEnd - TStart));

            TValue += (int)dT;

            return TValue;
        }
        private double LinearityCalibration_Ans(double TValue)
        {
            double TStart = 0, TEnd = 0;
            double DStart = 0, DEnd = 0;
            double dT;

            for (int i = 0; i < 10; i++)
            {
                if (i == 9) break;

                double Offset_T1 = ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Temp1 + i);
                double Offset_T2 = ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Temp1 + i + 1);
                double Offset_C1 = ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Offset1 + i);
                double Offset_C2 = ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Offset1 + i + 1);

                if (TValue >= Offset_T1 && TValue < Offset_T2 && i < 3)
                {
                    TStart = Offset_T1;
                    TEnd = Offset_T2;
                    DStart = Offset_C1;
                    DEnd = Offset_C2;
                    break;
                }
                else if (TValue >= Offset_T1 && TValue <= Offset_T2 && i == 3)
                {
                    TStart = Offset_T1;
                    TEnd = Offset_T2;
                    DStart = Offset_C1;
                    DEnd = Offset_C2;
                    break;
                }
                else
                {
                    //不補償
                    TStart = -300;
                    TEnd = 300;
                    DStart = 0;
                    DEnd = 0;
                }
            }

            dT = DStart + (((DEnd - DStart) * (TValue - TStart)) / (TEnd - TStart));

            TValue += (int)dT;

            return TValue;
        }
        private double FivePointCalibration_Ans(string temperature_value)
        {
            double Now_Value = Convert.ToDouble(temperature_value) - Offset_Value;
            //WriteLogData($"Calculate Offset_Value{Offset_Value}");
            double GetTempOffset = 0, TempGradient = 0, CalculateOffset = 0;
            if (Now_Value > 180 && Now_Value <= 200)
            {
                GetTempOffset = ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Offset1);
                TempGradient = GetTempOffset / (200 - 180);
                CalculateOffset = (TempGradient * (Now_Value - 180));
            }
            else if (Now_Value > 150 && Now_Value <= 180)
            {
                GetTempOffset = ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Offset2);
                TempGradient = GetTempOffset / (180 - 150);
                CalculateOffset = (TempGradient * (Now_Value - 150));
            }
            else if (Now_Value >= 120 && Now_Value <= 150)
            {
                GetTempOffset = ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Offset3);
                TempGradient = GetTempOffset / (150 - 120);
                CalculateOffset = (TempGradient * (Now_Value - 120));
            }
            else if (Now_Value >= 85 && Now_Value < 120)
            {
                GetTempOffset = ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Offset4);
                TempGradient = GetTempOffset / (120 - 85);
                CalculateOffset = (TempGradient * (Now_Value - 85));
            }
            else if (Now_Value < 85)
            {
                GetTempOffset = ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Offset5);
                TempGradient = GetTempOffset / (85 - ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Temp5));
                CalculateOffset = (TempGradient * (Now_Value - ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Temp5)));
            }

            return CalculateOffset;
        }
        private double FivePointCalibration()
        {
            double Compensate = 0, Temperature = 0;
            int Value = ApplicationSetting.Get_Int_Recipe((int)eFormAppSet.TxtBx_Target);

            if (Value > 180 && Value <= 200)
            {
                Temperature = ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Temp1);
                Compensate = ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Comp1);
            }
            else if (Value > 151 && Value <= 180)
            {
                Temperature = ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Temp2);
                Compensate = ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Comp2);
            }
            else if (Value >= 120 && Value <= 150)
            {
                Temperature = ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Temp3);
                Compensate = ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Comp3);
            }
            else if (Value >= 85 && Value < 120)
            {
                Temperature = ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Temp4);
                Compensate = ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Comp4);
            }
            else if (Value < 85)
            {
                Temperature = ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Temp5);
                Compensate = ApplicationSetting.Get_Double_Recipe((int)eFormAppSet.TxtBx_Comp5);
            }

            double CorrectValue = Compensate / (200 - Temperature);
            Offset_Value = Math.Round(CorrectValue * (Value - Temperature), 1);
            double SV_Value = Math.Round(Value + CorrectValue * (Value - Temperature), 1);

            return SV_Value;
        }
        private bool Write(byte[] buffer, int offset, int count)
        {
            bool res = false;
            if (Comport.IsOpen)
            {
                try
                {
                    Comport.Write(buffer, offset, count);
                    res = true;
                }
                catch (Exception ex)
                {
                    Tool.SaveHistoryToFile($"TemperatureController_TPT8000:Write Fail{ex}");
                }
            }
            else
            {
            }

            return res;
        }
        private byte[] Read(int expect_read_length)
        {
            byte[] received_buf = null;

            if (Comport.IsOpen)
            {
                try
                {
                    var tick = Environment.TickCount;
                    int time_count;
                    while (Comport.BytesToRead != expect_read_length)
                    {
                        time_count = Environment.TickCount - tick;
                        if (time_count > 1000)
                        {
                            break;
                        }
                    }

                    int num = Comport.BytesToRead;
                    received_buf = new byte[num];
                    Comport.Read(received_buf, 0, num);
                }
                catch (Exception ex)
                {
                    Tool.SaveHistoryToFile($"TemperatureController_TPT8000:Read Fail{ex}");
                    received_buf = null;
                }
            }
            else
            {
                received_buf = null;
            }

            return received_buf;
        }
        private void DiscardOutBuffer()
        {
            if (Comport.IsOpen)
            {
                try
                {
                    if (Comport.BytesToWrite != 0)
                    {
                        Comport.DiscardOutBuffer();
                    }
                }
                catch (Exception ex)
                {
                    Tool.SaveHistoryToFile($"TemperatureController_TPT8000:DiscardOutBuffer Fail{ex}");
                }
            }
        }
        private void DiscarInBuffer()
        {
            if (Comport.IsOpen)
            {
                try
                {
                    if (Comport.BytesToRead != 0)
                    {
                        Comport.DiscardInBuffer();
                    }
                }
                catch (Exception ex)
                {
                    Tool.SaveHistoryToFile($"TemperatureController_TPT8000:DiscarInBuffer Fail{ex}");
                }
            }
        }
        private void Clear()
        {
            DiscardOutBuffer();
            DiscarInBuffer();
        }
        enum ASK_ITEM
        {
            PV,
            MV,
            SV,
            PID,
        }
        private byte[] CRC16(byte[] pDataBytes)
        {
            ushort crc = 0xffff;
            ushort polynom = 0xA001;

            for (int i = 0; i < pDataBytes.Length; i++)
            {
                crc ^= pDataBytes[i];
                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 0x01) == 0x01)
                    {
                        crc >>= 1;
                        crc ^= polynom;
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
            }

            var send_crc = BitConverter.GetBytes(crc);
            return send_crc;
        }
        //byte[] GetAskCommand(ASK_ITEM item)
        //{
        //    byte[] cmd = new byte[9];

        //    try
        //    {
        //        byte[] temp = new byte[6];
        //        switch (item)
        //        {
        //            case ASK_ITEM.PV:
        //                {
        //                    temp[0] = 0x01;
        //                    temp[1] = 0x03;
        //                    temp[2] = 0x63;
        //                    temp[3] = 0x8D;
        //                    temp[4] = 0x00;
        //                    temp[5] = 0x02;
        //                }
        //                break;
        //            case ASK_ITEM.MV:
        //                {
        //                    temp[0] = 0x01;
        //                    temp[1] = 0x03;
        //                    temp[2] = 0x63;
        //                    temp[3] = 0x91;
        //                    temp[4] = 0x00;
        //                    temp[5] = 0x02;
        //                }
        //                break;
        //            case ASK_ITEM.SV:
        //                {
        //                    temp[0] = 0x01;
        //                    temp[1] = 0x03;
        //                    temp[2] = 0x63;
        //                    temp[3] = 0x8E;
        //                    temp[4] = 0x00;
        //                    temp[5] = 0x02;
        //                }
        //                break;
        //            case ASK_ITEM.PID:
        //                {
        //                    temp[0] = 0x01;
        //                    temp[1] = 0x03;
        //                    temp[2] = 0x70;
        //                    temp[3] = 0x00;
        //                    temp[4] = 0x00;
        //                    temp[5] = 0x03;
        //                }
        //                break;
        //        }

        //        byte[] crc = CRC16(temp);

        //        for (int i = 0; i < 6; i++)
        //        {
        //            cmd[i] = temp[i];
        //        }
        //        cmd[6] = crc[0];
        //        cmd[7] = crc[1];
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLogData($"Get{item}Command exception: {ex}");
        //        cmd = null;
        //    }

        //    return cmd;
        //}
        //bool Ask(ASK_ITEM item)
        //{
        //    bool res = false;
        //    String ReadTemperature_Order = "B00,GTEMP,0\r\n";
        //    try
        //    {
        //        Clear();
        //        byte[] cmd = Encoding.Default.GetBytes(ReadTemperature_Order);
        //        if (cmd != null)
        //        {
        //            Comport.Write(ReadTemperature_Order);
        //            res = true;
        //        }
        //        else
        //        {
        //            WriteLogData($"Read Temperature Fail");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLogData($"Read Temperature Success: {ex}");
        //    }
        //    return res;
        //}



        #endregion

        #region Log
        //StreamWriter sw;
        //String LastDateTime;
        //void CreateLogFile()
        //{
        //    try
        //    {
        //        var Stamp = DateTime.Now.ToString(@"yyyyMMdd");

        //        var FilePath = $@"{Directories.LedProbe}\Log_TemperatureControl";
        //        var FileName = $@"{Directories.LedProbe}\Log_TemperatureControl\Log_TemperatureControl_SDC15_{Stamp}.csv";

        //        if (!Directory.Exists(FilePath))
        //            Directory.CreateDirectory(FilePath);

        //        sw = new StreamWriter(FileName, true, Encoding.Default);
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLogData($"Exception : {ex.Message}");
        //    }
        //}
        //void WriteLogData(String Msg)
        //{
        //    CheckLogFile();

        //    if (sw == null)
        //        return;

        //    String time = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}],";

        //    Msg = time + $"{Msg},";

        //    sw.WriteLine(Msg);
        //    sw.Flush();
        //}
        //public void CloseLogFile()
        //{
        //    if (sw != null)
        //    {
        //        sw.Close();
        //        sw = null;
        //    }
        //}
        //void CheckLogFile()
        //{
        //    String date_now = DateTime.Now.ToString(@"yyyyMMdd");
        //    if (date_now != LastDateTime || sw == null)
        //    {
        //        CloseLogFile();

        //        LastDateTime = date_now;
        //        CreateLogFile();
        //    }
        //}
        #endregion
    }
}
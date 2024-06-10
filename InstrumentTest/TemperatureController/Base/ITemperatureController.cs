using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentTest
{
    public delegate void UpdatePresentValueCallBack(double value);
    public delegate void UpdateSetValueCallBack(double value);
    public delegate string GetFiveRTDValueCallBack(string aa);

    public interface ITemperatureController
    {
        bool Open(String com, String baudrate, String parity);
        bool Close();
        bool Start();
        bool Start(int ctrl_box, string ch, int Value);
        bool Stop();
        bool AskPV();
        String GetAns();


    }
}

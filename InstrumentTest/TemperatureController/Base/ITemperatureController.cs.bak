using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentTest
{
    public delegate void UpdatePresentValueCallBack(double value);
    public delegate void UpdateSetValueCallBack(double value);

    public interface ITemperatureController
    {
        bool Open(String com, String baudrate, String parity);
        bool Close();
        bool SetValue(int value);
        bool Start();
        bool Stop();


    }
}

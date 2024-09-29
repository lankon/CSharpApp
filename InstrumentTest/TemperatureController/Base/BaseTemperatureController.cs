using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentTest
{
    public abstract class BaseTemperatureController
    {
        #region abstract
        /// <summary>
        /// 開啟通訊。
        /// </summary>
        /// <param name="com">comport</param>
        /// <param name="baudrate">包率</param>
        /// /// <param name="parity">校驗碼</param>
        /// <returns>是否成功。</returns>
        public abstract bool Open(String com, String baudrate, String parity);
        /// <summary>
        /// 關閉通訊。
        /// </summary>
        /// <returns>是否成功。</returns>
        public abstract bool Close();
        /// <summary>
        /// 開始控溫。
        /// </summary>
        /// <returns>是否成功。</returns>
        public abstract bool Start();
        /// <summary>
        /// 停止控溫。
        /// </summary>
        /// <returns>是否成功。</returns>
        public abstract bool Stop();
        /// <summary>
        /// 詢問目前溫度。
        /// </summary>
        /// <returns>是否成功。</returns>
        public abstract bool AskPV();
        /// <summary>
        /// 取得回覆。
        /// </summary>
        /// <returns>控制器回傳訊息。</returns>
        public abstract String GetAns();
        #endregion

        #region virtual
        /// <summary>
        /// 開始控溫。
        /// </summary>
        /// <param name="ctrl_box">控制箱編號</param>
        /// <param name="ch">控制箱Channel</param>
        /// /// <param name="Value">溫度</param>
        /// <returns>是否成功。</returns>
        public virtual bool Start(int ctrl_box, string ch, int Value)
        {
            return false;
        }
        /// <summary>
        /// 停止控溫。
        /// </summary>
        /// <param name="ctrl_box">控制箱編號</param>
        /// <param name="ch">控制箱Channel</param>
        /// <returns>是否成功。</returns>
        public virtual bool Stop(int ctrl_box, string ch)
        {
            return false;
        }
        /// <summary>
        /// 詢問目前溫度。
        /// </summary>
        /// <param name="ctrl_box">控制箱編號</param>
        /// <param name="ch">控制箱Channel</param>
        /// <returns>是否成功。</returns>
        public virtual bool AskPV(int ctrl_box, string ch)
        {
            return false;
        }
        /// <summary>
        /// 五點RTD溫度。
        /// </summary>
        /// <returns>溫度。</returns>
        public virtual String GetFivePointValue()
        {
            return "-1";
        }
        #endregion


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentTest
{
    public interface IState
    {
        void Handle(Context context);
    }

    public class Context
    {
        public IState State { get; set; }

        public Context(IState state)
        {
            this.State = state;
        }

        public void Request()
        {
            // 让当前状态对象处理请求
            State.Handle(this);
        }
    }


    class Task_Idle
    {
        public enum SubTaskType
        {
            IDLE,
            DELTA_LOADCELL,
        }


        public class Initial : IState
        {
            public SubTaskType CurrentSubTask { get; set; }

            public Initial()
            {
                // 设置初始子状态
                this.CurrentSubTask = SubTaskType.IDLE;
            }

            public void Handle(Context context)
            {
                switch (CurrentSubTask)
                {
                    case SubTaskType.IDLE:
                        // 执行SubStateA1相关的逻辑
                        //Console.WriteLine("Handling SubStateA1.");
                        // 根据需要更改子状态
                        //CurrentSubTask = SubTaskType.GET_DATA;
                        int aaa = 10;
                        aaa = 156;
                        break;
                    case SubTaskType.DELTA_LOADCELL:
                        
                        break;
                        // 更多子状态处理...
                }
            }
        }
    }


}

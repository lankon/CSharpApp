using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentTest
{
    


    class Task_DeltaLoadCell
    {
        public class START:IState
        {
            public enum SubTaskType
            {
                START,
                OPEN,
                SUCCESS,
                END,
                GET_DATA,
            }

            public SubTaskType CurrentSubTask { get; set; }

            public START()
            {
                // 设置初始子状态
                this.CurrentSubTask = SubTaskType.START;
            }

            public void Handle(Context context)
            {
                switch (CurrentSubTask)
                {
                    case SubTaskType.START:
                        CurrentSubTask = SubTaskType.SUCCESS;
                        break;

                    case SubTaskType.OPEN:

                        break;

                    case SubTaskType.SUCCESS:

                        CurrentSubTask = SubTaskType.END;
                        break;

                    case SubTaskType.END:

                        CurrentSubTask = SubTaskType.END;
                        break;

                    case SubTaskType.GET_DATA:
                        // 执行SubStateA2相关的逻辑
                        //Console.WriteLine("Handling SubStateA2.");
                        // 根据需要更改子状态或主状态
                        // 示例：更改主状态
                        //context.State = new ConcreteStateB();
                        break;
                        // 更多子状态处理...
                }
            }
        }
    }
}

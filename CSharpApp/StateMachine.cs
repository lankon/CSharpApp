using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpApp
{
    public interface IState
    {
        void Handle(Context context);
    }

    public class Context
    {
        public IState current_state { get; set; }
        private IState task_state;

        public void Get_SubTaskState()
        {
            string aa = task_state.ToString();
        }

        public Context(IState initial_state)
        {
            current_state = initial_state;
            task_state = initial_state;


        }

        public void Request()
        {
            current_state.Handle(this);
        }
    }

    #region Task
    public class Task_LoadCell:IState
    {
        public void Handle(Context context)
        {
            context.current_state = new SubTask_Delta_LoadCell();
        }
    }
    #endregion
    #region SubTask
    public class SubTask_Delta_LoadCell:IState
    {
        public void Handle(Context context)
        {

        }
    }
    #endregion

    



   
}

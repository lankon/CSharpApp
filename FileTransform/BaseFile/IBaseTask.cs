using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace FileTransform
{
    public delegate void UpdateTaskStateCallBack(string text);
    public delegate void SetErrorMsgCallBack(string msg);
    public delegate void SetPauseAbortContinueCallBack(TASK_STATUS status);

    public abstract class IBaseTask
    {
        #region public abstract
        public abstract TASK_STATUS Run(TASK_STATUS task_status = TASK_STATUS.NONE);
        public abstract void GoToAbort();
        public abstract TASK_STATUS GoToPause();
        public abstract TASK_STATUS SetUserCtrl();
        #endregion

        #region protected abstract
        protected abstract void CheckResult(TASK_STATUS check);
        protected abstract void SetStatus(TASK_STATUS st);
        protected abstract void SetSubTaskProcessing(bool is_processing);
        protected abstract bool GetSubTaskProcessing();
        protected abstract void SetStatusCommand(TASK_STATUS task_status);
        protected abstract TASK_STATUS GetStatusCommand();
        #endregion

        #region public virtual
        public virtual UpdateTaskStateCallBack UpdateTaskState { get; set; }
        public virtual SetErrorMsgCallBack SetErrorMsg { get; set; }
        public virtual SetPauseAbortContinueCallBack SetPauseAbortContinue { get;set;}
        public virtual void SetForm(Form form)
        {
            
        }
        #endregion
    }

    public enum TASK_STATUS
    {
        NONE,
        
        SUCCESS,
        FAIL,

        PAUSE,
        ABORT,
        CONTINUE,
        ABORT_CONTINUE,
    }


}

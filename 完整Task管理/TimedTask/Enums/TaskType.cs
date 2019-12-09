using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimedTask
{
    /// <summary>
    /// 任务类型  0:定时任务,1:定时提醒,2:定时关机,3:关闭显示器
    /// </summary>
    public enum TaskType
    {
        /// <summary>
        /// 定时任务
        /// </summary>
        TimingTask = 0,
        /// <summary>
        /// 定时提醒
        /// </summary>
        Remind = 1,
        /// <summary>
        /// 定时关机
        /// </summary>
        Shutdown = 2,
        /// <summary>
        /// 关闭显示器
        /// </summary>
        TurnOffMonitor = 3,
        /// <summary>
        /// 开启显示器
        /// </summary>
        TurnOnMonitor = 4,
        /// <summary>
        /// 定时锁屏
        /// </summary>
        LockMonitor = 5
    }
}

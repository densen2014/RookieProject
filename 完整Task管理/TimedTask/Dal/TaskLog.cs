
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimedTask.Dal
{
    /// <summary>
    /// 任务日志访问类
    /// </summary>
    public class TaskLog : DalBase<Entity.TaskLog>
    {
        public TaskLog()
            : base("TaskLog", "Id")
        {

        }
        public TaskLog(string connString)
            : base(connString, "TaskLog", "Id")
        {

        }
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override int Add(Entity.TaskLog model)
        {
            if (!Entity.App.SaveLog)
                return 0;

            return base.Add(model);
        }

        /// <summary>
        /// 删除历史日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void DeleteHistory()
        {
            base.Delete(" TimeType IN('2','5') AND CreateDate<'" + DateTime.Now.AddDays(-3) + "' ");//日 一次 3天前
            base.Delete(" TimeType IN('1') AND CreateDate<'" + DateTime.Now.AddMonths(-3) + "' ");//月 3月前
            base.Delete(" TimeType IN('3','4') AND CreateDate<'" + DateTime.Now.AddDays(-3) + "' ");//小时、分钟 2天前
        }
    }
}

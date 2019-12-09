
using System;
using System.Collections.Generic;
using System.Text;

namespace TimedTask.Entity
{
    /// <summary>
    /// 任务运行日志
    /// </summary>
    public class TaskLog
    {
        /// <summary>
        /// 唯一编码
        /// </summary>
        public Int64 Id { get; set; }

        /// <summary>
        /// 任务名
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 唯一编码
        /// </summary>
        public Int64 TaskId { get; set; }

        /// <summary>
        /// 运行类型
        /// </summary>
        public string TimeType { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public string TaskType { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// 是否运行成功
        /// </summary>
        public string IsRun { get; set; }
    }
}

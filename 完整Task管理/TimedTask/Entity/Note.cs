using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimedTask.Entity
{
    /// <summary>
    /// 记事本实体
    /// </summary>
    public class Note : NotifyBaseEntity
    {
        string _title;
        string _content;
        /// <summary>
        /// 唯一编码
        /// </summary>
        public Int64 Id { get; set; }
        /// <summary>
        /// 记事本id 
        /// </summary>
        public Int64 TypeId { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string Title
        {
            get { return _title; }
            set
            {
                this._title = value;
                NotifyPropertyChange(() => Title);
            }
        }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            get { return _content; }
            set
            {
                this._content = value;
                NotifyPropertyChange(() => Content);
            }
        }
    }
}

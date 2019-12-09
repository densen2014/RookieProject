// 版权所有：
// 文 件  名：AutoTask.cs
// 功能描述：自动运行实体
// 创建标识：Seven Song(m.sh.lin0328@163.com) 2014/1/18 14:14:47
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Text;

namespace TimedTask.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class AutoTask : NotifyBaseEntity
    {
        string _title;
        string _startParameters;
        string _status;
        string _enable;
        string _remark;
        string _timeType;
        Int64 _dayth;
        Int64 _interval;
        string _taskType;
        string _audioPath;
        string _applicationPath;
        DateTime? _nextStartDate;
        DateTime? _stopDate;
        DateTime? _startDate;
        /// <summary>
        /// 唯一编码
        /// </summary>
        public Int64 Id { get; set; }
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
        /// 自动启动参数
        /// </summary>
        public string StartParameters
        {
            get { return _startParameters; }
            set
            {
                this._startParameters = value;
                NotifyPropertyChange(() => StartParameters);
            }
        }
        /// <summary>
        /// 可执行程序路径
        /// </summary>
        public string ApplicationPath
        {
            get { return _applicationPath; }
            set
            {
                this._applicationPath = value;
                NotifyPropertyChange(() => ApplicationPath);
            }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 启动时间
        /// </summary>
        public DateTime? StartDate
        {
            get { return _startDate; }
            set
            {
                this._startDate = value;
                NotifyPropertyChange(() => StartDate);
            }
        }
        /// <summary>
        /// 终止时间
        /// </summary>
        public DateTime? StopDate
        {
            get { return _stopDate; }
            set
            {
                this._stopDate = value;
                NotifyPropertyChange(() => StopDate);
            }
        }
        /// <summary>
        /// 下次启动时间
        /// </summary>
        public DateTime? NextStartDate
        {
            get { return _nextStartDate; }
            set
            {
                this._nextStartDate = value;
                NotifyPropertyChange(() => NextStartDate);
            }
        }
        /// <summary>
        /// 任务描述
        /// </summary>
        public string Remark
        {
            get { return _remark; }
            set
            {
                this._remark = value;
                NotifyPropertyChange(() => Remark);
            }
        }
        /// <summary>
        /// 是否启用 0：禁用，1：启用，2：失效，3：过期
        /// </summary>
        public string Enable
        {
            get { return _enable; }
            set
            {
                this._enable = value;
                NotifyPropertyChange(() => Enable);
            }
        }
        /// <summary>
        /// 运行状态
        /// </summary>
        public string Status
        {
            get { return _status; }
            set
            {
                this._status = value;
                NotifyPropertyChange(() => Status);
            }
        }
        /// <summary>
        /// 第几天
        /// </summary>
        public Int64 Dayth
        {
            get { return _dayth; }
            set
            {
                this._dayth = value;
                NotifyPropertyChange(() => Dayth);
            }
        }
        /// <summary>
        /// 任务类型
        /// </summary>
        public string TaskType
        {
            get { return _taskType; }
            set { _taskType = value; }
        }
        /// <summary>
        /// 音乐路径
        /// </summary>
        public string AudioPath
        {
            get { return _audioPath; }
            set { _audioPath = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TimeType
        {
            get { return _timeType; }
            set { _timeType = value; }
        }
        /// <summary>
        /// 时间间隔分钟
        /// </summary>
        public Int64 Interval
        {
            get { return _interval; }
            set { _interval = value; }
        }
        /// <summary>
        /// 是否响铃 
        /// </summary>
        public string AudioEnable { get; set; }
        /// <summary>
        /// 声音大小 
        /// </summary>
        public Int64 AudioVolume { get; set; }
        /// <summary>
        /// 程序运行状态
        /// </summary>
        public enum Application
        {
            Strat,
            End
        }
    }
}

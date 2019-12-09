// 版权所有：
// 文 件  名：cs
// 功能描述：程序公共实体
// 创建标识：Seven Song(m.sh.lin0328@163.com) 2014/1/19 12:08:42
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TimedTask.Entity
{
    /// <summary>
    /// 程序公共变量类
    /// </summary>
    public class App
    {
        private static string applicationName = "TimedTask";
        private static System.Collections.Hashtable audioHt;
        private static Hashtable _htType = new Hashtable();
        private static string _lockbgImg = String.Empty;
        private static bool _mintoTray = false;
        private static string _appbgImg = String.Empty;
        private static char _spiderChar = '⊙';//字符串特殊分割符

        /// <summary>
        /// 字符串特殊分割符
        /// </summary>
        public static char SpiderChar
        {
            get { return App._spiderChar; }
            set { App._spiderChar = value; }
        }

        /// <summary>
        /// 锁屏背景
        /// </summary>
        public static string LockBgImg
        {
            get { return _lockbgImg; }
            set { _lockbgImg = value; }
        }
        /// <summary>
        /// 是否记录运行日志
        /// </summary>
        public static bool SaveLog { set; get; }
        /// <summary>
        /// 锁屏时间 分钟
        /// </summary>
        public static int LockMinute { get; set; }
        /// <summary>
        /// 窗体背景
        /// </summary>
        public static string AppBgImg
        {
            get { return _appbgImg; }
            set { _appbgImg = value; }
        }
        /// <summary>
        /// 是否最小化到托盘
        /// </summary>
        public static bool MinToTray
        {
            get { return _mintoTray; }
            set { _mintoTray = value; }
        }
        /// <summary>
        /// 任务类型
        /// </summary>
        public static Hashtable HtTaskType
        {
            get
            {
                if (_htType == null || _htType.Count == 0)
                {
                    _htType.Add("0", "定时任务");
                    _htType.Add("1", "定时提醒");
                    _htType.Add("2", "定时关机");
                    _htType.Add("3", "关闭显示器");
                    _htType.Add("4", "打开显示器");
                    _htType.Add("5", "定时锁屏");
                }
                return _htType;
            }
        }
        /// <summary>
        /// 可执行文件启动目录
        /// </summary>
        public static string StartPath { get; set; }
        /// <summary>
        /// 计划任务配置文件路径
        /// </summary>
        public static string TaskConfig { get; set; }
        /// <summary>
        /// 是否已经锁屏
        /// </summary>
        public static bool IsLockScreen { get; set; }
        /// <summary>
        /// 配置文件路径
        /// </summary>
        public static string Config { get; set; }
        /// <summary>
        /// 程序名称
        /// </summary>
        public static string ApplicationName
        {
            get { return applicationName; }
            set { applicationName = value; }
        }
        /// <summary>
        /// 声音列表
        /// </summary>
        public static System.Collections.Hashtable AudioList
        {
            get
            {
                if (audioHt == null || audioHt.Count == 0)
                {
                    string path = StartPath + "\\Audio\\";
                    if (System.IO.Directory.Exists(path))
                    {
                        string name = "";
                        audioHt = new System.Collections.Hashtable();
                        foreach (string f in System.IO.Directory.GetFileSystemEntries(path))
                        {
                            path = System.IO.Path.GetFullPath(f);
                            name = System.IO.Path.GetFileName(f);
                            name = name.Substring(0, name.LastIndexOf("."));

                            if (!audioHt.ContainsKey(name))
                                audioHt.Add(name, path);
                        }
                    }
                }
                return audioHt;
            }
        }
        /// <summary>
        /// 主窗口是否显示
        /// </summary>
        public static bool IsMainWinShow { set; get; }
    }
}

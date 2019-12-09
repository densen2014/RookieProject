using System;
using System.Collections.Generic;
using System.IO;

using System.Text;

namespace TimedTask
{
    /// <summary>
    /// 简单的日志文件操作类
    /// </summary>
    public class Log : IDisposable
    {
        #region 变量/属性

        private static String _logSuffix = "log";
        private static StreamWriter _sw;
        private static FileInfo _openFile;
        private static FileStream _newFS = null;
        /// <summary>
        /// 存放路径
        /// </summary>
        public static string LogPath { get; set; }
        /// <summary>
        /// 日志文件后缀名
        /// </summary>
        public static string LogSuffix
        {
            get { return _logSuffix; }
            set { _logSuffix = value; }
        }
        #endregion

        #region 日常日志

        /// <summary>
        /// 日常日志
        /// </summary>
        /// <param name="logType">日志类型</param>
        /// <param name="title">标题</param>
        /// <param name="message">内容</param>
        public static bool SaveLog(string title, string message)
        {
            return WriteLog("error_", LogPath, title, message);
        }
        /// <summary>
        ///  日常日志
        /// </summary>
        /// <param name="logType">日志类型</param>
        /// <param name="title">标题</param>
        /// <param name="message">内容</param>
        /// <param name="autoLinefeed">是否自动换行</param>
        /// <returns></returns>
        public static bool SaveLog(string logType, string title, string message)
        {
            return WriteLog(logType, LogPath, title, message);
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="path">日志文件路径 如:D:/log</param>
        /// <param name="title">标题</param>
        /// <param name="message">信息</param>
        /// <returns>是否写入成功</returns>
        private static bool WriteLog(string logType, string path, string title, params string[] message)
        {
            string logName = String.Format("{0}.{1}", DateTime.Now.ToString("yyyy-MM-dd"), _logSuffix);
            if (logType.Length > 0)
            {
                logName = logType + String.Format("{0}.{1}", DateTime.Now.ToString("yyyy-MM-dd"), _logSuffix);
            }
            Helper.CreateFolder(path);
            string logFile = String.Format("{0}{1}", path, logName);
            if (!File.Exists(logFile))
            {
                CreateNewLog(logFile);
            }
            if (_openFile == null && _sw == null)
            {
                _openFile = new FileInfo(logFile);
                _newFS = _openFile.Open(FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                _sw = new StreamWriter(_newFS);
            }

            if (_sw == null) return false;
            _sw.WriteLine("================{0} {1}=================", DateTime.Now.ToString(), title);
            _sw.WriteLine();
            if (message != null)
            {
                foreach (string str in message)
                {
                    _sw.WriteLine(str);
                }
            }
            _sw.WriteLine();
            _sw.Flush();
            return true;
        }

        /// <summary>
        /// 建立一个新日志文件
        /// </summary>
        /// <param name="logFile"></param>
        private static void CreateNewLog(string logFile)
        {
            if (!File.Exists(logFile))
            {
                using (_newFS = new FileStream(logFile, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    _sw = new StreamWriter(_newFS, Encoding.UTF8);
                    _sw.WriteLine("文件创建时间:{0}", DateTime.Now.ToString());
                    _sw.WriteLine(_sw.NewLine);
                    _newFS.Flush();
                    _sw.Dispose();
                    _sw = null;
                }
            }
        }
        #endregion

        #region Dispose

        private bool disposed = false;

        /// <summary>
        /// 垃圾回收器执行函数
        /// </summary>
        ~Log()
        {
            //如果有就释放非托管
            Dispose(false);
        }

        /// <summary>
        /// 关闭并释放资源
        /// </summary>
        public void Dispose()
        {
            //全部释放
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// 关闭并释放资源
        /// </summary>
        public void Close()
        {
            Dispose();
        }

        /// <summary>
        /// 关闭并释放资源
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            // 清理托管资源
            if (disposing)
            {
                if (_sw != null)
                {
                    _sw.Close();
                    _sw.Dispose();
                    _sw = null;
                }
                if (_newFS != null)
                {
                    _newFS.Close();
                    _newFS.Dispose();
                    _newFS = null;
                }

                if (_openFile != null)
                {
                    _openFile = null;
                }
            }
            //非托管
            disposed = true;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using TimedTask.Entity;

namespace TimedTask
{
    public class Sys
    {
        private static Microsoft.Win32.RegistryKey registryKeyApp = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

        #region 自动启动

        /// <summary>
        /// 读取注册表
        /// </summary>
        /// <returns></returns>
        public static string Read(string name)
        {
            if (registryKeyApp.GetValue(name) == null)
            {
                return "";
            }
            return registryKeyApp.GetValue(name).ToString();
        }
        /// <summary>
        /// 是否开机启动
        /// </summary>
        public static bool IsAutoStartup()
        {
            string app = Sys.Read(Entity.App.ApplicationName).Trim();
            if (app.Length > 0 && app.EndsWith(".exe\""))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 取消自动启动
        /// </summary>
        /// <param name="isRun">设置/取消自动启动</param>
        public static void AutoStartup(bool isRun)
        {
            AutoStartup(isRun, Environment.CommandLine);
        }

        /// <summary>
        /// 系统设置/取消自动启动
        /// </summary>
        /// <param name="isRun">是否自动启动</param>
        /// <param name="exePath">系统执行路径（可增加配置参数）</param>
        public static void AutoStartup(bool isRun, string exePath)
        {
            try
            {
                if (isRun)
                {
                    registryKeyApp.SetValue(Entity.App.ApplicationName, exePath);
                }
                else
                {
                    registryKeyApp.DeleteValue(Entity.App.ApplicationName, false);
                }
            }
            catch (Exception ex)
            {
                Log.SaveLog("SysHelper AutoStartup", ex.ToString());
            }
        }

        #endregion
    }
}

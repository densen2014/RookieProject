using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;

namespace TimedTask
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        //单实例运行代码
        Mutex mutex;
        protected override void OnStartup(StartupEventArgs e)
        {
            //base.OnStartup(e);
            bool canCreateNew;
            try
            {
                mutex = new Mutex(true, Entity.App.ApplicationName, out canCreateNew);
                if (!canCreateNew)
                {
                    //MessageBox.Show("程序已经运行！");
                    //Shutdown();
                    if (!Entity.App.IsMainWinShow)
                    {
                        Entity.App.IsMainWinShow = true;
                        //激活已运行实例
                        //
                        //
                    }
                }
                else
                {
                    if (e.Args.Length > 0)//启动参数
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                Log.SaveLog("App", ex.ToString());
            }
        }
    }
}

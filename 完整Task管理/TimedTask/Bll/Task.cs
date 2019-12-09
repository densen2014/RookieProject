using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading;
using TimedTask.Entity;

namespace TimedTask.Bll
{
    public class Task
    {
        private static Task _instance;
        private static object _lock = new object();//使用static object作为互斥资源
        private static readonly object _obj = new object();
        private Dal.AutoTask _dalTask = new Dal.AutoTask();
        private Dal.TaskLog _dalTaskLog = new Dal.TaskLog();

        #region 单例
        /// <summary>
        /// 
        /// </summary>
        private Task() { }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static Task GetInstense()
        {
            if (_instance == null)
            {
                lock (_obj)
                {
                    if (_instance == null)
                    {
                        _instance = new Task();
                    }
                }
            }
            return _instance;
        }
        /// <summary>
        /// 返回唯一实例
        /// </summary>
        public static Task Instance
        {
            get
            {
                return GetInstense();
            }
        }
        #endregion

        /// <summary>
        /// 任务启动
        /// </summary>
        public void StartTask()
        {
            string proccessName = "";
            bool isTask = true;//是否是定时任务
            try
            {
                List<Entity.AutoTask> list = _dalTask.GetList(" 1=1 ", null, "CreateDate DESC ");
                if (list == null || list.Count == 0)
                {
                    return;
                }
                foreach (AutoTask model in list)
                {
                    isTask = true;

                    if (model.TaskType.Length > 0 && model.TaskType != "0")//声音、窗口提醒 
                    {
                        isTask = false;
                    }
                    if (isTask && (
                            model.ApplicationPath.Length == 0
                            || model.NextStartDate == null
                            || (model.TimeType == TimeType.Month.ToString() && model.Dayth != DateTime.Now.Day)
                        ))
                    {
                        continue;
                    }
                    if (isTask && !File.Exists(model.ApplicationPath))
                    {
                        Log.SaveLog("exe_not_exists", "Task StartTask", "任务路径错误，名称：" + model.Title + ",路径：" + model.ApplicationPath + "\r\n");
                        model.Status = "路径不存在";
                        model.Enable = "2";//失效
                        _dalTask.Update(model, " Id=" + model.Id);
                        continue;
                    }

                    try
                    {
                        #region 失效

                        if (model.StopDate != null && DateTime.Now >= model.StopDate)
                        {
                            model.Status = "任务过期";
                            model.Enable = "3";
                            _dalTask.Update(model, " Id=" + model.Id);

                            continue;
                        }
                        else if (model.Enable != "1")
                        {
                            model.Status = "任务禁用";
                            model.Enable = "0";
                            _dalTask.Update(model, " Id=" + model.Id);
                            continue;
                        }
                        #endregion

                        if (isTask)
                        {
                            proccessName = model.ApplicationPath.Substring(model.ApplicationPath.LastIndexOf("\\") + 1).Replace(".exe", "");
                            Helper.EndApp(proccessName);
                        }
                        if (model.NextStartDate != null && DateTime.Now >= model.NextStartDate)
                        {
                            bool result = true;
                            if (isTask)
                            {
                                result = StartApp(model, proccessName);
                            }
                            else
                            {
                                result = StartWarn(model, false);
                            }
                            string nextSTime = Task.Instance.GetNextStartDate(Int64.Parse(model.TimeType), model.Dayth, model.Interval);

                            model.NextStartDate = Convert.ToDateTime(nextSTime);
                            model.Status = result ? "正常" : "启动失败";
                            model.Enable = (model.TimeType == "5") ? "0" : "1";//运行一次 的执行后设置不可用 
                            _dalTask.Update(model, " Id=" + model.Id);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.SaveLog("Task StartTask", "更新启动列表配置出错" + ex.ToString() + "\r\n");
                    }
                }
            }
            catch (Exception e)
            {
                Log.SaveLog("Task StartTask", "任务出现异常" + e.ToString());
            }
        }

        /// <summary>
        /// 开始提醒
        /// </summary>
        /// <param name="taskType">任务类别</param>
        /// <param name="title">标题</param>
        /// <param name="remark">任务说明</param>
        /// <param name="audioName">声音名称</param>
        /// <param name="isTest">是否测试，测试时不会关机</param>
        /// <returns></returns>
        public bool StartWarn(AutoTask model, bool isTest)
        {
            bool result = true;
            string tmp = "";
            string command = "";

            Entity.TaskLog modLog = new TaskLog();
            modLog.TaskId = model.Id;
            modLog.TaskType = model.TaskType;
            modLog.TimeType = model.TimeType;
            modLog.IsRun = "1";
            modLog.Title = model.Title;
            modLog.CreateDate = DateTime.Now;

            #region 关机/显示器/锁屏

            if (model.TaskType == ((Int32)TaskType.Shutdown).ToString())//关机
            {
                tmp = "系统将于60秒后关闭，此操作不能撤销，请保存好您的工作！";
                command = "shutdown -s -t 60";

                //Common.HookHelper hook = new Common.HookHelper();
                //hook.HookStart();//安装钩子
            }
            else if (model.TaskType == ((Int32)TaskType.TurnOffMonitor).ToString())//关闭显示器
            {
                Helper.CloseMonitor();
                this._dalTaskLog.Add(modLog);
                return true;
            }
            else if (model.TaskType == ((Int32)TaskType.TurnOnMonitor).ToString())//
            {
                Helper.OpenMonitor();
                this._dalTaskLog.Add(modLog);
                return true;
            }
            else if (model.TaskType == ((Int32)TaskType.LockMonitor).ToString())//锁屏
            {
                if (!Entity.App.IsLockScreen)
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        View.LockScreen lockScreen = new View.LockScreen();
                        lockScreen.IsTest = isTest;
                        lockScreen.PointText = model.Remark.Contains("⊙") ? model.Remark.Split('⊙')[1] : model.Remark;
                        lockScreen.ShowDialog();
                    }));
                }
                this._dalTaskLog.Add(modLog);
                return true;
            }
            #endregion

            #region POP提醒
            try
            {
                if (model.AudioEnable == "1")
                {
                    Thread t = new Thread(new ThreadStart(() =>
                    {
                        Helper.PalyAudio(model.AudioPath, model.AudioVolume);
                    }));

                    t.IsBackground = true;
                    t.Start();
                }
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    View.PopUP pop = new View.PopUP();
                    if (model.Remark.Contains(Entity.App.SpiderChar))
                    {
                        pop.Subject = model.Remark.Split(Entity.App.SpiderChar)[0];
                        pop.Info = model.Remark.Split(Entity.App.SpiderChar)[1] + tmp;
                    }
                    else
                    {
                        pop.Subject = model.Remark + tmp;
                    }
                    pop.PopTitle = model.Title;
                    pop.Show();
                }));
            }
            catch (Exception ex)
            {
                Log.SaveLog("Task StartWarn", ex.ToString());
                result = false;
                modLog.IsRun = "0";
            }
            #endregion

            if (!isTest)
            {
                Helper.Speek(tmp);
                Helper.Run(command);
            }
            this._dalTaskLog.Add(modLog);
            return result;
        }
        /// <summary>
        /// 启动程序
        /// </summary>
        /// <param name="name">任务名称</param>
        /// <param name="proccessName">进程名</param>
        /// <param name="path">程序路径</param>
        /// <param name="startParameters">启动参数</param>
        /// <returns>是否启动成功</returns>
        public bool StartApp(AutoTask model, string proccessName)
        {
            //杀死
            Helper.EndApp(proccessName);
            //启动
            System.Threading.Thread.Sleep(2000);
            if (!File.Exists(model.ApplicationPath))//不存在
            {
                return false;
            }
            try
            {
                if (model.StartParameters.Length > 0)
                {
                    System.Diagnostics.Process.Start(model.ApplicationPath, model.StartParameters);
                }
                else
                {
                    System.Diagnostics.Process.Start(model.ApplicationPath);
                }
            }
            catch (Exception ex)
            {
                string log = "程序启动错误，路径：" + model.ApplicationPath + (model.StartParameters.Length == 0 ? "" :
                   ",参数为：" + model.StartParameters) + ex.ToString();
                Log.SaveLog("Task StartApplication", log);
            }

            Entity.TaskLog modLog = new TaskLog();
            modLog.TaskId = model.Id;
            modLog.Title = model.Title;
            modLog.IsRun = "0";
            modLog.TimeType = model.TimeType;
            modLog.TaskType = model.TaskType;
            modLog.CreateDate = DateTime.Now;

            #region 检测程序启动信息

            foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcesses())
            {
                if (proccessName == p.ProcessName)
                {
                    //Log.SaveLog("success_", "任务" + model.Title + "启动成功", log);
                    modLog.IsRun = "1";
                    return true;
                }
            }
            this._dalTaskLog.Add(modLog);

            return false;

            #endregion
        }

        /// <summary>
        /// 获取下次启动时间
        /// </summary>
        /// <param name="timeType">启动类型</param>
        /// <param name="dayth">每月第几日</param>
        /// <param name="interval"></param>
        /// <returns></returns>
        public string GetNextStartDate(long timeType, long dayth, long interval)
        {
            string result = "";
            /*
                Year = 0,
                Month = 1,
                Day = 2,
                Hour = 3,
                Minute = 4,
                Once = 5
                 */
            string dateFormat = "yyyy-MM-dd HH:mm:00";
            switch (timeType)
            {
                case 5://一次
                    result = DateTime.Now.ToString(dateFormat);
                    break;
                case 1://每月
                    result = Convert.ToDateTime(String.Format(DateTime.Now.AddMonths(1).ToString("yyyy-MM-{0} HH:mm:00"), dayth)).ToString(dateFormat); //DateTime.Now.AddMonths(1).ToString(dateFormat).ToString();
                    break;
                case 2:
                    result = (DateTime.Now.AddDays(1)).ToString(dateFormat);
                    break;
                case 3:
                    result = (DateTime.Now.AddHours(1)).ToString(dateFormat);
                    break;
                case 4:
                    result = (DateTime.Now.AddMinutes(interval)).ToString(dateFormat);
                    break;
            }
            return result;
        }

        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <param name="isFirstLoad">是否是第一次加载</param>
        /// <returns></returns>
        public List<Entity.AutoTask> GetTaskList(bool isFirstLoad)
        {
            Dal.AutoTask dalAutoTask = new Dal.AutoTask();
            List<Entity.AutoTask> list = dalAutoTask.GetList(" 1=1 ", null, " CreateDate DESC"); //Helper.GetTaskList(_xml);//任务列表
            if (isFirstLoad)
            {
                List<Entity.AutoTask> listTmp = list.Where(m => m.TaskType == "5").ToList<Entity.AutoTask>();//锁屏任务 下次启动时间从打开软件算起
                if (listTmp.Count > 0)
                {
                    foreach (Entity.AutoTask m in listTmp)
                    {
                        m.NextStartDate = Convert.ToDateTime(Task.Instance.GetNextStartDate(Int32.Parse(m.TimeType), m.Dayth, m.Interval));
                        dalAutoTask.Update(m, " Id=" + m.Id);
                    }
                }
                listTmp = list.Where(m => m.TaskType == "2").ToList<Entity.AutoTask>();//关机任务 日期换成当前年月日 
                if (listTmp.Count > 0)
                {
                    foreach (Entity.AutoTask m in listTmp)
                    {
                        string nextDate = ((DateTime)m.NextStartDate).ToString("yyyy-MM-dd");
                        m.NextStartDate = Convert.ToDateTime(
                            ((DateTime)m.NextStartDate).ToString("yyyy-MM-dd HH:mm:00").Replace(nextDate, DateTime.Now.ToString("yyyy-MM-dd"))
                            );
                        dalAutoTask.Update(m, " Id=" + m.Id);
                    }
                }
            }
            return list;
        }
    }
}

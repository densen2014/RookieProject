// 版权所有：
// 文 件  名：Common.cs
// 功能描述：
// 创建标识：Seven Song(m.sh.lin0328@163.com) 2014/1/19 11:28:48
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Xml;
using Visifire.Charts;

namespace TimedTask
{
    public static class Helper
    {
        private static object _lock = new object();
        private static System.Windows.Media.MediaPlayer _player = new System.Windows.Media.MediaPlayer();

        #region 进程

        /// <summary>
        /// 杀死进程
        /// </summary>
        /// <param name="proccessName">进程名</param>
        public static void EndApp(string proccessName)
        {
            if ((proccessName + "").Length == 0)
            {
                return;
            }
            foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcesses())
            {
                if (p.ProcessName == proccessName)
                {
                    try
                    {
                        p.Kill();
                    }
                    catch (Exception e)
                    {
                        Log.SaveLog("Common KillProccess", e.ToString());
                    }
                }
            }
        }
        #endregion

        #region CMD

        /// <summary>
        /// 运行cmd命令
        /// </summary>
        /// <param name="command">cmd命令文本</param>
        public static void Run(string command)
        {
            if (String.IsNullOrEmpty(command))
                return;
            try
            {
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                //p.StandardInput.WriteLine("shutdown -s -t 30 -c \"如果此时不想关机，就点击“取消关机”按钮！\"");
                //p.StandardInput.WriteLine("shutdown -s -t 30 -c \"如果此时不想关机，就点击“取消关机”按钮！\"");
                p.StandardInput.WriteLine(command);
                p.StandardInput.WriteLine("exit");
                string strRst = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
            }
            catch (Exception ex)
            {
                Log.SaveLog("Common Run", ex.ToString());
            }
        }
        #endregion

        #region 声音

        /// <summary>
        /// 文字转语音
        /// </summary>
        /// <param name="message">文字信息</param>
        /// <param name="speekType">声音类别 0：单词男声Sam,1:单词男声Mike,2:单词女声Mary,3:中文发音，如果是英文，就依单词字母一个一个发音</param>
        public static void Speek(string message)
        {
            try
            {
                //引入COM组件:Microsoft speech object Library
                SpeechLib.SpVoice voice = new SpeechLib.SpVoice();
                voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(0);
                voice.Speak(message, SpeechLib.SpeechVoiceSpeakFlags.SVSFlagsAsync);
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 停止播放声音
        /// </summary>
        public static void StopAudio()
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                try
                {
                    if (_player != null)
                        _player.Stop();

                    _player = null;
                }
                catch (Exception ex)
                {
                    Log.SaveLog("Core Common_StopAudio", ex.ToString());
                }
            }));
        }
        /// <summary>
        /// 播放声音
        /// </summary>
        /// <param name="soundName">声音名</param>
        public static void PalyAudio(string soundName, double volume)
        {
            if (String.IsNullOrEmpty(soundName) || soundName.Contains("无"))
                return;

            string path = Entity.App.AudioList[soundName].ToString();
            if (!System.IO.File.Exists(path))
                return;
            /*
              SoundPlayer类特点
              1）仅支持.wav音频文件；
            2）不支持同时播放多个音频（任何新播放的操作将终止当前正在播放的）；
            3）无法控制声音的音量；
              if (path.EndsWith(".wav"))
              {
                  using (System.Media.SoundPlayer player = new System.Media.SoundPlayer(path))
                  {
                      player.Play();//播放波形文件
                  }
              }
            */
            if (_player != null)
            {
                StopAudio();
            }
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                if (path.EndsWith(".mp3") || path.EndsWith(".wma") || path.EndsWith(".wav"))
                {
                    try
                    {
                        _player = new System.Windows.Media.MediaPlayer();
                        _player.Open(new Uri(path));
                        _player.Volume = volume / 100;//大小为0~1.0
                        _player.Play();
                        //_player.MediaOpened+=
                    }
                    catch (Exception ex)
                    {
                        Log.SaveLog("Core Common_PalyAudio", ex.ToString());
                    }
                }
            }));
        }

        #endregion

        #region 获得程序版本

        /// <summary>
        /// 获得程序版本
        /// </summary>
        /// <returns></returns>
        public static string GetVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
        #endregion

        #region 显示器

        private const uint WM_SYSCOMMAND = 0x0112;
        private const uint SC_MONITORPOWER = 0xF170;
        private static readonly IntPtr HWND_HANDLE = new IntPtr(0xffff);
        [System.Runtime.InteropServices.DllImport("user32")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint wMsg, uint wParam, int lParam);

        /// <summary>
        /// 打开显示器
        /// </summary>
        public static void OpenMonitor()
        {
            // 2 为关闭显示器， －1则打开显示器
            SendMessage(HWND_HANDLE, WM_SYSCOMMAND, SC_MONITORPOWER, -1);
        }
        /// <summary>
        /// 关闭显示器
        /// </summary>
        public static void CloseMonitor()
        {
            // 2 为关闭显示器， －1则打开显示器
            SendMessage(HWND_HANDLE, WM_SYSCOMMAND, SC_MONITORPOWER, 2);
        }
        #endregion

        #region IO

        /// <summary>
        /// 创建日志文件夹
        /// </summary>
        /// <param name="path"></param>
        public static void CreateFolder(string path)
        {
            if (!System.IO.Directory.Exists(path))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                catch (System.IO.IOException)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                catch (Exception e)
                {
                    Log.SaveLog("Common CreateMatchFolder", e.ToString());
                }
            }
        }
        ///　<summary>
        ///　获取媒体文件属性信息
        ///　</summary>
        ///　<param name="path">媒体文件具体路径</param>
        ///　<param name="icolumn">具体属性的顺序值（-1简介信息 1文件大小 21时长 22比特率 34文件描述）</param>
        ///　<returns></returns>
        public static string GetFileDetailInfo(string path, int icolumn)
        {
            try
            {
                Shell32.Shell shell = new Shell32.Shell();
                Shell32.Folder folder = shell.NameSpace(System.IO.Path.GetDirectoryName(path));
                Shell32.FolderItem folderItem = folder.ParseName(path.Substring(path.LastIndexOf("\\") + 1));
                return folder.GetDetailsOf(folderItem, icolumn);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return "";
            }
        }
        /// <summary>
        /// 递归删除文件夹及其下的所有文件
        /// </summary>
        /// <param name="dir"></param>
        public static void DeleteFolder(string dir)
        {
            if (System.IO.Directory.Exists(dir)) //如果存在这个文件夹删除之 
            {
                foreach (string d in System.IO.Directory.GetFileSystemEntries(dir))
                {
                    if (System.IO.File.Exists(d))
                        System.IO.File.Delete(d); //直接删除其中的文件                        
                    else
                        DeleteFolder(d); //递归删除子文件夹 
                }
                System.IO.Directory.Delete(dir, true); //删除已空文件夹                 
            }
        }
        #endregion

        #region 删除x天前的文件

        /// <summary>
        /// 删除x天前创建的文件  2013-03-26 mashanlin 添加<br/>
        /// </summary>
        /// <param name="suffix">文件名中包含</param>
        /// <param name="suffixArray">文件后缀名 如：.rar  .doc</param>
        /// <param name="days">x天前的</param>
        public static void DropFiles(string filepath, string suffix, string[] suffixArray, int days)
        {
            if (filepath == "")
                return;
            string path = "";
            foreach (string p in GetAllDir(filepath, suffixArray))
            {
                path = filepath + p;
                if (!String.IsNullOrEmpty(suffix) && !p.Contains(suffix))
                    continue;

                System.IO.FileInfo f = new System.IO.FileInfo(path);
                if (f.Exists && f.CreationTime < DateTime.Now.AddDays(-(days)))
                {
                    try
                    {

                        f.Delete();
                    }
                    catch
                    {
                        //Response.Write("<script>alert('删除出错！');                }
                    }
                }
            }
        }

        /// <summary> 
        /// 获取指定目录和扩展名的所有文件  2013-03-26 mashanlin 添加
        /// </summary>
        /// <param name="path">绝对路径</param>
        /// <param name="extensionArray">文件后缀数组</param>
        /// <returns></returns>
        public static List<String> GetAllDir(string path, string[] extensionArray)
        {
            List<String> list = new List<string>();
            if (System.IO.Directory.Exists(path))
            {
                string[] fileList = System.IO.Directory.GetFileSystemEntries(path);
                list.Clear();
                foreach (string f in fileList)
                {
                    string filename = System.IO.Path.GetFileName(f);
                    string strExtension = System.IO.Path.GetExtension(System.IO.Path.GetFullPath(f)).ToLower();
                    if (extensionArray == null || extensionArray.Length == 0)
                    {
                        list.Add(filename);
                        continue;
                    }
                    foreach (string ss in extensionArray)
                    {
                        if (string.Equals(strExtension, ss, StringComparison.CurrentCultureIgnoreCase) || string.Equals(strExtension, ss.StartsWith(".") ? ss : "." + ss, StringComparison.CurrentCultureIgnoreCase))
                        {
                            list.Add(filename);
                        }
                    }
                }
            }
            return list;
        }
        #endregion

        #region 启动外部程序

        /// <summary>
        /// 启动外部Windows应用程序
        /// </summary>
        /// <param name="appName">应用程序路径名称</param>
        /// <returns></returns>
        public static bool StartApp(string appName)
        {
            return StartApp(appName, null, System.Diagnostics.ProcessWindowStyle.Normal);
        }

        /// <summary>
        /// 启动外部应用程序
        /// </summary>
        /// <param name="appName">应用程序路径名称</param>
        /// <param name="arguments">启动参数</param>
        /// <param name="style">进程窗口模式</param>
        /// <returns></returns>
        public static bool StartApp(string appName, string arguments, System.Diagnostics.ProcessWindowStyle style)
        {
            bool result = false;
            using (System.Diagnostics.Process p = new System.Diagnostics.Process())
            {
                p.StartInfo.FileName = appName;//exe,bat and so on
                p.StartInfo.WindowStyle = style;
                p.StartInfo.Arguments = arguments;
                try
                {
                    p.Start();
                    p.WaitForExit();
                    p.Close();
                    result = true;
                }
                catch
                {
                }
            }
            return result;
        }
        #endregion

        #region List

        /// <summary>
        /// 将IDataReader转换为 集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(IDataReader dr)
        {
            using (dr)
            {
                List<string> field = new List<string>(dr.FieldCount);
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    field.Add(dr.GetName(i).ToLower());
                }
                List<T> list = new List<T>();
                while (dr.Read())
                {
                    T model = Activator.CreateInstance<T>();
                    foreach (PropertyInfo property in model.GetType().GetProperties(BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance))
                    {
                        if (field.Contains(property.Name.ToLower()))
                        {
                            if (!IsNullOrDBNull(dr[property.Name]))
                            {
                                property.SetValue(model, HackType(dr[property.Name], property.PropertyType), null);
                            }
                        }
                    }
                    list.Add(model);
                }
                return list;
            }
        }
        /// <summary>
        /// 将DataTable转换为 集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(DataTable dt)
        {
            List<T> list = new List<T>();
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    T model = Activator.CreateInstance<T>();

                    foreach (PropertyInfo property in model.GetType().GetProperties(BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance))
                    {
                        if (!dt.Columns.Contains(property.Name))
                            continue;

                        if (!IsNullOrDBNull(dr[property.Name]))
                        {
                            property.SetValue(model, HackType(dr[property.Name], property.PropertyType), null);
                        }
                    }
                    list.Add(model);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }
        /// <summary>
        /// DataTable 转 对象要IList
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>行中是对象的类，类的属性与数据字段一致</returns>
        public static IList ToIList<T>(DataTable dt)
        {
            IList list = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                T obj = Activator.CreateInstance<T>();
                // 获得公共属性    
                PropertyInfo[] propertys = obj.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    if (!dt.Columns.Contains(pi.Name))
                        continue;
                    // 是否可写
                    if (!pi.CanWrite) continue;

                    object value = dr[pi.Name];
                    if (value != DBNull.Value)
                        pi.SetValue(obj, value, null);
                }
                list.Add(obj);
            }
            return list;
        }
        /// <summary>
        /// 将DataRow转换为 实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static T ToEntity<T>(DataRow dr)
        {
            T model = Activator.CreateInstance<T>();
            foreach (PropertyInfo pi in model.GetType().GetProperties(BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance))
            {
                if (dr.Table.Columns.Contains(pi.Name) && !IsNullOrDBNull(dr[pi.Name]))
                {
                    pi.SetValue(model, HackType(dr[pi.Name], pi.PropertyType), null);
                }
            }
            return model;
        }
        /// <summary>
        /// 对可空类型进行判断转换，要不然会报错
        /// </summary>
        /// <param name="value"></param>
        /// <param name="conversionType"></param>
        /// <returns></returns>
        public static object HackType(object value, Type conversionType)
        {
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                    return null;
                System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }
            return Convert.ChangeType(value, conversionType);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNullOrDBNull(object obj)
        {
            return ((obj is DBNull) || string.IsNullOrEmpty(obj.ToString())) ? true : false;
        }
        #endregion

        #region 读文件
        /// <summary>
        /// 读文件（读取文本内容）
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <returns></returns>
        public static string ReadFile(string path)
        {
            string s = "";
            if (!System.IO.File.Exists(path))
                s = "不存在相应的目录";
            else
            {
                StreamReader f2 = new StreamReader(path, System.Text.Encoding.GetEncoding("gb2312"));
                s = f2.ReadToEnd();
                f2.Close();
                f2.Dispose();
            }
            return s;
        }
        #endregion

        #region 写文件
        /// <summary>
        /// 写文件（当文件不存时，则创建文件，并追加文件）
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <param name="Strings">文件内容</param>
        public static void WriteFile(string path, string content)
        {
            if (!System.IO.File.Exists(path))
            {
                System.IO.FileStream f = System.IO.File.Create(path);
                f.Close();
                f.Dispose();
            }
            System.IO.StreamWriter f2 = new System.IO.StreamWriter(path, true, System.Text.Encoding.UTF8);
            f2.WriteLine(content);
            f2.Close();
            f2.Dispose();
        }
        #endregion

        #region 饼状图
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title">标题名称</param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="valuex"></param>
        /// <param name="valuey"></param>
        public static Chart CreatePie(string title, int width, int height, List<string> valuex, List<string> valuey)
        {
            Chart chart = new Chart();
            chart.Width = width;
            chart.Height = height;
            chart.BorderThickness = new Thickness(1);//禁用边框
            //chart.Margin = new Thickness(100, 5, 10, 5);
            //title.Padding = new Thickness(0, 5, 5, 0);

            chart.ToolBarEnabled = true;//是否启用打印
            chart.ScrollingEnabled = true;//是否启用滚动
            chart.View3D = true;//3D效果显示

            chart.Titles.Add(new Title()//设置标题的名称
            {
                Text = title
            });
            DataSeries series = new DataSeries();//数据线
            series.RenderAs = RenderAs.Pie;//柱状Stacked 设置数据线的格式
            DataPoint point;// 设置数据点   
            for (int i = 0; i < valuex.Count; i++)
            {
                point = new DataPoint();
                point.AxisXLabel = valuex[i];// 设置X轴点   
                point.LegendText = "##" + valuex[i];

                point.YValue = double.Parse(valuey[i]);//设置Y轴点
                //point.MouseLeftButtonDown += new MouseButtonEventHandler(dataPoint_MouseLeftButtonDown);
                series.DataPoints.Add(point); //添加数据点 
            }

            chart.Series.Add(series); // 添加数据线到数据序列。
            return chart;
        }

        #endregion
    }
}

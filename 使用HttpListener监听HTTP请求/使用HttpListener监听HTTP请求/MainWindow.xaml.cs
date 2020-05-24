using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 使用HttpListener监听HTTP请求
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string _listenerUri = "http://127.0.0.1/3330/";
        private HttpListener _listener;

        public MainWindow()
        {
            InitializeComponent();
            checkListen_Checked(null,null);
        }

        //启用HTTP请求监听
     private void checkListen_Checked(object sender, RoutedEventArgs e)
            {
                _listener = new HttpListener();
                _listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
                _listener.Prefixes.Add(_listenerUri);
                _listener.Start();
                WriteToStatus("启用数据监听！");
                _listener.BeginGetContext(ListenerHandle, _listener);
            }

        //停止HTTP请求监听
        private void checkListen_Unchecked(object sender, RoutedEventArgs e)
        {
            if (_listener != null)
            {
                _listener.Close();
                WriteToStatus("停止数据监听");
            }
        }
        private void WriteToStatus(string msg)
        {
            Debug.WriteLine(msg);
        }
        private void ListenerHandle(IAsyncResult result)
        {
            try
            {
                _listener = result.AsyncState as HttpListener;
                if (_listener.IsListening)
                {
                    _listener.BeginGetContext(ListenerHandle, result);
                    HttpListenerContext context = _listener.EndGetContext(result);
                    //解析Request请求
                    HttpListenerRequest request = context.Request;
                    string content = "";
                    switch (request.HttpMethod)
                    {
                        case "POST":
                            {
                                Stream stream = context.Request.InputStream;
                                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                                content = reader.ReadToEnd();
                            }
                            break;
                        case "GET":
                            {
                                var data = request.QueryString;
                            }
                            break;
                    }
                    WriteToStatus("收到数据：" + content + request.RawUrl  );

                    //构造Response响应
                    HttpListenerResponse response = context.Response;
                    response.StatusCode = 200;
                    response.ContentType = "application/json;charset=UTF-8";
                    response.ContentEncoding = Encoding.UTF8;
                    response.AppendHeader("Content-Type", "application/json;charset=UTF-8");

                    using (StreamWriter writer = new StreamWriter(response.OutputStream, Encoding.UTF8))
                    {
                        writer.Write("Rep:" + DateTime.Now.ToString());
                        writer.Close();
                        response.Close();
                    }
                }

            }
            catch (Exception ex)
            {

                WriteToStatus(ex.Message);
                _listener.EndGetContext(result);

                checkListen_Checked(null,null);
            }
        } 
    }
}

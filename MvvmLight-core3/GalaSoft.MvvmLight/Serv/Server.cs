using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using WpfApp1.Model;

class Server
{

    TcpListener server = null;
    实体类 _userInfo;
    public Server(string ip, int port, 实体类 userInfo)
    {
        _userInfo = userInfo;
        IPAddress localAddr = IPAddress.Parse(ip);
        server = new TcpListener(localAddr, port);
        server.Start();
        StartListener();
    }

    public void StartListener()
    {
        try
        {
            while (true)
            {
                Console.WriteLine("Waiting for a connection...");
                _userInfo.调试信息 = "Waiting for a connection...";

                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Connected!");
                _userInfo.调试信息 = "Connected!";

                Thread t = new Thread(new ParameterizedThreadStart(HandleDeivce));
                t.Start(client);
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
            _userInfo.调试信息 = string.Format("SocketException: {0}", e.ToString());
            server.Stop();
        }
    }

    public void HandleDeivce(Object obj)
    {
        TcpClient client = (TcpClient)obj;
        var stream = client.GetStream();
        string imei = String.Empty;

        string data = null;
        Byte[] bytes = new Byte[256];
        int i;
        try
        {
            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                string hex = BitConverter.ToString(bytes);
                data = Encoding.ASCII.GetString(bytes, 0, i);
                Console.WriteLine("{1}: Received: {0}", data, Thread.CurrentThread.ManagedThreadId);
                _userInfo.调试信息 =string.Format( "{1}: Received: {0}", data, Thread.CurrentThread.ManagedThreadId);

                string str = "Hey Device!";
                Byte[] reply = System.Text.Encoding.ASCII.GetBytes(str);
                stream.Write(reply, 0, reply.Length);
                Console.WriteLine("{1}: Sent: {0}", str, Thread.CurrentThread.ManagedThreadId);
                _userInfo.调试信息 = string.Format("{1}: Sent: {0}", str, Thread.CurrentThread.ManagedThreadId);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: {0}", e.ToString());
            _userInfo.调试信息 = string.Format("Exception: {0}", e.ToString());
            client.Close();
        }
    }
}
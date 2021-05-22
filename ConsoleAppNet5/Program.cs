using System;
using System.Diagnostics;
using System.IO;

namespace ConsoleAppNet5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Net5/Core 执行程序测试");
            var FileName = "test.doc";
            File.WriteAllText(FileName, "Hello World!");
            var fullname = Path.Combine(System.Environment.CurrentDirectory, FileName);
            Console.WriteLine(fullname);
            //System.Diagnostics.Process.Start(fullname);


            var psi = new ProcessStartInfo
            {
                FileName = "cmd",
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                CreateNoWindow = true,
                Arguments = $"/c start {FileName}"
            };
            Process.Start(psi);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;

namespace WindowsFormsControlLibrary1
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
            label1.Text = ShowMyVer() + "   " + ShowMyVer2();
        }
        public static string ShowMyVer()
        {
            try
            {
                //通过反射加载dll文件，然后获取其版本信息
                Assembly assembly = Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + "//WindowsFormsControlLibrary1.dll");
                AssemblyName assemblyName = assembly.GetName();
                Version version = assemblyName.Version;
                Console.WriteLine(version);
                return version.ToString();
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public static string ShowMyVer2()
        {
            try
            {

                AssemblyName assemblyName2 = AssemblyName.GetAssemblyName("WindowsFormsControlLibrary1.dll");
                Version version2 = assemblyName2.Version;
                Console.WriteLine(version2);
                return version2.ToString();
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

    }
}

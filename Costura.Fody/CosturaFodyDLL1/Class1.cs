using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CosturaFodyDLL1
{
    public class Class1
    {
        public static string ShowMyVer()
        {
            try
            {
                var x = Assembly.GetExecutingAssembly();
                return x.ToString();
                Assembly asm = Assembly.Load("CosturaFodyDLL1");
                return asm.ToString ();

            }
            catch (Exception)
            {

                throw;
            }
            try
            {
                //通过反射加载dll文件，然后获取其版本信息
                Assembly assembly = Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + "//CosturaFodyDLL1.dll");
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
                AssemblyName assemblyName2 = AssemblyName.GetAssemblyName("CosturaFodyDLL1");
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

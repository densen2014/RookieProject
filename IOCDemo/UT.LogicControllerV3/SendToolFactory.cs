using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UT.CoreV20;
using System.Reflection;
using System.IO;
using System.Configuration;

namespace UT.LogicControllerV3
{
    public abstract class SendToolFactory
    {
        public static ISendable GetInstance()
        {
            try
            {
                Assembly assembly = Assembly.LoadFile(GetAssembly()); // 加载程序集
                object obj = assembly.CreateInstance(GetObjectType()); // 创建类的实例 
                return obj as ISendable;
            }
            catch
            {
                return null;
            }
        }

        static string GetAssembly()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["AssemblyString"]);            
        }

        static string GetObjectType()
        {
            return ConfigurationManager.AppSettings["TypeString"];
        }
    }
}

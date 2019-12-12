using DotNet.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace LetMeWin.Common
{
   public class LoginLogic
    {
        private readonly string 网址 = "";       
        private Timer 检测时钟 = new Timer();
        private string Get帐号;
        private string[] 登录信息;

        /// <summary>
        /// 帐号登录，登录成功则返回0
        /// </summary>
        /// <param name="帐号"></param>
        /// <param name="密码"></param>
        /// <returns></returns>
        public int 登录(string 帐号, string 密码)
        {
            
            return 1;
        }
       
        /// <summary>
        ///  检测帐号在不在线
        /// </summary>
        private void 检测在线()
        {
            
        }

        /// <summary>
        /// MD5加密处理
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        private string 加密(string Str)
        {
            string Result = GetMD5(Str);
            // "BE175DE124D3D8A198A5E77D16AE2EE7"
            // "BE175DE124D3D8A198A5E77D16AE2EE7B7E4D"

            for (int i = 0; i <= 4; i++)
                Result += Result.Substring(i * 2 + i, 1);
            return Result;
        }

        private string GetMD5(string str)
        {
            var md5__1 = MD5.Create();
            var bs = md5__1.ComputeHash(Encoding.UTF8.GetBytes(str));
            var sb = new StringBuilder();
            foreach (byte b in bs)
                sb.Append(b.ToString("x2"));
            // 所有字符转为大写
            return sb.ToString().ToUpper();
        }

        /// <summary>
        /// 分析返回来的Html
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        private string[] 解析Html(string Data)
        {
            var html = Data.Replace("\r\n", "");
            var k = html.IndexOf("<body>") + ("<body>").Length;
            var w = html.IndexOf("</body>");
            var str = html.Substring(k, w - k);
            var arry = str.Split('|');
            return arry;
        }

        /// <summary>
        /// 时钟事件
        /// </summary>
        private void 检测时钟_Tick(object source, ElapsedEventArgs e) 
        {
            //异步检测用户权限帐号
            Run();
        }
        
        /// <summary>
        /// 异步方法
        /// </summary>
        private async void Run()
        {
            await Task.Delay(300);
            检测在线();
        }
    }
}

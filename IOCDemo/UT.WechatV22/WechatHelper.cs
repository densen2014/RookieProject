using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UT.CoreV20;

namespace UT.WechatV22
{
    public class WechatHelper : ISendable
    {
        public void Send(string message)
        {
            Console.WriteLine("Frome wechat: " + message);
        }
    }
}

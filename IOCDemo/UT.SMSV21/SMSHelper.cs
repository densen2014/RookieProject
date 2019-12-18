using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UT.CoreV20;

namespace UT.SMSV21
{
    public class SMSHelper : ISendable
    {
        public void Send(string message)
        {
            Console.WriteLine("Frome SMS: " + message);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UT.CoreV20;

namespace UT.EmailV20
{
    public class EmailHelper : ISendable
    {
        public void Send(string message)
        {
            Console.WriteLine("Frome email: " + message);
        }
    }
}

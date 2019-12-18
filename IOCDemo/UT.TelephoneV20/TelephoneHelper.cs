using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UT.CoreV20;

namespace UT.TelephoneV20
{
    public class TelephoneHelper : ISendable
    {
        public void Send(string message)
        {
            Console.Write("Frome telephone: " + message);
        }
    }
}

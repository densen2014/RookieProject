using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UT.CoreV20;

namespace UT.MessageServiceV20
{
    public class GreetMessageService
    {
        ISendable greetTool;

        public GreetMessageService(ISendable sendtool)
        {
            greetTool = sendtool;
        }

        public GreetMessageService(SendToolType sendToolType)
        {
            if (sendToolType == SendToolType.Email)
            {
                greetTool = new UT.EmailV20.EmailHelper();
            }
            else if (sendToolType == SendToolType.Telephone)
            {
                greetTool = new UT.TelephoneV20.TelephoneHelper();
            }
        }

        public void Greet(string message)
        {
            greetTool.Send(message);
        }
    }
}

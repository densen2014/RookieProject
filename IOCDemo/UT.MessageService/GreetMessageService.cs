using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UT.Email;

namespace UT.MessageService
{
    public class GreetMessageService
    {
        EmailHelper greetTool;

        public GreetMessageService()
        {
            greetTool = new EmailHelper();
        }

        public void Greet(string message)
        {
            greetTool.Send(message);
        }
    }
}

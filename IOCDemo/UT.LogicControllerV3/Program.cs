using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UT.CoreV20;
using UT.MessageServiceV20;

namespace UT.LogicControllerV3
{
    class Program
    {
        static void Main(string[] args)
        {
            string message = "新年快乐！ 过节费5000.";

            ISendable greetTool = SendToolFactory.GetInstance();
            GreetMessageService service = new GreetMessageService(greetTool);
            service.Greet(message);

            Console.ReadLine();
        }
    }
}

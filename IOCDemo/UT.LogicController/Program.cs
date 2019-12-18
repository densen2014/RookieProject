using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UT.LogicController
{
    class Program
    {
        static void Main(string[] args)
        {
            MessageService.GreetMessageService service = new MessageService.GreetMessageService();
            service.Greet("新年快乐！ 过节费5000.");

            Console.ReadLine();
        }
    }
}

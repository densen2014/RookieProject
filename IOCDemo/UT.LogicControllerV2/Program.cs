using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UT.CoreV20;
using UT.MessageServiceV20;
using UT.TelephoneV20;
using UT.SMSV21;
using UT.WechatV22;

namespace UT.LogicControllerV2
{
    class Program
    {
        static void Main(string[] args)
        {

            //string message = "新年快乐！ 过节费5000.";
            //Console.WriteLine("If using email to greeting, you can do like this:");
            //MessageServiceV20.GreetMessageService service = new MessageServiceV20.GreetMessageService(SendTool.Email);
            //service.Greet(message);

            //Console.WriteLine("\r\nIf using telephone to greeting, you can do like this:");
            //service = new MessageServiceV20.GreetMessageService(SendTool.Telephone);
            //service.Greet(message);

            //return;
            ////
            string message = "新年快乐！ 过节费5000.";
            //Console.WriteLine("If using email to greeting, you can do like this:");
            //ISendable greetTool = new EmailV20.EmailHelper();
            //MessageServiceV20.GreetMessageService service = new MessageServiceV20.GreetMessageService(greetTool);
            //greetTool.Send(message);

            //Console.WriteLine("\r\nIf using telephone to greeting, you can do like this:");

            //ISendable greetTool = new TelephoneHelper();
            //GreetMessageService service = new GreetMessageService(greetTool);
            //service.Greet(message);

            //Console.WriteLine("\r\nIf using SMS to greeting, you can do like this:");

            //ISendable greetTool = new SMSHelper();
            //GreetMessageService service = new GreetMessageService(greetTool);
            //service.Greet(message);

            Console.WriteLine("\r\nIf using wechat to greeting, you can do like this:");

            ISendable greetTool = new WechatHelper();
            GreetMessageService service = new GreetMessageService(greetTool);
            service.Greet(message);

            Console.ReadLine();

        }
    }
}

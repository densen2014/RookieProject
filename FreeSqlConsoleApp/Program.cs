using MiniProgramApi.Model;
using System;

namespace FreeSqlConsoleApp
{
    class Program
    {
        static IFreeSql fsql { get; set; }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            fsql = new FreeSql.FreeSqlBuilder()
         .UseConnectionString(FreeSql.DataType.Sqlite, $"data source=dddddd.db;", typeof(FreeSql.Sqlite.SqliteProvider<>))
         .UseAutoSyncStructure(true)
         .UseMonitorCommand(cmd => Console.WriteLine("\r\n SQL==> \r\n" + cmd.CommandText))
         .UseNoneCommandParameter(true)
         .Build();

            var demo = fsql.Select<SellerSupport>().Where(a => a.ID == 1).ToList();
            Console.WriteLine("\r\n demo==> \r\n" + demo.Count);

            for (int i = 0; i < 10; i++)
            {
                fsql.Insert<SellerSupport>().AppendData(new SellerSupport() { description = i.ToString() }).ExecuteAffrows();
            }
             
            Console.WriteLine("\r\n demo count ==> \r\n" + fsql.Select<SellerSupport>().Count());
        }
    }
}

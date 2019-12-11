using LetMeWin.Model;
using SqliteService.Service;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteService
{
    public class DbContext
    {
        public DbContext()
        {
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "Data Source=LetMeWin.db3;Pooling=true;FailIfMissing=false",
                DbType = DbType.Sqlite,      //设置数据库类型
                InitKeyType = InitKeyType.Attribute,//从特性读取主键和自增列信息
                IsAutoCloseConnection = true,//开启自动释放模式和EF原理一样我就不多解释了

            });
            //调式代码 用来打印SQL 
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" +
                    Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };

        }
        //注意：不能写成静态的
        public SqlSugarClient Db;//用来处理事务多表查询和复杂的操作

        /// <summary>
        /// 用户表实例化
        /// </summary>
        public SimpleClient<LoginModel> UesrDB
        {
            get { return new SimpleClient<LoginModel>(Db); }
        }

        public SimpleClient<AccountGridService> AccountDB
        {
            get { return new SimpleClient<AccountGridService>(Db); }
        }

    }
}

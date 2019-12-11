using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteService.Model
{
    
    [SugarTable("UserData")]
    public class UserDB
    {
        //指定主键和自增列，当然数据库中也要设置主键和自增列才会有效
      
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 帐号
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 记录帐号密码
        /// </summary>
        public int Record { get; set; }
    }

}

using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeWin.Model
{
   public class VipModel
    {
    }
    [SugarTable("AccountGrid")]
    public class AccountDridModel
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int    Id   { get; set; }

        public int    序号 { get; set; }

        public int    勾选 { get; set; }

        public string 帐号 { get; set; }

        public string 密码 { get; set; }

        public double 积分 { get; set; }

        public int    类型 { get; set; }
    }
}

using GalaSoft.MvvmLight;
using LetMeWin.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeWin.Model
{
    [SugarTable("UserData")]
    public class LoginModel
    {

       
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public Int32 Id { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        /// 
        public String User { get; set; }
      
        /// <summary>
        ///   密码
        /// </summary>
        public String Password { get; set; }

        /// <summary>
        /// 记录帐号密码
        /// </summary>
        public Int32 Record { get; set; }
    }
}

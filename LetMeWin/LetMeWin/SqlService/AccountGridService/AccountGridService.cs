
using LetMeWin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteService.Service
{
   public  class AccountGridService : DbContext
    {
        /// <summary>
        /// 账号密码查询
        /// </summary>
        public  List<AccountDridModel> 查询()
        {
            //查询所有
            //Dim list = 轮播图Db.GetList()
            //收到设置为DESC排序’
            //Dim list = Db.Queryable(Of 轮播表)().OrderBy(Function(it) it.id, OrderByType.Desc).ToList()
            //默认为ASC排序’
            var List = Db.Queryable<AccountDridModel>().ToList();
             return (List.Count > 0 ? List : List);    
        }
        public List<T> 查询<T>()
        {
            var List = Db.Queryable<T>().ToList();
            return (List.Count > 0 ? List : List);
        }

        public List<AccountDridModel> 更新( List<AccountDridModel> Data)
        {
            var List = Db.Saveable<AccountDridModel>(Data).ExecuteReturnEntity();
            return null;
        }
 
       
    }
}

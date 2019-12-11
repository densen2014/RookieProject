using SqliteService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteService.Service
{
   public  class UserService:DbContext
    {
        public UserDB 查询()
        {
            //查询所有
            //Dim list = 轮播图Db.GetList()
            //收到设置为DESC排序’
            //Dim list = Db.Queryable(Of 轮播表)().OrderBy(Function(it) it.id, OrderByType.Desc).ToList()
            //默认为ASC排序’
            var GetAll = Db.Queryable<UserDB>().ToList();
            Console.WriteLine(0);
            return GetAll[0];
        }
    }
}

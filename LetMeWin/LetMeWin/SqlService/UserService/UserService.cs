
using LetMeWin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteService.Service
{
   public  class UserService:DbContext
    {
        /// <summary>
        /// 账号密码查询
        /// </summary>
        public LoginModel 查询()
        {
            //查询所有
            //Dim list = 轮播图Db.GetList()
            //收到设置为DESC排序’
            //Dim list = Db.Queryable(Of 轮播表)().OrderBy(Function(it) it.id, OrderByType.Desc).ToList()
            //默认为ASC排序’
            var List = Db.Queryable<LoginModel>().ToList();
             return (List.Count > 0 ? List[0] :  new LoginModel());    
        }

        /// <summary>
        /// 账号密码保存
        /// </summary>
        /// <param name="Data"></param>
        public void 更新(LoginModel Data)
        {
              Db.Updateable(Data).ExecuteCommand();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimedTask.Dal
{
    /// <summary>
    /// 
    /// </summary>
    public class TypeList : DalBase<Entity.TypeList>
    {
        public TypeList()
            : base("TypeList", "Id")
        {

        }
        public TypeList(string connString)
            : base(connString, "TypeList", "Id")
        {

        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public override int Delete(string strWhere)
        {
            if (String.IsNullOrEmpty(strWhere))
                return 0;

            List<Entity.TypeList> list = base.GetList(strWhere, "Id,FatherID", null);
            if (list == null || list.Count == 0)
                return 0;

            string ids = "";
            foreach (Entity.TypeList m in list)
            {
                ids += m.Id + ",";
            }
            ids = ids.Remove(ids.Length - 1);

            int num = base.Count(" FatherID IN(" + ids + ")");
            if (num > 0)
                throw new Exception("操作失败，被删除项包含子节点");

            return base.Delete(strWhere);
        }
    }
}

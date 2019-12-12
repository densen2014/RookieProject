using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMLightDemo.Model
{
    public class ResTypeModel
    {
        private Int32 selectIndex;
        /// <summary>
        /// 选中索引
        /// </summary>
        public Int32 SelectIndex
        {
            get { return selectIndex; }
            set { selectIndex = value; }
        }
        

        private List<ComplexInfoModel> list;
        /// <summary>
        /// 下拉框列表
        /// </summary>
        public List<ComplexInfoModel> List
        {
            get { return list; }
            set { list = value; }
        }
    }
}

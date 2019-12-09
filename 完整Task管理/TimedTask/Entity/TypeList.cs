using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimedTask.Entity
{
    /// <summary>
    /// 词典类
    /// </summary>
    public class TypeList
    {
        /// <summary>
        /// 唯一编码
        /// </summary>
        public Int64 Id { get; set; }
        /// <summary>
        /// 父编码
        /// </summary>
        public Int64 FatherId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public String Code { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public String Remark { get; set; }
    }
}

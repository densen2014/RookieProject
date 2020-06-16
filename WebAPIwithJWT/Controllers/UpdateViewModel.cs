using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIwithJWT.Model
{
    public class UpdateViewModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public long Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public long Password { get; set; }
    }
}

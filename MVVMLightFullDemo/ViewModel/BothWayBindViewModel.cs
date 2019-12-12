using GalaSoft.MvvmLight;
using MVVMLightDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMLightDemo.ViewModel
{
    public class BothWayBindViewModel:ViewModelBase
    {
        public BothWayBindViewModel()
        {
            UserInfo = new UserInfoModel();
        }
        
        #region 属性

        private UserInfoModel userInfo;
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfoModel UserInfo
        {
            get { return userInfo; }
            set { userInfo = value; RaisePropertyChanged(() => UserInfo); }
        }

        #endregion
        
        #region 命令
        #endregion
    }
}

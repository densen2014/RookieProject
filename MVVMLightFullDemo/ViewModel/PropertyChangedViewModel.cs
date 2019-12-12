using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMLightDemo.ViewModel
{
    public class PropertyChangedViewModel:ViewModelBase
    {
        public const string PropertyName = "UserName"; //注册为该属性，该属性变化时进行消息发送
        public PropertyChangedViewModel() { }

        #region 全局变量
        private String userName;
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                String oldValue = userName;
                userName = value;
                RaisePropertyChanged(()=>UserName,oldValue,value,true);//这边相应配置上发送参数
            }
        }
        #endregion
    }
}
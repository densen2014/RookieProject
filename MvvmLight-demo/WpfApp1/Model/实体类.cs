using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{

    /// <summary>
    /// 用户信息
    /// </summary>
    public class 实体类 : ObservableObject
    {
        private String _名字;
        /// <summary>
        /// 用户名称
        /// </summary>
        public String 名字
        {
            get { return _名字; }
            set { _名字 = value; RaisePropertyChanged(() => 名字); }
        }

        private Int64 _电话;
        /// <summary>
        /// 用户电话
        /// </summary>
        public Int64 电话
        {
            get { return _电话; }
            set { _电话 = value; RaisePropertyChanged(() => 电话); }
        }

        private Int32 _性别;
        /// <summary>
        /// 用户性别
        /// </summary>
        public Int32 性别
        {
            get { return _性别; }
            set { _性别 = value; RaisePropertyChanged(() => 性别); }
        }

        private String _地址;
        /// <summary>
        /// 用户地址
        /// </summary>
        public String 地址
        {
            get { return _地址; }
            set { _地址 = value; RaisePropertyChanged(() => 地址); }
        }
    }

    /// <summary>
    /// 下拉框
    /// </summary>
    public class ComplexInfoModel : ObservableObject
    {
        private String key;
        /// <summary>
        /// Key值
        /// </summary>
        public String Key
        {
            get { return key; }
            set { key = value; RaisePropertyChanged(() => Key); }
        }

        private String text;
        /// <summary>
        /// Text值
        /// </summary>
        public String Text
        {
            get { return text; }
            set { text = value; RaisePropertyChanged(() => Text); }
        }
    }
}

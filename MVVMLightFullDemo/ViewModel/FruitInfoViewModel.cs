using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMLightDemo.ViewModel
{
    public class FruitInfoViewModel : ViewModelBase
    {
        #region 属性

        private String img;
        /// <summary>
        /// 图片
        /// </summary>
        public String Img
        {
            get { return img; }
            set { img = value; RaisePropertyChanged(() => Img); }
        }


        private String info;
        /// <summary>
        /// 信息
        /// </summary>
        public String Info
        {
            get { return info; }
            set { info = value; RaisePropertyChanged(()=>Info); }
        }


        #endregion
    }
}

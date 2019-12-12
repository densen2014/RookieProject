using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMLightDemo.Model
{
    public class ComplexInfoModel:ObservableObject
    {
        private String key;
        /// <summary>
        /// Key值
        /// </summary>
        public String Key
        {
            get { return key; }
            set { key = value; RaisePropertyChanged(()=>Key); }
        }


        private String text;
        /// <summary>
        /// Text值
        /// </summary>
        public String Text
        {
            get { return text; }
            set { text = value; RaisePropertyChanged(()=>Text); }
        }
    }
}

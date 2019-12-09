using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstMVVM.ViewModels
{
   public  class FirstViewModel:Screen
    {
        private string _FirtName = "向量";

        public string FirtName
        {
            get { return _FirtName; }
            set
            {
                _FirtName = value;
                NotifyOfPropertyChange(() => FirtName);
                NotifyOfPropertyChange(() => FullName);
            }
        }

        private string _SecondName;

        public string SecondName
        {
            get { return _SecondName; }
            set
            {
                _SecondName = value;
                NotifyOfPropertyChange(() => SecondName);
                NotifyOfPropertyChange(() => FullName);
            }
        }

       
        public string FullName
        {
            get { return FirtName  + SecondName; }
             
        }

    }
}

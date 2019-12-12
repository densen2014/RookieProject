﻿using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMLightDemo.Model
{
    public class WelcomeModel : ObservableObject
    {
        private String introduction;
        /// <summary>
        /// 欢迎词
        /// </summary>
        public String Introduction
        {
            get { return introduction; }
            set { introduction = value; RaisePropertyChanged(()=>Introduction); }
        }
    }
}
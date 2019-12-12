using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MVVMLightDemo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMLightDemo.ViewModel
{
    public class ComboBoxViewModel : ViewModelBase
    {
        public ComboBoxViewModel()
        {
            GetList();
        }

        private List<ComplexInfoModel> portList;
        /// <summary>
        /// 数据列表
        /// </summary>
        public List<ComplexInfoModel> PortList
        {
            get { return portList; }
            set { portList = value; RaisePropertyChanged(() => PortList); }
        }



        private ComplexInfoModel port;
        /// <summary>
        /// 选择的数据
        /// </summary>
        public ComplexInfoModel Port
        {
            get
            {
                return port;
            }

            set
            {
                port = value;
                RaisePropertyChanged(()=>Port);
            }
        }



        private String cbTxt;
        public string CbTxt
        {
            get
            {
                return cbTxt;
            }

            set
            {
                cbTxt = value;
                RaisePropertyChanged(()=>CbTxt);
            }
        }



        private RelayCommand search;
        /// <summary>
        /// 传递单个参数命令
        /// </summary>
        public RelayCommand Serach
        {
            get
            {
                if (search == null)
                    search = new RelayCommand(() => ExecutePassArgStr());
                return search;

            }
            set { search = value; }
        }               

        private void ExecutePassArgStr()
        {
            GetList();
        }



        #region 辅助函数

        private void GetList()
        {
            var list = new List<ComplexInfoModel>() {
              new ComplexInfoModel(){ Key="1",Text="ab" },
              new ComplexInfoModel(){ Key="2",Text="sad" },
              new ComplexInfoModel(){ Key="3",Text="dcsd" },
              new ComplexInfoModel(){ Key="4",Text="myjuy" },
            };
            if (String.IsNullOrEmpty(CbTxt))
            {
                PortList = list;
            }
            else
            {
                PortList = list.Where(p => p.Text.Contains(CbTxt)).ToList();
            }
        }

        #endregion


    }
}
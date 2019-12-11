using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LetMeWin.Model;
using SqliteService.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeWin.ViewModel
{
    public class VipHome:ViewModelBase
    {
       public VipHome()
       {
            //读取数据
            InitData();

       }

        #region 局部变量    
        readonly AccountGridService 数据库 = new AccountGridService();
        #endregion

        #region 全局属性

        /// <summary>
        /// 会员表格数据集合
        /// </summary>
        private List<AccountDridModel> accountDridModel;

        public List<AccountDridModel> AccountGridData
        {
            get { return accountDridModel; }
            set
            {
                accountDridModel = value;
                RaisePropertyChanged(() => AccountGridData);
            }
        }
        #endregion

        #region 命令事件
        private RelayCommand selectionChanged;
        /// <summary>
        /// 传递单个参数命令
        /// </summary>
        public RelayCommand SelectionChanged
        {
            get
            {
                if (selectionChanged == null)
                    selectionChanged = new RelayCommand(() => ExecutePassArgStr());
                return selectionChanged;

            }
            set { selectionChanged = value; }
        }

        private void ExecutePassArgStr()
        {
            Console.WriteLine(0);

        }

        public RelayCommand<object> OKCommand
        {
            get
            {
                if (_okCommand == null)
                    _okCommand = new RelayCommand<object>(OkCommand_Execute);
                return _okCommand;
            }
        }
        private RelayCommand<object> _okCommand = null;

        private void OkCommand_Execute(object obj)
        {
            Console.WriteLine(obj.ToString());
           // Result = true;
        }

        public RelayCommand<object> OKCommand2
        {
            get
            {
                if (_okCommand2 == null)
                    _okCommand2 = new RelayCommand<object>(OkCommand2_Execute);
                return _okCommand2;
            }
        }
        private RelayCommand<object> _okCommand2 = null;

        private void OkCommand2_Execute(object obj)
        {
            Console.WriteLine(obj.ToString());
            // Result = true;
        }

        #endregion

        #region 附加方法
        private void InitData()
        {
            var laq = 数据库.查询();
            accountDridModel = laq.Where(x => x.类型 == 1).ToList();
        }
        #endregion 
    }
}

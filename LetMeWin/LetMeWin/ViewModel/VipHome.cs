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
        private List<AccountDridModel> 帐号List = new List<AccountDridModel>();
       
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
        

        private int select = 0;
        /// <summary>
        /// 分类选中
        /// </summary>
        public int Select
        {
            get { return select; }
            set
            {
                select = value;
                RaisePropertyChanged(() => Select);
            }
        }

        private RelayCommand userAddChanged ;
        /// <summary>
        /// 会员Add
        /// </summary>
        public RelayCommand SserAddChanged
        {
            get
            {
                if (userAddChanged == null) userAddChanged = new RelayCommand(() => SserAddChangedSub());
                return userAddChanged;
            }
            set
            {
                userAddChanged = value;
            }
        }

        #endregion

        #region 命令事件
        private RelayCommand<object> selectionChanged = null;
        /// <summary>
        /// 传递参数命令
        /// </summary>
        public RelayCommand<object> SelectionChanged
        {
            get
            {
                if (selectionChanged == null)
                    //selectionChanged = new RelayCommand(() => ExecutePassArgStr());
                selectionChanged = new RelayCommand<object>(ExecutePassArgStr);
                return selectionChanged;

            }
            set { selectionChanged = value; }
        }
      
        private void ExecutePassArgStr(object obj)
        {
            if (select != Convert.ToInt32(obj))
            {
                select = Convert.ToInt32(obj);
                AccountGridData = 帐号List.Where(x => x.类型 == (select + 1)).ToList();
                Console.WriteLine(obj);
            }

          
        }

        #endregion

        #region 辅助方法
        /// <summary>
        /// 会员条件事件
        /// </summary>
        private void SserAddChangedSub()
        {
            accountDridModel.Add(new AccountDridModel { 勾选 = 1, 帐号 = "1789", 密码 = "123" });
            accountDridModel.Add(new AccountDridModel { 勾选 = 1, 帐号 = "1789", 密码 = "123" });
            accountDridModel.Add(new AccountDridModel { 勾选 = 1, 帐号 = "1789", 密码 = "123" });
            accountDridModel.Add(new AccountDridModel { 勾选 = 1, 帐号 = "1789", 密码 = "123" });

            AccountGridData.Add(new AccountDridModel { 勾选 = 1, 帐号 = "1222789", 密码 = "122223" });

            //AccountGridData = accountDridModel;
            //AccountGridData[0].积分 += 100;
            //AccountGridData[1].帐号 = DateTime.Now .ToString();

            //RaisePropertyChanged(() => AccountGridData);
            Console.WriteLine(0);
        }
        #endregion

        #region 附加方法
        private void InitData()
        {
             帐号List = 数据库.查询();
            accountDridModel = 帐号List.Where(x => x.类型 == 1).ToList();

        }
        #endregion 
    }
}

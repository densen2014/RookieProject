using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using LetMeWin.Model;
using LetMeWin.View;
using SqliteService.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LetMeWin.ViewModel
{
    public class VipHome:ViewModelBase
    {
       public VipHome()
       {
            //读取数据
            InitData();

            ///Messenger：信使
            ///Recipient：收件人
            ///接收添加会员页面传过来的数据
            Messenger.Default.Register<AccountDridModel>(this, "会员添加", Data =>
            {
                //ReceiveInfo += msg + "\n";
                帐号List.Add(Data);
                重新渲染AccountGridData();
                UserAddChangedEnabled = true;
            });

        }

        #region 局部变量    
        readonly AccountGridService 数据库 = new AccountGridService();
        private List<AccountDridModel> 帐号List = new List<AccountDridModel>();
        #endregion

        #region 全局属性

        /// <summary>
        /// 会员表格数据集合
        /// </summary>
        private ObservableCollection<AccountDridModel> accountDridModel;
        public ObservableCollection<AccountDridModel> AccountGridData
        {
            get { return accountDridModel; }
            set
            {
                accountDridModel = value;
                RaisePropertyChanged(() => AccountGridData);
            }
        }

        /// <summary>
        /// 分类选中
        /// </summary>
        private int select = 0;
        public int Select
        {
            get { return select; }
            set
            {
                select = value;
                RaisePropertyChanged(() => Select);
            }
        }

        /// <summary>
        /// 会员添加按钮是否禁用
        /// </summary>
        private bool _UserAddChangedEnabled=true;
        public bool UserAddChangedEnabled
        {
            get { return _UserAddChangedEnabled; }
            set
            {
                _UserAddChangedEnabled = value;
                RaisePropertyChanged(() => UserAddChangedEnabled);
            }
        }
        #endregion

        #region 命令事件
        /// <summary>
        /// 类型选中事件命令（注意参数类型）
        /// </summary>
        private RelayCommand<object> selectionChanged = null;
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
                //保存选中了那个类型
                select = Convert.ToInt32(obj);
                重新渲染AccountGridData();
            }          
        }


        /// <summary>
        /// 会员Add事件命令
        /// </summary>
        private RelayCommand userAddChanged;
        public RelayCommand UserAddChanged
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
        private void SserAddChangedSub()
        {
            AddMemberView sender = new AddMemberView();
            sender.Show();
            UserAddChangedEnabled = false;
        }

        /// <summary>
        /// 保存会员数据。
        /// </summary>
        private RelayCommand _保存Changed;
        public RelayCommand 保存Changed
        {
            get
            {
                if (_保存Changed == null) _保存Changed = new RelayCommand(() => 会员保存Sub());
                return _保存Changed;
            }
            set
            {
                _保存Changed = value;
            }
        }
        private void 会员保存Sub()
        {
            数据库.更新(帐号List);
        }


        #endregion

        #region 辅助方法

        /// <summary>
        /// Sub new 数据
        /// </summary>
        private void InitData()
        {
            //实例化帐号表格绑定的变量
            AccountGridData = new ObservableCollection<AccountDridModel>();
            //帐号查询
            //帐号List = 数据库.查询();
            帐号List = 数据库.查询<AccountDridModel>();
            重新渲染AccountGridData();
        }

        /// <summary>
        /// 重新渲染AccountGridData集合
        /// </summary>
        private void 重新渲染AccountGridData()
        {
            var items = 帐号List.Where(x => x.类型 == (select + 1)).ToList();
            AccountGridData.Clear();
            var Int = 0;
            foreach (var item in items)
            {
                Int += 1;
                item.序号 = Int;
                AccountGridData.Add(item);
                login(item);
            }

        }
        void login(object c1)
        {
            Thread thread = new Thread(() => 模拟登录(c1)); thread.Start();
        }
        public void 模拟登录(object c1)
        {
            Thread.Sleep(3000);
            AccountDridModel item =(AccountDridModel) c1;
            item.登录状态 = "没钱登录";
        }

        #endregion

        #region 附加方法

        #endregion
    }
}

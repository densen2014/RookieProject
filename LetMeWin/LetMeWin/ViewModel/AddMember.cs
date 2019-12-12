using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using LetMeWin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeWin.ViewModel
{
      public class AddMember : ViewModelBase
    {
        public AddMember()
        {
            类型Sub();
            游戏Sub();
            输入的数据 = new AccountDridModel();
            DispatcherHelper.Initialize();
        }
        #region 全局属性
        private String selectInfo;
        /// <summary>
        /// 选中信息
        /// </summary>
        public String SelectInfo
        {
            get { return selectInfo; }
            set { selectInfo = value; RaisePropertyChanged(() => SelectInfo); }
        }

        /// <summary>
        /// 添加会员界面输出的数据
        /// </summary>
        private AccountDridModel _输入的数据;
        public AccountDridModel 输入的数据
        {
            get { return _输入的数据; }
            set
            {
                _输入的数据 = value;
                RaisePropertyChanged(() => 输入的数据);
            }
        }

        /// <summary>
        /// 是否要关闭窗口
        /// </summary>
        private bool toClose = false;
        public bool ToClose
        {
            get
            {
                return toClose;
            }
            set
            {
                toClose = value;
                if (toClose)
                {
                    this.RaisePropertyChanged("ToClose");
                }
            }
        }

        #endregion

        #region 资源类型列表

        /// <summary>
        /// 类型下拉框数据源
        /// </summary>
        private ResTypeModel _类型List;
        public ResTypeModel 类型List
        {
            get { return _类型List; }
            set { _类型List = value; RaisePropertyChanged(() => 类型List); }
        }

        /// <summary>
        /// 游戏下拉框数据源
        /// </summary>
        private ResTypeModel _游戏List;
        public ResTypeModel 游戏List
        {
            get { return _游戏List; }
            set { _游戏List = value; RaisePropertyChanged(() => 游戏List); }
        }

        #endregion


        #region 事件命令

        /// <summary>
        /// 类型选中事件转命令执行
        /// </summary>
        private RelayCommand _类型SelectCommand;
        public RelayCommand 类型SelectCommand
        {
            get
            {
                if (_类型SelectCommand == null)
                    _类型SelectCommand = new RelayCommand(() => 类型ExecuteSelect());
                return _类型SelectCommand;
            }
            set { _类型SelectCommand = value; }
        }
        private void 类型ExecuteSelect()
        {
            if (类型List != null && 类型List.SelectIndex > 0)
            {
                输入的数据.类型 = 类型List.List[类型List.SelectIndex].Key;
            }
        }

        /// <summary>
        /// 游戏选中事件转命令执行
        /// </summary>
        private RelayCommand _游戏SelectCommand;
        public RelayCommand 游戏SelectCommand
        {
            get
            {
                if (_游戏SelectCommand == null)
                    _游戏SelectCommand = new RelayCommand(() => 游戏ExecuteSelect());
                return _游戏SelectCommand;
            }
            set { _游戏SelectCommand = value; }
        }
        private void 游戏ExecuteSelect()
        {
            if (游戏List != null && 游戏List.SelectIndex > 0)
            {
                输入的数据.游戏 = 游戏List.List[游戏List.SelectIndex].Text;
            }
        }

        /// <summary>
        /// 会员Add事件命令
        /// </summary>
        private RelayCommand _会员AddCmd;
        public RelayCommand 会员AddCmd
        {
            get
            {
                if (_会员AddCmd == null)
                    _会员AddCmd = new RelayCommand(() => 会员AddCmdSub());
                return _会员AddCmd;
            }
            set
            {
                _会员AddCmd = value;
            }
        }
        private void 会员AddCmdSub()
        {       
          
            //DispatcherHelper.CheckBeginInvokeOnUI(() =>
            //{
                Messenger.Default.Send<AccountDridModel>(输入的数据, "会员添加");
                ReleaseRegister();
                ViewModelLocator.Cleanup();
                ToClose = true;
            //});
        }

        #endregion

        #region 资源初始化
        /// <summary>
        /// 初始化类型下拉框数据
        /// </summary>
        private void 类型Sub()
        {
            类型List = new ResTypeModel()
            {
                SelectIndex = 0,
                List = new List<ComplexInfoModel>()
                {
                    new ComplexInfoModel(){ Key=0, Text="请选择..." },
                    new ComplexInfoModel(){ Key=1 , Text="自我"},
                    new ComplexInfoModel(){ Key=2 , Text ="迪斯尼"}
                }
            };
        }

        /// <summary>
        /// 初始化游戏下拉框数据
        /// </summary>
        private void 游戏Sub()
        {
            游戏List = new ResTypeModel()
            {
                SelectIndex = 0,
                List = new List<ComplexInfoModel>()
                {
                    new ComplexInfoModel(){ Key=0, Text="请选择..." },
                    new ComplexInfoModel(){ Key=1 , Text ="赛车"},
                    new ComplexInfoModel(){ Key=2 , Text ="枪王"}
                }
            };
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 手动调用释放注册信息（该视图模型内的所有注册信息全部释放）
        /// </summary>
        public void ReleaseRegister()
        {
           
        }
        #endregion

    }
}

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MVVMLightDemo.Content;
using MVVMLightDemo.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMLightDemo.ViewModel
{
    public class BindingAdvancedViewModel:ViewModelBase
    {
        public BindingAdvancedViewModel()
        {
            InitCombbox();
            InitSingleRadio();
            InitCompRadio();
            InitCompCheck();
            InitTreeInfo();
            InitDataGrid();
            InitListBoxList();
            InitUCList();
        }

        #region 属性

        #region 下拉框相关
        private ComplexInfoModel combboxItem;
        /// <summary>
        /// 下拉框选中信息
        /// </summary>
        public ComplexInfoModel CombboxItem
        {
            get { return combboxItem; }
            set { combboxItem = value; RaisePropertyChanged(() => CombboxItem); }
        }


        private List<ComplexInfoModel> combboxList;
        /// <summary>
        /// 下拉框列表
        /// </summary>
        public List<ComplexInfoModel> CombboxList
        {
            get { return combboxList; }
            set { combboxList = value; RaisePropertyChanged(()=>CombboxList); }
        }
        #endregion

        #region 单选框相关

        private String singleRadio;
        /// <summary>
        /// 单选框相关
        /// </summary>
        public String SingleRadio
        {
            get { return singleRadio; }
            set { singleRadio = value; RaisePropertyChanged(()=>SingleRadio); }
        }


        private Boolean isSingleRadioCheck;
        /// <summary>
        /// 单选框是否选中
        /// </summary>
        public Boolean IsSingleRadioCheck
        {
            get { return isSingleRadioCheck; }
            set { isSingleRadioCheck = value; RaisePropertyChanged(()=>IsSingleRadioCheck); }
        }

        #endregion
        
        #region 组合单选框

        private List<CompBottonModel> radioButtons;
        /// <summary>
        /// 组合单选框列表
        /// </summary>
        public List<CompBottonModel> RadioButtons
        {
            get { return radioButtons; }
            set
            {
                radioButtons = value; RaisePropertyChanged(() => RadioButtons);               
            }
        }

        private CompBottonModel radioButton;
        /// <summary>
        /// 组合单选框 选中值
        /// </summary>
        public CompBottonModel RadioButton
        {
            get { return radioButton; }
            set { radioButton = value; RaisePropertyChanged(()=>RadioButton); }
        }
                
        #endregion
        
        #region 复选框
                
        private List<CompBottonModel> checkButtons;
        /// <summary>
        /// 组合复选框
        /// </summary>
        public List<CompBottonModel> CheckButtons
        {
            get { return checkButtons; }
            set
            {
                checkButtons = value; RaisePropertyChanged(() => CheckButtons);
            }
        }

        private String checkInfo;
        /// <summary>
        /// 确认框选中信息
        /// </summary>
        public String CheckInfo
        {
            get { return checkInfo; }
            set { checkInfo = value; RaisePropertyChanged(() => CheckInfo); }
        }        

        #endregion

        #region 树控件

        private List<TreeNodeModel> treeInfo;
        /// <summary>
        /// 树控件数据信息
        /// </summary>
        public List<TreeNodeModel> TreeInfo
        {
            get { return treeInfo; }
            set { treeInfo = value; RaisePropertyChanged(()=>TreeInfo); }
        }


        #endregion

        #region 数据控件 DataGrid

        private ObservableCollection<UserInfoModel> userList;
        /// <summary>
        /// 数据列表
        /// </summary>
        public ObservableCollection<UserInfoModel> UserList
        {
            get { return userList; }
            set { userList = value; RaisePropertyChanged(()=>UserList); }
        }

        #endregion

        #region ListBox 模板

        private IEnumerable listBoxData;
        /// <summary>
        /// LisBox数据模板
        /// </summary>
        public IEnumerable ListBoxData
        {
            get { return listBoxData; }
            set { listBoxData = value;RaisePropertyChanged(()=>ListBoxData); }
        }


        #endregion


        #region 用户控件信息列表

        private ObservableCollection<FruitInfoViewModel> fiList;
        /// <summary>
        /// 用户控件模板列表
        /// </summary>
        public ObservableCollection<FruitInfoViewModel> FiList
        {
            get { return fiList; }
            set { fiList = value; RaisePropertyChanged(() => FiList); }
        }

        #endregion

        #endregion

        #region 命令

        private RelayCommand radioCheckCommand;
        /// <summary>
        /// 单选框命令
        /// </summary>
        public RelayCommand RadioCheckCommand
        {
            get 
            {
                if (radioCheckCommand == null)
                    radioCheckCommand = new RelayCommand(() => ExcuteRadioCommand());
                return radioCheckCommand;
            
            }
            set { radioCheckCommand = value; }
        }
        private void ExcuteRadioCommand()
        {
            RadioButton = RadioButtons.Where(p=>p.IsCheck).First();
        }



        private RelayCommand checkCommand;
        /// <summary>
        /// 复选框命令
        /// </summary>
        public RelayCommand CheckCommand
        {
            get
            {
                if (checkCommand == null)
                    checkCommand = new RelayCommand(() => ExcuteCheckCommand());
                return checkCommand;

            }
            set { checkCommand = value; }
        }
        private void ExcuteCheckCommand()
        { 
            CheckInfo = "";
            if (CheckButtons != null && CheckButtons.Count > 0)
            {
                var list = CheckButtons.Where(p=>p.IsCheck);
               if (list.Count() > 0)
               {  
                   foreach (var l in list)
                   {
                       CheckInfo += l.Content + ",";
                   }
               }
            }
        }



        #endregion

        #region 辅助函数

        private void InitCombbox()
        {
            CombboxList = new List<ComplexInfoModel>() { 
              new ComplexInfoModel(){ Key="1",Text="苹果" },
              new ComplexInfoModel(){ Key="2",Text="香蕉" },
              new ComplexInfoModel(){ Key="3",Text="梨" },
              new ComplexInfoModel(){ Key="4",Text="樱桃" },
            };
        }

        private void InitSingleRadio()
        {
            SingleRadio = "喜欢吃苹果？";
            IsSingleRadioCheck = false;
        }
        
        private void InitCompRadio()
        {
            RadioButtons = new List<CompBottonModel>()
            {
                 new CompBottonModel(){ Content="苹果", IsCheck = false },
                 new CompBottonModel(){ Content="香蕉", IsCheck = false },
                 new CompBottonModel(){ Content="梨", IsCheck = false },
                 new CompBottonModel(){ Content="樱桃", IsCheck = false },
            };
        }
        
        private void InitCompCheck()
        {
            CheckButtons = new List<CompBottonModel>()
            {
                 new CompBottonModel(){ Content="苹果", IsCheck = false },
                 new CompBottonModel(){ Content="香蕉", IsCheck = false },
                 new CompBottonModel(){ Content="梨", IsCheck = false },
                 new CompBottonModel(){ Content="樱桃", IsCheck = false }
            };
        }
        
        private void InitTreeInfo()
        {
            TreeInfo = new List<TreeNodeModel>()
            {
                new TreeNodeModel(){
                  NodeID = "1", NodeName = "苹果",Children=new List<TreeNodeModel>(){
                    new TreeNodeModel(){ NodeID="1.1", NodeName ="苹果A" },
                    new TreeNodeModel(){ NodeID="1.2", NodeName ="苹果B" },
                    new TreeNodeModel(){ NodeID="1.3", NodeName ="苹果C",Children = new List<TreeNodeModel>(){
                       new TreeNodeModel(){ NodeID="1.3.1", NodeName ="苹果C1" },
                       new TreeNodeModel(){ NodeID="1.3.2", NodeName ="苹果C2" },
                    } },

                  }
                },
                new TreeNodeModel(){
                  NodeID = "2", NodeName = "香蕉",Children=new List<TreeNodeModel>(){
                    new TreeNodeModel(){ NodeID="2.1", NodeName ="香蕉A" },
                    new TreeNodeModel(){ NodeID="2.2", NodeName ="香蕉B" },
                    new TreeNodeModel(){ NodeID="2.3", NodeName ="香蕉C" }
                  }
                }
            };
        }

        private void InitDataGrid()
        {
            UserList = new ObservableCollection<UserInfoModel>()
            {
                 new UserInfoModel(){ UserName="周杰伦", UserAdd="沙士大夫擦伤的发送到发送到发送到发送到发送到发送到发生", UserPhone =88888888, UserSex=1 },
                 new UserInfoModel(){ UserName="刘德华", UserAdd="沙士大夫擦伤的发送到发送到发送到发送到发送到发送到发生", UserPhone =88888888, UserSex=1 },
                 new UserInfoModel(){ UserName="刘若英", UserAdd="沙士大夫擦伤的发送到发送到发送到发送到发送到发送到发生", UserPhone =88888888, UserSex=0 }
            };
        }

        private void InitListBoxList()
        {
            ListBoxData = new ObservableCollection<dynamic>(){
              new { Img="/MVVMLightDemo;component/Images/1.jpg",Info="樱桃" },
              new { Img="/MVVMLightDemo;component/Images/2.jpg",Info="葡萄" },
              new { Img="/MVVMLightDemo;component/Images/3.jpg",Info="苹果" },
              new { Img="/MVVMLightDemo;component/Images/4.jpg",Info="猕猴桃" },
              new { Img="/MVVMLightDemo;component/Images/5.jpg",Info="柠檬" },
           };
        }



        private void InitUCList()
        {
            FiList = new ObservableCollection<FruitInfoViewModel>() {
                 new FruitInfoViewModel{ Img = "/MVVMLightDemo;component/Images/1.jpg", Info= "樱桃"},
              new FruitInfoViewModel{ Img = "/MVVMLightDemo;component/Images/2.jpg", Info = "葡萄"}
            };
        }

        #endregion
    }
}
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MVVMLightDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MVVMLightDemo.ViewModel
{
    public class CommandAdvanceViewModel : ViewModelBase
    {
        public CommandAdvanceViewModel()
        {
            //构造 Todo
            ResType = new ResTypeModel()
            {
                SelectIndex = 0,
                List = new List<ComplexInfoModel>() { 
                    new ComplexInfoModel() { Key="0", Text="请选择..." },
                    new ComplexInfoModel() { Key="1", Text="苹果" }, 
                    new ComplexInfoModel() { Key="2", Text="香蕉" },
                    new ComplexInfoModel() { Key="3", Text="樱桃"} }
            };
        }

        #region 属性

        #region 传递单个参数       

        private String argStrTo;
        //目标参数
        public String ArgStrTo
        {
            get { return argStrTo; }
            set { argStrTo = value; RaisePropertyChanged(() => ArgStrTo); }
        }
        #endregion
        
        #region 传递参数对象

        private UserParam objParam;
        public UserParam ObjParam
        {
            get { return objParam; }
            set { objParam = value; RaisePropertyChanged(() => ObjParam); }
        }

        #endregion

        #region 动态参数传递

        private UserParam argsTo;
        /// <summary>
        /// 动态参数传递
        /// </summary>
        public UserParam ArgsTo
        {
            get { return argsTo; }
            set { argsTo = value; RaisePropertyChanged(() => ArgsTo); }
        }

        #endregion

        #region 事件转命令执行

        private String selectInfo;
        /// <summary>
        /// 选中信息
        /// </summary>
        public String SelectInfo
        {
            get { return selectInfo; }
            set { selectInfo = value; RaisePropertyChanged(() => SelectInfo); }
        }



        private ResTypeModel resType;
        /// <summary>
        /// 资源类型列表
        /// </summary>
        public ResTypeModel ResType
        {
            get { return resType; }
            set { resType = value; RaisePropertyChanged(()=>ResType); }
        }

        #endregion

        #region 传递原事件参数

        private String fileAdd;
        /// <summary>
        /// 原事件参数
        /// </summary>
        public String FileAdd
        {
            get { return fileAdd; }
            set { fileAdd = value; RaisePropertyChanged(() => FileAdd); }
        }

        #endregion

        #endregion

        #region 命令

        private RelayCommand<String> passArgStrCommand;
        /// <summary>
        /// 传递单个参数命令
        /// </summary>
        public RelayCommand<String> PassArgStrCommand
        {
            get
            {
                if (passArgStrCommand == null)
                    passArgStrCommand = new RelayCommand<String>((p) => ExecutePassArgStr(p));
                return passArgStrCommand;

            }
            set { passArgStrCommand = value; }
        }
        private void ExecutePassArgStr(String arg)
        {
            ArgStrTo = arg;
        }

        //=================================================================================================================
        private RelayCommand<UserParam> passArgObjCmd;
        /// <summary>
        /// 传递对象参数
        /// </summary>
        public RelayCommand<UserParam> PassArgObjCmd
        {
            get
            {
                if (passArgObjCmd == null)
                    passArgObjCmd = new RelayCommand<UserParam>((p) => ExecutePassArgObj(p));
                return passArgObjCmd;    
            }
            set { passArgObjCmd = value; }
        }
        private void ExecutePassArgObj(UserParam up)
        {
            ObjParam = up;
        }

        //=================================================================================================================
        private RelayCommand<UserParam> dynamicParamCmd;
        /// <summary>
        /// 动态参数传递
        /// </summary>
        public RelayCommand<UserParam> DynamicParamCmd
        {
            get
            {
                if (dynamicParamCmd == null)
                    dynamicParamCmd = new RelayCommand<UserParam>(p => ExecuteDynPar(p));
                return dynamicParamCmd;
            }
            set
            {
               
                dynamicParamCmd = value;
            }
        }

        private void ExecuteDynPar(UserParam up)
        {
            ArgsTo = up;
        }

        //=================================================================================================================

        private RelayCommand selectCommand;
        /// <summary>
        /// 事件转命令执行
        /// </summary>
        public RelayCommand SelectCommand
        {
            get
            {
                if (selectCommand == null)
                    selectCommand = new RelayCommand(() => ExecuteSelect());
                return selectCommand;
            }
            set { selectCommand = value; }
        }
        private void ExecuteSelect()
        {
            if (ResType != null && ResType.SelectIndex > 0)
            {
                SelectInfo = ResType.List[ResType.SelectIndex].Text;
            }
        }

        //=================================================================================================================

        private RelayCommand<DragEventArgs> dropCommand;
        /// <summary>
        /// 传递原事件参数
        /// </summary>
        public RelayCommand<DragEventArgs> DropCommand
        {
            get
            {
                if (dropCommand == null)
                    dropCommand = new RelayCommand<DragEventArgs>(e => ExecuteDrop(e));
                return dropCommand;
            }
            set { dropCommand = value; }
        }    

        private void ExecuteDrop(DragEventArgs e)
        {
            FileAdd = ((System.Array)e.Data.GetData(System.Windows.DataFormats.FileDrop)).GetValue(0).ToString(); 
        }


        #endregion
    }
}

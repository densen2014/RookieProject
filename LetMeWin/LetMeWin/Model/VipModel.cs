using GalaSoft.MvvmLight;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeWin.Model
{
   public class VipModel
    {
    }
    [SugarTable("AccountGrid")]
    public class AccountDridModel : ObservableObject
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int      Id { get; set; }

        [SugarColumn(IsIgnore = true)]
        public int    序号 { get; set; }

        public int    勾选 { get; set; }

        public string 帐号 { get; set; }

        public string 密码 { get; set; }

        public double 积分 { get; set; }

        public int    类型 { get; set; }

        public int    停止 { get; set; }

        public string 比例 { get; set; }

        public string 游戏 { get; set; }

         private string _登录状态;

         [SugarColumn(IsIgnore = true)]
       public string 登录状态
        {
            get { return _登录状态; }
            set
            {
                _登录状态 = value;
                RaisePropertyChanged(() => 登录状态);
            }
        }
    }

    //[SugarTable("AccountGrid")]
    //public class AccountDridModel: ObservableObject
    //{

    //    private int id;
    //    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    //    public int Id
    //    {
    //        get { return id; }
    //        set
    //        {
    //            id = value;
    //            RaisePropertyChanged(() => Id);
    //        }
    //    }

    //    private int _序号;

    //    public int 序号
    //    {
    //        get { return _序号; }
    //        set
    //        {
    //            _序号 = value;
    //            RaisePropertyChanged(() => 序号);
    //        }
    //    }
    //    private int _勾选;

    //    public int 勾选
    //    {
    //        get { return _勾选; }
    //        set
    //        {
    //            _勾选 = value;
    //            RaisePropertyChanged(() => 勾选);
    //        }
    //    }

    //    private string _帐号;

    //    public string 帐号
    //    {
    //        get { return _帐号; }
    //        set
    //        {
    //            _帐号 = value;
    //            RaisePropertyChanged(() => 帐号);
    //        }
    //    }

    //    private string _密码;

    //    public string 密码
    //    {
    //        get { return _密码; }
    //        set
    //        {
    //            _密码 = value;
    //            RaisePropertyChanged(() => 密码);
    //        }
    //    }

    //    private double _积分;

    //    public double 积分
    //    {
    //        get { return _积分; }
    //        set
    //        {
    //            _积分 = value;
    //            RaisePropertyChanged(() => 积分);
    //        }
    //    }

    //    private int _类型;

    //    public int 类型
    //    {
    //        get { return _类型; }
    //        set
    //        {
    //            _类型 = value;
    //            RaisePropertyChanged(() => 类型);
    //        }
    //    }




    //}
}

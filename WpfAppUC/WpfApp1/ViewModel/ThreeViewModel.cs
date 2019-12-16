using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model;

namespace WpfApp1.ViewModel
{
    public class ThreeViewModel : ViewModelBase
    {
        public ThreeViewModel()
        {
            AccountGridData = new ObservableCollection<ThreeModel>();

            AccountGridData.Add(new ThreeModel { Id =1, 勾选 =1, 序号=1, 索引=0,  帐号="123", 密码="qwe", 游戏="舞动"});
        }

        private ObservableCollection<ThreeModel> accountDridModel;
        public ObservableCollection<ThreeModel> AccountGridData
        {
            get { return accountDridModel; }
            set
            {
                accountDridModel = value;   
                RaisePropertyChanged(() => AccountGridData);
            }
        }

    }
}

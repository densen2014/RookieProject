using GalaSoft.MvvmLight;
using System.Collections.Generic;
using WpfApp1.Model;

namespace WpfApp1.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ʵ��ViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
      
         public ʵ��ViewModel()
        {
            UserInfo = new ʵ����();

            //������ĳ�ʼ����
            CombboxList = new List<ComplexInfoModel>()
            {
                  new ComplexInfoModel(){  Key="1",Text="ƻ��"},
                  new ComplexInfoModel(){  Key="2",Text="�㽶"}
            };
        }

         

        #region ˫���

        private ʵ���� userInfo;
        /// <summary>
        /// �û���Ϣ
        /// </summary>
        public ʵ���� UserInfo
        {
            get { return userInfo; }
            set { userInfo = value; RaisePropertyChanged(() => UserInfo); }
        }
        #endregion

        #region ������
        private ComplexInfoModel combboxItem;
        /// <summary>
        /// ������ѡ����Ϣ
        /// </summary>
        public ComplexInfoModel CombboxItem
        {
            get { return combboxItem; }
            set { combboxItem = value; RaisePropertyChanged(() => CombboxItem); }
        }


        private List<ComplexInfoModel> combboxList;
        /// <summary>
        /// �������б�
        /// </summary>
        public List<ComplexInfoModel> CombboxList
        {
            get { return combboxList; }
            set { combboxList = value; RaisePropertyChanged(() => CombboxList); }
        }

       



        #endregion

        #region ����
        #endregion

    }
}
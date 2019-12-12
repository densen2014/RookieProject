using MVVMLightDemo.Common;
using MVVMLightDemo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MVVMLightDemo.View
{
    /// <summary>
    /// Interaction logic for DispatchBasic.xaml
    /// </summary>
    public partial class DispatchBasic : Window
    {
       private ObservableCollection<UserParam> userList { get; set; }
        public DispatchBasic()
        {
            InitializeComponent();
            InitData();
        }


        /// <summary>
        /// 执行提交操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtUserName.Text))
            {
                MessageBox.Show("姓名不为空", "错误提示！");
                return;
            }
            if (String.IsNullOrEmpty(txtUserPhone.Text))
            {
                MessageBox.Show("电话不为空", "错误提示！");
                return;
            }
            if (String.IsNullOrEmpty(txtUserAdd.Text))
            {
                MessageBox.Show("地址不为空", "错误提示！");
                return;
            }
            if (String.IsNullOrEmpty(txtUserSex.Text))
            {
                MessageBox.Show("性别不为空", "错误提示！");
                return;
            }
           

            UserParam up = new UserParam() { UserAdd = txtUserAdd.Text, UserName = txtUserName.Text, UserPhone = txtUserPhone.Text, UserSex = txtUserSex.Text };
            CreateUserInfoHelper creatUser = new CreateUserInfoHelper(up);
            creatUser.CreateProcess += new EventHandler<CreateUserInfoHelper.CreateArgs>(CreateProcess);
            creatUser.Create();
            processPanel.Visibility = Visibility.Visible;
        }


        private void CreateProcess(object sender, CreateUserInfoHelper.CreateArgs args)
        {
            this.Dispatcher.BeginInvoke((Action)delegate()
            {
                processBar.Value = args.process;
                processInfo.Text = String.Format("创建进度：{0}/100",args.process);
                if (args.isFinish)
                {
                    if (args.userInfo != null)
                    {
                        ObservableCollection<UserParam> data = (ObservableCollection<UserParam>)dg.DataContext;
                        data.Add(args.userInfo);
                        dg.DataContext = data;
                    }
                    processPanel.Visibility = Visibility.Hidden;
                    ClearForm();
                }
            });
        }

        private void InitData()
        {
            userList = new ObservableCollection<UserParam>()
            {
                 new UserParam(){ UserName="周杰伦", UserAdd="周杰伦地址", UserPhone ="88888888", UserSex="男" },
                 new UserParam(){ UserName="刘德华", UserAdd="刘德华地址", UserPhone ="88888888", UserSex="男" },
                 new UserParam(){ UserName="刘若英", UserAdd="刘若英地址", UserPhone ="88888888", UserSex="女" }
            };
            dg.DataContext = userList;
        }


        private void ClearForm()
        {
            txtUserSex.Text = "";
            txtUserPhone.Text = "";
            txtUserName.Text = "";
            txtUserAdd.Text = "";
        }

    }
}

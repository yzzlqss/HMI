using HMIWindows.ToolsCs;
using System;
using System.Collections.Generic;
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

namespace HMIWindows
{
    /// <summary>
    /// WPFLogin.xaml 的交互逻辑
    /// </summary>
    public partial class WPFLogin : Window
    {
        //==========================Public================================================
        public List<String> LstSource { get; set; }//必须定义public
        public string LstSelect { get; set; }//必须定义public

        /// <summary>
        /// Initial
        /// </summary>
        public WPFLogin()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }
        private void Window_Activated(object sender, EventArgs e)
        {
            //绑定下拉框数据
            LstSource = new List<string>() { "Admin", "User" };
            LstSelect = "Admin";
            this.DataContext = this;
        }

        /// <summary>
        /// 登录按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (cmbUserName.SelectedItem.ToString() == "Admin" && pwUserPassword.Password == "123456")
            {
                OpenWindows.OpenWPF(10);
                OpenWindows.MiniWPF("WPFMain");
            }
            else
            {
                MessageBox.Show("密码错误！请输入正确的密码！");
            }
        }

       
    }
}

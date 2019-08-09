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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using HMIWindows.ToolsCs;
using Sharp7;

namespace HMIWindows
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WPFMain : Window
    {
        //==========================Public================================================
        //定义timer，刷新数据
        private static DispatcherTimer timer { get; set; }
        //-------------- Create and connect the client//
        private S7Client client = new S7Client();
        //TimerProcess tp = new TimerProcess();
        //Label lb = new Label();

        /// <summary>
        /// Initial
        /// </summary>
        public WPFMain()
        {
            InitializeComponent();
            //WindowState = WindowState.Maximized;
            //WindowStyle = WindowStyle.None;
            //-------------- Disconnect the client
            //client.Disconnect();
        }
        private void Window_Activated(object sender, EventArgs e)
        {

            //加载timer定时器，不停刷新数据
            Loaded += new RoutedEventHandler(TempTest_Loaded);
        }

        #region Timer
        //======================Timer====================================================
        /// <summary>
        /// 定时器加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TempTest_Loaded(object sender, RoutedEventArgs e)//
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer1_Tick;
            timer.Start();
        }
        /// <summary>
        /// 定时器刷新处理数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer1_Tick(object sender, EventArgs e)
        {
            //你的定时处理

            //主窗口显示系统时间
            txbTime.Text = DateTime.Now.ToString();

            //建立与plc连接
            int result = Sharp7AndPlcComm.ConnectToPlc(client);
            //判断通讯状态
            Sharp7AndPlcComm.JudgeCommStu(result, ref labCommStu);
        }
        #endregion


        #region OpenWindows
        //=======================OpenWindows=================================================================
        /// <summary>
        /// 打开主窗口 10
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMain_Click(object sender, RoutedEventArgs e)
        {
            //OpenWindows.OpenWPF(10);
            //OpenWindows.MiniWPF("WPFMain");
        }
        /// <summary>
        /// 打开手动窗口 20
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMannal_Click(object sender, RoutedEventArgs e)
        {
            OpenWindows.OpenWPF(20);
            OpenWindows.MiniWPF("WPFManual");
        }
        /// <summary>
        /// 打开自动窗口 30
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAuto_Click(object sender, RoutedEventArgs e)
        {
            OpenWindows.OpenWPF(30);
            OpenWindows.MiniWPF("WPFAuto");
        }
        /// <summary>
        /// 打开信号窗口 40
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSignals_Click(object sender, RoutedEventArgs e)
        {
            OpenWindows.OpenWPF(40);
            OpenWindows.MiniWPF("WPFSignals");
        }
        /// <summary>
        /// 打开报警窗口 50
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAlarm_Click(object sender, RoutedEventArgs e)
        {
            OpenWindows.OpenWPF(50);
            OpenWindows.MiniWPF("WPFAlarm");
        }

        /// <summary>
        /// 退出程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnShutdown_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        #endregion

    }
}

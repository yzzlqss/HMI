using HMIWindows.ToolsCs;
using Sharp7;
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
using System.Windows.Threading;

namespace HMIWindows
{
    /// <summary>
    /// WPFWPFSignals.xaml 的交互逻辑
    /// </summary>
    public partial class WPFSignals : Window
    {
        //==========================Public================================================
        //-------------- Create and connect the client//
        private S7Client client = new S7Client();

        //定义timer，刷新数据
        private static DispatcherTimer timer { get; set; }

        //定义气缸显示数组
        private Ellipse[] ellipses;


        /// <summary>
        /// Initial
        /// </summary>
        public WPFSignals()
        {
            InitializeComponent();
            //WindowState = WindowState.Maximized;
            //WindowStyle = WindowStyle.None;

        }
        private void Window_Activated(object sender, EventArgs e)
        {

            //初始化气缸显示
            ellipses = new Ellipse[] { Open12A15, Open12A16, Open12A17, Open12A18, Open12A19, Open12A20, Open12A21, Open12A22, Open12A23, Open12A24, Open12A25, Open12A26, Close12A1, Close12A2, Close12A3, Close12A4 };

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

            //建立与plc连接
            int result = Sharp7AndPlcComm.ConnectToPlc(client);
            //判断通讯状态
            Sharp7AndPlcComm.JudgeCommStu(result, ref labCommStu);

            //如果通讯成功，则调用读取或写入函数
            if (result == 0)
            {
                // ReadPlcData.ReadValveSignals(ref ellipses, client, ref txtReadServoPos);
                //WritePlcData.WriteDb(client, ref txtWriteServoPos);
            }
            else//如果通讯失败，清除所有气缸信号
            {
                ReadPlcData.ClearValveSignals(client, ref ellipses);
            }
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
            OpenWindows.OpenWPF(10);
            OpenWindows.MiniWPF("WPFMain");
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
            //OpenWindows.OpenWPF(40);
            //OpenWindows.MiniWPF("WPFSignals");
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
        #endregion
    }
}

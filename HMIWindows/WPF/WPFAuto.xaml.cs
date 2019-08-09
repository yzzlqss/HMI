using HMIWindows.ToolsCs;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Sharp7;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Dynamic;
using Microsoft.Research.DynamicDataDisplay;
using DynamicDataDisplaySample.VoltageViewModel;

namespace HMIWindows
{
    /// <summary>
    /// WPFAuto.xaml 的交互逻辑
    /// </summary>
    public partial class WPFAuto : Window
    {
        #region 曲线刷新

        private ObservableDataSource<Point> dataSource = new ObservableDataSource<Point>();

        private int i = 0;

        private double current;

        private void AnimatedPlot()// private void AnimatedPlot(object sender, EventArgs e)
        {
            double x = i;
            double y = current;

            Point point = new Point(x, y);
            dataSource.AppendAsync(base.Dispatcher, point);
            dataSource.ResumeUpdate();
            cpuUsageText.Text = string.Format("{0:0}V", y);//数值显示，非曲线！
            i++;
        }

        /// <summary>
        /// 窗口加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            plotter.AddLineGraph(dataSource, Colors.Green, 2, "Percentage");//增加曲线

            plotter.Viewport.FitToView();//调用曲线刷新

        }


        #endregion




        //==========================Public================================================

        //定义timer，刷新数据
        private static DispatcherTimer timer { get; set; }
        //-------------- Create and connect the client//
        private S7Client client = new S7Client();
        private string[] strs = new string[2];

        /// <summary>
        /// Initial
        /// </summary>
        public WPFAuto()
        {
            InitializeComponent();

            //WindowState = WindowState.Maximized;
            //WindowStyle = WindowStyle.None;
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

            //建立与plc连接
            int result = Sharp7AndPlcComm.ConnectToPlc(client);
            //判断通讯状态
            Sharp7AndPlcComm.JudgeCommStu(result, ref labCommStu);

            if (result == 0)
            {
                strs = ReadPlcData.ReadAutoStep(client, 14, 209, 206);
                txbAutoStep1.Text = strs[0];//当前步骤
                txbAutoStep2.Text = strs[1];//下一步骤

                current = ReadPlcData.ReadWeldingCurrent(client);
            }

            AnimatedPlot();


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
            //OpenWindows.OpenWPF(30);
            //OpenWindows.MiniWPF("WPFAuto");
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
        #endregion


    }
}

using HMIWindows.ToolsCs;
using Sharp7;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// WPFAlarm.xaml 的交互逻辑
    /// </summary>
    public partial class WPFAlarm : Window
    {
        //==========================Public================================================
        //定义timer，刷新数据
        private static DispatcherTimer timer { get; set; }
        //-------------- Create and connect the client//
        private S7Client client = new S7Client();
        private const int dbNum = 30;
        private const int dbLength = 98;
        private const int dbStartElement = 0;
        private const int dbStartBit = 0;


        /// <summary>
        /// Initial
        /// </summary>
        public WPFAlarm()
        {
            InitializeComponent();
            //WindowState = WindowState.Maximized;
            //WindowStyle = WindowStyle.None;
        }
        private void Window_Activated(object sender, EventArgs e)
        {
            //加载timer定时器，不停刷新数据
            Loaded += new RoutedEventHandler(TimerAlarm_Loaded);//01

        }

        #region Timer
        //======================Timer====================================================
        /// <summary>
        /// 定时器加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TimerAlarm_Loaded(object sender, RoutedEventArgs e)//02
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
        void timer1_Tick(object sender, EventArgs e)//03
        {
            //你的定时处理

            //建立与plc连接
            int result = Sharp7AndPlcComm.ConnectToPlc(client);
            //判断通讯状态
            Sharp7AndPlcComm.JudgeCommStu(result, ref labCommStu);
            //调用报警列表，刷新报警文本
            AlarmList.AlarmListView(client, listView, dbNum, dbLength, dbStartElement, dbStartBit);
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
            //OpenWindows.OpenWPF(50);
            //OpenWindows.MiniWPF("WPFAlarm");
        }
        #endregion

        //======================================ResetAlarm==============================================================
        /// <summary>
        /// 故障复位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = listView.SelectedIndex;
                listView.Items.RemoveAt(index);
            }
            catch
            {
                MessageBox.Show("历史报警已清除完毕!");
                return;
            }

        }

        /// <summary>
        /// 点击listview列进行排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_Click_1(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is GridViewColumnHeader)
            {
                //获得点击的列
                GridViewColumn clickedColumn = (e.OriginalSource as GridViewColumnHeader).Column;
                if (clickedColumn != null)
                {
                    //Get binding property of clicked column
                    string bidingProperty = (clickedColumn.DisplayMemberBinding as Binding).Path.Path;

                    //获得listview项是如何排序的
                    SortDescriptionCollection sdc = this.listView.Items.SortDescriptions;

                    //按升序进行排序
                    ListSortDirection sortDirection = ListSortDirection.Ascending;
                    if (sdc.Count > 0)
                    {
                        SortDescription sd = sdc[0];
                        sortDirection = (ListSortDirection)(((-(int)sd.Direction) + 1) % 2);//((((int)sd.Direction) + 1) % 2)，原实例没有-号
                        sdc.Clear();
                    }
                    sdc.Add(new SortDescription(bidingProperty, sortDirection));
                }
            }

        }

    }
}

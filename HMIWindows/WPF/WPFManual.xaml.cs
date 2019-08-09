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
    /// WPFManual.xaml 的交互逻辑
    /// </summary>
    public partial class WPFManual : Window
    {
        //==========================Public================================================
        //-------------- Create and connect the client//
        private S7Client client = new S7Client();
        //定义timer，刷新数据
        private static DispatcherTimer timer { get; set; }
        //定义气缸显示数组
        private Ellipse[] ellipses;
        //定义按钮数组
        //private Button[] btns;

        private int result { get; set; }

        private const int readValveDbNum = 14;
        private const int readValveDbByteLength = 208;
        private const int readValveDbStartElement = 190;
        private const int readValveDbStartBit = 6;
        //private const int writeBtnDbNum = 100;
        //private const int writeBtnDbStartElement = 201;
        //private const int writeBtnDbStartBit = 6;

        /// <summary>
        /// Initial
        /// </summary>
        public WPFManual()
        {
            InitializeComponent();
            //WindowState = WindowState.Maximized;
            //WindowStyle = WindowStyle.None;

        }
        private void Window_Initialized(object sender, EventArgs e)
        {
            //添加气缸显示数组
            ellipses = new Ellipse[] { ellOpen08A1, ellOpen08A2, ellOpen08A3, ellOpen08A4, ellOpen08A5, ellOpen08A6, ellOpen08A7, ellOpen08A8, ellOpen08A9, ellOpen08A10, ellOpen08A11, ellOpen08A12, ellOpen08A13,
            ellOpen08A14, ellOpen08A15, ellOpen08A16, ellOpen08A17, ellOpen08A18, ellOpen08A19, ellOpen08A20, ellOpen08A21, ellOpen08A22, ellOpen08A23,ellOpen08A24,ellOpen08A25,ellOpen08A26,
            ellClose08A1,ellClose08A2,ellClose08A3,ellClose08A4,ellClose08A5,ellClose08A6,ellClose08A7,ellClose08A8,ellClose08A9,ellClose08A10,ellClose08A11,ellClose08A12,ellClose08A13,
            ellClose08A14,ellClose08A15,ellClose08A16,ellClose08A17,ellClose08A18,ellClose08A19,ellClose08A20,ellClose08A21,ellClose08A22,ellClose08A23,ellClose08A24,ellClose08A25,ellClose08A26};
            ////添加按钮数组
            //btns = new Button[] {btnOpen08A1,btnClose08A1, btnOpen08A2, btnClose08A2, btnOpen08A3, btnClose08A3, btnOpen08A4, btnClose08A4, btnOpen08A5, btnClose08A5, btnOpen08A6, btnClose08A6, btnOpen08A7, btnClose08A7,
            //btnOpen08A8,btnClose08A8,btnOpen08A9,btnClose08A9,btnOpen08A10,btnClose08A10,btnOpen08A11,btnClose08A11,btnOpen08A12,btnClose08A12,btnOpen08A14,btnClose08A14,btnOpen08A15,btnClose08A15,btnOpen08A16,btnClose08A16,
            //btnOpen08A17,btnClose08A17,btnOpen08A18,btnClose08A18,btnOpen08A19,btnClose08A19,btnOpen08A20,btnClose08A20,btnOpen08A21,btnClose08A21,btnOpen08A22,btnClose08A22,btnOpen08A23,btnClose08A23,btnOpen08A24,btnClose08A24,
            //btnOpen08A25,btnClose08A25,btnOpen08A26,btnClose08A26};
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
            result = Sharp7AndPlcComm.ConnectToPlc(client);
            //判断通讯状态
            Sharp7AndPlcComm.JudgeCommStu(result, ref labCommStu);
            //如果通讯成功，则调用读取或写入函数
            if (result == 0)
            {
                ReadPlcData.ReadValveSignals(client, ref ellipses, readValveDbNum, readValveDbByteLength, readValveDbStartElement, readValveDbStartBit);
                position.Text = ReadPlcData.ReadManualData(client, 14, 208, 52);
                WritePlcData.WriteDb(client, ref setPosition, ref setSpeed, 14, new int[] { 60, 68 });
                //WritePlcData.WriteDb(client, ref txtWriteServoPos);
                //WritePlcData.BtnToPLC(client,ref btns, writeBtnDbNum, writeBtnDbStartElement, writeBtnDbStartBit);
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
            //OpenWindows.OpenWPF(20);
            //OpenWindows.MiniWPF("WPFManual");
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






        #endregion

        #region Buttons
        private void BtnOpen08A1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 201, 6);
        }

        private void BtnOpen08A1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 201, 6);
        }

        private void BtnClose08A1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 201, 7);
        }

        private void BtnClose08A1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 201, 7);
        }

        private void BtnOpen08A2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 202, 0);
        }

        private void BtnOpen08A2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 202, 0);
        }

        private void BtnClose08A2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 202, 1);
        }

        private void BtnClose08A2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 202, 1);
        }

        private void BtnOpen08A3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 202, 2);
        }

        private void BtnOpen08A3_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 202, 2);
        }

        private void BtnClose08A3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 202, 3);
        }

        private void BtnClose08A3_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 202, 3);
        }

        private void BtnOpen08A4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 202, 4);
        }

        private void BtnOpen08A4_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 202, 4);
        }

        private void BtnClose08A4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 202, 5);
        }

        private void BtnClose08A4_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 202, 5);
        }

        private void BtnOpen08A5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 202, 6);
        }

        private void BtnOpen08A5_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 202, 6);
        }

        private void BtnClose08A5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 202, 7);
        }

        private void BtnClose08A5_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 202, 7);
        }

        private void BtnOpen08A6_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 203, 0);
        }

        private void BtnOpen08A6_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 203, 0);
        }

        private void BtnClose08A6_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 203, 1);
        }

        private void BtnClose08A6_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 203, 1);
        }

        private void BtnOpen08A7_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 203, 2);
        }

        private void BtnOpen08A7_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 203, 2);
        }

        private void BtnClose08A7_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 203, 3);
        }

        private void BtnClose08A7_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 203, 3);
        }

        private void BtnOpen08A8_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 203, 4);
        }

        private void BtnOpen08A8_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 203, 4);
        }

        private void BtnClose08A8_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 203, 5);
        }

        private void BtnClose08A8_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 203, 5);
        }

        private void BtnOpen08A9_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 203, 6);
        }

        private void BtnOpen08A9_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 203, 6);
        }

        private void BtnClose08A9_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 203, 7);
        }

        private void BtnClose08A9_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 203, 7);
        }

        private void BtnOpen08A10_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 204, 0);
        }

        private void BtnOpen08A10_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 204, 0);
        }

        private void BtnClose08A10_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 204, 1);
        }

        private void BtnClose08A10_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 204, 1);
        }

        private void BtnOpen08A11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 204, 2);
        }

        private void BtnOpen08A11_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 204, 2);
        }

        private void BtnClose08A11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 204, 3);
        }

        private void BtnClose08A11_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 204, 3);
        }

        private void BtnOpen08A12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 204, 4);
        }

        private void BtnOpen08A12_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 204, 4);
        }

        private void BtnClose08A12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 204, 5);
        }

        private void BtnClose08A12_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 204, 5);
        }
        //A13=====================
        private void BtnOpen08A13_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // WritePlcData.BtnToPlcSet(client, 100, 203, 4);
        }

        private void BtnOpen08A13_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // WritePlcData.BtnToPlcReset(client, 100, 203, 4);
        }
        private void BtnClose08A13_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // WritePlcData.BtnToPlcSet(client, 100, 202, 1);
        }

        private void BtnClose08A13_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // WritePlcData.BtnToPlcReset(client, 100, 202, 1);
        }

        private void BtnOpen08A14_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 204, 6);
        }

        private void BtnOpen08A14_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 204, 6);
        }

        private void BtnClose08A14_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 204, 7);
        }

        private void BtnClose08A14_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 204, 7);
        }

        private void BtnOpen08A15_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 205, 0);
        }

        private void BtnOpen08A15_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 205, 0);
        }

        private void BtnClose08A15_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 205, 1);
        }

        private void BtnClose08A15_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 205, 1);
        }

        private void BtnOpen08A16_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 205, 2);
        }

        private void BtnOpen08A16_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 205, 2);
        }

        private void BtnClose08A16_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 205, 3);
        }

        private void BtnClose08A16_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 205, 3);
        }

        private void BtnOpen08A17_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 205, 4);
        }

        private void BtnOpen08A17_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 205, 4);
        }

        private void BtnClose08A17_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 205, 5);
        }

        private void BtnClose08A17_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 205, 5);
        }

        private void BtnOpen08A18_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 205, 6);
        }

        private void BtnOpen08A18_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 205, 6);
        }

        private void BtnClose08A18_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 205, 7);
        }

        private void BtnClose08A18_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 205, 7);
        }

        private void BtnOpen08A19_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 206, 0);
        }

        private void BtnOpen08A19_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 206, 0);
        }

        private void BtnClose08A19_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 206, 1);
        }

        private void BtnClose08A19_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 206, 1);
        }

        private void BtnOpen08A20_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 206, 2);
        }

        private void BtnOpen08A20_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 206, 2);
        }

        private void BtnClose08A20_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 206, 3);
        }

        private void BtnClose08A20_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 206, 3);
        }

        private void BtnOpen08A21_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 206, 4);
        }

        private void BtnOpen08A21_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 206, 4);
        }

        private void BtnClose08A21_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 206, 5);
        }

        private void BtnClose08A21_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 206, 5);
        }

        private void BtnOpen08A22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 206, 6);
        }

        private void BtnOpen08A22_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 206, 6);
        }

        private void BtnClose08A22_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 206, 7);
        }

        private void BtnClose08A22_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 206, 7);
        }

        private void BtnOpen08A23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 207, 0);
        }

        private void BtnOpen08A23_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 207, 0);
        }

        private void BtnClose08A23_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 207, 1);
        }

        private void BtnClose08A23_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 207, 1);
        }

        private void BtnOpen08A24_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 207, 2);
        }

        private void BtnOpen08A24_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 207, 2);
        }

        private void BtnClose08A24_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 207, 3);
        }

        private void BtnClose08A24_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 207, 3);
        }

        private void BtnOpen08A25_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 207, 4);
        }

        private void BtnOpen08A25_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 207, 4);
        }

        private void BtnClose08A25_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 207, 5);
        }

        private void BtnClose08A25_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 207, 5);
        }

        private void BtnOpen08A26_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 207, 6);
        }

        private void BtnOpen08A26_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 207, 6);
        }

        private void BtnClose08A26_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 100, 207, 7);
        }

        private void BtnClose08A26_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 100, 207, 7);
        }

        #endregion

        #region ButtonsServo
        private void BtnServoStart_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 14, 76, 0);
        }

        private void BtnServoStart_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 14, 76, 0);
        }

        private void BtnServoHalt_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 14, 76, 1);
        }

        private void BtnServoHalt_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 14, 76, 1);
        }

        private void BtnServoForward_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 14, 76, 2);
        }

        private void BtnServoForward_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 14, 76, 2);
        }

        private void BtnServoBackward_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 14, 76, 3);
        }

        private void BtnServoBackward_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 14, 76, 3);
        }

        private void BtnServoGoHome_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 14, 76, 4);
        }

        private void BtnServoGoHome_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 14, 76, 4);
        }

        private void BtnServoReset_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcSet(client, 14, 76, 5);
        }

        private void BtnServoReset_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WritePlcData.BtnToPlcReset(client, 14, 76, 5);
        }
        #endregion

     
 
    }
}

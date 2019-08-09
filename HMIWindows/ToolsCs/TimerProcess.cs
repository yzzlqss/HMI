using Sharp7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace HMIWindows.ToolsCs
{
    class TimerProcess
    {
        //定义timer，刷新数据
        private DispatcherTimer timer { get; set; }
        public Label labCommStu = new Label();
        // public RoutedEventHandler Loaded { get; private set; }

        public Label TimerEvent()
        {

            //加载timer定时器，不停刷新数据
            RoutedEventHandler Loaded = new RoutedEventHandler(TempTest_Loaded);

            return labCommStu;
        }
        //======================Timer====================================================
        /// <summary>
        /// 定时器加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TempTest_Loaded(object sender, RoutedEventArgs e)//
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.5);
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
            //-------------- Create and connect the client//
            S7Client client = new S7Client();
            //建立与plc连接
            int result = Sharp7AndPlcComm.ConnectToPlc(client);
            //判断通讯状态
            Sharp7AndPlcComm.JudgeCommStu(result, ref labCommStu);
        }


    }
}

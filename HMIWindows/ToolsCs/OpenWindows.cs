using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HMIWindows.ToolsCs
{
    public static class OpenWindows
    {
        /// <summary>
        /// 打开或新建窗口
        /// </summary>
        /// <param name="intWPF"></param>
        public static void OpenWPF(int intWPF)
        {
            WPFMain main;
            WPFManual manual;
            WPFAuto auto;
            WPFSignals signal;
            WPFAlarm alarm;

            //如果存在窗口，则打开窗口且最大化。
            foreach (Window item in Application.Current.Windows)
            {
                if (intWPF == 10 && item.Title == "WPFMain")
                {
                    item.Show();
                    //item.WindowState = WindowState.Maximized;
                    item.WindowState = WindowState.Normal;
                    return;
                }

                if (intWPF == 20 && item.Title == "WPFManual")
                {
                    item.Show();
                    //item.WindowState = WindowState.Maximized;
                    item.WindowState = WindowState.Normal;
                    return;
                }

                if (intWPF == 30 && item.Title == "WPFAuto")
                {
                    item.Show();
                    //item.WindowState = WindowState.Maximized;
                    item.WindowState = WindowState.Normal;
                    return;
                }

                if (intWPF == 40 && item.Title == "WPFSignals")
                {
                    item.Show();
                    //item.WindowState = WindowState.Maximized;
                    item.WindowState = WindowState.Normal;
                    return;
                }

                if (intWPF == 50 && item.Title == "WPFAlarm")
                {
                    item.Show();
                    //item.WindowState = WindowState.Maximized;
                    item.WindowState = WindowState.Normal;
                    return;
                }

            }

            //如果没有窗口则新建窗口
            //main=10;manual=20;WPFAuto=30;signal=40;alarm=50;
            switch (intWPF)
            {
                case 10:
                    main = new WPFMain();
                    main.Show();
                    break;
                case 20:
                    manual = new WPFManual();
                    manual.Show();
                    break;
                case 30:
                    auto = new WPFAuto();
                    auto.Show();
                    break;
                case 40:
                    signal = new WPFSignals();
                    signal.Show();
                    break;
                case 50:
                    alarm = new WPFAlarm();
                    alarm.Show();
                    break;
            }
        }


        /// <summary>
        /// 最小化非当前显示窗口
        /// </summary>
        /// <param name="str">当前窗口标题</param>
        public static void MiniWPF(string str)
        {
            foreach (Window item in Application.Current.Windows)
            {
                if (item.Title == "") continue;//忽略空标题窗口

                if (item.Title != str)
                {
                    //关闭
                    //item.Close();
                    //最小化
                    item.WindowState = WindowState.Minimized;
                }
            }
        }


    }
}

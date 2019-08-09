using Sharp7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HMIWindows.ToolsCs
{
    public static class Sharp7AndPlcComm
    {
        //-------------- Create and connect the client
        //public static S7Client client = new S7Client();

        /// <summary>
        /// 建立与plc通讯
        /// </summary>
        public static int ConnectToPlc(S7Client client)
        {
            
            int result = client.ConnectTo("10.50.77.151", 0, 1);//机架0；插槽1//"10.50.77.151", 0, 1
      
            return result;
        }

        /// <summary>
        /// 通讯状态
        /// </summary>
        /// <param name="result"></param>
        public static void JudgeCommStu(int result, ref Label labCommStu)
        {

            if (result == 0)
            {
                // Console.WriteLine("Connected to 10.50.77.135");
                labCommStu.Background = Brushes.Green;
                labCommStu.Content = "已连接";
            }
            else
            {
                labCommStu.Background = Brushes.Red;
                labCommStu.Content = "已断开";
                //MessageBox.Show(client.ErrorText(result));
                //return;
            }
        }

    }
}

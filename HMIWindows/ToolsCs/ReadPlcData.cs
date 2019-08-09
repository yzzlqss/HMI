using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Sharp7;
using HMIWindows;
using System.Windows.Controls;

namespace HMIWindows.ToolsCs
{
    public static class ReadPlcData
    {
        private static int result { get; set; }//读取数据结果
        private static string[] strAutoStep = { "", "" };//返回自动步骤与下一步骤
        private static bool readResult { get; set; }//读取数据bit结果
        private static byte[] ReadBuffer = new byte[1024];//Create ReadBuffer
        private static string[] strAutoSteps = {"A1气缸伸出","等待搬运机器人上小面板","等待小面板传感器到位","等待搬运机器人离开","A15气缸伸出",
            "人工第一次干预","等待搬运机器人strAutoSteps上南导轨","等待南导轨传感器到位","等待搬运机器人离开","等待搬运机器人上北导轨","等待北导轨传感器到位","等待搬运机器人离开","A16气缸伸出","A11,A12,A24气缸伸出","K3,K4充磁",
             "人工第二次干预","A9气缸关闭","人工第三次干预","等待焊接机器人可调用状态","调用焊接机器人主程序2","调用焊接机器人子程序1","等待焊接机器人焊接完成","人工第四次干预","A9气缸缩回","等待焊接机器人可调用状态",
            "调用焊接机器人主程序2","调用焊接机器人子程序2","等待焊接机器人焊接完成","等待搬运机器人上前横版","A4气缸伸出","等待搬运机器人离开","人工第五次干预","K2充磁","等待焊接机器人可调用状态","调用焊接机器人主程序2",
            "调用焊接机器人子程序3","等待焊接机器人焊接完成","人工第六次干预","A4,A11,A12,A16气缸缩回","等待搬运机器人上大面板","等待大面板传感器到位","等待搬运机器人离开","A16气缸伸出","A11,A12气缸伸出","K5充磁",
            "等待搬运机器人上后横板","A4气缸伸出","等待搬运机器人离开","A6气缸伸出，A16气缸缩回","确认伺服电机正常","等待伺服电机到达上料位置","A18,A19,A20,A21气缸伸出","A11气缸缩回","等待搬运机器人上南侧后轮安装板","等待南侧后轮安装板传感器",
            "等待搬运机器人离开","K7充磁，A11气缸伸出","等待焊接机器人可调用状态","调用焊接机器人主程序2","调用焊接机器人子程序10","等待焊接机器人旋转45度到位","A12气缸缩回","等待搬运机器人上北侧后轮安装板","等待北侧后轮安装板传感器","等待搬运机器人离开",
            "K8充磁，A12气缸伸出","等待焊接机器人可调用状态","调用焊接机器人主程序2","调用焊接机器人子程序11","等待焊接机器人旋转0度到位","确认伺服电机正常","等待伺服电机到达焊接位置","K6充磁","人工第七次干预","等待焊接机器人可调用状态",
            "调用焊接机器人主程序2","调用焊接机器人子程序4","等待焊接机器人焊接完成","A6气缸缩回","A5,A11,A12,A15,A24气缸打开","等待搬运机器人上左侧板","A14气缸伸出","A11气缸伸出","等待搬运机器人离开","等待焊接机器人可调用状态",
            "调用焊接机器人主程序2","调用焊接机器人子程序10","等待焊接机器人旋转45度到位","等待搬运机器人上右侧板","A2气缸伸出","A12气缸伸出","等待搬运机器人离开","等待焊接机器人可调用状态","调用焊接机器人主程序2","调用焊接机器人子程序11",
            "等待焊接机器人旋转0度到位","A11,A12气缸缩回","A5,A11,A12,A15,A24,A25,A26气缸伸出","等待左右侧板传感器","K6,K7,K8消磁","A18,A19,A20,A21,A24气缸缩回","确认伺服电机正常","等待伺服电机到达零位","等待搬运机器人上前板","等待前板传感器",
            "等待搬运机器人离开","人工第八次干预","等待后板传感器","K1充磁","A16,A22气缸伸出","A17,A24气缸伸出","人工第九次干预","等待前板，后板传感器","等待焊接机器人可调用状态","调用焊接机器人主程序2",
            "调用焊接机器人子程序5","等待焊接机器人焊接完成","确认伺服电机正常","等待伺服电机到达上爬梯托板位置","A23气缸伸出","人工第十次干预","等待焊接机器人可调用状态","调用焊接机器人主程序2","调用焊接机器人子程序6","等待焊接机器人焊接完成",
            "等待搬运机器人上底板","等待搬运机器人离开","人工第十一次干预","等待焊接机器人可调用状态","调用焊接机器人主程序2","调用焊接机器人子程序7","等待焊接机器人焊接完成","K1退磁","A17,A22气缸缩回","A10气缸伸出",
            "等待焊接机器人可调用状态","调用焊接机器人主程序2","调用焊接机器人子程序12","等待焊接机器人旋转180度到位","A3气缸缩回","人工第十二次干预","等待焊接机器人可调用状态", "调用焊接机器人主程序2", "调用焊接机器人子程序8", "等待焊接机器人焊接完成",
            "等待焊接机器人旋转0度到位", "A3气缸缩回", "A10气缸缩回", "A1,A2,A5,A11,A12,A14,A15,A16,A23,A24,A25,A26气缸缩回", "K2,K3,K4,K5退磁", "确认伺服电机正常", "等待伺服电机到零位", "人工第十三次干预", "等待搬运机器人来取车架，等待零件传感器信号全无", "08车架流程结束" };//自动步数文本
        private static string strManualData = "";//手动界面数据返回字符
        private static List<Ellipse> listEllipse = new List<Ellipse>();//定义ellipse集合
        //定义气缸信号颜色，为1是草绿色，为0是灰色
        private static SolidColorBrush lawnGreen = new SolidColorBrush(Colors.LawnGreen);
        private static SolidColorBrush gray = new SolidColorBrush(Colors.Gray);

        /// <summary>
        /// 读取气缸信号 test db14 186.0--189.7
        /// </summary>
        /// <returns></returns>
        public static void ReadValveSignals(S7Client client, ref Ellipse[] ellipses, int dbNum, int dbByteLength, int dbStartElement, int dbStartBit)
        {
            result = client.DBRead(dbNum, 0, dbByteLength, ReadBuffer);//db14 from 0 length 208byte

            //如果当前集合没有内容，则遍历添加所有气缸内容
            if (listEllipse.Count != ellipses.Length)
            {
                for (int i = 0; i < ellipses.Length; i++)
                {
                    listEllipse.Add(ellipses[i]);
                }
            }

            //遍历所有气缸读取状态，并赋予颜色
            for (int i = dbStartBit; i < listEllipse.Count + dbStartBit; i++)
            {
                if (i <= 7)
                {
                    readResult = S7.GetBitAt(ReadBuffer, dbStartElement, i);
                    if (readResult) listEllipse[i - dbStartBit].Fill = lawnGreen;
                    else listEllipse[i - dbStartBit].Fill = gray;
                }
                else if (i > 7 && i <= 15)
                {
                    readResult = S7.GetBitAt(ReadBuffer, dbStartElement + 1, i - 8);
                    if (readResult) listEllipse[i - dbStartBit].Fill = lawnGreen;
                    else listEllipse[i - dbStartBit].Fill = gray;
                }
                else if (i > 15 && i <= 23)
                {
                    readResult = S7.GetBitAt(ReadBuffer, dbStartElement + 2, i - 16);
                    if (readResult) listEllipse[i - dbStartBit].Fill = lawnGreen;
                    else listEllipse[i - dbStartBit].Fill = gray;
                }
                else if (i > 23 && i <= 31)
                {
                    readResult = S7.GetBitAt(ReadBuffer, dbStartElement + 3, i - 24);
                    if (readResult) listEllipse[i - dbStartBit].Fill = lawnGreen;
                    else listEllipse[i - dbStartBit].Fill = gray;
                }
                else if (i > 31 && i <= 39)
                {
                    readResult = S7.GetBitAt(ReadBuffer, dbStartElement + 4, i - 32);
                    if (readResult) listEllipse[i - dbStartBit].Fill = lawnGreen;
                    else listEllipse[i - dbStartBit].Fill = gray;
                }
                else if (i > 39 && i <= 47)
                {
                    readResult = S7.GetBitAt(ReadBuffer, dbStartElement + 5, i - 40);
                    if (readResult) listEllipse[i - dbStartBit].Fill = lawnGreen;
                    else listEllipse[i - dbStartBit].Fill = gray;
                }
                else if (i > 47 && i <= 55)
                {
                    readResult = S7.GetBitAt(ReadBuffer, dbStartElement + 6, i - 48);
                    if (readResult) listEllipse[i - dbStartBit].Fill = lawnGreen;
                    else listEllipse[i - dbStartBit].Fill = gray;
                }
                else if (i > 55 && i <= 63)
                {
                    readResult = S7.GetBitAt(ReadBuffer, dbStartElement + 7, i - 56);
                    if (readResult) listEllipse[i - dbStartBit].Fill = lawnGreen;
                    else listEllipse[i - dbStartBit].Fill = gray;
                }
                //else if....
            }
        }

        /// <summary>
        /// 清除气缸信号颜色
        /// </summary>
        /// <param name="ellipses"></param>
        /// <param name="client"></param>
        public static void ClearValveSignals(S7Client client, ref Ellipse[] ellipses)
        {
            for (int i = 0; i < listEllipse.Count; i++)
            {
                listEllipse[i].Fill = gray;
            }
        }

        /// <summary>
        ///手动数据读取
        /// </summary>
        /// <returns>返回数据组</returns>
        public static string ReadManualData(S7Client client, int dbNum, int dbByteLength, int dbStartElement)
        {
            result = client.DBRead(dbNum, 0, dbByteLength, ReadBuffer);//dbNum from 0 length dbByteLength byte
            double dbLReal = S7.GetLRealAt(ReadBuffer, dbStartElement);//读取Real位置在94
            strManualData = dbLReal.ToString();

            return strManualData;
        }

        /// <summary>
        /// 自动步骤
        /// </summary>
        /// <param name="client"></param>
        /// <param name="dbNum"></param>
        /// <param name="dbByteLength"></param>
        /// <param name="dbStartElement"></param>
        /// <returns></returns>
        public static string[] ReadAutoStep(S7Client client, int dbNum, int dbByteLength, int dbStartElement)
        {
            // int dbInt;
            // dbInt = S7.GetIntAt(ReadBuffer, Convert.ToInt16(element));
            // value = "" + dbInt;

            //double dbLReal = S7.GetLRealAt(ReadBuffer, 94);//读取Real位置在94
            //txtReadServoPos.Text = dbLReal.ToString();

            result = client.DBRead(dbNum, 0, dbByteLength, ReadBuffer);//dbNum from 0 length dbByteLength byte
            int dbInt = S7.GetIntAt(ReadBuffer, dbStartElement);

            for (int i = 1; i <= strAutoSteps.Length; i++)
            {
                if (i == dbInt)
                {
                    strAutoStep[0] = strAutoSteps[i - 1];//当前步骤
                    if (i == strAutoSteps.Length) strAutoStep[1] = strAutoSteps[0];//下一步骤
                    else strAutoStep[1] = strAutoSteps[i];
                }
            }
            return strAutoStep;
        }


        public static double ReadWeldingCurrent(S7Client client)
        {
            result = client.DBRead(1024, 0, 8, ReadBuffer);
            double dbCurrent = S7.GetDIntAt(ReadBuffer, 4);
            return dbCurrent;
        }

    }
}

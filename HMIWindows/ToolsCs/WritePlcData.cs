using Sharp7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HMIWindows.ToolsCs
{
    public static class WritePlcData
    {
        private static int writeResult { get; set; }
        private static byte[] writeBuffer = new byte[208];
        private static byte[] btnBuffer = new byte[3199];//3199

        //private static S7Client client;
        //private static int dbNum;
        //private static int dbStartElement;
        //private static int dbStartBit;
        //private static Button[] btns;
        //private static int btnNum;

        /// <summary>
        /// 写入plc函数
        /// </summary>
        /// <param name="txtWriteServoPos"></param>
        /// <param name="client"></param>
        public static void WriteDb(S7Client client, ref TextBox txb1, ref TextBox txb2, int dbNum, int[] dbElements)
        {
            //初始界面文本框未输入时不能执行转换，否则抛异常。
            try
            {
                double value1 = Convert.ToDouble(txb1.Text);
                double value2 = Convert.ToDouble(txb2.Text);
                S7.SetLRealAt(writeBuffer, dbElements[0], (float)value1);
                S7.SetLRealAt(writeBuffer, dbElements[1], (float)value2);
                int writeResult = client.DBWrite(dbNum, 0, writeBuffer.Length, writeBuffer);
            }
            catch
            {
                return;
            }

        }

        /// <summary>
        /// 按钮触发调用函数
        /// </summary>
        public static void BtnToPlcSet(S7Client client, int dbNum, int dbStartElement, int dbStartBit)
        {
            S7.SetBitAt(ref btnBuffer, dbStartElement, dbStartBit, true);
            writeResult = client.DBWrite(dbNum, 0, btnBuffer.Length, btnBuffer);//从db100 的200开始写。

        }

        /// <summary>
        /// 按钮释放调用函数
        /// </summary>
        /// <param name="client"></param>
        /// <param name="dbNum"></param>
        /// <param name="dbStartElement"></param>
        /// <param name="dbStartBit"></param>
        public static void BtnToPlcReset(S7Client client, int dbNum, int dbStartElement, int dbStartBit)
        {
            S7.SetBitAt(ref btnBuffer, dbStartElement, dbStartBit, false);
            writeResult = client.DBWrite(dbNum, 0, btnBuffer.Length, btnBuffer);//从db100 的200开始写。
        }

        /*
        /// <summary>
        /// 按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Btn_Click(object sender, RoutedEventArgs e)
        {
            //    S7.SetBitAt(ref WriteBuffer, element, bit, b);
            //Console.WriteLine("点击了按钮！");
            for (int i = dbStartBit; i < btns.Length + dbStartBit; i++)
            {
                //只执行当前按钮触发事件
                if (i == btnNum + dbStartBit)
                {
                    if (i <= 7)
                    {
                        S7.SetBitAt(ref btnBuffer, dbStartElement, i, true);
                        writeResult = client.DBWrite(dbNum, 200, btnBuffer.Length, btnBuffer);//从db100 的200开始写。
                    }
                    else if (i > 7 && i <= 15)
                    {
                        S7.SetBitAt(ref btnBuffer, dbStartElement + 1, i - 8, true);
                        writeResult = client.DBWrite(dbNum, 200, btnBuffer.Length, btnBuffer);
                    }
                    else if (i > 15 && i <= 23)
                    {
                        S7.SetBitAt(ref btnBuffer, dbStartElement + 2, i - 16, true);
                        writeResult = client.DBWrite(dbNum, 200, btnBuffer.Length, btnBuffer);
                    }
                    else if (i > 23 && i <= 31)
                    {
                        S7.SetBitAt(ref btnBuffer, dbStartElement + 3, i - 24, true);
                        writeResult = client.DBWrite(dbNum, 200, btnBuffer.Length, btnBuffer);
                    }
                    else if (i > 31 && i <= 39)
                    {
                        S7.SetBitAt(ref btnBuffer, dbStartElement + 4, i - 32, true);
                        writeResult = client.DBWrite(dbNum, 200, btnBuffer.Length, btnBuffer);
                    }
                    else if (i > 39 && i <= 47)
                    {
                        S7.SetBitAt(ref btnBuffer, dbStartElement + 5, i - 40, true);
                        writeResult = client.DBWrite(dbNum, 200, btnBuffer.Length, btnBuffer);
                    }
                    else if (i > 47 && i <= 55)
                    {
                        S7.SetBitAt(ref btnBuffer, dbStartElement + 6, i - 48, true);
                        writeResult = client.DBWrite(dbNum, 200, btnBuffer.Length, btnBuffer);
                    }
                    else if (i > 55 && i <= 63)
                    {
                        S7.SetBitAt(ref btnBuffer, dbStartElement + 7, i - 56, true);
                        writeResult = client.DBWrite(dbNum, 200, btnBuffer.Length, btnBuffer);
                    }
                    //else if.... 
                }
            }
        }*/
    }
}

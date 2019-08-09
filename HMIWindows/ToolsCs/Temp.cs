using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIWindows.ToolsCs
{
    class Temp
    {
        //写入按钮功能
        /*
             /// <summary>
        /// 按钮触发事件
        /// </summary>
        public static void BtnToPLC(S7Client RecClient, Button[] RecBtns, int RecDbNum, int RecDbStartElement, int RecDbStartBit)
        {
            client = RecClient;
            dbNum = RecDbNum;
            dbStartElement = RecDbStartElement;
            dbStartBit = RecDbStartBit;
            btns = RecBtns;
            for (int i = 0; i < btns.Length; i++)
            {
                btnNum = i;
                btns[i].Click += Btn_Click;

            }

        }

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
        }
         */












    }
}

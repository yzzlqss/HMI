using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Sharp7;

namespace HMIWindows.ToolsCs
{
    public static class AlarmList
    {
        //标准功能类list
        private static List<ListFunc> list = new List<ListFunc>();
        //报警文本存储
        private static List<string> listStr = new List<string>();

        private static ListFunc[] listFunc = new ListFunc[240];


        //Create ReadBuffer
        private static byte[] ReadBuffer = new byte[1024];

        private static bool boolFirst = false;


        /// <summary>
        /// 报警文本
        /// </summary>
        private static void AlarmTxt()
        {
            //调用一次文本
            if (boolFirst == false)
            {
                #region 报警文本
                //1000-1015--08报警数据1
                listStr.AddRange(new string[] { "A11,A12,A24气缸伸出未到位", "K3,K4未充磁成功", "A9气缸伸出未到位", "机器人不在原点或者不可控", "调用焊接机器人主程序不成功", "调用焊接机器人子程序不成功", "A9气缸缩回不到位", "机器人不在原点或者不可控", "气缸磁性开关为到位", "电磁铁未退磁", "A1气缸伸出未到位", "小面板传感器不到位", "A15气缸伸出未到位", "南导轨传感器不到位", "北导轨传感器不到位", "A16气缸伸出不到位" });
                //1016-1031--08报警数据2
                listStr.AddRange(new string[] { "大面板传感器不到位", "A16气缸伸出不到位", "A11,A12伸出不到位", "电磁铁K5未充磁", "A4气缸伸出不到位", "A6气缸伸出不到位，A16气缸缩回不到位", "伺服无使能", "伺服运行超时，请检查伺服位置", "调用焊接机器人主程序不成功", "调用焊接机器人子程序不成功", "A4气缸伸出不到位", "电磁铁K2未充磁", "机器人不在原点或者不可控", "调用焊接机器人主程序不成功", "调用焊接机器人子程序不成功", "A4,A11,A12,A16气缸缩回不到位" });
                //1032-1047--08报警数据3
                listStr.AddRange(new string[] { "北侧后轮安装板传感器不到位", "K8电磁铁未充磁，A12气缸伸出不到位", "机器人不在原点或者不可控", "调用焊接机器人主程序不成功", "调用焊接机器人子程序不成功", "伺服无使能", "伺服运行超时，请检查伺服位置", "K6电磁铁未充磁", "A18,A19,A20,A21气缸伸出不到位", "A11气缸缩回不到位", "南侧后轮安装板传感器不到位", "K7电磁铁未充磁，A11气缸伸出不到位", "机器人不在原点或者不可控", "调用焊接机器人主程序不成功", "调用焊接机器人子程序不成功", "A12气缸缩回不到位" });
                //1048-1063--08报警数据4
                listStr.AddRange(new string[] { "调用焊接机器人子程序不成功", "A2气缸伸出不到位", "机器人不在原点或者不可控", "调用焊接机器人主程序不成功", "调用焊接机器人子程序不成功", "A11,A12气缸缩回不到位", "A25,A26气缸伸出不到位", "左右侧板传感器不到位", "机器人不在原点或者不可控", "调用焊接机器人主程序不成功", "调用焊接机器人子程序不成功", "A6气缸缩回不到位", "A5 A11 A12 A15 A24气缸缩回不到位", "A14气缸伸出不到位", "机器人不在原点或者不可控", "调用焊接机器人主程序不成功" });
                //1064-1079--08报警数据5
                listStr.AddRange(new string[] { "A17,A24气缸伸出不到位", "前板，后板传感器不到位", "机器人不在原点或者不可控", "调用焊接机器人主程序不成功", "调用焊接机器人子程序不成功", "伺服无使能", "伺服运行超时，请检查伺服位置", "A23气缸伸出", "K6,K7,K8电磁铁未消磁", "A18,A19,A20,A21,A24气缸缩回不到位", "伺服无使能", "伺服运行超时，请检查伺服位置", "前板传感器不到位", "后板传感器不到位", "K1电磁铁未充磁", "A22气缸伸出不到位" });
                //1080-1095--08报警数据6
                listStr.AddRange(new string[] { "A10气缸伸出不到位", "机器人不在原点或者不可控", "调用焊接机器人主程序不成功", "调用焊接机器人子程序不成功", "A3气缸伸出不到位", "机器人不在原点或者不可控", "调用焊接机器人主程序不成功", "调用焊接机器人子程序不成功", "机器人不在原点或者不可控", "调用焊接机器人主程序不成功", "调用焊接机器人子程序不成功", "机器人不在原点或者不可控", "调用焊接机器人主程序不成功", "调用焊接机器人子程序不成功", "K1电磁铁未退磁", "A17,A22气缸缩回不到位" });
                //1096-1111--08报警数据7
                listStr.AddRange(new string[] { "", "", "", "", "", "", "", "", "A3气缸缩回不到位", "A10气缸缩回不到位", "A1 A2 A5 A11 A12 A14 A15 A16 A23 A24 A25 A26气缸缩回不到位", "K2,K3,K4,K5电磁铁未退磁", "伺服无使能", "伺服运行超时，请检查伺服位置", "", "" });


                //1112-1127--12报警数据1
                listStr.AddRange(new string[] { "A11,A12,A24气缸伸出未到位", "K3,K4未充磁成功", "A9气缸伸出未到位", "调用焊接主程序未成功", "调用焊接子程序未成功", "机器人未在原点或者不可控", "调用焊接主程序未成功", "调用焊接子程序未成功", "气缸磁性开关为到位", "电磁铁未退磁", "A1气缸伸出未到位", "机器人放件未成功", "A15气缸伸出未到位", "未检测到传感器", "未检测到传感器", "A16气缸伸出未到位" });
                //1128-1143--12报警数据2
                listStr.AddRange(new string[] { "A11,A12气缸伸出未到位", "K5未充磁成功", "A5气缸伸出未到位", "A6气缸伸出未到位", "A16气缸缩回未到位", "伺服未使能", "伺服运行超时，请检查伺服位置", "A18,A19,A20,A21气缸伸出未到位", "A4气缸伸出未到位", "K2未充磁成功", "调用焊接主程序未成功", "调用焊接子程序未成功", "备用", "A4,A11,A12,A16气缸缩回未到位", "未检测到传感器", "A16气缸伸出未到位" });
                //1144-1159--12报警数据3
                listStr.AddRange(new string[] { "未检测到传感器", "K7未充磁成功", "A12气缸伸出未到位", "机器人未在原点或者不可控", "调用焊接主程序未成功", "调用焊接子程序未成功", "伺服未使能", "伺服运行超时，请检查伺服位置", "A11气缸缩回未到位", "未检测到传感器", "K6未充磁成功", "A11气缸缩回未到位", "机器人未在原点或者不可控", "调用焊接主程序未成功", "调用焊接子程序未成功", "A12气缸缩回未到位" });
                //1160-1175--12报警数据4
                listStr.AddRange(new string[] { "调用焊接主程序未成功", "调用焊接子程序未成功", "A14气缸伸出未到位", "备用", "机器人未在原点或者不可控", "调用焊接主程序未成功", "调用焊接子程序未成功", "A7,A8,A11,A12气缸缩回未到位", "K8未充磁成功", "机器人未在原点或者不可控", "调用焊接主程序未成功", "调用焊接子程序未成功", "A6气缸缩回未到位", "A5,A11,A12,A15,A24气缸缩回未到位", "A2气缸伸出未到位", "机器人未在原点或者不可控" });
                //1176-1191--12报警数据5
                listStr.AddRange(new string[] { "A16，A22气缸伸出未到位", "A17，A24气缸伸出未到位", "未检测到传感器", "机器人未在原点或者不可控", "调用焊接主程序未成功", "调用焊接子程序未成功", "伺服未使能", "伺服运行超时，请检查伺服位置", "A5,A7,A8,A11,A12,A15,A25,A26气缸伸出未到位", "K6，K7，K8未消磁成功", "A18 ,A19 ,A20, A21气缸缩回未到位", "伺服未使能", "伺服运行超时，请检查伺服位置", "未检测到传感器", "未检测到传感器", "K1未充磁成功" });
                //1192-1207--12报警数据6
                listStr.AddRange(new string[] { "机器人未在原点或者不可控", "调用焊接主程序未成功", "调用焊接子程序未成功", "K1未消磁成功", "A17,A22气缸缩回未到位", "A16气缸缩回未到位", "A10气缸伸出未到位", "机器人未在原点或者不可控或者关键电磁铁未充磁", "A23气缸伸出未到位", "未检测到传感器", "机器人未在原点或者不可控", "调用焊接主程序未成功", "调用焊接子程序未成功", "A23气缸缩回未到位", "伺服未使能", "伺服运行超时，请检查伺服位置" });
                //1208-1223--12报警数据7
                listStr.AddRange(new string[] { "机器人未在原点或者不可控", "调用焊接主程序未成功", "调用焊接子程序未成功", "A1 A2 A5 A7 A8 A11 A12 A13 A14 A15 A24 A25 A26气缸缩回未到位", "K2,K3,K4,K5未消磁成功", "", "", "", "调用焊接主程序未成功", "调用焊接子程序未成功", "A3气缸伸出未到位", "机器人未在原点或者不可控", "调用焊接主程序未成功", "调用焊接子程序未成功", "A3气缸缩回未到位", "A10气缸缩回未到位" });
                //1224-1239--12报警数据8
                listStr.AddRange(new string[] { "", "", "", "", "", "", "", "", "hmi报警显示", "西侧安全门打开", "", "", "", "", "", "" });

                //1240---
                //listStr.AddRange(new string[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" });
                #endregion
            }
        }

        /// <summary>
        /// 被list报警调用
        /// </summary>
        /// <param name="listView"></param>
        public static void AlarmListView(S7Client client, ListView listView, int dbSum, int dbLength, int dbStartElement, int dbStartBit)
        {
            int result = client.DBRead(dbSum, 0, dbLength, ReadBuffer);//dbx30 从0读取 length98 
            //调用报警文本
            AlarmTxt();
            //调用一次文本
            boolFirst = true;

            //添加报警文本
            for (int i = 0; i < listStr.Count; i++)
            {
                listFunc[i] = new ListFunc(1000 + i, DateTime.Now, listStr[i]);
            }
            //添加所有报警列表到list集合
            for (int i = 0; i < listStr.Count; i++)
            {
                list.Add(listFunc[i]);
            }

            bool boolResult;

            #region 遍历报警
            //遍历报警
            for (int i = dbStartBit; i < listStr.Count + dbStartBit; i++)
            {
                if (i <= 7)// //1000-1015--08报警数据1
                {
                    boolResult = S7.GetBitAt(ReadBuffer, dbStartElement, i);
                    //如果不包含当前项，则添加
                    if (boolResult && !listView.Items.Contains(list[i - dbStartBit]))
                    {
                        listView.Items.Add(list[i - dbStartBit]);
                    }
                    //如果包含当前项则刷新
                    else if (boolResult && listView.Items.Contains(list[i - dbStartBit]))
                    {
                        listView.Items.Remove(list[i - dbStartBit]);//移除
                        listView.Items.Add(list[i - dbStartBit]);//再添加
                        list[i - dbStartBit].Date = DateTime.Now;//刷新时间
                    }

                }
                else if (i > 7 && i <= 15)
                {
                    //boolResult = S7.GetBitAt(ReadBuffer, 51, i - 8);
                    ////如果不包含当前项，则添加
                    //if (boolResult && !listView.Items.Contains(list[i]))
                    //{
                    //    listView.Items.Add(list[i]);
                    //}
                    ////如果包含当前项则刷新
                    //else if (boolResult && listView.Items.Contains(list[i]))
                    //{
                    //    listView.Items.Remove(list[i]);
                    //    listView.Items.Add(list[i]);
                    //    list[i].Date = DateTime.Now;//刷新时间
                    //}

                    boolResult = S7.GetBitAt(ReadBuffer, dbStartElement + 1, i - 8);
                    if (boolResult && !listView.Items.Contains(list[i - dbStartBit]))
                    {
                        listView.Items.Add(list[i - dbStartBit]);
                    }
                    else if (boolResult && listView.Items.Contains(list[i - dbStartBit]))
                    {
                        listView.Items.Remove(list[i - dbStartBit]);
                        listView.Items.Add(list[i - dbStartBit]);
                        list[i - dbStartBit].Date = DateTime.Now;
                    }

                }
                else if (i > 15 && i <= 23)//1016-1031--08报警数据2
                {
                    boolResult = S7.GetBitAt(ReadBuffer, dbStartElement + 2, i - 16);
                    if (boolResult && !listView.Items.Contains(list[i - dbStartBit]))
                    {
                        listView.Items.Add(list[i - dbStartBit]);
                    }
                    else if (boolResult && listView.Items.Contains(list[i - dbStartBit]))
                    {
                        listView.Items.Remove(list[i - dbStartBit]);
                        listView.Items.Add(list[i - dbStartBit]);
                        list[i - dbStartBit].Date = DateTime.Now;
                    }
                }
                else if (i > 23 && i <= 31)
                {
                    boolResult = S7.GetBitAt(ReadBuffer, dbStartElement + 3, i - 24);
                    if (boolResult && !listView.Items.Contains(list[i - dbStartBit]))
                    {
                        listView.Items.Add(list[i - dbStartBit]);
                    }
                    else if (boolResult && listView.Items.Contains(list[i - dbStartBit]))
                    {
                        listView.Items.Remove(list[i - dbStartBit]);
                        listView.Items.Add(list[i - dbStartBit]);
                        list[i - dbStartBit].Date = DateTime.Now;
                    }
                }
                else if (i > 31 && i <= 39)//1032-1047--08报警数据3
                {
                    boolResult = S7.GetBitAt(ReadBuffer, dbStartElement + 4, i - 32);
                    if (boolResult && !listView.Items.Contains(list[i - dbStartBit]))
                    {
                        listView.Items.Add(list[i - dbStartBit]);
                    }
                    else if (boolResult && listView.Items.Contains(list[i - dbStartBit]))
                    {
                        listView.Items.Remove(list[i - dbStartBit]);
                        listView.Items.Add(list[i - dbStartBit]);
                        list[i - dbStartBit].Date = DateTime.Now;
                    }
                }
                else if (i > 39 && i <= 47)
                {
                    boolResult = S7.GetBitAt(ReadBuffer, dbStartElement + 5, i - 40);
                    if (boolResult && !listView.Items.Contains(list[i - dbStartBit]))
                    {
                        listView.Items.Add(list[i - dbStartBit]);
                    }
                    else if (boolResult && listView.Items.Contains(list[i - dbStartBit]))
                    {
                        listView.Items.Remove(list[i - dbStartBit]);
                        listView.Items.Add(list[i - dbStartBit]);
                        list[i - dbStartBit].Date = DateTime.Now;
                    }
                }
                else if (i > 47 && i <= 55)//1048-1063--08报警数据4
                {
                    boolResult = S7.GetBitAt(ReadBuffer, dbStartElement + 6, i - 48);
                    if (boolResult && !listView.Items.Contains(list[i - dbStartBit]))
                    {
                        listView.Items.Add(list[i - dbStartBit]);
                    }
                    else if (boolResult && listView.Items.Contains(list[i - dbStartBit]))
                    {
                        listView.Items.Remove(list[i - dbStartBit]);
                        listView.Items.Add(list[i - dbStartBit]);
                        list[i - dbStartBit].Date = DateTime.Now;
                    }
                }
                else if (i > 55 && i <= 63)
                {
                    boolResult = S7.GetBitAt(ReadBuffer, dbStartElement + 7, i - 56);
                    if (boolResult && !listView.Items.Contains(list[i - dbStartBit]))
                    {
                        listView.Items.Add(list[i - dbStartBit]);
                    }
                    else if (boolResult && listView.Items.Contains(list[i - dbStartBit]))
                    {
                        listView.Items.Remove(list[i - dbStartBit]);
                        listView.Items.Add(list[i - dbStartBit]);
                        list[i - dbStartBit].Date = DateTime.Now;
                    }
                }
                else if (i > 63 && i <= 71)//1064-1079--08报警数据5
                {
                    boolResult = S7.GetBitAt(ReadBuffer, dbStartElement + 8, i - 64);
                    if (boolResult && !listView.Items.Contains(list[i - dbStartBit]))
                    {
                        listView.Items.Add(list[i - dbStartBit]);
                    }
                    else if (boolResult && listView.Items.Contains(list[i - dbStartBit]))
                    {
                        listView.Items.Remove(list[i - dbStartBit]);
                        listView.Items.Add(list[i - dbStartBit]);
                        list[i - dbStartBit].Date = DateTime.Now;
                    }
                }
                else if (i > 71 && i <= 79)
                {
                    boolResult = S7.GetBitAt(ReadBuffer, dbStartElement + 9, i - 72);
                    if (boolResult && !listView.Items.Contains(list[i - dbStartBit]))
                    {
                        listView.Items.Add(list[i - dbStartBit]);
                    }
                    else if (boolResult && listView.Items.Contains(list[i - dbStartBit]))
                    {
                        listView.Items.Remove(list[i - dbStartBit]);
                        listView.Items.Add(list[i - dbStartBit]);
                        list[i - dbStartBit].Date = DateTime.Now;
                    }
                }
                else if (i > 79 && i <= 87)//1080-1095--08报警数据6
                {
                    boolResult = S7.GetBitAt(ReadBuffer, dbStartElement + 10, i - 80);
                    if (boolResult && !listView.Items.Contains(list[i - dbStartBit]))
                    {
                        listView.Items.Add(list[i - dbStartBit]);
                    }
                    else if (boolResult && listView.Items.Contains(list[i - dbStartBit]))
                    {
                        listView.Items.Remove(list[i - dbStartBit]);
                        listView.Items.Add(list[i - dbStartBit]);
                        list[i - dbStartBit].Date = DateTime.Now;
                    }
                }
                else if (i > 87 && i <= 95)
                {
                    boolResult = S7.GetBitAt(ReadBuffer, dbStartElement + 11, i - 88);
                    if (boolResult && !listView.Items.Contains(list[i - dbStartBit]))
                    {
                        listView.Items.Add(list[i - dbStartBit]);
                    }
                    else if (boolResult && listView.Items.Contains(list[i - dbStartBit]))
                    {
                        listView.Items.Remove(list[i - dbStartBit]);
                        listView.Items.Add(list[i - dbStartBit]);
                        list[i - dbStartBit].Date = DateTime.Now;
                    }
                }
                else if (i > 95 && i <= 103)//1096-1111--08报警数据7
                {
                    boolResult = S7.GetBitAt(ReadBuffer, dbStartElement + 12, i - 96);
                    if (boolResult && !listView.Items.Contains(list[i - dbStartBit]))
                    {
                        listView.Items.Add(list[i - dbStartBit]);
                    }
                    else if (boolResult && listView.Items.Contains(list[i - dbStartBit]))
                    {
                        listView.Items.Remove(list[i - dbStartBit]);
                        listView.Items.Add(list[i - dbStartBit]);
                        list[i - dbStartBit].Date = DateTime.Now;
                    }
                }
                else if (i > 103 && i <= 111)
                {
                    boolResult = S7.GetBitAt(ReadBuffer, dbStartElement + 13, i - 104);
                    if (boolResult && !listView.Items.Contains(list[i - dbStartBit]))
                    {
                        listView.Items.Add(list[i - dbStartBit]);
                    }
                    else if (boolResult && listView.Items.Contains(list[i - dbStartBit]))
                    {
                        listView.Items.Remove(list[i - dbStartBit]);
                        listView.Items.Add(list[i - dbStartBit]);
                        list[i - dbStartBit].Date = DateTime.Now;
                    }
                }



                else if (i > 111 && i <= 119)//1112-1127--12报警数据1
                {

                }




            }

            #endregion

        }

        /// <summary>
        /// 标准功能类
        /// </summary>
        private class ListFunc
        {
            public int ID { get; set; }
            public DateTime Date { get; set; }
            public string Message { get; set; }
            public ListFunc(int id, DateTime date, string message)// 
            {
                this.ID = id;
                this.Date = date;
                this.Message = message;
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Research.DynamicDataDisplay.Common;

namespace DynamicDataDisplaySample.VoltageViewModel
{
    public class VoltagePointCollection : RingArray<VoltagePoint>
    {
        private const int TOTAL_POINTS = 300;

        public VoltagePointCollection()
            : base(TOTAL_POINTS) // 在这里设置要显示多少值
        {
        }
    }

    public class VoltagePoint
    {
        public DateTime Date { get; set; }

        public double Voltage { get; set; }

        public VoltagePoint(double voltage, DateTime date)
        {
            this.Date = date;
            this.Voltage = voltage;
        }
    }
}

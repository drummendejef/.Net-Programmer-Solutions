using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NQueens
{
    class QueensCounter
    {
        private static PerformanceCounter _counter;

        private static void CreateCounter()
        {
            CounterCreationDataCollection counterData = new CounterCreationDataCollection();
            CounterCreationData counter = new CounterCreationData();
            counter.CounterType = PerformanceCounterType.NumberOfItems32;
            counter.CounterName = "Queens counter";
            counterData.Add(counter);
            if (!PerformanceCounterCategory.Exists("PS Demo2"))
                PerformanceCounterCategory.Create("PS Demo2", "Programmer Solutions Performance Counter Demo",
                    PerformanceCounterCategoryType.SingleInstance, counterData);
        }

        public static void WriteToCounter()
        {
            if (!PerformanceCounterCategory.Exists("PS Demo2"))
                CreateCounter();

            if (_counter == null)
                _counter = new PerformanceCounter("PS Demo2", "Queens counter", false);
            _counter.Increment();
        }
    }
}

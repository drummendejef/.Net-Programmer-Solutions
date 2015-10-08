using System.Diagnostics;

namespace _11.AOP
{
    class FibonacciCounter
    {
        private static PerformanceCounter _counter;

        private static void CreateCounter()
        {
            CounterCreationDataCollection counterData = new CounterCreationDataCollection();
            CounterCreationData counter = new CounterCreationData();
            counter.CounterType = PerformanceCounterType.NumberOfItems32;
            counter.CounterName = "Fibonacci counter";
            counterData.Add(counter);
            PerformanceCounterCategory.Create("PS Demo", "Programmer Solutions Performance Counter Demo",
                PerformanceCounterCategoryType.SingleInstance, counterData);
        }

        public static void WriteToCounter()
        {
            if (!PerformanceCounterCategory.Exists("PS Demo"))
                CreateCounter();

            if (_counter == null)
                _counter = new PerformanceCounter("PS Demo", "Fibonacci counter", false);
            _counter.Increment();
        }
    }
}

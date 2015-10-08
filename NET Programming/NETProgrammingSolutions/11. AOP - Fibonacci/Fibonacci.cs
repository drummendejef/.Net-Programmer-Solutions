using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09.AOP___Fibonacci
{
    [clsEntryTeller]
    class Fibonacci
    {
        public List<int> FibonacciList { get; set; }

        public Fibonacci(int length)
        {
            int i = 2;
            FibonacciList = new List<int>();
            FibonacciList.Insert(0, 0);
            FibonacciList.Insert(1, 1);

            for (; i < length; i++)
            {
                int nr = FibonacciList[i - 2] + FibonacciList[i - 1];
                FibonacciList.Insert(i, nr);
                //FibonacciList.Insert(i, Fib(i));
            }
        }

        public static int Fib(int i)
        {
            if (i == 0) return 0;
            if (i == 1) return 1;
            return Fib(i - 1) + Fib(i - 2);
        }
    }
}

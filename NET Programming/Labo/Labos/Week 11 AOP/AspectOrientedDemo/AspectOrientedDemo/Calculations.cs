using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspectOrientedDemo
{
    class Calculations
    {
        //
        //EXAMEN: parameters van dit attribuut gebruiken, bv om attribuut op alle methodes toe te passen
        //
        [AOPEntryCounter()]
        [AOPPerformanceCounter()]
        public static int Fibonacci(int i)
        {
            if (i <= 0)
                return 0;
            if (i == 1)
                return 1;

            return Fibonacci(i - 1) + Fibonacci(i - 2);
        }
    }
}

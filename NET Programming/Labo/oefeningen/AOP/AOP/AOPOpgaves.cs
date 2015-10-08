using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOP
{
    [AOPValidator]
    class AOPOpgaves
    {
        ////0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181, 6765
        //[Fibonacci("Fibonacci", 6765)]
        //[OnderBovenGrens(15,20,15)]
        [AOPValidator]
        public AOPOpgaves()
        {
            //Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
        }

        public void DoeIets([OnderBovenGrens(15,20)] int iGetal)        //igetal is de formele parameter
        {

        }        
    }
}

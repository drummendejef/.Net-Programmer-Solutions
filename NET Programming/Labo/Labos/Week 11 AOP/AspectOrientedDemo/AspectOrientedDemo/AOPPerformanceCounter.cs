using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspectOrientedDemo
{
    [Serializable]
    class AOPPerformanceCounter : PostSharp.Aspects.OnMethodBoundaryAspect
    {
        
        public override void OnEntry(PostSharp.Aspects.MethodExecutionArgs args)
        {
            base.OnEntry(args);

            FibonacciCounter.WriteToCounter();
        }

        
    }
}

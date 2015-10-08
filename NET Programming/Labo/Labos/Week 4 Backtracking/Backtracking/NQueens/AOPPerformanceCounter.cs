using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NQueens
{
    [Serializable]
    class AOPPerformanceCounter : PostSharp.Aspects.OnMethodBoundaryAspect
    {
        public override void OnEntry(PostSharp.Aspects.MethodExecutionArgs args)
        {
            base.OnEntry(args);

            QueensCounter.WriteToCounter();
        }
    }
}

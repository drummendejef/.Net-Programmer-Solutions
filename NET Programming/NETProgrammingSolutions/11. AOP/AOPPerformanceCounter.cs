using System;

namespace _11.AOP
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

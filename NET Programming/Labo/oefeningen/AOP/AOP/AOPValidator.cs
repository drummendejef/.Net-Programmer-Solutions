using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOP
{
    [Serializable]
    public class AOPValidator : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            base.OnEntry(args);

            var test = args.Arguments[0];
            var test2 = args.Method.GetParameters();
        }
    }
}

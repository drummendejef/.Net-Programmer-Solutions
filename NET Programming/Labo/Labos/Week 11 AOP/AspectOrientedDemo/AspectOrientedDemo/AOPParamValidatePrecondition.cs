using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AspectOrientedDemo
{
    [Serializable]
    class AOPParamValidatePrecondition : PostSharp.Aspects.OnMethodBoundaryAspect
    {
        public override void OnEntry(PostSharp.Aspects.MethodExecutionArgs args)
        {
            base.OnEntry(args);

            ParameterInfo[] paramInfos = args.Method.GetParameters();
            
            for(int i = 0; i < paramInfos.Length; i++)
            {
                foreach(object attr in paramInfos[i].GetCustomAttributes())
                {
                    IAOPValidate rangeAttr = attr as IAOPValidate;
                    if (rangeAttr == null)
                        return;
                    rangeAttr.Validate(args.Arguments[i]);
                }
            }
        }
    }
}

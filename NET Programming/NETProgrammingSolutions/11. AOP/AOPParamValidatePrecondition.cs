using System;
using System.Reflection;

namespace _11.AOP
{
    [Serializable]
    class AOPParamValidatePrecondition : PostSharp.Aspects.OnMethodBoundaryAspect
    {
        public override void OnEntry(PostSharp.Aspects.MethodExecutionArgs args)
        {
            base.OnEntry(args);

            ParameterInfo[] paramInfos = args.Method.GetParameters();

            for (int i = 0; i < paramInfos.Length; i++)
            {
                foreach (object attr in paramInfos[i].GetCustomAttributes())
                {
                    IAOPValidation rangeAttr = attr as IAOPValidation;
                    if (rangeAttr == null)
                        return;
                    rangeAttr.Validate(args.Arguments[i]);
                }
            }
        }
    }
}

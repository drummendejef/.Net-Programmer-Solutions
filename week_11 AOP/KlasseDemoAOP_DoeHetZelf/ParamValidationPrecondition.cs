using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace KlasseDemoAOP_DoeHetZelf
{
    [Serializable]
    class ParamValidationPrecondition : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            base.OnEntry(args);

            ParameterInfo[] paramInfos = args.Method.GetParameters();

            for(int i = 0; i < paramInfos.Length; i++)
            {
                foreach(object attr in paramInfos[i].GetCustomAttributes())
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

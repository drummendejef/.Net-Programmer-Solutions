using PostSharp.Aspects;
using System;

namespace _09.AOP___Examenpunten
{
    [Serializable]
    class clsParamLimit : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            base.OnEntry(args);
            Console.WriteLine(args);
            checkMethod(args);
        }

        private void checkMethod(MethodExecutionArgs args)
        {
            //IntegerRangeAttribute attr = param.GetCustomAttribute<IntegerRangeAttribute>();
            //overloop alle attributen die de IValideer interface implementeren
            //voor elk van die IValideer attributen: roep de validate methode op met de actuele parameter die je vind in args.arguments
            Console.WriteLine(args.Method.Attributes);
            Console.WriteLine(args.Arguments);

            //check alle arguments
            //args.arguments[i], formalarguments
            //object actualarg, parameterinfo formalattr)
            //elke attribute in getattri
            //foreach(attribute attr in formalatt.getcunstomattribute(true)
            //cast naar iface validatie
            //iface val = attr as iface;
            //if(val!=null)val.valideer(param)
        }

        private bool IsValid()
        {
            return false;
        }
    }
}

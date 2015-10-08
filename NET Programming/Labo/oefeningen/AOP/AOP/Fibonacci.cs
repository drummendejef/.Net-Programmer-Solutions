using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AOP
{
    [Serializable]
    public class Fibonacci : OnMethodBoundaryAspect
    {
        private string name;
        private int Teller;
        private string getallen = "";
        private Boolean isNietGevonden = false;

        public Fibonacci(string name, int teller)
        {
            this.name = name;
            Teller = teller;
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            //string test = string.Format("Entering: {0}.{1}", args.Method.DeclaringType.Name, args.Method.Name, Teller);
            //Console.WriteLine(name + " " + Teller);

            int temp1 = 1, temp2 = 1, temp3 = 0, aatalPogingen = 10000, aantal = 0;

            getallen += temp1 + ", ";
            getallen += temp2 + ", ";

            while (temp3 != Teller && aantal < aatalPogingen)
            {
                aantal++;
                temp3 = temp1 + temp2;
                getallen += temp3 + ", ";
                temp1 = temp2;
                temp2 = temp3;
            }
            if (aantal == aatalPogingen) isNietGevonden = true;
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            if (isNietGevonden) MessageBox.Show(name + ": " + "Geen waarde gevonden");
            else MessageBox.Show(name + ": " + getallen);
        }

        public override void OnException(MethodExecutionArgs args)
        {

        } 
    }
}

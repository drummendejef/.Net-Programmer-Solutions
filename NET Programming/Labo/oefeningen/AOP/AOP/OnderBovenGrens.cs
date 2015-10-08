using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AOP
{
    [Serializable]
    public class OnderBovenGrens : Attribute
    {
        private int onderGrens;
        private int bovenGrens;
        private Boolean isOk = false;

        public OnderBovenGrens(int onderGrens, int bovenGrens)
        {
            this.onderGrens = onderGrens;
            this.bovenGrens = bovenGrens;
        }



        //public override void OnEntry(MethodExecutionArgs args)
        //{
        //    //if (getal > onderGrens && getal < bovenGrens) isOk = true;
        //}

        //public override void OnSuccess(MethodExecutionArgs args)
        //{
        //    //if (isOk) MessageBox.Show("Getal " + getal + " ligt tussen " + onderGrens + " en " + bovenGrens);
        //    //else MessageBox.Show("Getal " + getal + " ligt niet " + onderGrens + " en " + bovenGrens);
        //}

        //public override void OnException(MethodExecutionArgs args)
        //{
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labo1_oef2Versie2
{
    public class TellerArg : EventArgs
    {
        public int teller = 0;
        public TellerArg(int teller)
        {
            this.teller = teller;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examenpunten
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true)]
    class clsParamLimitParam : Attribute, IAOPValidation
    {
        public int Floor { get; set; }
        public int Ceil { get; set; }

        public clsParamLimitParam(int floor, int ceil)
        {
            Floor = floor;
            Ceil = ceil;
        }
 
    }
}

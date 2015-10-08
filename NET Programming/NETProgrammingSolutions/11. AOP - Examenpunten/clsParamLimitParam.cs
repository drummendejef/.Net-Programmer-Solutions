using System;

namespace _09.AOP___Examenpunten
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

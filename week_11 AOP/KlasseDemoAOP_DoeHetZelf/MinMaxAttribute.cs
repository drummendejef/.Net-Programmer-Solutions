using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace KlasseDemoAOP_DoeHetZelf
{
    class MinMaxAttribute : Attribute, IAOPValidation
    {
        private int _min, _max;
        public MinMaxAttribute(int min, int max)
        {
            _min = min;
            _max = max;
        }

        public void Validate(object arg)
        {
            int val = (int)arg;
            if(val < _min)
                throw new AOPRangeException(val + " is kleiner dan " + _min + ".");
            if (val > _max)
                throw new AOPRangeException(val + " is groter dan " + _max + ".");
        }
    }

    
     class AOPRangeException : Exception
    {
        public AOPRangeException(string message) : base(message)
        {
        }
    }
}

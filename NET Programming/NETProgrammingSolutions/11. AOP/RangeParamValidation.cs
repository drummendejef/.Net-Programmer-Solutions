using System;

namespace _11.AOP
{
    class RangeParamValidation : Attribute, IAOPValidation
    {
        private int _min, _max;
        public RangeParamValidation(int min, int max)
        {
            _min = min;
            _max = max;

        }
        public void Validate(object arg)
        {
            int val = (int)arg;
            if (val < _min)
                throw new AOPRangeException(val + " is less than " + _min + ".");
            if (val > _max)
                throw new AOPRangeException(val + " is greater than " + _max + ".");
        }
    }

    class AOPRangeException : Exception
    {
        public AOPRangeException(string message)
            : base(message)
        {

        }
    }
}
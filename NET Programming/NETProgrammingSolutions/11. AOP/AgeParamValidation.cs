using System;

namespace _11.AOP
{
    class AgeParamValidation : Attribute, IAOPValidation
    {
        public AgeParamValidation()
        {
        }
        public void Validate(object arg)
        {
            if (!(arg is DateTime))
                return;

            DateTime date = (DateTime)arg;
            DateTime now = DateTime.Now;

            if (date >= now)
                throw new AOPAgeException("The birth date is in the future.");

            TimeSpan span = now - date;
            if (span.Days / 365 > 120)
                throw new AOPAgeException("People older than 120 years are not allowed.");
        }

    }

    class AOPAgeException : Exception
    {
        public AOPAgeException(string message)
            : base(message)
        {

        }
    }
}

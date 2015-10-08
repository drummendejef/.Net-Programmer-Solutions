using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Aspects;

namespace Examenpunten
{
    public interface IAOPValidation
    {
        void IAOPValidation();
        string ErrorMessage { get; set; }
        bool IsValid { get; set; }
    }
}

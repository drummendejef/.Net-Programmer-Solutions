using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examenpunten
{
    class Student
    {
        public Student(){}

        [clsParamLimit]
        public static void enterStudentModulePoint(string student, string course, [clsParamLimitParam(0, 20)] int point)
        {
            Console.WriteLine(student + " got for " + course + " " + point + " points");
        }
    }
}

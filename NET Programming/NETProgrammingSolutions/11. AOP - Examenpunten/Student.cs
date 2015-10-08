using System;

namespace _09.AOP___Examenpunten
{
    class Student
    {
        public Student() { }

        [clsParamLimit]
        public static void enterStudentModulePoint(string student, string course, [clsParamLimitParam(0, 20)] int point)
        {
            Console.WriteLine(student + " got for " + course + " " + point + " points");
        }
    }
}

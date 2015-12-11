using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlasseDemoAOP_DoeHetZelf
{
    [Serializable]
    class EntryTellerAttribute : OnMethodBoundaryAspect
    {
        private static Dictionary<string, int> _teldict = new Dictionary<string, int>();

        public override void OnEntry(MethodExecutionArgs args)
        {
            base.OnEntry(args);//Stond er al
            string methodfullname = args.Method.DeclaringType.FullName + "." + args.Method.Name;//Hebben we zelf geschreven.

            //We gaan proberen te tellen hoeveel keer een methode opgeroepen wordt.
            if (_teldict.Keys.Contains(methodfullname))
            {
                int already = _teldict[methodfullname];
                _teldict.Remove(methodfullname);
                already += 1;
                _teldict.Add(methodfullname, already);
            }
            else
                _teldict.Add(methodfullname, 1);
        }

        public static void DumpInfo()
        {
            foreach(String x in _teldict.Keys)
            {
                int t = _teldict[x];
                Console.WriteLine(x + ": " + t);
            }
        }
    }
}

using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _09.AOP___Fibonacci
{
    [Serializable]
    class clsEntryTeller : OnMethodBoundaryAspect
    {
        private static Dictionary<MethodBase, int> _entries = new Dictionary<MethodBase, int>();
        public override void OnEntry(MethodExecutionArgs args)
        {
            base.OnEntry(args);
            updateEntries(args.Method);
        }

        private void updateEntries(MethodBase m)
        {
            if (_entries.ContainsKey(m))
            {
                int val = _entries[m];
                _entries.Remove(m);
                _entries.Add(m, val + 1);
            }
            else
            {
                _entries.Add(m, 1);
            }
        }

        public static void dumpIntoConsole() {
            foreach (KeyValuePair<MethodBase, int> pair in _entries)
            {
                Console.WriteLine("{0}, {1}", pair.Key, pair.Value);
            }
        }
    }
}

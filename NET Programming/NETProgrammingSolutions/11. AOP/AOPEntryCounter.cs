using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace _11.AOP
{
    [Serializable]
    class AOPEntryCounter : PostSharp.Aspects.OnMethodBoundaryAspect
    {
        private static Dictionary<string, int> _counterDictionary = new Dictionary<string, int>();
        public override void OnEntry(PostSharp.Aspects.MethodExecutionArgs args)
        {
            base.OnEntry(args);

            AddToCounter(args.Method.Name);
        }

        private static void AddToCounter(string method)
        {
            if (_counterDictionary.ContainsKey(method))
                _counterDictionary[method]++;
            else
            {
                _counterDictionary.Add(method, 1);
            }
        }

        public static void DumpOnEntryCounters()
        {
            foreach (string key in _counterDictionary.Keys)
                Debug.WriteLine(key + ": " + _counterDictionary[key]);
        }
    }
}
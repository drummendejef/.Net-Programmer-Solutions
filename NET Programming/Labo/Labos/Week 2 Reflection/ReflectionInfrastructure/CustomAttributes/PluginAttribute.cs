using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
    public class PluginAttribute : Attribute
    {
        public bool IsPlugin { get; private set; }
        public string Name { get; private set; }

        public PluginAttribute(bool isPlugin, string name)
        {
            IsPlugin = isPlugin;
            Name = name;
        }
    }
}

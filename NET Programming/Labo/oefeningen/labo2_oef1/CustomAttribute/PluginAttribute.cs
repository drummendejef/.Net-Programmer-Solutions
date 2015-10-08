using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAttribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class PluginAttribute : Attribute
    {
        public bool IsPlugin { get; set; }
        public string Name { get; set; }

        public PluginAttribute(bool isPlugin, string name)
        {
            IsPlugin = isPlugin;
            Name = name;
        }
    }
}

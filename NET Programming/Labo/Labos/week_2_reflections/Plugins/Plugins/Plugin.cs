using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugins
{
    [AttributeUsage(AttributeTargets.Class)]
    class Plugin:Attribute
    {
        private bool isPlugin;

        public bool IsPlugin
        {
            get { return isPlugin; }
        }

        private string description;

        public string Description
        {
            get { return description; }
        }
        

        public Plugin(bool isPlugin, string description)
        {
            this.isPlugin = isPlugin;
            this.description = description;
        }
    }
}

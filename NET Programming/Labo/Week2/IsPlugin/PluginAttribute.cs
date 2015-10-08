using System;

namespace CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PluginAttribute : Attribute
    {
        public bool IsPlugin { get; private set; }
        public string Description { get; private set; }

        public PluginAttribute(bool isPlugin, string description)
        {
            IsPlugin = isPlugin;
            Description = description;
        }

        public override string ToString()
        {
            return this.Description + " [Plugin: " + this.IsPlugin + "]";
        }
    }
}

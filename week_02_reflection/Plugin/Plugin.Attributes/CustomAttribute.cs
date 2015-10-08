using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Attributes
{
    //Waar kan de attribute gebruikt worden?
    [AttributeUsage(AttributeTargets.Class)]//Enkel op de klasse niveau werken
    public class CustomAttribute:Attribute
    {
        public string Naam { get; set; }
        public bool IsPlugin { get; set; }

        public CustomAttribute(string naam, bool isPlugin)
        {
            this.Naam = naam;
            this.IsPlugin = isPlugin;
        }
    }
}

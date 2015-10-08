using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Plugins
{
    [Plugin(true,"Basic stuff to test stuff and stuff")]
    class clsPersoon
    {
        private string naam;

        public string Naam
        {
            get { return naam; }
            set { naam = value; }
        }

        public void DoeIets()
        {
            MessageBox.Show("Ik doe iets");
        }
    }
}

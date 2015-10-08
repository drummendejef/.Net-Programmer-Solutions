using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HostApp
{
    class PluginItem : MenuItem
    {
        Type type;

        public PluginItem(string header, Type t) //Header is van microsoft, is de tekst die je gaat zien
        {
            Header = header;
            type = t;
            Click += PluginItem_Click;
        }

        private void PluginItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Window window = (Window)Activator.CreateInstance(type);//Aanmaken van een nieuw scherm
            window.Show();
        }
    }
}

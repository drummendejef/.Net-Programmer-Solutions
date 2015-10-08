using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Reflection_Infrastructure
{
    class PluginMenuItem : MenuItem
    {
        Type _t;

        public PluginMenuItem(string header, Type t)
        {
            Header = header;
            _t = t;
            Click += PluginMenuItem_Click;
        }

        void PluginMenuItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                Window w = Activator.CreateInstance(_t) as Window;
                w.Show();
            } catch (Exception ex)
            {
                Debug.WriteLine("Failed to create window from " + _t.Name);
            }
        }
    }
}

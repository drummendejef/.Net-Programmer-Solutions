using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace labo2_oef1
{
    class ReflectionMenuItem : MenuItem
    {
        Type type;

        public ReflectionMenuItem(string header, Type t)
        {
            Header = header;
            type = t;
            Click += pluginMenuItem_Click;
        }

        private void pluginMenuItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                Window w = Activator.CreateInstance(type) as Window;
                w.Show();
            }
            catch (Exception ex)
            {
            }
        }
    }
}

using Plugin.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HostApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            zoekPlugin();
        }


        //Het zoeken van de plugins
        private void zoekPlugin()
        {
            string pluginDir = AppDomain.CurrentDomain.BaseDirectory + @"plugins\";//Het pad van waar de plugins staan, de @ staat er omdat \ een escape char is, en het anders problemen geeft.
            string[] filters = new string[] { "*.exe", "*.dll" }; //Filters aanmaken

            List<string> filePaths = new List<string>();//Alle paden van dll, exe, die we hierboven hebben opgezocht.

            foreach(string filter in filters)//Filters overlopen
            {
                foreach(string path in Directory.GetFiles(pluginDir, filter)) //Waar, moet hij wat zoeken?
                {
                    filePaths.Add(path);//Toevoegen aan de lijst van paden
                }
            }

            foreach(string path in filePaths)//Alle bestanden inladen
            {
                Assembly ass = Assembly.LoadFile(path);
                VerwerkPlugin(ass);
            }
        }


        //Het verwerken van de plugins
        private void VerwerkPlugin(Assembly ass)
        {
            foreach(Type t in ass.GetTypes())
            {
                ZoekAttributes(t);
            }
        }

        //Zoek attributen en lees ze in
        private void ZoekAttributes(Type t)
        {
            foreach(Attribute pluginAtt in t.GetCustomAttributes())//alle plugins die bestaan in de microsoft framework
            {
                if (pluginAtt.GetType().Equals(typeof(CustomAttribute)))
                {
                    CustomAttribute ca = pluginAtt as CustomAttribute;//Casting manier!!!!

                    if(ca.IsPlugin)
                    {
                        PluginItem pi = new PluginItem(ca.Naam, t);

                        menu.Items.Add(pi);
                    }
                }
                     
            }
        }

        
        //private void()
    }
}

using CustomAttribute;
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

namespace labo2_oef1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private string path = "C:\\Users\\alisio\\Desktop\\howest\\3NMCT\\dotnet\\oefeningen";
        //private string path2 = "C:\\Users\\alisio\\Desktop\\howest\\3NMCT\\dotnet\\oefeningen\\labo2_oef1\\labo2_oef1\\bin\\Debug\\plugins";
        private string path2 = AppDomain.CurrentDomain.BaseDirectory + "plugins";
        private string[] filters = new string[] { "*.exe", "*.dll" };
        //private List<string> files = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
            //loadPlugins();
            loadAssembly();
        }

        private List<string> loadPlugins()
        {
            List<String> files = new List<string>();
            foreach (string filter in filters)
            {
                getFiles(path2, filter, files);
            }
            return files;
        }

        private void getFiles(string path, string filter, List<string> files)
        {
            foreach (string file in Directory.GetFiles(path, filter))
            {
                files.Add(file);
            }
            //foreach (string file in Directory.GetDirectories(path))
            //{
            //    getFiles(file, filter);
            //}
        }

        private void loadAssembly()
        {
            List<string> files = loadPlugins();
            foreach (string file in files)
            {
                try
                {
                    Assembly ass = Assembly.LoadFile(file);
                    foreach (Type t in ass.GetTypes())
                    {
                        CheckAttributes(t);
                    }
                }
                catch(Exception e){

                }
            }
        }

        private void CheckAttributes(Type t)
        {
            foreach (PluginAttribute a in t.GetCustomAttributes(typeof(PluginAttribute)))
            {
                if (a.IsPlugin)
                {
                    ReflectionMenuItem item = new ReflectionMenuItem(a.Name, t);
                    mnuPlugins.Items.Add(item);
                }
            }
        }
    }
}

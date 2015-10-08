using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.IO;
using CustomAttributes;

namespace Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            LoadPlugins(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Plugins");
        }

        /*private void verwerkUI() {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = ".NET assemblies|*.exe;*.dll";
            ofd.InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) + "\\Plugins";
            if(ofd.ShowDialog().Value) {
                verwerkUI(ofd.FileName);
            }
        }*/
        private void LoadPlugins(string sAssembly)
        {
            string[] files = SearchFilesUsingFilters(sAssembly, new string[] { "*.exe", "*.dll" });

            foreach (string file in files)
            {
                LoadAssembly(file);
            }
            /*
            try
            {
                string[] exe = Directory.GetFiles(sAssembly, "*.exe");
                string[] dll = Directory.GetFiles(sAssembly, "*.dll");
                string[] exeORdll = exe.Concat(dll).ToArray();
                //Array.Sort(exeORdll);

                foreach (string s in exeORdll)
                {
                    Assembly ass = Assembly.LoadFile(s);
                    foreach (Type t in ass.GetTypes())
                    {
                        //Console.WriteLine(t.Name + "\n" + t.Attributes + "\n" + t.GetProperties() + "\n----------");
                        CheckAttributes(t);
                    }
                }
            }
            catch (Exception ex)
            {
                if (MessageBox.Show("Problem within verwerkUI:\n-------------------------\n" + ex, "Error", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    this.Close();
                }
            }*/
        }
        private string[] SearchFilesUsingFilters(string dir, string[] filters)
        {
            List<string> paths = new List<string>();

            foreach (string filter in filters)
            {
                paths.AddRange(System.IO.Directory.GetFiles(dir, filter));
            }

            return paths.ToArray();
        }

        private void LoadAssembly(string path)
        {
            Assembly ass = Assembly.LoadFile(path);
            foreach (Type t in ass.GetTypes())
            {
                CheckAttributes(t);
            }
        }

        private void CheckAttributes(Type t)
        {
            foreach (PluginAttribute a in t.GetCustomAttributes(typeof(PluginAttribute)))
            {
                if (a.IsPlugin)
                {
                    //Console.WriteLine("add " + t.Name + " to list");
                    PluginMenuItem item = new PluginMenuItem(a.Description, t);
                    Menu.Items.Add(t.Name);
                }
            }
        }

        private void Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine("lala");
        }
    }
}

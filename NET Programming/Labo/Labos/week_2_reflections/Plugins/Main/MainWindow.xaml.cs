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
using System.Windows.Shapes;

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

            verwerkDemoUI();
        }

        private void verwerkDemoUI()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = ".NET assemblies|*.exe;*.dll";
            ofd.InitialDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);

            if (ofd.ShowDialog().Value)
                verwerkDemoUI(ofd.FileName);

        }

        private void verwerkDemoUI(string assembly)
        {
            Assembly ass = Assembly.LoadFile(assembly);

            foreach (Type t in ass.GetTypes())
                foreach (Attribute attr in t.GetCustomAttributes())
                {
                    if (attr.GetType().Name.Equals("Plugin"))
                        MessageBox.Show(""+t.Name);
                }





                //if (t.Name.Equals("MainWindow"))
                //    verwerkIndienWindow(t);
        }

        private void verwerkIndienWindow(Type t)
        {
            Window wndw = (Window) Activator.CreateInstance(t);
            wndw.Show();
        }
    }
}

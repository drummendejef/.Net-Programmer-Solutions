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

namespace verwerkDemo
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
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Filter = ".NET assemblies |*.exe;*.dll";
            ofd.InitialDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);

            if (ofd.ShowDialog().Value)
                verwerkDemoUI(ofd.FileName);
        }
        private void verwerkDemoUI(string sAssembly)
        {
            Assembly ass = Assembly.LoadFile(sAssembly);
            foreach (Type t in ass.GetTypes())
                Console.WriteLine(t.Name);
        }
    }
}

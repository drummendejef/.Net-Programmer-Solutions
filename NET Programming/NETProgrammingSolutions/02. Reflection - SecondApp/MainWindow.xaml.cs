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

namespace _02.Reflection___SecondApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _voornaam = "Voornaam", _achternaam = "Achternaam";
        private int _leeftijd = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void verwerkDemoUI()
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Filter = ".NET assemblies|*.exe;*.dll";
            ofd.InitialDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);

            if (ofd.ShowDialog().Value)
            {
                verwerkDemoUI(ofd.FileName);
            }
        }
        private void verwerkDemoUI(string sAssembly)
        {
            Assembly ass = Assembly.LoadFile(sAssembly);
            foreach (Type t in ass.GetTypes())
            {
                Console.WriteLine(t.Name);
                verwerkIndienWindow(t);
                verwerkIndienPersoon(t);
            }
        }

        private void verwerkIndienWindow(Type t)
        {
            if (t.Name.Equals("MainWindow"))
            {
                Window wdw = (Window)Activator.CreateInstance(t);
                wdw.Show();
            }
        }

        private void verwerkIndienPersoon(Type t)
        {
            if (t.Name.Equals("Person"))
            {
                object p = Activator.CreateInstance(t);
                t.InvokeMember("FirstName", BindingFlags.SetProperty, null, p, new object[] { _voornaam });
                t.InvokeMember("LastName", BindingFlags.SetProperty, null, p, new object[] { _achternaam });
                t.InvokeMember("Age", BindingFlags.SetProperty, null, p, new object[] { _leeftijd });
                string sPerson = (string)t.InvokeMember("ToString", BindingFlags.InvokeMethod, null, p, new object[] { });
                MessageBox.Show(sPerson);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            verwerkDemoUI();
        }

        private void Button_Send(object sender, RoutedEventArgs e)
        {
            _voornaam = txtVoornaam.Text.ToString();
            _achternaam = txtAchternaam.Text.ToString();
            int i;
            bool b = Int32.TryParse(txtLeeftijd.Text, out i);
            if (b)
            {
                _leeftijd = i;
            }
        }
    }
}

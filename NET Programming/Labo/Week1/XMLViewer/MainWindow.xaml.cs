using Microsoft.Win32;
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

namespace XMLViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private XMLView xmlview;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadXML(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML files|*.xml";
            if (ofd.ShowDialog().Value)
            {
                txtFile.Text = ofd.FileName;
                xmlview = new XMLView(ofd.FileName);

                treeview.ItemsSource = xmlview.ItemList;
                Console.WriteLine("itemsource set");
            }
        }
    }
}

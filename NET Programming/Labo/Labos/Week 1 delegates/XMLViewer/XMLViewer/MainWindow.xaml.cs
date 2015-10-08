using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Xml;
using CustomAttributes;
using Microsoft.Win32;

namespace XMLViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [PluginAttribute(true, "XML Viewer")]
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog().Value)
            {
                if (xmlReader.Items.Count > 0)
                {
                    xmlReader.Items.RemoveAt(0);
                    xmlReader2.Items.RemoveAt(0);
                }
                    
                xmlReader.LoadXml(dialog.FileName);
                xmlReader2.LoadXml(dialog.FileName);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            xmlReader.ColorItemDelegate = XmlItemDelegates.GetNodeColor;
            xmlReader.NameItemDelegate = XmlItemDelegates.NameBooks;
            xmlReader.TraverseNodesDelegate = XmlItemDelegates.ListBookNodes;
        }


    }
}

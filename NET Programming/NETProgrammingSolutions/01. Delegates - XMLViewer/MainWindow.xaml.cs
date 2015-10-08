using _02.Reflection___A.Custom_Attributes;
using Microsoft.Win32;
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

namespace _01.Delegates___XMLViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [PluginAttribute(true, "XML Viewer")]
    public partial class MainWindow : Window
    {
        string _filename;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGetFileClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog().Value)
            {
                if (xmlReader.Items.Count > 0)
                {
                    xmlReader.Items.RemoveAt(0);
                    xmlReader2.Items.RemoveAt(0);
                }
                _filename = dialog.FileName;

                xmlReader.LoadXml(_filename);
            }
        }

        private void btnPlumbClick(object sender, RoutedEventArgs e)
        {
            xmlReader2.ColorItemDelegate = XmlItemDelegates.GetNodeColor;
            xmlReader2.NameItemDelegate = XmlItemDelegates.NameBooks;
            xmlReader2.TraverseNodesDelegate = XmlItemDelegates.ListBookNodes;
            xmlReader2.LoadXml(_filename);
            xmlReader2.Items.Refresh();
            xmlReader2.UpdateLayout();
        }
    }
}

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

namespace XMLViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOpenFileDialog_Click(object sender, RoutedEventArgs e)
        {
            string path = this.xmlViewer.GetFileName();

            this.txtFilePath.Text = path;
        }

        private void btnPlumbCode_Click(object sender, RoutedEventArgs e)
        {
            this.xmlViewer.ColorDelegate = CustomDelegates.MyColorDelegate;
            this.xmlViewer.NameDelegate = CustomDelegates.MyNameDelegate;
            this.xmlViewer.ReadXmlNodeDelegate = CustomDelegates.MyReadXmlNodeDelegate;
        }
    }
}

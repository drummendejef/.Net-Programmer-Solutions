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

namespace iTextSharp
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

        private void btnSimplePDF_Click(object sender, RoutedEventArgs e)
        {
            WriteSimpleTextToPDF();
        }

        private void WriteSimpleTextToPDF()
        {
            PDFWriter w = new PDFWriter();
            w.writeSimpleTextToPDF();
        }
    }
}

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

namespace iTextSharpNew
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        APUPDFWriter w = new APUPDFWriter();
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
            w.writeSimpleTextToPDF();
        }

        private void btnExtra_Click(object sender, RoutedEventArgs e)
        {
            scale();
        }

        private void scale()
        {
            w.scale();
        }

        private void btnMerge_Click(object sender, RoutedEventArgs e)
        {
            merge();
        }

        private void merge()
        {
            w.mergePDF();
        }

        private void btnWatermark_Click(object sender, RoutedEventArgs e)
        {
            w.watermark();
        }
    }
}

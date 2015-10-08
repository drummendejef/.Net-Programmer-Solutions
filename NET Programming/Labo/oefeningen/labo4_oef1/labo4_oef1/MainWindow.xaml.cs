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

namespace labo4_oef1
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

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            startNQueens();
        }

        private void startNQueens()
        {
            cnvBord.setSizeBord(int.Parse(iudAantal.Text));
        }

        private void btnAsync_Click(object sender, RoutedEventArgs e)
        {
            cnvBord.setSizeBordAsync(int.Parse(iudAantal.Text));
        }
    }
}

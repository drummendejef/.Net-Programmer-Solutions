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

namespace labo1_oef2Versie3
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

        public Boolean IsMultiThreaded
        {
            get { return chkThread.IsChecked.Value; }
        }

        private void btnStart1_Click(object sender, RoutedEventArgs e)
        {
            Teller1Starten();
        }

        private void btnStart2_Click(object sender, RoutedEventArgs e)
        {
            Teller2Starten();
        }

        private void Teller1Starten()
        {
            txtTeller1.TellerStarten(IsMultiThreaded);
        }

        private void Teller2Starten()
        {
            txtTeller2.TellerStarten(IsMultiThreaded);
        }
    }
}

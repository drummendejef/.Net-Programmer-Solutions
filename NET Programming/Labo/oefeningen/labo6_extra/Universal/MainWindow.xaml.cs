using System;
using System.Collections;
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

namespace Universal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int aantal = 4;
        public MainWindow()
        {
            InitializeComponent();
            lstRow.aantal = this.aantal;
        }

        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            verhoogIndexMet1();
        }

        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            verlaagIndexMet1();
        }

        private void btnOphalen_Click(object sender, RoutedEventArgs e)
        {
            ophalenGegevens();
        }

        private void ophalenGegevens()
        {
            lstTable.getTables(txtDatabase.Text, lstRow);
        }

        private void verhoogIndexMet1()
        {
            lstRow.aantal = this.aantal;
            lstTable.getDataNext();
        }

        private void verlaagIndexMet1()
        {
            lstRow.aantal = this.aantal;
            lstTable.getDataPrevious();
        }
    }
}

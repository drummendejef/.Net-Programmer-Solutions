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
using System.Windows.Threading;

namespace labo1_oef2Versie2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       // Boolean isMultiThreaded = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        //private void chkThread_Checked(object sender, RoutedEventArgs e)
        //{
        //    threaded();
        //}
        //private void threaded()
        //{
        //    if (chkThread.IsChecked == true) isMultiThreaded = true;
        //    else isMultiThreaded = false;
        //}

        public Boolean IsMultiThreaded
        {
            get { return chkThread.IsChecked.Value; }
        }

        private void btnStart1_Click(object sender, RoutedEventArgs e)
        {
            if (IsMultiThreaded) Teller1Starten();
            else notThreaded1();
        }

        private void btnStart2_Click(object sender, RoutedEventArgs e)
        {
            if (IsMultiThreaded) Teller2Starten();
            else notThreaded2();
        }

        private void Teller1Starten()
        {
            /*txtTeller1.threaded = true;
            txtTeller1.timer.Start();
            txtTeller1.Optellen += txt_Optellen1;*/
            txtTeller1.TellerStarten(true);
        }

        private void Teller2Starten()
        {
            /*txtTeller2.threaded = true;
            txtTeller2.timer.Start();
            txtTeller2.Optellen += txt_Optellen2;*/
            txtTeller2.TellerStarten(true);
        }

        private void notThreaded1()
        {
            /*txtTeller1.threaded = false;
            txtTeller1.timer.Start();*/
            txtTeller1.TellerStarten(false);
        }
        private void notThreaded2()
        {
            /*txtTeller2.threaded = false;
            txtTeller2.timer.Start();*/
            txtTeller2.TellerStarten(false);
        }
        void txt_Optellen1(object sender, EventArgs e)
        {
            //txtTeller1.teller++;
        }

        void txt_Optellen2(object sender, EventArgs e)
        {
            //txtTeller2.teller++;
        }
    }
}

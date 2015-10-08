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

namespace labo1_oef2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        Boolean isMultiThreaded = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        void txt_Optellen1(object sender, EventArgs e)
        {
            txtTeller1.teller++;
        }

        void txt_Optellen2(object sender, EventArgs e)
        {
            txtTeller2.teller+=2;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (txtTeller1.isEnable) txtTeller1.Text = txtTeller1.teller.ToString();
            if (txtTeller2.isEnable) txtTeller2.Text = txtTeller2.teller.ToString();

            if (!isMultiThreaded)
            {
                txtTeller1.teller++;
                txtTeller2.teller+=2;
            }
        }

        private void timerStarten()
        {
            if (timer.IsEnabled == false)
            {
                timer.Tick += new EventHandler(timer_Tick);
                timer.Interval = new TimeSpan(0, 0, 0,1);

                timer.Start();
            }
        }

        private void Teller1Starten(){
            timerStarten();
            txtTeller1.isEnable = true;
            txtTeller1.Optellen += txt_Optellen1;
            txtTeller1.Text = "0";
        }

        private void Teller2Starten()
        {
            timerStarten();
            txtTeller2.isEnable = true;
            txtTeller2.Optellen += txt_Optellen2;
            txtTeller2.Text = "0";
        }

        private void btnStart1_Click(object sender, RoutedEventArgs e)
        {
            if(isMultiThreaded) Teller1Starten();
            else notThreaded1();
        }

        private void btnStart2_Click(object sender, RoutedEventArgs e)
        {
            if (isMultiThreaded) Teller2Starten();
            else notThreaded2();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            threaded();
        }

        private void threaded()
        {
            if (chkThreaded.IsChecked == true) isMultiThreaded = true;
            else isMultiThreaded = false;
        }

        private void notThreaded1()
        {
            timerStarten();
            txtTeller1.isEnable = true;
            txtTeller1.Text = "0";
        }
        private void notThreaded2()
        {
            timerStarten();
            txtTeller2.isEnable = true;
            txtTeller2.Text = "0";
        }
    }
}

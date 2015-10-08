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

namespace Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Counter c;
        static TextBlock b;
        public MainWindow()
        {
            InitializeComponent();


            c = new Counter(new Random().Next(10));
            c.ThresholdReached += c_ThresholdReached;

            label.Content = "press button to increase total";
            b = tekst;
        }



        static void c_ThresholdReached(object sender, ThresholdReachedEventArgs e)
        {
            Console.WriteLine("The threshold of {0} was reached at {1}.", e.Threshold, e.TimeReached);
            b.Text = "The threshold of " + e.Threshold + " was reached at " + e.TimeReached;
            //Environment.Exit(0);
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            c.Add(1);
        }
    }
}

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

namespace _01.Delegates___Multithreading
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
            StartCounter1();
        }

        private void btnStart2_Click(object sender, RoutedEventArgs e)
        {
            StartCounter2();
        }

        private void StartCounter1()
        {
            txtCounter1.CounterStart(IsMultiThreaded);
        }

        private void StartCounter2()
        {
            txtCounter2.CounterStart(IsMultiThreaded);
        }
    }
}

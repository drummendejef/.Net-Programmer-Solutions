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

namespace _09.AOP___Fibonacci
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Fibonacci f = new Fibonacci(10);
            foreach (int i in f.FibonacciList)
            {
                Console.WriteLine(i);
            }
            clsEntryTeller.dumpIntoConsole();
        }

        private void btnCalc_Click(object sender, RoutedEventArgs e)
        {
            int number;
            bool result = Int32.TryParse(txtInput.Text, out number);
            if (result)
            {
                Fibonacci f = new Fibonacci(number);
                listResult.ItemsSource = f.FibonacciList;
            }
        }
    }
}

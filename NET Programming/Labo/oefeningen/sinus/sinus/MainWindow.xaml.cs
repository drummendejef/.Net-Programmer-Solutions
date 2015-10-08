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

namespace sinus
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            berekenSinus();
        }

        private void berekenSinus()
        {
            lstGetal.Items.Clear();
            int aantal = int.Parse(txtAantal.Text);
            double x = Double.Parse(txtX.Text);
            //double angle = Math.PI * x / 180.0;
            /*for (int i = 0; i < aantal;i++)
            {
                //lstGetal.Items.Add(berekenGetal(i, x) + " " + Math.Sin(x));
                lstGetal.Items.Add(berekenGetal(i, x));
            }*/
            lstGetal.Items.Add(berekenGetal(aantal, x));
        }

        private double berekenGetal(int n, double x)
        {
            /*
            double teller = Math.Pow((-1),aantal);
            double noemer = ((2 * aantal) + 1);
            double teller2 = Math.Pow(x,((2*aantal) + 1));
            return teller / (berekenNoemer(noemer) * noemer) * teller2;
            */
            //formule
            // ((-1)^n / (2n + 1)!) * x^2n +1
            // x - 
            var teller = Math.Pow((-1), n);
            Console.WriteLine(teller);
            var n1 = (n * n + 1);
            var noemer = 1;
            for (; n1 > 0; n1--)
            {
                noemer *= n1; 
            }
            Console.WriteLine(noemer);
            var n2 = Math.Pow(x, n*n + 1);
            Console.WriteLine(n2);
            return teller/noemer * n2;
        }

        private double berekenNoemer(double noemer)
        {
            double n = noemer;
            if (n < 0)
            {
                return -1;
            }
            else if (n == 1 || n == 0)
            {
                return 1;
            }
            else
            {
                return n * berekenNoemer(n - 1);
            }
        }
    }
}

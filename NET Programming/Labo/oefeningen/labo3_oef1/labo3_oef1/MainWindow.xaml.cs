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

namespace labo3_oef1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Functie graph = new Functie();
            RuntimeCompiler rc = new RuntimeCompiler();
        }

        private void btnDraw_Click(object sender, RoutedEventArgs e)
        {
            drawFunction();
        }

        private void drawFunction()
        {
            cnvFunctie.generateFunction(txtFunctie.Text);
        }
    }
}

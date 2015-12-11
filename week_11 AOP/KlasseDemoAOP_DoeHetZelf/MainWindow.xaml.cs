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

namespace KlasseDemoAOP_DoeHetZelf
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

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            TestAOP();
        }

        private void TestAOP()
        {
            Wiskundige ivo = new Wiskundige();
            int n = 0;
            if (int.TryParse(txtN.Text, out n))
            {
                int f = ivo.Fibonacci(n);
                txtF.Text = f.ToString();
            }
        }

        
        

        //De dump button (gaat de aantal keren dat hij door een method is gelopen afdrukken)
        private void button_Click(object sender, RoutedEventArgs e)
        {
            EntryTellerAttribute.DumpInfo();
        }

        //Als de tekst in het inputvak veranderd is.
        private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            int value;
            if (!int.TryParse(txtN.Text, out value))
                return;
            try
            {
                TestRangeAttribute(value);
            }
            catch(AOPRangeException ex)
            {
                lblMinMaxException.Text = ex.Message;
            }
        }

        [ParamValidationPrecondition()]
        private void TestRangeAttribute([MinMaxAttribute(0,100)] int value)
        {
            lblMinMaxException.Text = "";
        }
    }
}

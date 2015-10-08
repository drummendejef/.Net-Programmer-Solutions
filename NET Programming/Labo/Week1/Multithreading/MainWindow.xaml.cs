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


/*multithreading: maak een window waarin verschillende tellers asynchroon kunnen oplopen. De teller wordt gerealiseerd door een control die erft van TextBox. Om dit te realiseren zal u de prioriteit van de visualisatie op background priority moeten uitvoeren.
De demo toont de volgende elementen:
    - singlethreaded is er maar een activiteit actief;
    - multithreaded zijn er verschillende acties terzelfdertijd mogelijk: het tellen in twee textboxen en het verplaatsen van het window.
*/
namespace Multithreading
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Boolean _bThreading = false;

        public MainWindow()
        {
            InitializeComponent();
            txtThreading.Text = "Threading: " + _bThreading;

            txtCounter1.AddToNumber += txtCounter1_AddToNumber;
            txtCounter2.AddToNumber += txtCounter2_AddToNumber;
        }

        void txtCounter1_AddToNumber(object sender, EventArgs e)
        {
            txtCounter1.Counter();
        }

        void txtCounter2_AddToNumber(object sender, EventArgs e)
        {
            txtCounter2.Counter();
        }

        private void changeThreading(object sender, RoutedEventArgs e)
        {
            _bThreading = !_bThreading;
            txtCounter1.ChangeThreading();
            txtCounter2.ChangeThreading();
            txtThreading.Text = "Threading: " + _bThreading;
        }

        private void StartCounter(object sender, RoutedEventArgs e)
        {
            string nr = e.Source.ToString().Last().ToString();
            if (nr == "1")
            {
                txtCounter1.StartTeller();
                if (!_bThreading && txtCounter2.Running) txtCounter2.ChangeRunning(); 
            }
            else if (nr == "2")
            {
                txtCounter2.StartTeller();
                if (!_bThreading && txtCounter1.Running) txtCounter1.ChangeRunning(); 
            }
        }
    }
}

// textbox class, int = 0, starteller via btn click (in textbox class)

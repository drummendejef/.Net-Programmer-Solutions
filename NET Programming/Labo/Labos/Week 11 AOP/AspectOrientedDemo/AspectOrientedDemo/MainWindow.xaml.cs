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

namespace AspectOrientedDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            lblFibonacci.Text = Calculations.Fibonacci(50).ToString();
            AOPEntryCounter.DumpOnEntryCounters();
        }

        private void txtValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            int value;
            if (!int.TryParse(txtValue.Text, out value))
                return;
            try
            {
                TestRangeAttribute(value);
            }
            catch(AOPRangeException ex)
            {
                lblRangeException.Text = ex.Message;
            }
        }
        
        [AOPParamValidatePrecondition()]
        private void TestRangeAttribute( [RangeParamValidation(0,100)] int value)
        {
            lblRangeException.Text = "";
        }

        private void DatePicker_CalendarClosed(object sender, RoutedEventArgs e)
        {
            DatePicker picker = sender as DatePicker;

            if (!picker.SelectedDate.HasValue)
                return;

            try
            {
                TestAgeAttribute(picker.SelectedDate.Value);
            }
            catch(AOPAgeException ex)
            {
                lblAgeException.Text = ex.Message;
            }
        }

        [AOPParamValidatePrecondition()]
        private void TestAgeAttribute( [AgeParamValidation()] DateTime date)
        {
            lblAgeException.Text = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TestAdmin();
            }
            catch(AOPNotAdminException ex)
            {
                lblAdmin.Foreground = new SolidColorBrush(Colors.Red);
                lblAdmin.Text = ex.Message;
            }
        }

        [AOPAdminRequired()]
        private void TestAdmin()
        {
            lblAdmin.Foreground = new SolidColorBrush(Colors.Black);
            lblAdmin.Text = "Yes!";
        }


    }
}

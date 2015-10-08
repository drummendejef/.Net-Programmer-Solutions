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

namespace _01.Delegates___CustomEvent
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            demo.TextChangedToEmpty += demo_TextChangedToEmpty;
            demo.TextChangedToFilled += demo_TextChangedToFilled;
        }

        void demo_TextChangedToFilled(object sender, EventArgs e)
        {
            label.Text = "";
        }

        void demo_TextChangedToEmpty(object sender, EventArgs e)
        {
            label.Text = "textbox is leeg";
        }
    }
}

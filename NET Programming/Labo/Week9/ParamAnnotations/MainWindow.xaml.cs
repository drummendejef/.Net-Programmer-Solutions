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

namespace ParamAnnotations
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Object str = "string";
            Console.WriteLine(str.GetType());
            Object i = 153;
            Console.WriteLine(i.GetType());
            Object d = 3265.645;
            Console.WriteLine(d.GetType());
            Object datetime = DateTime.Now;
            Console.WriteLine(datetime.GetType());
        }
    }
}

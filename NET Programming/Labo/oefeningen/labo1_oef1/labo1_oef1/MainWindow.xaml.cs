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

namespace labo1_oef1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //de event var van klasse += methode dat het resultaat weergeeft
            txt.IkBenLeeg += txt_IkBenLeeg;
        }

        void txt_IkBenLeeg(object sender, EventArgs e)
        {
            MessageBox.Show("ik ben leeg");
        }
    }
}

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

namespace labo6_extra
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Persoon persoon = new Persoon();
        Table table = new Table();
        private int index = 0;
        private int aantal = 4;
        public MainWindow()
        {
            InitializeComponent();
            getPersonen(index, aantal);
        }

        private void getPersonen(int index, int aantal)
        {
            lstPersoon.Items.Clear();
            persoon.inlezenDatabase();
            int min = index * aantal;
            int max = 0;
            if ((index * aantal) + aantal < persoon.Personen.Count()) max = (index * aantal) + aantal;
            else max = persoon.Personen.Count();
            for (int i = min; i < max; i++)
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = persoon.Personen[i];
                lstPersoon.Items.Add(item);
            }
        }

        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            verhoogIndexMet1();
        }

        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            verlaagIndexMet1();
        }

        private void verhoogIndexMet1()
        {
            if ((index * aantal) + aantal < persoon.Personen.Count())
            {
                index++;
                getPersonen(index, aantal);
            }
        }

        private void verlaagIndexMet1()
        {
            if (index > 0)
            {
                index--;
                getPersonen(index, aantal);
            }
        }
    }
}

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

namespace NQueens
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool[,] bord;

        public MainWindow()
        {
            InitializeComponent();


        }

        private void txtAantalQuee_TextChanged(object sender, TextChangedEventArgs e)
        {
            int aantal; 
            bool vlag = Int32.TryParse(txtAantalQuee.Text, out aantal);
            if (vlag)
            { 
                bord = new bool[aantal, aantal];

                if (this.bordContentControl.zoekOplossing(aantal, bord, 0))
                {
                    bordContentControl.tekenBord();
                }
                else
                {
                    this.bordContentControl.Background = Brushes.Black;
                }
            }
                
        }
  
    }
}

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

namespace PDFCreatie
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //create btn
            PDFCreate btn1 = new PDFCreate();
            btn1.Height = 50;
            btn1.Width = 150;
            btn1.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            btn1.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            btn1.Content = "Create PDF";
            btn1.Title = "noedelsoep met balletjes";
            btn1.Author = "Celine Gardier";
            btn1.Keywords = "Noedels, Soep, Balletjes";
            btn1.Subject = "Lekker noedelsoep met vlees balletjes";
            btn1.StandardValues();
            grd.Children.Add(btn1);


            PDFManipulate btn2 = new PDFManipulate();
            btn2.Height = 50;
            btn2.Width = 150;
            btn2.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            btn2.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            btn2.Content = "Merge PDF";
            grd.Children.Add(btn2);


            PDFMultiplePerPage btn3 = new PDFMultiplePerPage();
            btn3.Height = 50;
            btn3.Width = 150;
            btn3.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            btn3.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            btn3.Content = "MMP PDF";
            grd.Children.Add(btn3);


            PDFWatermark btn4 = new PDFWatermark();
            btn4.Height = 50;
            btn4.Width = 150;
            btn4.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            btn4.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            btn4.Content = "Watermark PDF";
            grd.Children.Add(btn4);
        }
    }
}

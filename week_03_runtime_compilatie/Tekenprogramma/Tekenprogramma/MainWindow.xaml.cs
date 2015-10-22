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

namespace Tekenprogramma
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Properties
        public string formule { get; set; }

        public int teller { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            

            //tekenLijn();
        }

        

        private void tekenLijn()
        {
            //Polyline aanmaken
            Polyline line = new Polyline();
            line.Stroke = Brushes.Aquamarine;
            line.Visibility = Visibility.Visible;
            line.StrokeThickness = 3;

            //Punten
            PointCollection punten = new PointCollection();

            //Dingen die in de formule ingevuld gaan worden
            double a = 100;// canvas.Height / 2; //De evenwichtsstand
            double b = 2;// canvas.Height / 4; //De amplitude
            //double d = Convert.ToDouble(ActualHeightProperty.ToString()) ; //de x-coördinaat van een punt waar de grafiek stijgend de evenwichtsstand snijdt

            double windowbreedte = canvas.ActualWidth;
            double windowhoogte = canvas.ActualHeight/2;

            //Alle punten overlopen
            for (int i = 0; i < windowbreedte; i++)
            {
                //punten.Add(new Point() { X = i, Y = windowhoogte * (1 - Math.Sin(i * (Math.PI / (windowbreedte/6))))});
                punten.Add(new Point(i,  Math.Sin(((2*Math.PI )/ b) * (i - windowbreedte)) * windowhoogte));
                //2,5 + 2,5 sin(2π / 9(x – 3))

            }

            line.Points = punten;

            //Lijn tekenen
            canvas.Children.Add(line);

        }


        //private void textBox_MouseDoubleClick(object sender, RoutedEventArgs e)
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {

                formule = textBoxFormule.Text.ToString();

                canvas.Children.Clear();

                tekenLijn();        
        }
    }
}

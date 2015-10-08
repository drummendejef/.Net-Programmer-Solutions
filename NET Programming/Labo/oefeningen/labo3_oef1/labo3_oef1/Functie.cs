using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace labo3_oef1
{
    class Functie : Canvas
    {
        Polyline polyLine;
        public Functie()
        {
            polyLine = new Polyline();
            polyLine.Stroke = Brushes.Red;
            polyLine.StrokeThickness = 1;
            this.Children.Add(polyLine);
        }

        public void generateFunction(string functie)
        {
            //AddPoints();
        }

        private void AddPoints()
        {
            for (Int32 x = 0; x < 90; x += 1)
            {
                polyLine.Points.Add(new Point(x, 50 + 50 * Math.Sin(x / Math.PI)));
            }
        }
    }
}

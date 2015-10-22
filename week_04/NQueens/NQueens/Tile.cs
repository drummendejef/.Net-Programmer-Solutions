using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NQueens
{
    class Tile
    {
        public double _width { get; set; }
        public double _height { get; set; }

        //Het geven van de afmetingen van de tile
        public Tile(double width, double height)
        {
            this._width = width;
            this._height = height;
        }

        //De tiles tekenen
        public Rectangle placeTiles(SolidColorBrush color)
        {
            Rectangle rect = new Rectangle();
            rect.Stroke = new SolidColorBrush(Colors.Black);
            rect.StrokeThickness = 1;
            rect.Fill = color;
            rect.Width = _width;
            rect.Height = _height;
            return rect;
        }
    }
}

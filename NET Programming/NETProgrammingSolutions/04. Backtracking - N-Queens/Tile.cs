using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _04.Backtracking___N_Queens
{
    class Tile
    {
        public double _width { get; set; }
        public double _height { get; set; }
        public Tile(double width, double height)
        {
            this._width = width;
            this._height = height;
        }

        public Rectangle placeTiles(SolidColorBrush color)
        {
            Rectangle rect = new Rectangle();
            rect.Stroke = new SolidColorBrush(Colors.Black);
            rect.StrokeThickness = 2;
            rect.Fill = color;
            rect.Width = _width;
            rect.Height = _height;
            return rect;
        }
    }
}

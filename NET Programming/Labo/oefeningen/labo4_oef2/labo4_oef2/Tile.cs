using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace labo4_oef2
{
    public class Tile : TextBox
    {
        public Boolean isEditable = true;
        public string value = "";
        public Point position;
        public Tile placeTiles(SolidColorBrush color)
        {
            this.Text = value;
            BorderThickness = new Thickness(1);
            BorderBrush = new SolidColorBrush(Colors.Black);
            Padding = new Thickness(12);
            TextAlignment = TextAlignment.Center;
            FontSize = 16;
            Foreground = color;
            if (isEditable) IsEnabled = true;
            else IsEnabled = false;
            return this;
        }
    }
}

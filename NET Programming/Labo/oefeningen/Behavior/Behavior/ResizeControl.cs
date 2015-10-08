using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Behavior
{
    public class ResizeControl : DragControl
    {
        protected override void MouseMoveHandler(object sender, RoutedEventArgs e)
        {
            resizeBehavior(sender as DependencyObject);
        }

        private void resizeBehavior(DependencyObject sender)
        {
            if (clickDrag)
            {
                FrameworkElement element = (FrameworkElement)sender;
                Point point = Mouse.GetPosition(element);
                double deltaX = point.X - (element.Width / 2);
                element.Width = Math.Abs(deltaX * 2);
                double deltaY = point.Y - (element.Height / 2);
                element.Height = Math.Abs(deltaY * 2);
            }
        }

        protected override void MouseDownHandler(object sender, RoutedEventArgs e)
        {
            clickDrag = true;
        }
    }
}

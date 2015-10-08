using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Behaviours
{
    class ResizeBehavior : DragBehaviour
    {
        private Point _previousMousePosition = new Point(-1,-1);

        protected override void MouseMoveHandler(object sender, RoutedEventArgs e)
        {
            if (!AssociatedObject.IsMouseCaptured) return;

            Point currentMousePosition = Mouse.GetPosition(_window);

            Vector difference;
            if (_previousMousePosition == new Point(-1, -1))
            {
                _previousMousePosition = currentMousePosition;
                return;
            }
                
            else
                difference = currentMousePosition - _previousMousePosition;

            double newWidth = ((FrameworkElement)AssociatedObject).Width + difference.X;

            if (newWidth < 50) newWidth = 50;

            ((FrameworkElement)AssociatedObject).Width = newWidth;

            _previousMousePosition = currentMousePosition;
        }

        protected override void MouseUpHandler(object sender, RoutedEventArgs e)
        {
            base.MouseUpHandler(sender, e);

            _previousMousePosition = new Point(-1, -1);
        }
    }
}

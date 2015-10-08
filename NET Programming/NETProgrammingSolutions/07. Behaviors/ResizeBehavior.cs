using System.Windows;
using System.Windows.Input;

namespace _07.Behaviors
{
    class ResizeBehavior : DragBehaviour
    {
        private Point _previousMousePosition = new Point(-1, -1);

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
            double newHeight = ((FrameworkElement)AssociatedObject).Height + difference.Y;

            if (newWidth < 50) newWidth = 50;
            else if (newWidth > 500) newWidth = 500;
            if (newHeight < 30) newHeight = 30;
            else if (newHeight > 500) newHeight = 500;

            ((FrameworkElement)AssociatedObject).Width = newWidth;
            ((FrameworkElement)AssociatedObject).Height = newHeight;

            _previousMousePosition = currentMousePosition;
        }

        protected override void MouseUpHandler(object sender, RoutedEventArgs e)
        {
            base.MouseUpHandler(sender, e);

            _previousMousePosition = new Point(-1, -1);
        }
    }
}

using System.Windows;
using System.Windows.Input;

namespace _07.Behaviors
{
    class GripBehavior : DragBehaviour
    {

        protected override void MouseMoveHandler(object sender, RoutedEventArgs e)
        {
            if (!AssociatedObject.IsMouseCaptured) return;
            Vector difference = Mouse.GetPosition(_window) - _mouseStartPosition;

            _window.Left += difference.X;
            _window.Top += difference.Y;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Behaviours
{
    class GripBehaviour : DragBehaviour
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

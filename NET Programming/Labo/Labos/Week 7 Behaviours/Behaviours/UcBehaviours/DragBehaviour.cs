using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace Behaviours
{
    class DragBehaviour : Behavior<UIElement>
    {
        protected Point _mouseStartPosition;
        private Point _elementStartTransform = new Point();
        private TranslateTransform _translateTransform = new TranslateTransform();
        protected Window _window = Application.Current.MainWindow;
        protected override void OnAttached()
        {

            AssociatedObject.RenderTransform = _translateTransform;

            AssociatedObject.AddHandler(UIElement.MouseDownEvent, new RoutedEventHandler(MouseDownHandler), true);

            AssociatedObject.AddHandler(UIElement.MouseMoveEvent, new RoutedEventHandler(MouseMoveHandler), true);

            AssociatedObject.AddHandler(UIElement.MouseUpEvent, new RoutedEventHandler(MouseUpHandler), true);
        }

        protected void MouseDownHandler(object sender, RoutedEventArgs e)
        {
            _mouseStartPosition = Mouse.GetPosition(_window);
            AssociatedObject.CaptureMouse();
        }

        protected virtual void MouseMoveHandler(object sender, RoutedEventArgs e)
        {
            if (!AssociatedObject.IsMouseCaptured) return;
            Vector difference = Mouse.GetPosition(_window) - _mouseStartPosition;

            _translateTransform.X = _elementStartTransform.X + difference.X;
            _translateTransform.Y = _elementStartTransform.Y + difference.Y;
        }
        protected void MouseUpHandler(object sender, RoutedEventArgs e)
        {
            AssociatedObject.ReleaseMouseCapture();
            _elementStartTransform.X = _translateTransform.X;
            _elementStartTransform.Y = _translateTransform.Y;
        }


    }
}

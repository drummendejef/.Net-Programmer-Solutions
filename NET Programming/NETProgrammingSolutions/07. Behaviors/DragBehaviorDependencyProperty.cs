using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace _07.Behaviors
{
    class DragBehaviorDependencyProperty : DependencyObject
    {
        private Point _mouseStartPosition;
        private Point _elementDragEndPosition;
        private TranslateTransform _translateTransform = new TranslateTransform();

        private UIElement _element;
        public UIElement Element
        {
            get { return _element; }
            set
            {
                _element = value;

                _element.AddHandler(UIElement.MouseDownEvent, new RoutedEventHandler(MouseDownHandler), true);
                _element.AddHandler(UIElement.MouseMoveEvent, new RoutedEventHandler(MouseMoveHandler), true);
                _element.AddHandler(UIElement.MouseUpEvent, new RoutedEventHandler(MouseUpHandler), true);
            }
        }

        private void MouseDownHandler(object sender, RoutedEventArgs e)
        {
            _mouseStartPosition = Mouse.GetPosition(Application.Current.MainWindow);
            _element.RenderTransform = _translateTransform;
            _element.CaptureMouse();
        }

        private void MouseMoveHandler(object sender, RoutedEventArgs e)
        {
            if (!Element.IsMouseCaptured) return;

            Point currentPosition = Mouse.GetPosition(Application.Current.MainWindow);
            Vector delta = currentPosition - _mouseStartPosition;

            _translateTransform.X = _elementDragEndPosition.X + delta.X;
            _translateTransform.Y = _elementDragEndPosition.Y + delta.Y;
        }

        private void MouseUpHandler(object sender, RoutedEventArgs e)
        {
            _element.ReleaseMouseCapture();
            _elementDragEndPosition.X = _translateTransform.X;
            _elementDragEndPosition.Y = _translateTransform.Y;
        }

        [Browsable(true)]
        public static bool GetIsDraggable(UIElement target)
        {
            return (bool)target.GetValue(IsDraggableProperty);
        }

        public static void SetIsDraggable(UIElement target, bool value)
        {
            target.SetValue(IsDraggableProperty, value);
        }

        //REGISTER ATTACHED GEBRUIKEN! Anders wordt de property changed callback niet gefired
        public static readonly DependencyProperty IsDraggableProperty =
            DependencyProperty.RegisterAttached("IsDraggableProperty", typeof(bool), typeof(DragBehaviorDependencyProperty), new UIPropertyMetadata(new PropertyChangedCallback(PropertyChangedCallBack)));

        private static void PropertyChangedCallBack(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            if (!(bool)e.NewValue) return;

            UIElement element = target as UIElement;
            if (element == null) return;

            DragBehaviorDependencyProperty escapeStatic = new DragBehaviorDependencyProperty()
            {
                Element = element
            };
        }


    }
}

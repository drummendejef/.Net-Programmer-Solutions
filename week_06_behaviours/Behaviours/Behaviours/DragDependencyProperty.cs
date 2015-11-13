using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Behaviours
{
    class DragDependencyProperty : DependencyObject
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

                //Zorgen dat het UI object naar onderstaande events gaat luisteren.
                _element.AddHandler(UIElement.MouseDownEvent, new RoutedEventHandler(MouseDownHandler), true);
                _element.AddHandler(UIElement.MouseMoveEvent, new RoutedEventHandler(MouseMoveHandler), true);
                _element.AddHandler(UIElement.MouseUpEvent, new RoutedEventHandler(MouseUpHandler), true);
            }
        }


        private void MouseDownHandler(object sender, RoutedEventArgs e)
        {
            //Debug.WriteLine("Mouse Down");
            _mouseStartPosition = Mouse.GetPosition(Application.Current.MainWindow);
            _element.RenderTransform = _translateTransform;
            _element.CaptureMouse();
        }
        private void MouseMoveHandler(object sender, RoutedEventArgs e)
        {
            
            if (!Element.IsMouseCaptured) return; //Niets doen als de muis niet ingehouden is. Wordt op true gezet in de MouseDownHandler, wordt op false gezet in de MouseUpHandler.

            Point currentPosition = Mouse.GetPosition(Application.Current.MainWindow);
            Vector delta = currentPosition - _mouseStartPosition;

            _translateTransform.X = _elementDragEndPosition.X + delta.X;
            _translateTransform.Y = _elementDragEndPosition.Y + delta.Y;


        }
        private void MouseUpHandler(object sender, RoutedEventArgs e)
        {
            //Debug.WriteLine("Mouse Up");
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

        //REGISTER ATTACHED GEBRUIKEN! Anders wordt de property changed callback niet getriggerd
        public static readonly DependencyProperty IsDraggableProperty =
            DependencyProperty.RegisterAttached("IsDraggableProperty", typeof(bool), typeof(DragDependencyProperty), new UIPropertyMetadata(new PropertyChangedCallback(PropertyChangedCallBack)));

        private static void PropertyChangedCallBack(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            if (!(bool)e.NewValue) return;

            UIElement element = target as UIElement;
            if (element == null) return;

            DragDependencyProperty escapeStatic = new DragDependencyProperty()
            {
                Element = element
            };
        }



    }
}

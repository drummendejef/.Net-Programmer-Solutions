using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Behavior
{
    class DragControlDependency : DependencyObject
    {
        public static Boolean GetIsDraggable(UIElement target)
        {
            return (Boolean)target.GetValue(DragControlDependency.IsDraggable);
        }

        public static void SetIsDraggable(UIElement target, Boolean value)
        {
            target.SetValue(DragControlDependency.IsDraggable, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDraggable = DependencyProperty.RegisterAttached("IsDraggable", typeof(Boolean), typeof(DragControlDependency), 
            new UIPropertyMetadata(new PropertyChangedCallback(PropertyChangedCallBack)));

        private static void PropertyChangedCallBack(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            if (!(bool)e.NewValue) return;

            UIElement element = target as UIElement;
            if (element == null) return;

            DragControlDependency d = new DragControlDependency()
            {
                Element = element
            };
        }

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

        private Point startPoint = new Point();
        private Boolean clickDrag = false;
        private TranslateTransform _tt = new TranslateTransform();

        private void MouseUpHandler(object sender, RoutedEventArgs e)
        {
            clickDrag = false;
        }

        private void MouseMoveHandler(object sender, RoutedEventArgs e)
        {
            dragBehavior(sender as DependencyObject);
        }

        private void MouseDownHandler(object sender, RoutedEventArgs e)
        {
            UIElement uie = sender as UIElement;
            UIElement parent = VisualTreeHelper.GetParent(sender as DependencyObject) as UIElement;
            clickDrag = true;
            Point pointParent = Mouse.GetPosition(parent);
            if ((startPoint.X == 0.0) && startPoint.Y == 0.0) //dit is de eerste keer dat er geklikt wordt op dit uie
            {
                startPoint.X = pointParent.X;
                startPoint.Y = pointParent.Y;
            }
            uie.RenderTransform = _tt;
        }

        protected virtual void dragBehavior(DependencyObject sender)
        {
            if (clickDrag)
            {
                UIElement parent = VisualTreeHelper.GetParent(sender as DependencyObject) as UIElement;
                Point pointParent = Mouse.GetPosition(parent);

                double deltaX = pointParent.X - startPoint.X;
                double deltaY = pointParent.Y - startPoint.Y;

                _tt.X = deltaX;
                _tt.Y = deltaY;
            }
        }
    }
}

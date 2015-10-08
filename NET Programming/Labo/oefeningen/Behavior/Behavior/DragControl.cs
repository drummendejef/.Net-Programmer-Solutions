using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace Behavior
{
    public  class DragControl : Behavior<UIElement>
    {
        protected Point startPoint = new Point();
        protected Boolean clickDrag = false;
        protected TranslateTransform _tt = new TranslateTransform();

        protected override void OnAttached()
        {
            AssociatedObject.AddHandler(UIElement.MouseDownEvent, new RoutedEventHandler(MouseDownHandler), true);

            AssociatedObject.AddHandler(UIElement.MouseMoveEvent, new RoutedEventHandler(MouseMoveHandler), true);

            AssociatedObject.AddHandler(UIElement.MouseUpEvent, new RoutedEventHandler(MouseUpHandler), true);
        }

        protected virtual void MouseUpHandler(object sender, RoutedEventArgs e)
        {
            clickDrag = false;
        }

        protected virtual void MouseMoveHandler(object sender, RoutedEventArgs e)
        {
            dragBehavior(sender as DependencyObject);
        }

        protected virtual void MouseDownHandler(object sender, RoutedEventArgs e)
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

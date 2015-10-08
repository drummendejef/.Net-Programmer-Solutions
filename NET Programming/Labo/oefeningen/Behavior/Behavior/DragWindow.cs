
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace Behavior
{
    public class DragWindow : DragControl
    {
        protected override void MouseDownHandler(object sender, RoutedEventArgs e)
        {
            Window window = Application.Current.MainWindow;
            UIElement parentWindow = VisualTreeHelper.GetParent(window) as UIElement;
            UIElement uie = sender as UIElement;
            UIElement parent = VisualTreeHelper.GetParent(sender as DependencyObject) as UIElement;
            clickDrag = true;
            Point pointParent = Mouse.GetPosition(window);
            if ((startPoint.X == 0.0) && startPoint.Y == 0.0) //dit is de eerste keer dat er geklikt wordt op dit uie
            {
                startPoint.X = pointParent.X;
                startPoint.Y = pointParent.Y;
            }
            window.RenderTransform = _tt;
        }

        protected override void dragBehavior(DependencyObject sender)
        {
            if (clickDrag)
            {
                Window window = Application.Current.MainWindow;
                Point pointWindow = Mouse.GetPosition(window);
                pointWindow.X -= startPoint.X;
                pointWindow.Y-= startPoint.Y;

                window.Left += pointWindow.X;
                window.Top += pointWindow.Y;
            }
        }
    }
}

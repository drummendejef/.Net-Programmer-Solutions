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

namespace TemplatedControlsWindow
{
    public class DragWindow : Behavior<UIElement>
    {
        public Point startPoint = new Point();
        public Boolean clickDrag = false;
        public TranslateTransform _tt = new TranslateTransform();

        public void onMouseDown(DependencyObject sender)
        {
            Window window = Application.Current.MainWindow;
            UIElement parentWindow = VisualTreeHelper.GetParent(window) as UIElement;
            UIElement uie = sender as UIElement;
            UIElement parent = VisualTreeHelper.GetParent(sender as DependencyObject) as UIElement;
            clickDrag = true;
            Point pointParent = Mouse.GetPosition(window);
            if ((startPoint.X == 0.0) && startPoint.Y == 0.0)
            {
                startPoint.X = pointParent.X;
                startPoint.Y = pointParent.Y;
            }
            window.RenderTransform = _tt;
        }

        public void onMouseUp()
        {
            clickDrag = false;
        }

        public void onMouseMove()
        {
            dragBehavior();
        }

        public void dragBehavior()
        {
            if (clickDrag)
            {
                Window window = Application.Current.MainWindow;
                Point pointWindow = Mouse.GetPosition(window);
                pointWindow.X -= startPoint.X;
                pointWindow.Y -= startPoint.Y;

                window.Left += pointWindow.X;
                window.Top += pointWindow.Y;
            }
        }
    }
}

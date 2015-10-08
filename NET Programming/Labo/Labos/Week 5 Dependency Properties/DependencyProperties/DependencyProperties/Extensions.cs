using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace DependencyProperties
{
    public static class Extensions
    {
        public static void FadeInOut(this Control control)
        {
            DoubleAnimation animation = new DoubleAnimation(0, 1, new Duration(new TimeSpan(0, 0, 1)));
            animation.SetValue(Storyboard.TargetNameProperty, control.Name);
            animation.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath(Control.OpacityProperty));
            animation.RepeatBehavior = RepeatBehavior.Forever;
            animation.AutoReverse = true;

            Storyboard board = new Storyboard();
            board.Children.Add(animation);
            board.Begin(Window.GetWindow(control));
        }
    }
}

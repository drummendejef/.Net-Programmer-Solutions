using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace labo5_oef1
{
    class Animation
    {
        public Storyboard animeerBreedte(Button btnInner)
        {
            DoubleAnimation anWidth = new DoubleAnimation(btnInner.Width, btnInner.Width * 1.2, new Duration(new TimeSpan(0, 0, 1)));
            anWidth.SetValue(Storyboard.TargetNameProperty, btnInner.Name);
            anWidth.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath(Button.WidthProperty));
            anWidth.AutoReverse = true;
            anWidth.RepeatBehavior = RepeatBehavior.Forever;
            Storyboard sb = new Storyboard();
            sb.Children.Add(anWidth);
            sb.Begin(btnInner);
            return sb;
        }

        public Storyboard animeerLengte(Button btnInner)
        {
            DoubleAnimation anHeight = new DoubleAnimation(btnInner.Height, btnInner.Height * 1.2, new Duration(new TimeSpan(0, 0, 1)));
            anHeight.SetValue(Storyboard.TargetNameProperty, btnInner.Name);
            anHeight.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath(Button.HeightProperty));
            anHeight.AutoReverse = true;
            anHeight.RepeatBehavior = RepeatBehavior.Forever;
            Storyboard sb = new Storyboard();
            sb.Children.Add(anHeight);
            sb.Begin(btnInner);
            return sb;
        }

        public Storyboard animeerBackground(Button btnInner)
        {
            ColorAnimation anKleur = new ColorAnimation(Colors.Red, new Duration(new TimeSpan(0, 0, 1)));
            Storyboard.SetTargetName(anKleur, btnInner.Name);
            Storyboard.SetTargetProperty(anKleur, new PropertyPath("(Panel.Background).(SolidColorBrush.Color)"));
            anKleur.AutoReverse = true;
            anKleur.RepeatBehavior = RepeatBehavior.Forever;
            Storyboard sb = new Storyboard();
            sb.Children.Add(anKleur);
            sb.Begin(btnInner);
            return sb;
        }

        public Storyboard animeerRotation(Button btnInner)
        {
            RotateTransform rot = new RotateTransform(90);
            rot.CenterX = btnInner.Width/2;
            rot.CenterY = btnInner.Height/2;
            btnInner.RenderTransform = rot;

            btnInner.RegisterName("myRot", rot);
            DoubleAnimation anAngle = new DoubleAnimation(rot.Angle, rot.Angle + 360, new Duration(new TimeSpan(0, 0, 8)));
            anAngle.SetValue(Storyboard.TargetNameProperty, "myRot");
            anAngle.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath(RotateTransform.AngleProperty));
            anAngle.RepeatBehavior = RepeatBehavior.Forever;
            Storyboard sb = new Storyboard();
            sb.Children.Add(anAngle);
            sb.Begin(btnInner);
            return sb;
        }

        public Storyboard animeerFadeInOut(FrameworkElement control)
        {
            DoubleAnimation fadeInOut = new DoubleAnimation(0, 1, new Duration(new TimeSpan(0, 0, 2)));
            fadeInOut.SetValue(Storyboard.TargetNameProperty, control.Name);
            fadeInOut.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath(Control.OpacityProperty));
            fadeInOut.RepeatBehavior = RepeatBehavior.Forever;
            fadeInOut.AutoReverse = true;

            Storyboard sb = new Storyboard();
            sb.Children.Add(fadeInOut);
            sb.Begin(Window.GetWindow(control));
            return sb;
        }
    }
}

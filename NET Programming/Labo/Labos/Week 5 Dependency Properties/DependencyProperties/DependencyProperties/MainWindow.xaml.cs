using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DependencyProperties
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Animate(AnimateButtonWidth(), AnimateColor(), RotateButton());
            btnAnimate.FadeInOut();
        }

        private void Animate(params Timeline[] animations)
        {
            Storyboard board = new Storyboard();
            
            foreach(Timeline anim in animations)
            {
                board.Children.Add(anim);
                board.Begin(this);
            }
        }

        private DoubleAnimation AnimateButtonWidth()
        {
            DoubleAnimation animation = new DoubleAnimation(btnAnimate.Width, btnAnimate.Width * 2, 
                new Duration(new TimeSpan(0,0,2)));
            animation.SetValue(Storyboard.TargetNameProperty, btnAnimate.Name);
            animation.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath(Button.WidthProperty));
            animation.AutoReverse = true;
            animation.RepeatBehavior = RepeatBehavior.Forever;

            return animation;
        }

        private ColorAnimation AnimateColor()
        {
            ColorAnimation animation = new ColorAnimation(Colors.Blue, Colors.Cyan, new Duration(new TimeSpan(0, 0, 2)));
            //2de wijze om dependencyproperties in te vullen
            Storyboard.SetTargetName(animation, btnAnimate.Name);
            //property path om kleur in te stellen, background property verwacht een brush
            Storyboard.SetTargetProperty(animation, new PropertyPath("(Button.Background).(SolidColorBrush.Color)"));
            animation.AutoReverse = true;
            animation.RepeatBehavior = RepeatBehavior.Forever;

            return animation;
        }

        private DoubleAnimation RotateButton()
        {
            RotateTransform rotation = new RotateTransform(0);
            SkewTransform skew = new SkewTransform(10, 0);
            TransformGroup transforms = new TransformGroup();
            transforms.Children.Add(rotation);
            transforms.Children.Add(skew);
            btnAnimate.RenderTransform = transforms;

            RegisterName("buttonRotation", rotation);

            DoubleAnimation animation = new DoubleAnimation(0, 360, new Duration(new TimeSpan(0,0,5)));
            animation.SetValue(Storyboard.TargetNameProperty, "buttonRotation");
            animation.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath(RotateTransform.AngleProperty));
            animation.RepeatBehavior = RepeatBehavior.Forever;

            return animation;
        }
    }
}

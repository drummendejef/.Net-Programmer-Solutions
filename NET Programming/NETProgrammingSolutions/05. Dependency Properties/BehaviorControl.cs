using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace _05.Dependency_Properties
{
    public class BehaviorControl : Behavior<UIElement>
    {
        public static readonly DependencyProperty URLProperty = DependencyProperty.RegisterAttached("URL", typeof(string), typeof(BehaviorControl), new UIPropertyMetadata(null));
        [System.ComponentModel.Browsable(true)]
        public static string GetURL(UIElement target)
        {
            return (string)target.GetValue(BehaviorControl.URLProperty);
        }

        public static void SetURL(UIElement target, string value)
        {
            target.SetValue(BehaviorControl.URLProperty, value);
        }
        protected override void OnAttached()
        {
            AssociatedObject.AddHandler(UIElement.MouseUpEvent, new RoutedEventHandler(MouseUpHandler), true);
        }

        public void MouseUpHandler(object sender, RoutedEventArgs e)
        {
            UIElement element = (UIElement)sender;
            element.KeyUp += new KeyEventHandler(URLF1_KeyUp);
        }

        void URLF1_KeyUp(object sender, KeyEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;
            if (e.Key == Key.F1)
            {
                if (element == null) return;
                string name = element.Name;
                string url = (string)element.GetValue(BehaviorControl.URLProperty);
                if (url != null)
                {
                    showURL(url, name);
                }
            }
        }
        private void showURL(string URL, string name)
        {
            try
            {
                Process.Start(URL);
            }
            catch (Exception e)
            {

            }
        }
    }
}

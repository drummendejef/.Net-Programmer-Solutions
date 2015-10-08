using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace labo5_oef1
{
    public class F1URL : Grid
    {
        public static readonly DependencyProperty URLProperty = DependencyProperty.RegisterAttached("URL", typeof(string), typeof(F1URL), new UIPropertyMetadata(null));
        [System.ComponentModel.Browsable(true)]
        public static string GetURL(UIElement target)
        {
            return (string)target.GetValue(F1URL.URLProperty);
        }

        public static void SetURL(UIElement target, string value)
        {
            target.SetValue(F1URL.URLProperty, value);
        }

        public F1URL()
            : base()
        {
            this.KeyUp += new KeyEventHandler(URLF1_KeyUp);
        }

        void URLF1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                //verwerkF1(e.OriginalSource as FrameworkElement);
                HitTestResult htr = VisualTreeHelper.HitTest(this, Mouse.GetPosition(this));
                FrameworkElement fwe = htr.VisualHit as FrameworkElement;
                if (fwe != null) verwerkF1(fwe);
                //VisualTreeHelper.HitTest(this, null, new HitTestResultCallback(testHitResult), new PointHitTestParameters(Mouse.GetPosition(this)));
            }
        }

        private HitTestResultBehavior testHitResult(HitTestResult result)
        {
            verwerkF1(VisualTreeHelper.GetParent(result.VisualHit) as FrameworkElement);
            return HitTestResultBehavior.Continue;
        }

        private void verwerkF1(FrameworkElement fwe)
        {
            if (fwe == null) return;
            string naam = fwe.Name;
            string url = (string)fwe.GetValue(F1URL.URLProperty);
            if (url != null)
                toonURL(url, naam);
            else
            {
                FrameworkElement p =VisualTreeHelper.GetParent(fwe) as FrameworkElement;
                if (p == null) p = fwe.Parent as FrameworkElement;
                verwerkF1(p);
            }
        }

        private void toonURL(string URL, string naam)
        {
            try
            {
                Process.Start(URL);
            }catch(Exception e){

            }
        }
    }
}

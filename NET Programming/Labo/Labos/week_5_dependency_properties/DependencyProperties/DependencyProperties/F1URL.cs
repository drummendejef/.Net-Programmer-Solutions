using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Data;
using System.Windows;
using System.ComponentModel;
using System.Windows.Input;

namespace DependencyProperties
{
    class F1URL:Grid
    {
        #region "dependency properties"

        [System.ComponentModel.Browsable(true)]
        public static string GetURL(UIElement target)
        {
            return (string)target.GetValue(F1URL.URLProperty);
        }

        public static void SetURL(UIElement target, string value)
        {
            target.SetValue(F1URL.URLProperty, value);
        }

        // Using a DependencyProperty as the backing store for URL.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty URLProperty =
            DependencyProperty.RegisterAttached("URL", typeof(string), typeof(F1URL), new PropertyMetadata(null));

        #endregion


        public F1URL():base()
        {
            this.KeyUp += F1URL_KeyUp;
        }

        void F1URL_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.F1)
            {
                HitTestResult hit = VisualTreeHelper.HitTest(this, Mouse.GetPosition(this));
                
                if (hit.VisualHit!=null)
                {
                    verwerkF1(hit.VisualHit as FrameworkElement);
                }
                
            }
        }

        //private HitTestResultBehavior HitTestCallback(HitTestResult htrResult)
        //{
        //    IntersectionDetail idDetail = ((GeometryHitTestResult)htrResult).IntersectionDetail;
        //    switch (idDetail)
        //    {
        //        case IntersectionDetail.FullyContains:
        //            m_lstHitList.Add((FrameworkElement)htrResult.VisualHit);
        //            return HitTestResultBehavior.Continue;
        //        case IntersectionDetail.Intersects:
        //            m_lstHitList.Add((FrameworkElement)htrResult.VisualHit);
        //            return HitTestResultBehavior.Continue;
        //        case IntersectionDetail.FullyInside:
        //            m_lstHitList.Add((FrameworkElement)htrResult.VisualHit);
        //            return HitTestResultBehavior.Continue;
        //        default:
        //            return HitTestResultBehavior.Stop;
        //    }
        //}

        //private void verwerkF1(List<FrameworkElement> listFwe)
        //{
        //    foreach (FrameworkElement fwe in listFwe)
        //    { 
        //         if (fwe == null) return;

        //        string naam = fwe.Name;
        //        string url = (string)fwe.GetValue(F1URL.URLProperty);

        //        if (url != null)
        //            toonURL(url, naam);

        //        verwerkF1(VisualTreeHelper.GetParent(fwe) as FrameworkElement);
        //    }
           
        //}

        private void verwerkF1(FrameworkElement fwe)
        {
            if (fwe == null) return;

            string naam = fwe.Name;
            string url = (string)fwe.GetValue(F1URL.URLProperty);

            if (url != null)
                toonURL(url, naam);
            else

            verwerkF1(VisualTreeHelper.GetParent(fwe) as FrameworkElement);
        }

        private void toonURL(string url, string naam)
        {
            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch (Exception)
            {
                MessageBox.Show("Kon data niet openen");
            }
        }
    }
}

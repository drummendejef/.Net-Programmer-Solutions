using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace labo4_oef1
{
    public class Cancelled
    {
        Bord bord = null;
        public Cancelled(Bord bord)
        {
            this.bord = bord;
            cancelTask routine = new cancelTask(checkCanceled);
            routine.BeginInvoke(true, null, null);
        }

        /*public static Boolean GetButton(DependencyObject obj)
        {
            return (Boolean)obj.GetValue(ButtonProperty);
        }

        public static void SetButton(DependencyObject obj, Boolean value)
        {
            obj.SetValue(ButtonProperty, value);
        }

        // Using a DependencyProperty as the backing store for Button.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonProperty =
            DependencyProperty.RegisterAttached("Button", typeof(Boolean), typeof(Cancelled), new UIPropertyMetadata(new PropertyChangedCallback(CanceledCallBack)));

        private static void CanceledCallBack(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            /*Cancelled b = (Cancelled)target;
            b.Canceledthreaded((Boolean)e.NewValue);
        }*/

        public delegate void cancelTask(Boolean value);

        public void checkCanceled(Boolean value)
        {
            bord.canceled = value;
        }

        /*public void Canceledthreaded(Boolean value)
        {
            cancelTask routine = new cancelTask(checkCanceled);
            routine.BeginInvoke(value, null, null);
        }*/
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DependencyProperties
{
    class F1UrlGrid : Grid
    {

        [Browsable(true)]
        public static string GetUrl(UIElement target)
        {
            return (string)target.GetValue(F1UrlProperty);
        }

        public static void SetUrl(UIElement target, string value)
        {
            target.SetValue(F1UrlProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty F1UrlProperty =
            DependencyProperty.Register("F1UrlProperty", typeof(string), typeof(F1UrlGrid), new PropertyMetadata(null));

        
        
        public F1UrlGrid() : base()
        {
            Loaded +=F1UrlGrid_Loaded;
        }

        private void F1UrlGrid_Loaded(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.AddHandler(PreviewKeyUpEvent, new RoutedEventHandler(F1UrlGrid_KeyUp));
        }

        void F1UrlGrid_KeyUp(object sender, RoutedEventArgs e)
        {
            var eventArgs = e as KeyEventArgs;
            if (eventArgs.Key != System.Windows.Input.Key.F1) return;

            VisualTreeHelper.HitTest(this, null, new HitTestResultCallback(TestHitResult), 
                new PointHitTestParameters(Mouse.GetPosition(this)));
        }

        private HitTestResultBehavior TestHitResult(HitTestResult result)
        {
            HandleF1(VisualTreeHelper.GetParent(result.VisualHit) as FrameworkElement);
            return HitTestResultBehavior.Continue;
        }



        //Framework element ipv control door opgave ("debug redenen")
        private void HandleF1(FrameworkElement element)
        {
            if (element == null) return;
            string url = element.GetValue(F1UrlProperty) as string;
            if (url != null)
                ShowUrl(url);

            //mvoe up in visual tree
            HandleF1(element.Parent as FrameworkElement);
        }

        private void ShowUrl(string url)
        {
            try
            {
                //Process start het juiste programma
                System.Diagnostics.Process.Start(url);

            } catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        //[DllImport("user32.dll")]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //internal static extern bool GetCursorPos(ref Win32Point pt);
        //
        //[StructLayout(LayoutKind.Sequential)]
        //internal struct Win32Point
        //{
        //    public Int32 X;
        //    public Int32 Y;
        //};
        //public static Point GetMousePosition()
        //{
        //    Win32Point w32Mouse = new Win32Point();
        //    GetCursorPos(ref w32Mouse);
        //    return new Point(w32Mouse.X, w32Mouse.Y);
        //}
    }
}

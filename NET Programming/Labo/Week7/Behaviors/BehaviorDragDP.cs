using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Behaviors
{
    public class BehaviorDragDP
    {
        private UIElement _el;
        private Window _parent;
        private Boolean _bMoveFlag = false;
        private Point _elementStartPosition;
        private Point _mouseStartPosition;
        public readonly TranslateTransform _tt = new TranslateTransform();

        private static BehaviorDragDP _instance = new BehaviorDragDP();
        public static BehaviorDragDP Instance
        {
            get { return _instance; }
            set { _instance = value; }
        }


        public static bool GetDrag(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsDragProperty);
        }

        public static void SetDrag(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDragProperty, value);
        }

        // Using a DependencyProperty as the backing store for Drag.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDragProperty =
            DependencyProperty.RegisterAttached("Drag", typeof(bool), typeof(BehaviorDragDP), new PropertyMetadata(false, OnDragChanged));


        private static void OnDragChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is UIElement)
            {
                Instance = new BehaviorDragDP();
                Instance._parent = Application.Current.MainWindow;
                Instance._el = (UIElement)sender;
                Instance._el.RenderTransform = Instance._tt;


                if ((bool)(e.NewValue))
                {
                    Instance._el.MouseLeave += Instance.OnMouseLeave;
                    Instance._el.MouseDown += Instance.OnMouseDown;
                    Instance._el.MouseUp += Instance.OnMouseUp;
                    Instance._el.MouseMove += Instance.OnMouseMove;
                }
                else
                {
                    Instance._el.MouseLeave -= Instance.OnMouseLeave;
                    Instance._el.MouseDown -= Instance.OnMouseDown;
                    Instance._el.MouseUp -= Instance.OnMouseUp;
                    Instance._el.MouseMove -= Instance.OnMouseMove;
                }
            }
            else
            {
                throw new Exception("Not an UIElement");
            }
        }

        void OnMouseLeave(object sender, MouseEventArgs e)
        {
            //Console.WriteLine("---------LEFT----------");
            Panel.SetZIndex(_el, 0);
            _bMoveFlag = false;
            _elementStartPosition = _el.TranslatePoint(new Point(), _parent);
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _mouseStartPosition = _el.TranslatePoint(new Point(), _parent);
            _elementStartPosition = e.GetPosition(_parent);
//            _mouseStartPosition = _el.TranslatePoint(new Point(), _parent);
            _bMoveFlag = true;
            Panel.SetZIndex(_el, 1);

            /*
            var mousePos = e.GetPosition(parent);
            var diff = (mousePos - _mouseStartPosition);
            if (!((UIElement)sender).IsMouseCaptured) return;
            _tt.X = _elementStartPosition.X + diff.X;
            _tt.Y = _elementStartPosition.Y + diff.Y;
            Console.WriteLine(_tt.Y.ToString() + " - " + _tt.X.ToString());
            */
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            Vector diff = e.GetPosition(_parent) - _mouseStartPosition;
            if (_bMoveFlag)
            {
                _tt.X = _elementStartPosition.X + diff.X;
                _tt.Y = _elementStartPosition.Y + diff.Y;
            }

            /*
            var parent = Application.Current.MainWindow;
            _mouseStartPosition = e.GetPosition(parent);
            ((UIElement)sender).CaptureMouse();
            */
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            //Console.WriteLine("----------UP---------");
            Panel.SetZIndex(_el, 0);
            _bMoveFlag = false;
            //_elementStartPosition = _el.TranslatePoint(new Point(), _parent);
            /*
            ((UIElement)sender).ReleaseMouseCapture();
            _elementStartPosition.X = _tt.X;
            _elementStartPosition.Y = _tt.Y;
            */
        }

        /*
        void _uie_MouseDown(object sender, MouseButtonEventArgs e) {
            UIElement uie = (UIElement)sender;
            _pntMouseDown = e.GetPosition(_parent);
            if(_pntOriginalLocation.X == 0 & _pntOriginalLocation.Y == 0)
                _pntOriginalLocation = uie.TranslatePoint(new Point(), _parent);
            
        }
         
         
        */
    }
}

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

namespace Behaviors
{
    class BehaviorDrag : Behavior<UIElement>
    {
        private UIElement _el;
        private Window _parent;
        private Boolean _bMoveFlag = false;
        private Point _elementStartPosition;
        private Point _mouseStartPosition;
        private TranslateTransform _tt = new TranslateTransform();

        protected override void OnAttached()
        {
            _parent = Application.Current.MainWindow;
            _el = AssociatedObject;
            _el.RenderTransform = _tt;

            _el.MouseDown += OnMouseDown;
            _el.MouseUp += OnMouseUp;
            _el.MouseMove += OnMouseMove;
            _el.MouseLeave += OnMouseLeave;
        }

        void OnMouseLeave(object sender, MouseEventArgs e)
        {
            _bMoveFlag = false;
        }

        void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _elementStartPosition = _el.TranslatePoint(new Point(), _parent);
            _mouseStartPosition = e.GetPosition(_parent);
            _bMoveFlag = true;
        }

        void OnMouseMove(object sender, MouseEventArgs e)
        {
            Vector diff = e.GetPosition(_parent) - _mouseStartPosition;
            if (_bMoveFlag)
            {
                _tt.X = _elementStartPosition.X + diff.X;
                _tt.Y = _elementStartPosition.Y + diff.Y;
            }
        }

        void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            _bMoveFlag = false;
        }
    }
}

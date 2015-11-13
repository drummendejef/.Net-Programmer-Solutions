using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace Behaviours
{
    class DragBehaviour : Behavior<UIElement>
    {
        protected Point _mouseStartPosition;
        private Point _elementStartTransform = new Point();
        private TranslateTransform _translateTransform = new TranslateTransform();
        protected Window _window = Application.Current.MainWindow;

        protected override void OnAttached()
        {
            
        }

    }
}

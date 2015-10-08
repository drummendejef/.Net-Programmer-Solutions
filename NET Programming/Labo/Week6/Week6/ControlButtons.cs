using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Week6
{
    class Minimize : UIElement
    {
        private Minimize _minimizeElement;
        private Minimize MinimizeElement {
            get {
                return _minimizeElement;
            }
            set {
                _minimizeElement = value;
            }
        }
    }

    class Maximize : UIElement
    {
        private Maximize _maximizeElement;
        private Maximize MaximizeElement
        {
            get
            {
                return _maximizeElement;
            }
            set
            {
                _maximizeElement = value;
            }
        }
    }

    class Close : UIElement
    {
        private Close _closeElement;
        private Close CloseElement
        {
            get
            {
                return _closeElement;
            }
            set
            {
                _closeElement = value;
            }
        }
    }
}


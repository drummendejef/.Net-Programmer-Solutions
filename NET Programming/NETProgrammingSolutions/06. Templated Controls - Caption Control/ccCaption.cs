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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _06.Templated_Controls___Caption_Control
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:_06.Templated_Controls___Caption_Control"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:_06.Templated_Controls___Caption_Control;assembly=_06.Templated_Controls___Caption_Control"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:ccCaption/>
    ///
    /// </summary>
    [TemplatePart(Name = "PART_Title", Type = typeof(TextBlock))]
    [TemplatePart(Name = "PART_Minimize", Type = typeof(UIElement))]
    [TemplatePart(Name = "PART_Maximize", Type = typeof(UIElement))]
    [TemplatePart(Name = "PART_Close", Type = typeof(UIElement))]
    [TemplatePart(Name = "PART_Grab", Type = typeof(UIElement))]
    [TemplateVisualState(GroupName = "WindowMode", Name = "Normal")]
    [TemplateVisualState(GroupName = "WindowMode", Name = "Maximized")]
    public class ccCaption : Control
    {
        private UIElement _grab;
        private DragWindow dragWindow = new DragWindow();
        public UIElement Grab
        {
            get { return _grab; }
            set
            {
                if (value == null) return;

                _grab = value;
                _grab.AddHandler(PreviewMouseDownEvent, new MouseButtonEventHandler(_grab_MouseDown));
                _grab.AddHandler(PreviewMouseUpEvent, new MouseButtonEventHandler(_grab_MouseUp));
                _grab.MouseMove += _grab_MouseMove;
            }
        }
        void _grab_MouseMove(object sender, MouseEventArgs e)
        {
            dragWindow.onMouseDown(sender as DependencyObject);
        }
        void _grab_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Minimized;
        }
        void _grab_MouseUp(object sender, MouseButtonEventArgs e)
        {
            dragWindow.onMouseUp();
        }


        private UIElement _minimize;
        public UIElement Minimize
        {
            get { return _minimize; }
            set
            {
                if (value == null) return;

                _minimize = value;
                _minimize.AddHandler(PreviewMouseUpEvent, new MouseButtonEventHandler(_minimize_MouseUp));
            }
        }
        void _minimize_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Minimized;
        }

        private UIElement _maximize;
        public UIElement Maximize
        {
            get { return _maximize; }
            set
            {
                if (value == null) return;

                _maximize = value;
                _maximize.AddHandler(PreviewMouseUpEvent, new MouseButtonEventHandler(_maximize_MouseUp));

            }
        }
        void _maximize_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Window.GetWindow(this).WindowState == WindowState.Normal)
            {
                Window.GetWindow(this).WindowState = WindowState.Maximized;
                VisualStateManager.GoToState(this, "Maximized", false);
            }
            else
            {
                Window.GetWindow(this).WindowState = WindowState.Normal;
                VisualStateManager.GoToState(this, "Normal", false);
            }

        }

        private UIElement _close;
        public UIElement Close
        {
            get { return _close; }
            set
            {
                if (value == null) return;

                _close = value;
                _close.AddHandler(PreviewMouseUpEvent, new MouseButtonEventHandler(_close_MouseUp));
            }
        }
        void _close_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Window.GetWindow(this).Close();
        }


        static ccCaption()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ccCaption), new FrameworkPropertyMetadata(typeof(ccCaption)));
        }

        public override void OnApplyTemplate()
        {
            Minimize = GetTemplateChild("PART_Minimize") as UIElement;
            Maximize = GetTemplateChild("PART_Maximize") as UIElement;
            Close = GetTemplateChild("PART_Close") as UIElement;
            Grab = GetTemplateChild("PART_Grab") as UIElement;
        }

    }
}

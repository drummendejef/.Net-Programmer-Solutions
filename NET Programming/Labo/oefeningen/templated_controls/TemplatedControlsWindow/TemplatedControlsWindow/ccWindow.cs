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

namespace TemplatedControlsWindow
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:TemplatedControlsWindow"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:TemplatedControlsWindow;assembly=TemplatedControlsWindow"
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
    ///     <MyNamespace:ccWindow/>
    ///
    /// </summary>
    [TemplatePart(Name = "PART_MIN", Type = typeof(UIElement))]
    [TemplatePart(Name = "PART_MAX", Type = typeof(UIElement))]
    [TemplatePart(Name = "PART_CLOSE", Type = typeof(UIElement))]
    [TemplatePart(Name = "PART_GRIP", Type = typeof(UIElement))]
    public class ccWindow : Control
    {
        private UIElement buttonMin;
        private UIElement buttonMax;
        private UIElement buttonClose;
        private UIElement buttonGrip;
        private DragWindow dragWindow = new DragWindow();
        static ccWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ccWindow), new FrameworkPropertyMetadata(typeof(ccWindow)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            buttonMin = GetTemplateChild("PART_MIN") as UIElement;
            buttonMax = GetTemplateChild("PART_MAX") as UIElement;
            buttonClose = GetTemplateChild("PART_CLOSE") as UIElement;
            buttonGrip = GetTemplateChild("PART_GRIP") as UIElement;

            if (buttonMin != null) buttonMin.MouseUp += buttonMin_MouseUp;
            if (buttonMax != null) buttonMax.MouseUp += buttonMax_MouseUp;
            if (buttonClose != null) buttonClose.MouseUp += buttonClose_MouseUp;
            if (buttonGrip != null)
            {
                buttonGrip.MouseDown += buttonGrip_MouseDown;
                buttonGrip.MouseUp += buttonGrip_MouseUp;
                buttonGrip.MouseMove += buttonGrip_MouseMove;
            }
        }

        void buttonGrip_MouseMove(object sender, MouseEventArgs e)
        {
            dragWindow.onMouseMove();
        }

        void buttonGrip_MouseUp(object sender, MouseButtonEventArgs e)
        {
            dragWindow.onMouseUp();
        }

        void buttonGrip_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dragWindow.onMouseDown(sender as DependencyObject);
        }

        void buttonClose_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        void buttonMax_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Maximized;
        }

        void buttonMin_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Minimized;
        }
    }
}

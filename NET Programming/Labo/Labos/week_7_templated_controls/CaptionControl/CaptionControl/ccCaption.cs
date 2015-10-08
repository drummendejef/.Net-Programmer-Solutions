using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CaptionControl
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:CaptionControl"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:CaptionControl;assembly=CaptionControl"
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
    [TemplatePart(Name = "PART_Minimize", Type = typeof(UIElement))]
    [TemplatePart(Name = "PART_Maximize", Type = typeof(UIElement))]
    [TemplatePart(Name = "PART_Close", Type = typeof(UIElement))]
    public class ccCaption : Control
    {
        static ccCaption()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ccCaption), new FrameworkPropertyMetadata(typeof(ccCaption)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            UIElement minimize = GetTemplateChild("PART_Minimize") as UIElement;
            if (minimize != null) minimize.AddHandler(ButtonBase.ClickEvent,(RoutedEventHandler) icon_MouseUp, true);

            UIElement maximize = GetTemplateChild("PART_Maximize") as UIElement;
            if (maximize != null) maximize.MouseUp += icon_MouseUp;

            UIElement close = GetTemplateChild("PART_Close") as UIElement;
            if (close != null) close.MouseUp += icon_MouseUp;
        }

        void icon_MouseUp(object sender, EventArgs e)
        {
            FrameworkElement iconClicked = sender as FrameworkElement;

            switch (iconClicked.Name)
            { 
                case "PART_Minimize":
                    //minimize
                    MinimizeWindow(iconClicked);
                    break;
                case "PART_Maximize":
                    //maximize
                    ToggleMaximizeWindow(iconClicked);
                    break;
                case "PART_Close":
                    //close
                    CloseWindow(iconClicked);
                    break;

            }
        }

        private void CloseWindow(FrameworkElement iconClicked)
        {
            MainWindow mainWindow = GetParentWindow(iconClicked);

            if (mainWindow != null)
                mainWindow.Close();
        }

        private void ToggleMaximizeWindow(FrameworkElement iconClicked)
        {
            MainWindow mainWindow = GetParentWindow(iconClicked);

            if (mainWindow != null)
            {
                if (mainWindow.WindowState == WindowState.Normal)
                    mainWindow.WindowState = WindowState.Maximized;
                else
                    mainWindow.WindowState = WindowState.Normal;
            }
                

        }

        private void MinimizeWindow(FrameworkElement iconClicked)
        {
            MainWindow mainWindow = GetParentWindow(iconClicked);

            if (mainWindow != null)
                mainWindow.WindowState = WindowState.Minimized;
        }

        private MainWindow GetParentWindow(FrameworkElement uiElement)
        {
            FrameworkElement parentUIElement = (FrameworkElement)VisualTreeHelper.GetParent(uiElement);

            if (parentUIElement.GetType().Equals(typeof(MainWindow)))
            {
                return parentUIElement as MainWindow;
            }
            else
            {
                return GetParentWindow(parentUIElement);
            }
        }
    }
}

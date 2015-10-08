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

namespace TemplatedControls
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:TemplatedControls"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:TemplatedControls;assembly=TemplatedControls"
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
    ///     <MyNamespace:verkeerslicht/>
    ///
    /// </summary>
    [TemplatePart(Name = "PART_RED", Type = typeof(UIElement))]
    [TemplatePart(Name = "PART_ORANGE", Type = typeof(UIElement))]
    [TemplatePart(Name = "PART_GREEN", Type = typeof(UIElement))]
    public class ccverkeerslicht : Control
    {
        private UIElement verkeerslichtRood;
        private UIElement verkeerslichtOrange;
        private UIElement verkeerslichtGreen;
        static ccverkeerslicht()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ccverkeerslicht), new FrameworkPropertyMetadata(typeof(ccverkeerslicht)));
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            verkeerslichtRood = GetTemplateChild("PART_RED") as UIElement;
            verkeerslichtOrange = GetTemplateChild("PART_ORANGE") as UIElement;
            verkeerslichtGreen = GetTemplateChild("PART_GREEN") as UIElement;
            if (verkeerslichtRood!=null) verkeerslichtRood.MouseUp += verkeerslichtRood_MouseUp;
            if (verkeerslichtOrange != null) verkeerslichtOrange.MouseUp += verkeerslichtOrange_MouseUp;
            if (verkeerslichtGreen != null) verkeerslichtGreen.MouseUp += verkeerslichtGreen_MouseUp;
        }

        void verkeerslichtGreen_MouseUp(object sender, MouseButtonEventArgs e)
        {
            VisualStateManager.GoToState(this, "STATE_GREEN",true);
        }

        void verkeerslichtOrange_MouseUp(object sender, MouseButtonEventArgs e)
        {
            VisualStateManager.GoToState(this, "STATE_ORANGE", true);
        }

        void verkeerslichtRood_MouseUp(object sender, MouseButtonEventArgs e)
        {
            VisualStateManager.GoToState(this, "STATE_RED", true);
        }

    }
}

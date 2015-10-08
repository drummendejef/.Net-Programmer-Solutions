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

namespace _06.Templated_Controls___States
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:_06.Templated_Controls___States"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:_06.Templated_Controls___States;assembly=_06.Templated_Controls___States"
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
    ///     <MyNamespace:ccTrafficLight/>
    ///
    /// </summary>

    [TemplateVisualState(Name = "Green", GroupName = "Light")]
    [TemplateVisualState(Name = "Yellow", GroupName = "Light")]
    [TemplateVisualState(Name = "Red", GroupName = "Light")]
    [TemplatePart(Name = "PART_Greenlight", Type = typeof(Shape))]
    [TemplatePart(Name = "PART_Yellowlight", Type = typeof(Shape))]
    [TemplatePart(Name = "PART_Redlight", Type = typeof(Shape))]

    public class ccTrafficLight : Control
    {
        private Shape _greenLight;

        public Shape GreenLight
        {
            get { return _greenLight; }
            set
            {
                if (value == null) return;

                _greenLight = value;
                _greenLight.MouseUp += _greenLight_MouseUp;
            }
        }

        void _greenLight_MouseUp(object sender, MouseButtonEventArgs e)
        {
            VisualStateManager.GoToState(this, "Green", true);
        }

        private Shape _yellowLight;

        public Shape YellowLight
        {
            get { return _yellowLight; }
            set
            {
                if (value == null) return;

                _yellowLight = value;
                _yellowLight.MouseUp += _yellowLight_MouseUp;
            }
        }

        void _yellowLight_MouseUp(object sender, MouseButtonEventArgs e)
        {
            VisualStateManager.GoToState(this, "Yellow", true);
        }

        private Shape _redLight;

        public Shape RedLight
        {
            get { return _redLight; }
            set
            {
                if (value == null) return;

                _redLight = value;
                _redLight.MouseUp += _redLight_MouseUp;
            }
        }

        void _redLight_MouseUp(object sender, MouseButtonEventArgs e)
        {
            VisualStateManager.GoToState(this, "Red", true);
        }


        static ccTrafficLight()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ccTrafficLight), new FrameworkPropertyMetadata(typeof(ccTrafficLight)));
        }

        public override void OnApplyTemplate()
        {
            GreenLight = GetTemplateChild("PART_Greenlight") as Shape;
            YellowLight = GetTemplateChild("PART_Yellowlight") as Shape;
            RedLight = GetTemplateChild("PART_Redlight") as Shape;
        }
    }
}

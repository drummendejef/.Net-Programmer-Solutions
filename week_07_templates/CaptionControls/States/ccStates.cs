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

namespace States
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:States"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:States;assembly=States"
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
    ///     <MyNamespace:ccStates/>
    ///
    /// </summary>
    [TemplateVisualState(Name ="Groen",GroupName ="Licht")]
    [TemplateVisualState(Name = "Yellow", GroupName = "Light")]
    [TemplateVisualState(Name = "Red", GroupName = "Light")]
    public class ccStates : Control
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

        static ccStates()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ccStates), new FrameworkPropertyMetadata(typeof(ccStates)));
        }
    }
}

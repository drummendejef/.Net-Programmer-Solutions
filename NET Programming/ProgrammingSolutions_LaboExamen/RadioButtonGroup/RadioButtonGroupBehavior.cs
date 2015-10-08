using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace RadioButtonGroup
{
    class RadioButtonGroupBehavior : Behavior<UIElement>
    {
        protected Window _window = Application.Current.MainWindow;

        public RadioButtonGroupBehavior() : base() { }

        protected override void OnAttached()
        {
            base.OnAttached();

            RadioButton isRadioButton = AssociatedObject as RadioButton;
            if (isRadioButton == null)
            {
                // not RadioButton
                throw new Exception("RadioButtonGroupBehaviour can only be set on an object of the RadioButton class, not on " + AssociatedObject.GetType());
            }
            isRadioButton.GroupName = this.InGroup;
        }

        public static readonly DependencyProperty InGroupProperty =
        DependencyProperty.RegisterAttached(
            "InGroup",
            typeof(string),
            typeof(RadioButtonGroupBehavior),
            //new PropertyMetadata(null, InGroup)
            new FrameworkPropertyMetadata(string.Empty)
        );

        public string InGroup
        {
            get { return (string)base.GetValue(InGroupProperty); }
            set { base.SetValue(InGroupProperty, value); }
        }
    }
}

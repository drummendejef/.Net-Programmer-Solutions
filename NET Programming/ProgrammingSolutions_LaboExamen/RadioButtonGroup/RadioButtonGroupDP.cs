using System;
using System.Windows;
using System.Windows.Controls;

namespace RadioButtonGroup
{
    class RadioButtonGroupDP //: DependencyObject
    {
        public string MyGroup { get; set; }

        public static readonly DependencyProperty InGroupProperty =
        DependencyProperty.RegisterAttached(
            "InGroup",
            typeof(string), 
            typeof(RadioButtonGroupDP), 
            new PropertyMetadata(null, InGroup)
        );
        public static void SetInGroup(DependencyObject dp, string value)
        {
            dp.SetValue(InGroupProperty, value);
        }
        public static string GetInGroup(DependencyObject dp)
        {
            return (string)dp.GetValue(InGroupProperty);
        }
        private static void InGroup(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb == null)
            {
                // not RadioButton
                throw new Exception("RadioButtonGroupDP can only be set on an object of the RadioButton class, not on " + sender.GetType());
                //return;
            }
            string newValue = (string)e.NewValue;
            if (newValue == "" || newValue == "abc")
            {
                //new Exception("RadioButtonGroupDP.Group has to have a value different from \"\" and \"abc\"");
                return;
            }
            //set radio group
            //rb.GroupName = newValue;

            RadioButtonGroupDP escapeStatic = new RadioButtonGroupDP()
            {
                MyGroup = newValue
            };
            rb.Checked += rb_Checked;
        }

        static void rb_Checked(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(sender);
        }
    }
}

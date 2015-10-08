using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Week1
{
    public class DemoTextBox : TextBox
    {
        //build, toolbox: sleep eigen textbox naar window
        public event EventHandler TextChangedToEmpty; //eventdefinition
        public event EventHandler TextChangedToFilled; //eventdefinition
        public DemoTextBox()
        {
            this.TextChanged += new TextChangedEventHandler(TextBoxEvent_TextChanged);
        }

        void TextBoxEvent_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Console.WriteLine("DemoTextBox_TextChanged");
            if (this.Text.Length == 0)
            {
                onTextChangedToEmpty();
            }
            else
            {
                onTextChangedToFilled();
            }
        }

        protected virtual void onTextChangedToEmpty()
        {
            if (TextChangedToEmpty != null) TextChangedToEmpty(this, EventArgs.Empty);
        }

        protected virtual void onTextChangedToFilled()
        {
            if (TextChangedToFilled != null) TextChangedToFilled(this, EventArgs.Empty);
        }
    }
}
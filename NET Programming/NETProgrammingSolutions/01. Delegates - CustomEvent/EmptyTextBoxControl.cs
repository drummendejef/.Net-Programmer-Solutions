using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace _01.Delegates___CustomEvent
{
    class EmptyTextBoxControl : TextBox
    {
        public event EventHandler TextChangedToEmpty; //eventdefinition
        public event EventHandler TextChangedToFilled; //eventdefinition
        public EmptyTextBoxControl()
        {
            this.TextChanged += new TextChangedEventHandler(EmptyTextBoxEvent_TextChanged);
        }

        void EmptyTextBoxEvent_TextChanged(object sender, TextChangedEventArgs e)
        {
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
            if (TextChangedToEmpty != null)
            {
                TextChangedToEmpty(this, EventArgs.Empty);
            }
        }

        protected virtual void onTextChangedToFilled()
        {
            if (TextChangedToFilled != null)
            {
                TextChangedToFilled(this, EventArgs.Empty);
            }
        }
    }
}

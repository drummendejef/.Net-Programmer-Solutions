using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TextBoxDelegate
{
    //public delegate void TextBoxEmptyHandler(object sender, EventArgs e);
    class TextBoxExtra:TextBox
    {
        public event EventHandler TextBoxEmptyEventHandler;

        public TextBoxExtra()
        {
            this.TextChanged += TextBoxExtra_TextChanged;
        }

        protected virtual void TextBoxExtra_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.Text.Length == 0)
                OnTextBoxEmpty();
        }

        protected void OnTextBoxEmpty()
        {
            if (TextBoxEmptyEventHandler != null)
                TextBoxEmptyEventHandler(this, EventArgs.Empty);
        }

    }
}

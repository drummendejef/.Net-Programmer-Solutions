using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomEvent {
    public delegate void EmptiedTextBox(object sender, EventArgs e);

    class CustomTextBox : TextBox {
        public event EmptiedTextBox EmptiedTextBoxEvent;

        public CustomTextBox() {
            base.TextChanged += CustomTextBox_TextChanged;
        }

        void CustomTextBox_TextChanged(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(Text))
                if (EmptiedTextBoxEvent != null)
                    EmptiedTextBoxEvent(this, EventArgs.Empty);
        }
    }
}

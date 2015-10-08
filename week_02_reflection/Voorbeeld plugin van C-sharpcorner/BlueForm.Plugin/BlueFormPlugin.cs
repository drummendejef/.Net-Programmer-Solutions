using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plugin.Interface;
using System.Windows.Forms;
using System.Drawing;

namespace BlueForm.Plugin
{
    public class BlueFormPlugin : IPlugin
    {
        public string Text
        {
            get { return "A Blue Form"; }
        }

        public Form Form
        {
            get { return new Form() { Text = this.Text, BackColor = Color.Blue }; }
        }

        public Color BackColor
        {
            get { return Color.Blue; }
        }
    }
}

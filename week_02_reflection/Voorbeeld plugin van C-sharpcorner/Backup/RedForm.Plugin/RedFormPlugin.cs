using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plugin.Interface;
using System.Windows.Forms;
using System.Drawing;

namespace BlueForm.Plugin
{
    public class RedFormPlugin : IPlugin
    {
        public string Text
        {
            get { return "A Red Form"; }
        }

        public Form Form
        {
            get { return new Form() { Text = this.Text, BackColor = Color.Red }; }
        }

        public Color BackColor
        {
            get { return Color.Red; }
        }
    }
}

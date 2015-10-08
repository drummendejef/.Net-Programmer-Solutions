using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomEvent {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            CustomTextBox textBox = new CustomTextBox();
            this.Controls.Add(textBox);
            textBox.EmptiedTextBoxEvent += textBox_EmptiedTextBoxEvent;
        }

        void textBox_EmptiedTextBoxEvent(object sender, EventArgs e) {
            MessageBox.Show("empty!");
        }
    }
}

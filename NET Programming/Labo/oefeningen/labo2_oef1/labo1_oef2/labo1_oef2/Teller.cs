using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace labo1_oef2
{
    class Teller : TextBox
    {
        public int teller = 0;
        public Boolean isEnable = false;
        public event EventHandler Optellen;
        public Teller()
        {
            this.TextChanged += Teller_TextChanged;
        }

        private void Teller_TextChanged(object sender, TextChangedEventArgs e)
        {
            OnClick();
        }

        protected virtual void OnClick()
        {
            if (Optellen != null)
            {
                TellerArg a = new TellerArg(teller);
                Optellen(this, a);
            }
        }
    }
}

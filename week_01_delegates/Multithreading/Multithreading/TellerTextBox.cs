using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Multithreading
{
    public delegate void ToonTeller(object sender, EventArgs args);
    public class TellerTextBox : TextBox
    {
        public void StartTeller()
        {
            Action teller = Teller;
            teller.BeginInvoke(null, null);
        }

        private void Teller()
        {
            int i = 0;

            while(true)
            {
                i++;


            }

        }
    }
}

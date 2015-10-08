using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace labo1_oef2Versie3
{
    class Teller : TextBox
    {
        public int _teller = 0;
        public delegate void toonTeller();

        public Teller()
        {

        }

        public void TellerStarten(Boolean threaded)
        {
            toonTeller routine = new toonTeller(incrementTeller);
            if (threaded) routine.BeginInvoke(null, null); //start, uitvoer als start is uitgevoerd
            else routine.Invoke();
        }

        private void toonTekst()
        {
            this.Text = _teller.ToString();
        }

        private void incrementTeller()
        {
            int current = 0;
            while (true)
            {
                current++;
                if (current == 10)
                {
                    current = 0;
                    _teller++;
                    Dispatcher.Invoke(toonTekst);
                }
                Thread.Sleep(10);
            }
        }
    }
}

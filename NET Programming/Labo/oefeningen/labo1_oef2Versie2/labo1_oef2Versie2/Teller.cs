using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace labo1_oef2Versie2
{
    class Teller : TextBox
    {
        public int _teller = 0;
        //public event EventHandler Optellen;
        public Boolean _threaded = false;
        //public DispatcherTimer timer = new DispatcherTimer();
        public Teller()
        {
            //timer.Tick += new EventHandler(timer_Tick);
            //timer.Interval = new TimeSpan(0, 0, 0,0,100);
        }

        //private void timer_Tick(object sender, EventArgs e)
        //{
        //    if (threaded) OnTick();
        //    else OnTickNotThreaded();
        //    this.Text = teller.ToString();
        //}

        //private void OnTickNotThreaded()
        //{
        //    teller++;
        //}

        //protected virtual void OnTick()
        //{
        //    if (Optellen != null)
        //    {
        //        TellerArg a = new TellerArg(teller);
        //        Optellen(this, a);
        //    }
        //}

        public void TellerStarten(Boolean threaded)
        {
            _threaded = threaded;
            //this.timer.Start();
            //this.Optellen += txt_Optellen1;

            //alisio,
            //u moet een routine maken die een teller in een oneindige lus met 1 verhoogt (while)
            //als je 100 000 bereikt zet je hem terug op 0
            //als je 10 bereikt moet je de text property aanpassen zodat de UI het bereikte getal kan tonen
            //deze routine moet ofwel syncrhoon ofwel asynchroon worden opgeroepen, afhankelijk van de threaded property

            incrementTeller();
        }

        private void incrementTeller()
        {
            int current = 0;
            while (true)
            {
                this.Text = current.ToString();
                _teller++;
                if (_teller >= 100.000) _teller = 0;
                current++;
            }
        }


        //methode die de teller verhoogt en vraagt om de ui aan te passen
        //UI aanpassen moet altijd! op de UI thread  => een extra routine: updatetekst die u altijd op de UI thread moet uitvoeren en de text property instelt
        //u moet die starten via de dispatcher property zodat u zeker bent dat u op de ui thread terecht komt
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Timers
{
    public delegate void StartTimerHandler();
    public delegate void ToonTekstDelegate(int i);
    public class TimerBox : TextBox
    {

        public void OnStartTimer(Boolean threaded)
        {
            //al dan niet a- synchroon starten, op basis van de actuele parameterwaarde

            StartTimerHandler startTimerHandler;
            startTimerHandler = new StartTimerHandler(StartTimerIntern);

            if (threaded)
            {
                //Asynchroon
                //Dispatcher.BeginInvoke(startTimerHandler);
                //  OF   Dispatcher.BeginInvoke(new AsyncCallback(StartTimerIntern), null);     
                startTimerHandler.BeginInvoke(null, null);



                //Meneer///////////////////////////////////////////////////////////////////////////////

                //Ik probeer om de timer asynchroon te starten (met en zonder dispatcher zoals in de 1e bovenstaande lijn code)
                //Met de dispatcher: de debugger overloopt de code 'zoals het moet', 
                //maar de code van de nieuwe thread wordt overlopen door de main thread na dat de main thread klaar is met zijn code
                //(dus niet door een nieuwe thread zoals het moet) waardoor het programma vastloop alsof alles synchroon gebeurt

                //de andere 2 lijnen code in commentaar geven errors

            }
            else
            {
                //Synchroon, loopt vast (zoals het moet)
                startTimerHandler.Invoke();
            }
        }

        private void StartTimerIntern()
        {
            ToonTekstDelegate ttdel = new ToonTekstDelegate(ToonTekst);
            //indien deze methode async op een niet ui-thread wordt uitgevoerd, dan kan u de Text property NIET wijzigen op deze thread
            //dispatcher gebruiken om terug te keren
            //indien u op de UI thread zit dan kan u ook de dispatcher gebruiken, dan is het netto effect gewoon 0
            //dus: altijd de dispatcher gebruiken

            int teller = 3;
            while (teller > 0)
            {
                if (teller > 10000)
                    teller = 3;

                if (teller % 20 == 0)
                    Dispatcher.Invoke(ttdel, DispatcherPriority.Background, new object[] { teller });
                teller++;

                Thread.Sleep(0);
            }
        }

        //private void StartTimerIntern(IAsyncResult iar)
        //{

        //    int teller = 3;
        //    while (teller > 0)
        //    {
        //        if (teller > 10000)
        //            teller = 3;

        //        this.Text = "" + teller;
        //        teller++;
        //    }
        //}


        private void ToonTekst(int teller)
        {
            this.Text = "" + teller;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MultithreadingDelegates
{
    public delegate void Counting();

    class CounterControl : TextBox
    {
        private int _counter;
        private Counting _countingDelegate;

        public CounterControl()
        {
            //_countingDelegate = new Counting(Count);

            _countingDelegate =
                () =>
                {
                    //lokale variabelen gedeclareerd binnen of buiten een delegate en gebruikt
                    //in de delegate worden fields van de delegate ipv lokale variabelen
                    int counter = 0;
                    while (true)
                    {
                        counter++;
                        if (counter == 10000) counter = 0;
                        //moet op background priority, anders komt gui niet aan bod
                        Dispatcher.Invoke(new Action<int>(UpdateText), System.Windows.Threading.DispatcherPriority.Background, counter);
                    }
                };
        }

        public void StartCounting()
        {
            _countingDelegate.BeginInvoke(null, null);
        }

        /*
        private void Count() {
            while (true) {
                _counter++;
                if (_counter == 10000) _counter = 0;
                //moet op background priority, anders komt gui niet aan bod
                Dispatcher.Invoke(new Action(UpdateText), System.Windows.Threading.DispatcherPriority.Background);
            }
        }
         */

        private void UpdateText(int count)
        {
            //Text = _counter.ToString();
            Text = count.ToString();
        }

    }
}

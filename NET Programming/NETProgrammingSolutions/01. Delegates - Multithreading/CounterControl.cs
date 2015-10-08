using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace _01.Delegates___Multithreading
{
    //delegate
    public delegate void Counter();

    class CounterControl : TextBox
    {
        //delegate
        private Counter _counterDelegate;

        public CounterControl() { }

        public void CounterStart(Boolean threaded)
        {
            _counterDelegate = new Counter(incrementCounter);
            if (threaded)
            {
                //start, execute when started
                _counterDelegate.BeginInvoke(null, null);
            }
            else
            {
                _counterDelegate.Invoke();
            }
        }
        private void incrementCounter()
        {
            int counter = 0;
            while (true)
            {
                counter++;
                if (counter == 10000) counter = 0;
                //moet op background priority, anders komt gui niet aan bod
                Dispatcher.Invoke(new Action<int>(UpdateText), System.Windows.Threading.DispatcherPriority.Background, counter);
            }
        }

        private void UpdateText(int count)
        {
            this.Text = count.ToString();
        }
    }
}

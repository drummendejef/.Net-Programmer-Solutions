using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Multithreading
{
    class TellerTextBox : TextBox
    {
        public event EventHandler AddToNumber; //eventdefinition

        private bool _bThreaded = false;
        public bool bThreaded { get; set; }
        private bool _bRunning = false;
        public bool Running { get; set; }
        private int _iTeller = 0;

        public TellerTextBox() {
            this.IsReadOnly = true;
        }
        
        public void StartTeller()
        {
            this.ChangeRunning();
            if (AddToNumber != null)
            {
                if (_bThreaded)
                {
                    AddToNumber.BeginInvoke(0, null, null, null);
                }
                else
                {
                    AddToNumber.Invoke(0, null);
                }
            }
        }

        public void Counter()
        {
            while (_bRunning)
            {
                _iTeller++;
                if (_iTeller == 10000)
                {
                    _iTeller = 0;
                }
                //Console.WriteLine(_iTeller);
                //background priority
                Dispatcher.Invoke(new Action<int>(UpdateText), System.Windows.Threading.DispatcherPriority.Background, _iTeller);
            }
        }

        private void UpdateText(int count)
        {
            this.Text = count.ToString();
        }

        public void ChangeThreading()
        {
            _bThreaded = !_bThreaded;
        }
        public void ChangeRunning()
        {
            _bRunning = !_bRunning;
            this.Background = (_bRunning) ? Brushes.GreenYellow : Brushes.White;
            //this.Text = "running is: " + _bRunning;
        }
    }
}

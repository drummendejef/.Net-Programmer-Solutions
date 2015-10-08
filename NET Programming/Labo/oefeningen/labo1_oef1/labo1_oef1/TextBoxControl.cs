using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace labo1_oef1
{
    //public delegate void ActieOpTxtBoxLeeg(string tekst);
    class TextBoxControl : TextBox
    {
        //public static event EventHandler<TextBox> events;
        public event EventHandler IkBenLeeg;//publieke var waar alle instanties aan kunnen

        public TextBoxControl()
        {
            //de textchanged van standaardcontrol van deze klasse moet elker keer oproepen wanneer mijn controle een textchaged uitvoert
            this.TextChanged += TextBoxControl_TextChanged;
        }

        void TextBoxControl_TextChanged(object sender, TextChangedEventArgs e)
        {
            //wanneer een coditie voltooid is methode uitvoeren
            if (this.Text == "")
                OnIkBenLeeg();
        }

        protected virtual void OnIkBenLeeg()
        {
            //wanneer object bestaat de event(globale var) met par zijn eigen klasse en de info die je wil meegeven ontrend de event
            if (IkBenLeeg != null) IkBenLeeg(this, EventArgs.Empty);
        }

    }
}

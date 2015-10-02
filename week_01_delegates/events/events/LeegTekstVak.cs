using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace events
{
    public delegate void EventTest(object sender, EventArgs args);//Delegate voor het event
    public class LeegTekstVak : TextBox//Public maken, moet openbaar zijn voor overerving
    {
        public event EventTest TekstisNul;//eventdefinitie
        public LeegTekstVak()
        {
            this.TextChanged += new TextChangedEventHandler(LeegTekstVak_TextChanged);
        }

        void LeegTekstVak_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.Text.Length == 0)
                if (TekstisNul != null) //Minstens 1 client voor dit event nodig, anders null
                    Console.WriteLine("Vak is leeg");
                        //MessageBox.Show("Hoi");
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Universal
{
    public class Item : ListBoxItem
    {     
        public Item(int value)
        {
            this.Content = value;
        }

        public Item(string value)
        {
            this.Content = value;
        }

        public override string ToString()
        {
            return this.Content.ToString();
        }
    }
}

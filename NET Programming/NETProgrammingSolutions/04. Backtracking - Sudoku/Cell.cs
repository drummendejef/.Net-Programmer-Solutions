using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace _04.Backtracking___Sudoku
{
    public class Cell : TextBox
    {
        public int Value { get; set; }
        public Point Position { get; set; }
        public int Square { get; set; }

        public Cell()
        {
            BorderThickness = new Thickness(1);
            BorderBrush = new SolidColorBrush(Colors.Black);
            Padding = new Thickness(6);
            TextAlignment = System.Windows.TextAlignment.Center;
            KeyDown += Cell_KeyDown;
            KeyUp += Cell_KeyUp;
            MaxLength = 1;
        }

        void Cell_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //CheckValue(sender as Cell);
        }

        private void CheckValue(Cell cell)
        {
            int userValue;
        }

        void Cell_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            string keySting = e.Key.ToString();
            if (!char.IsDigit(keySting.Substring(keySting.Length - 1, 1)[0]))
                e.Handled = true;
        }

        public void WriteValue(bool isTask)
        {
            if (Value == 0)
                return;
            //don't overwrite the existing task
            if (IsReadOnly)
                return;
            if (isTask)
            {
                IsReadOnly = true;
                FontWeight = FontWeights.Bold;
                Foreground = new SolidColorBrush(Colors.Blue);
            }
            Text = Value.ToString();
        }

        public void ClearCell()
        {
            IsReadOnly = false;
            FontWeight = FontWeights.Normal;
            Foreground = new SolidColorBrush(Colors.Black);
            Text = "";
        }
    }
}

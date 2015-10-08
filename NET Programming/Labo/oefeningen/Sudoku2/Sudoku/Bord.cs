using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Sudoku
{
    class Bord : Grid
    {
        private int[,] _bord;
        private Boolean won = false;
        private int size = 9;
        private double sizeTile = 50;

        public void SetBord()
        {
            won = false;
            _bord = new int[size, size];
            this.Loaded += Bord_Loaded;
        }

        void Bord_Loaded(object sender, RoutedEventArgs e)
        {
            DrawGrid();
            test();
        }

        private void test()
        {
            ClearBord();
            UpdateGrid(0, 2, 3, new SolidColorBrush(Colors.Blue));
            UpdateGrid(0, 5, 5, new SolidColorBrush(Colors.Blue));
            UpdateGrid(1, 2, 6, new SolidColorBrush(Colors.Blue));
            UpdateGrid(1, 5, 9, new SolidColorBrush(Colors.Blue));
            UpdateGrid(3, 2, 5, new SolidColorBrush(Colors.Blue));
            UpdateGrid(3, 4, 1, new SolidColorBrush(Colors.Blue));
            UpdateGrid(4, 0, 3, new SolidColorBrush(Colors.Blue));
            UpdateGrid(4, 6, 5, new SolidColorBrush(Colors.Blue));
            UpdateGrid(4, 7, 6, new SolidColorBrush(Colors.Blue));
            UpdateGrid(4, 8, 4, new SolidColorBrush(Colors.Blue));
            UpdateGrid(5, 1, 9, new SolidColorBrush(Colors.Blue));
            UpdateGrid(5, 2, 8, new SolidColorBrush(Colors.Blue));
            UpdateGrid(6, 3, 8, new SolidColorBrush(Colors.Blue));
            UpdateGrid(6, 4, 6, new SolidColorBrush(Colors.Blue));
            UpdateGrid(6, 5, 2, new SolidColorBrush(Colors.Blue));
            UpdateGrid(6, 7, 4, new SolidColorBrush(Colors.Blue));
            UpdateGrid(7, 3, 9, new SolidColorBrush(Colors.Blue));
            UpdateGrid(8, 0, 9, new SolidColorBrush(Colors.Blue));
            UpdateGrid(8, 2, 4, new SolidColorBrush(Colors.Blue));
            UpdateGrid(8, 7, 8, new SolidColorBrush(Colors.Blue));
        }

        public void SolveSudoku()
        {
            won = SolveSudokuAlgoritme();
        }

        //private bool SolveSudoku(int r, int c)
        //{
        //    if (r >= size) return true;
        //    for (int getal = 1; getal < 10; getal++)
        //    {
        //        if (NumberOK(r, c, getal))
        //        {
        //            if (_bord[r, c] == 0)
        //            {
        //                UpdateGrid(r, c, getal, new SolidColorBrush(Colors.Black));
        //            }
        //            /*if (c < size-1) SolveSudoku(r, c + 1);
        //            else SolveSudoku(r + 1, 0);*/

        //            if (c < size - 1)
        //            {
        //                if (SolveSudoku(r, c + 1)) return true;
        //            }
        //            else
        //            {
        //                if (SolveSudoku(r + 1, 0)) return true;
        //            }
        //        }
        //    }
        //    return false;
        //}

        //private bool SolveSudoku(int r, int c)
        //{
        //    if (r >= size) return true;
        //    for (int getal = 1; getal < 10; getal++)
        //    {
        //        if (_bord[r, c] == 0) {
        //            if (NumberOK(r, c, getal))
        //            {
        //                UpdateGrid(r, c, getal, new SolidColorBrush(Colors.Black));
        //                if (c < size - 1)
        //                {
        //                    if (SolveSudoku(r, c + 1)) return true;
        //                }
        //                else
        //                {
        //                    if (SolveSudoku(r + 1, 0)) return true;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (c < size - 1)
        //            {
        //                if (SolveSudoku(r, c + 1)) return true;
        //            }
        //            else
        //            {
        //                if (SolveSudoku(r + 1, 0)) return true;
        //            }
        //        }
        //    }
        //    return false;
        //}

        public Boolean SolveSudokuAlgoritme(int r = 0, int c = 0, int waarde = 0)
        {
            for (int getal = 1 + waarde; getal < 10; getal++)
            {
                Console.WriteLine(getal);
                if (NumberOK(r, c, getal))
                {
                    if (_bord[r, c] == 0) UpdateGrid(r, c, getal, new SolidColorBrush(Colors.LightGray));
                    if (c < size - 1)
                    {
                        SolveSudokuAlgoritme(r, c + 1);
                    }
                    else
                    {
                        SolveSudokuAlgoritme(r, c + 1);
                    }
                }
            }
            /*
            if (r >= size) return true;
            int waarde = 0;
            if (start)
            {
                waarde = startWaarde;
            }
            for (int getal = 1 + waarde; getal < 10; getal++)
            {
                if (NumberOK(r, c, getal))
                {
                    if (_bord[r, c] == 0) UpdateGrid(r, c, getal, new SolidColorBrush(Colors.Black));
                    if (c < size - 1)
                    {
                        if (SolveSudoku(r, c + 1, startWaarde, false)) return true;
                    }
                    else
                    {
                        if (SolveSudoku(r + 1, 0, startWaarde, false)) return true;
                    }
                }
            }*/
            /*if (startWaarde + 1 < size - 1)
            {
                for (int i = 0; i < size;i++)
                {
                    _bord[r, i] = 0;
                }
                if (SolveSudoku(r, 0, startWaarde + 1, true)) return true;
            }*/
            return false;
        }

        private bool NumberOK(int r, int c, int number)
        {
            if (!CheckRow(r, c, number)) return false;
            if (!CheckColumn(r, c, number)) return false;
            if (!CheckSquare(r, c, number)) return false;

            return true;
        }

        private bool CheckSquare(int r, int c, int number)
        {
            List<int> rowSquare = GetRowSquare(r, c, number);
            List<int> colSquare = GetColSquare(r, c, number);
            for (int row = colSquare[0]; row <= colSquare[colSquare.Count - 1]; row++)
            {
                for (int col = rowSquare[0]; col <= rowSquare[rowSquare.Count - 1]; col++)
                {
                    if (row != r || col != c) if (_bord[row, col] == number) return false;
                }
            }
            return true;
        }

        private List<int> GetRowSquare(int r, int c, int number)
        {
            if (c >= 6) return new List<int>() { 6, 7, 8 };
            if (c >= 3) return new List<int>() { 3, 4, 5 };
            else return new List<int>() { 0, 1, 2 };
        }

        private List<int> GetColSquare(int r, int c, int number)
        {
            if (r >= 6) return new List<int>() { 6, 7, 8 };
            if (r >= 3) return new List<int>() { 3, 4, 5 };
            else return new List<int>() { 0, 1, 2 };
        }

        private bool CheckColumn(int r, int c, int number)
        {
            for (int row = 0; row < size; row++)
            {
                if (row != r)
                {
                    if (_bord[row, c] == number) return false;
                }
            }
            return true;
        }

        private bool CheckRow(int r, int c, int number)
        {
            for (int col = 0; col < size; col++)
            {
                if (col != c)
                {
                    if (_bord[r, col] == number)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void UpdateGrid(int row, int col, int number, SolidColorBrush color)
        {
            for (int i = 0; i < this.Children.Count; i++)
            {
                UIElement e = this.Children[i];
                if (Grid.GetRow(e) == row && Grid.GetColumn(e) == col)
                {
                    if (e.GetType() == typeof(TextBox))
                    {
                        TextBox t = (TextBox)e;
                        t.Foreground = color;
                        this.Children.RemoveAt(i);
                        _bord[row, col] = number;
                        t.Text = number.ToString();
                        this.Children.Add(t);
                    }
                }
            }
        }

        private void DrawGrid()
        {
            this.Children.Clear();
            for (int i = 0; i < size; i++)
            {
                DrawRowAndColumns();
            }
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    if (row < 3 && col < 3) DrawOuterBorders(row, col);
                    NewTile(row, col, new SolidColorBrush(Colors.LightGray));
                }
            }
        }

        private void NewTile(int row, int col, SolidColorBrush color)
        {
            TextBox textBox = new TextBox();
            textBox.Text = _bord[row, col].ToString();
            textBox.TextAlignment = TextAlignment.Center;
            textBox.BorderThickness = new Thickness(1);
            textBox.BorderBrush = new SolidColorBrush(Colors.Black);
            textBox.Padding = new Thickness(12);
            textBox.FontSize = 16;
            textBox.Foreground = color;
            Grid.SetColumn(textBox, col);
            Grid.SetRow(textBox, row);
            this.Children.Add(textBox);
        }

        private void DrawRowAndColumns()
        {
            ColumnDefinition c = new ColumnDefinition();
            RowDefinition r = new RowDefinition();
            c.MinWidth = sizeTile;
            c.MaxWidth = sizeTile;
            r.MinHeight = sizeTile;
            r.MaxHeight = sizeTile;
            this.ColumnDefinitions.Add(c);
            this.RowDefinitions.Add(r);
        }

        private void DrawOuterBorders(int row, int col)
        {
            Border border = new Border()
            {
                BorderBrush = new SolidColorBrush(Colors.Blue),
                BorderThickness = new Thickness(2),
            };
            Grid.SetColumn(border, col * 3);
            Grid.SetRow(border, row * 3);
            Grid.SetColumnSpan(border, 3);
            Grid.SetRowSpan(border, 3);
            Grid.SetZIndex(border, 1);
            this.Children.Add(border);
        }

        private void ClearBord()
        {
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    _bord[row, col] = 0;
                }
            }
        }
    }
}

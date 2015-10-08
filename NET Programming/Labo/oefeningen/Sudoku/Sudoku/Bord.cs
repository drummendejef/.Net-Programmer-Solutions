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
        private Boolean[,] _bordEditable;
        private Boolean won = false;
        private int size = 9;
        private double sizeTile = 50;

        public void SetBord()
        {
            won = false;
            _bord = new int[size, size];
            _bordEditable = new Boolean[size, size];
            this.Loaded += Bord_Loaded;
        }

        void Bord_Loaded(object sender, RoutedEventArgs e)
        {
            drawGrid(true, new SolidColorBrush(Colors.Black));
            test();
        }

        private void test()
        {
            clearBord();
            _bord[0, 2] = 3;
            _bord[0, 5] = 5;
            _bord[1, 2] = 6;
            _bord[1, 5] = 9;
            _bord[3, 2] = 5;
            _bord[3, 4] = 1;
            _bord[4, 0] = 3;
            _bord[4, 6] = 5;
            _bord[4, 7] = 6;
            _bord[4, 8] = 4;
            _bord[5, 1] = 9;
            _bord[5, 2] = 8;
            _bord[6, 3] = 8;
            _bord[6, 4] = 6;
            _bord[6, 5] = 2;
            _bord[6, 7] = 4;
            _bord[7, 3] = 9;
            _bord[8, 0] = 9;
            _bord[8, 2] = 4;
            _bord[8, 7] = 8;

            _bordEditable[0, 2] = false;
            _bordEditable[0, 5] = false;
            _bordEditable[1, 2] = false;
            _bordEditable[1, 5] = false;
            _bordEditable[3, 2] = false;
            _bordEditable[3, 4] = false;
            _bordEditable[4, 0] = false;
            _bordEditable[4, 6] = false;
            _bordEditable[4, 7] = false;
            _bordEditable[4, 8] = false;
            _bordEditable[5, 1] = false;
            _bordEditable[5, 2] = false;
            _bordEditable[6, 3] = false;
            _bordEditable[6, 4] = false;
            _bordEditable[6, 5] = false;
            _bordEditable[6, 7] = false;
            _bordEditable[7, 3] = false;
            _bordEditable[8, 0] = false;
            _bordEditable[8, 2] = false;
            _bordEditable[8, 7] = false;
            drawGrid(false, new SolidColorBrush(Colors.Blue));
        }

        private void clearBord()
        {
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    _bord[row, col] = 0;
                    _bordEditable[row, col] = true;
                }
            }
        }

        private void drawGrid(Boolean isNew, SolidColorBrush color)
        {
            this.Children.Clear();
            for (int i = 0; i < size; i++)
            {
                drawRowAndColumns();
            }
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    if (row < 3 && col < 3) drawOuterBorders(row, col);
                    if (isNew) newTile(row, col, color);
                    else updateTile(row, col);
                }
            }
        }

        private void drawRowAndColumns()
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

        private void drawOuterBorders(int row, int col)
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

        private void updateTile(int row, int col)
        {
            SolidColorBrush color = new SolidColorBrush(Colors.Black);
            if (!_bordEditable[row, col]) color = new SolidColorBrush(Colors.Blue);

            TextBox text = new TextBox();
            text.Text = _bord[row, col].ToString();
            text.BorderThickness = new Thickness(1);
            text.BorderBrush = new SolidColorBrush(Colors.Black);
            text.Padding = new Thickness(12);
            text.TextAlignment = TextAlignment.Center;
            text.FontSize = 16;
            text.Foreground = color;

            Grid.SetColumn(text, col);
            Grid.SetRow(text, row);
            this.Children.Add(text);
        }

        private void newTile(int row, int col, SolidColorBrush color)
        {
            _bord[row, col] = 0;
            _bordEditable[row, col] = true;

            TextBox text = new TextBox();
            text.Text = "0";
            text.BorderThickness = new Thickness(1);
            text.BorderBrush = new SolidColorBrush(Colors.Black);
            text.Padding = new Thickness(12);
            text.TextAlignment = TextAlignment.Center;
            text.FontSize = 16;
            text.Foreground = color;

            Grid.SetColumn(text, col);
            Grid.SetRow(text, row);
            this.Children.Add(text);
        }

        public void solveSudoku()
        {
            solveSudoku(_bord[0, 0], 1, 0, 0);
            drawGrid(false, null);
            //writeToPDF();
        }

        private void writeToPDF()
        {
            PDFHandler handler = new PDFHandler();
            string path = handler.Directory + "\\pdf\\sudoku.pdf";
            handler.CreateFile(path);
            handler.OpenPDF();
            handler.MakeGrid(_bord, (int)sizeTile, size, _bordEditable);
            handler.ClosePDF();
            Process.Start(path);
        }

        private bool solveSudoku(int tile, int index, int y, int x)
        {
            if (index == 9) return true;
            for (int getal = index; getal < 10; getal++)
            {
                writeNumber(tile, getal, y, x);
                if (numberOK(_bord[y, x], y, x))
                {
                    if (nextTile(y, x) == -1)
                    {
                        Boolean isAgain = false;
                        for (int row = 0; row < size; row++)
                        {
                            for (int col = 0; col < size; col++)
                            {
                                if (_bord[row, col] == 0)
                                {
                                    isAgain = true;
                                    continue;
                                }
                            }
                        }
                        if (isAgain) solveSudoku(_bord[0, 0], index++, y, x);
                        else return true;
                    }
                    if (solveSudoku(nextTile(y, x), index, y, x)) return true;
                }
                eraseNumber(y, x);
            }
            return false;
        }

        private void writeNumber(int tile, int value, int y, int x)
        {
            if (_bord[y, x] == 0 && _bordEditable[y, x])
            {
                _bord[y, x] = value;
            }
        }

        private void eraseNumber(int row, int col)
        {
            if (_bordEditable[row, col])
            {
                _bord[row, col] = 0;
            }
        }

        private int nextTile(int row, int col)
        {
            if(col < size) return _bord[row, col++];
            if (row < size) return _bord[row++, col];
            else return -1;
        }

        private bool numberOK(int tile, int y, int x)
        {
            if (!checkRow(tile, y, x)) return false;
            if (!checkColumn(tile, y, x)) return false;
            if (!checkSquare(tile, y, x)) return false;

            return true;
        }

        private bool checkSquare(int tile, int y, int x)
        {
            List<int> rowSquare = getRowSquare(tile, y, x);
            List<int> colSquare = getColSquare(tile, y, x);
            for (int row = rowSquare[0]; row <= rowSquare[rowSquare.Count - 1]; row++)
            {
                for (int col = colSquare[0]; col <= colSquare[colSquare.Count - 1]; col++)
                {
                    if (row != y && col !=x) if (_bord[row, col] != 0 && _bord[row, col] == tile) return false;
                }
            }
            return true;
        }

        private List<int> getRowSquare(int tile, int y, int x)
        {
            if (y >= 6) return new List<int>() { 6, 7, 8 };
            if (y >= 3) return new List<int>() { 3, 4, 5 };
            else return new List<int>() { 0, 1, 2 };
        }

        private List<int> getColSquare(int tile, int y, int x)
        {
            if (x >= 6) return new List<int>() { 6, 7, 8 };
            if (x >= 3) return new List<int>() { 3, 4, 5 };
            else return new List<int>() { 0, 1, 2 };
        }

        private bool checkColumn(int tile, int y, int x)
        {
            for (int row = 0; row < size; row++)
            {
                if (row != y)
                {
                    if (_bord[row, x] == tile) return false;
                }
            }
            return true;
        }

        private bool checkRow(int tile, int y, int x)
        {
            for (int col = 0; col < size; col++)
            {
                if (col != x)
                {
                    if (_bord[y, col] == tile) return false;
                }
            }
            return true;
        }
    }
}

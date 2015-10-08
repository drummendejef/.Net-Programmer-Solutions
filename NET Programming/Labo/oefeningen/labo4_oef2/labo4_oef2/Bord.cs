using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows;

namespace labo4_oef2
{
    public class Bord : Grid
    {
        private Tile[,] bord;
        private Boolean won = false;
        private int size = 9;
        private double sizeTile = 50;

        public void setBord()
        {
            won = false;
            bord = new Tile[size, size];
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
            bord[0, 2].value = "3";
            bord[0, 5].value = "5";
            bord[1, 2].value = "6";
            bord[1, 5].value = "9";
            bord[3, 2].value = "5";
            bord[3, 4].value = "1";
            bord[4, 0].value = "3";
            bord[4, 6].value = "5";
            bord[4, 7].value = "6";
            bord[4, 8].value = "4";
            bord[5, 1].value = "9";
            bord[5, 2].value = "8";
            bord[6, 3].value = "8";
            bord[6, 4].value = "6";
            bord[6, 5].value = "2";
            bord[6, 7].value = "4";
            bord[7, 3].value = "9";
            bord[8, 0].value = "9";
            bord[8, 2].value = "4";
            bord[8, 7].value = "8";

            bord[0, 2].isEditable = false;
            bord[0, 5].isEditable = false;
            bord[1, 2].isEditable = false;
            bord[1, 5].isEditable = false;
            bord[3, 2].isEditable = false;
            bord[3, 4].isEditable = false;
            bord[4, 0].isEditable = false;
            bord[4, 6].isEditable = false;
            bord[4, 7].isEditable = false;
            bord[4, 8].isEditable = false;
            bord[5, 1].isEditable = false;
            bord[5, 2].isEditable = false;
            bord[6, 3].isEditable = false;
            bord[6, 4].isEditable = false;
            bord[6, 5].isEditable = false;
            bord[6, 7].isEditable = false;
            bord[7, 3].isEditable = false;
            bord[8, 0].isEditable = false;
            bord[8, 2].isEditable = false;
            bord[8, 7].isEditable = false;
            drawGrid(false, new SolidColorBrush(Colors.Blue));
        }

        public void fillInRandomNumbers()
        {
            clearBord();
            //enkel 1 getal per kolom
            List<int> availableColumns = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            Random random = new Random();
            for (int row = 0; row < size; row++)
            {
                int getal = random.Next(0, availableColumns.Count());
                int col = availableColumns[getal];
                writeNumber(bord[row, col], random.Next(1, 10).ToString());
                bord[row, col].isEditable = false;
                availableColumns.Remove(col);
            }
            drawGrid(false, new SolidColorBrush(Colors.Blue));
        }

        public void solveSudoku()
        {
            solveSudoku(bord[0, 0], 1);
            drawGrid(false, null);
            writeToPDF();
        }

        private void writeToPDF()
        {
            PDFHandler handler = new PDFHandler();
            string path = handler.Directory + "\\pdf\\sudoku.pdf";
            handler.CreateFile(path);
            handler.OpenPDF();
            handler.MakeGrid(bord, (int)sizeTile, size);
            handler.ClosePDF();
            Process.Start(path);
        }

        private bool solveSudoku(Tile tile, int index)
        {
            //if (tile == null) return true;

            //for (int getal = 1; getal < 10; getal++)
            //{
            //    writeNumber(tile, getal.ToString());
            //    if (numberOK(tile)) if (solveSudoku(nextTile(tile))) return true;
            //    eraseNumber(tile);
            //}

            //if (tile == null) return true;
            if (index == 9) return true;
            for (int getal = index; getal < 10; getal++)
            {
                writeNumber(tile, getal.ToString());
                if (numberOK(tile))
                {
                    if (nextTile(tile) == null)
                    {
                        Boolean isAgain = false;
                        for (int row = 0; row < size; row++)
                        {
                            for (int col = 0; col < size; col++)
                            {
                                if (bord[row, col].value == "")
                                {
                                    isAgain = true;
                                    continue;
                                }
                            }
                        }
                        if (isAgain) solveSudoku(bord[0, 0], index++);
                        else return true;
                    }
                    if (solveSudoku(nextTile(tile), index)) return true;
                }
                eraseNumber(tile);
            }
            /*Console.WriteLine(bord[0, 0].value + " " + bord[0, 1].value + " " + bord[0, 2].value + " | " + bord[0, 3].value + " " + bord[0, 4].value + " " + bord[0, 5].value + " | " + bord[0, 6].value + " " + bord[0, 7].value + " " + bord[0, 8].value);
            Console.WriteLine(bord[1, 0].value + " " + bord[1, 1].value + " " + bord[1, 2].value + " | " + bord[1, 3].value + " " + bord[1, 4].value + " " + bord[1, 5].value + " | " + bord[1, 6].value + " " + bord[1, 7].value + " " + bord[1, 8].value);
            Console.WriteLine(bord[2, 0].value + " " + bord[2, 1].value + " " + bord[2, 2].value + " | " + bord[2, 3].value + " " + bord[2, 4].value + " " + bord[2, 5].value + " | " + bord[2, 6].value + " " + bord[2, 7].value + " " + bord[2, 8].value);
            Console.WriteLine(bord[3, 0].value + " " + bord[3, 1].value + " " + bord[3, 2].value + " | " + bord[3, 3].value + " " + bord[3, 4].value + " " + bord[3, 5].value + " | " + bord[3, 6].value + " " + bord[3, 7].value + " " + bord[3, 8].value);
            Console.WriteLine(bord[4, 0].value + " " + bord[4, 1].value + " " + bord[4, 2].value + " | " + bord[4, 3].value + " " + bord[4, 4].value + " " + bord[4, 5].value + " | " + bord[4, 6].value + " " + bord[4, 7].value + " " + bord[4, 8].value);
            Console.WriteLine(bord[5, 0].value + " " + bord[5, 1].value + " " + bord[5, 2].value + " | " + bord[5, 3].value + " " + bord[5, 4].value + " " + bord[5, 5].value + " | " + bord[5, 6].value + " " + bord[5, 7].value + " " + bord[5, 8].value);
            Console.WriteLine(bord[6, 0].value + " " + bord[6, 1].value + " " + bord[6, 2].value + " | " + bord[6, 3].value + " " + bord[6, 4].value + " " + bord[6, 5].value + " | " + bord[6, 6].value + " " + bord[6, 7].value + " " + bord[6, 8].value);
            Console.WriteLine(bord[7, 0].value + " " + bord[7, 1].value + " " + bord[7, 2].value + " | " + bord[7, 3].value + " " + bord[7, 4].value + " " + bord[7, 5].value + " | " + bord[7, 6].value + " " + bord[7, 7].value + " " + bord[7, 8].value);
            Console.WriteLine(bord[8, 0].value + " " + bord[8, 1].value + " " + bord[8, 2].value + " | " + bord[8, 3].value + " " + bord[8, 4].value + " " + bord[8, 5].value + " | " + bord[8, 6].value + " " + bord[8, 7].value + " " + bord[8, 8].value);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();*/
            return false;
        }

        private bool numberOK(Tile tile)
        {
            if (!checkRow(tile)) return false;
            if (!checkColumn(tile)) return false;
            if (!checkSquare(tile)) return false;

            return true;
        }

        private bool checkSquare(Tile tile)
        {
            List<int> rowSquare = getRowSquare(tile);
            List<int> colSquare = getColSquare(tile);
            for (int row = rowSquare[0]; row <= rowSquare[rowSquare.Count - 1]; row++)
            {
                for (int col = colSquare[0]; col <= colSquare[colSquare.Count - 1]; col++)
                {
                    if (!bord[row, col].position.Equals(tile.position)) if (bord[row, col].value != "" && bord[row, col].value == tile.value) return false;
                }
            }
            return true;
        }

        private List<int> getRowSquare(Tile tile)
        {
            if (tile.position.Y >= 6) return new List<int>() { 6, 7, 8 };
            if (tile.position.Y >= 3) return new List<int>() { 3, 4, 5 };
            else return new List<int>() { 0, 1, 2 };
        }

        private List<int> getColSquare(Tile tile)
        {
            if (tile.position.X >= 6) return new List<int>() { 6, 7, 8 };
            if (tile.position.X >= 3) return new List<int>() { 3, 4, 5 };
            else return new List<int>() { 0, 1, 2 };
        }

        private bool checkColumn(Tile tile)
        {
            //for (int row = 0; row < (int)tile.position.Y; row++)
            //{
            //    if (bord[row, (int)tile.position.X].value == tile.value) return false;
            //}
            for (int row = 0; row < size; row++)
            {
                if (row != (int)tile.position.Y)
                {
                    if (bord[row, (int)tile.position.X].value == tile.value) return false;
                }
            }
            return true;
        }

        private bool checkRow(Tile tile)
        {
            //for (int col = 0; col <= (int)tile.position.X; col++)
            //{
            //    if (bord[(int)tile.position.Y, col].value == tile.value) return false;
            //}
            for (int col = 0; col < size; col++)
            {
                if (col != (int)tile.position.X)
                {
                    if (bord[(int)tile.position.Y, col].value == tile.value) return false;
                }
            }
            return true;
        }

        private void eraseNumber(Tile tile)
        {
            if (tile.isEditable)
            {
                tile.value = "";
            }
        }

        private Tile nextTile(Tile tile)
        {
            int x = (int)tile.position.X;
            int y = (int)tile.position.Y;
            if (x == size - 1 && y == size - 1)
            {
                return null;
            }
            if (x < size - 1) return bord[y, x + 1];
            else return bord[y + 1, 0];
        }

        private void clearBord()
        {
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    bord[row, col].value = "";
                    bord[row, col].isEditable = true;
                }
            }
        }

        private void writeNumber(Tile tile, string value)
        {
            if (tile.value == "" && tile.isEditable)
            {
                tile.value = value;
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

        private void updateTile(int row, int col)
        {
            SolidColorBrush color = new SolidColorBrush(Colors.Black);
            if (!bord[row, col].isEditable) color = new SolidColorBrush(Colors.Blue);
            Grid.SetColumn(bord[row, col], col);
            Grid.SetRow(bord[row, col], row);
            bord[row, col].placeTiles(color);
            this.Children.Add(bord[row, col]);
        }

        private void newTile(int row, int col, SolidColorBrush color)
        {
            bord[row, col] = new Tile();
            bord[row, col].value = "";
            bord[row, col].isEditable = true;
            bord[row, col].position.X = col;
            bord[row, col].position.Y = row;
            bord[row, col].placeTiles(color);
            Grid.SetColumn(bord[row, col], col);
            Grid.SetRow(bord[row, col], row);
            this.Children.Add(bord[row, col]);
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _04.Backtracking___N_Queens
{
    class Board : Canvas
    {
        public int _size { get; set; }

        public Boolean[,] bord;

        public Boolean won = false;
        Tile tile;
        public async void setSizeBordAsync(int size)
        {
            won = false;
            this._size = size;
            double h = ((Panel)Application.Current.MainWindow.Content).ActualHeight;
            double w = ((Panel)Application.Current.MainWindow.Content).ActualWidth;
            double width = (w - 20) / size;
            double height = (h - 20 - 40) / size;
            //double width = (SystemParameters.PrimaryScreenWidth - 10) / size;//(this.Width - 10) / size;
            //double height = (SystemParameters.PrimaryScreenHeight - 10 - 40) / size;//(this.Width - 10) / size;
            bord = new Boolean[size, size];
            tile = new Tile(width, height);
            drawGrid();
            won = await findSolutionAsync(_size, bord, 0);
            drawGrid();
            Console.WriteLine("success");
        }

        public void setSizeBord(int size)
        {
            won = false;
            this._size = size;
            double h = ((Panel)Application.Current.MainWindow.Content).ActualHeight;
            double w = ((Panel)Application.Current.MainWindow.Content).ActualWidth;
            double width = (w - 20) / size;
            double height = (h - 20 - 40) / size;
            //double width = (SystemParameters.PrimaryScreenWidth - 10) / size;//(this.Width - 10) / size;
            //double height = (SystemParameters.PrimaryScreenHeight - 10 - 40) / size;//(this.Width - 10) / size;
            bord = new Boolean[size, size];
            tile = new Tile(width, height);
            drawGrid();
            while (!won)
            {
                won = findSolution(_size, bord, 0);
                drawGrid();
            }
            Console.WriteLine("success");
        }

        private Task<Boolean> findSolutionAsync(int _size, bool[,] bord, int p)
        {
            return Task.Run(() =>
            {
                return findSolution(_size, bord, p);
            });
        }

        private void drawGrid()
        {
            for (int row = 0; row < _size; row++)
            {
                for (int col = 0; col < _size; col++)
                {
                    Rectangle rect = tile.placeTiles(new SolidColorBrush(Colors.White));
                    if (bord[row, col]) rect = tile.placeTiles(new SolidColorBrush(Colors.Green));
                    Canvas.SetLeft(rect, row * tile._width);
                    Canvas.SetTop(rect, col * tile._height);
                    this.Children.Add(rect);
                }
            }
        }

        public Boolean findSolution(int N, Boolean[,] bord, int huidig)
        {
            int kol = huidig;
            if (huidig > N - 1) return true;
            for (kol = 0; kol < N; kol++)
            {
                bord[huidig, kol] = true;
                if (gotProblem(huidig, kol, bord))
                {
                    if (findSolution(N, bord, huidig + 1)) return true;

                }
                bord[huidig, kol] = false;
            }
            return false;
        }

        private bool gotProblem(int huidig, int kol, bool[,] bord)
        {
            if (!checkAbove(huidig, kol, bord)) return false;
            if (!checkLeftDiagonal(huidig, kol, bord)) return false;
            if (!checkRightDiagonal(huidig, kol, bord)) return false;
            return true;
        }

        private bool checkRightDiagonal(int huidig, int kol, bool[,] bord)
        {
            for (int i = 1; i <= huidig; i++)
            {
                int andereRij = huidig - i;
                int andereKolom = kol + i;
                if (andereKolom >= bord.GetUpperBound(0) + 1) return true;
                if (bord[andereRij, andereKolom]) return false;
            }
            return true;
        }

        private bool checkLeftDiagonal(int huidig, int kol, bool[,] bord)
        {
            for (int i = 1; i < huidig + 1; i++)
            {
                int andereRij = huidig - i;
                int andereKolom = kol - i;
                if (andereKolom < bord.GetLowerBound(0)) return true;
                if (bord[andereRij, andereKolom]) return false;
            }
            return true;
        }

        private Boolean checkAbove(int huidig, int kol, bool[,] bord)
        {
            for (int row = 0; row < huidig; row++)
            {
                if (bord[row, kol]) return false;
            }
            return true;
        }
    }
}

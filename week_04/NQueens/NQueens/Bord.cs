using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NQueens
{
    class Bord : Canvas
    {
        public int _size { get; set; }//Int die de aantal konigingen (en dus ook het aantal rijen) bij gaat houden.
        public Boolean[,] bord;
        Tile tile; //Een tegel

        public Boolean won = false;//De koniginen zijn allemaal gevonden

        #region Normaal

        //De afmetingen berekenen om het bord zelf te tekenen
        public void setSizeBord(int size)
        {
            //Gemaakt om te kijken hoe lang het duurde om iets te berekenen
            var watch = Stopwatch.StartNew();

            Console.WriteLine("Normal start");

            won = false;
            this._size = size;
            double h = ((Panel)Application.Current.MainWindow.Content).ActualHeight;
            double w = ((Panel)Application.Current.MainWindow.Content).ActualWidth;
            double width = (w - 20) / size;//Randen eruit laten
            double height = (h - 20 - 40) / size;//Randen en balkje bovenaan met de knoppen eruit laten

            bord = new Boolean[size, size];
            tile = new Tile(width, height);

            drawGrid();//Het grid een eerste keer tekenen, nog geen enkel vakje koniging is gezocht.

            //Zolang dat won niet op 0 staat
            while(!won)
            {//Alle oplossingen gaan zoeken, als er geen problemen meer gevonden kunnen worden, stoppen.
                won = zoekOplossing(_size, bord, 0);
            }
            drawGrid();

            watch.Stop();
            Console.WriteLine("Normal done, time elapsed: " + watch.ElapsedMilliseconds + " miliseconden");
            
        }

        #endregion

        #region Multithreaded
        public async void setSizeBordAsync(int size)
        {
            //Gemaakt om te kijken hoe lang het duurde om iets te berekenen
            var watch = Stopwatch.StartNew();

            Console.WriteLine("Async start");

            won = false;
            this._size = size;

            double h = ((Panel)Application.Current.MainWindow.Content).ActualHeight;
            double w = ((Panel)Application.Current.MainWindow.Content).ActualWidth;
            double width = (w - 20) / size;//Randen eruit laten (margin)
            double height = (h - 20 - 40) / size;//Randen en balkje bovenaan met de knoppen eruit laten

            bord = new Boolean[size, size];
            tile = new Tile(width, height);
            drawGrid();

            won = await zoekOplossingAsync(_size, bord, 0);
            drawGrid();

            watch.Stop();
            Console.WriteLine("Async drawing succes, time elapsed: " + watch.ElapsedMilliseconds + " miliseconden");
            
        }

        private Task<Boolean> zoekOplossingAsync(int _size, bool[,] bord, int v)
        {
            return Task.Run(() =>
            {
                return zoekOplossing(_size, bord, v);
            });
        }
        #endregion

        //Het bord zelf tekenen
        private void drawGrid()
        {
            //Alle vakjes overlopen
            for(int row = 0; row < _size; row++)
            {
                for(int col = 0; col < _size; col++)
                {
                    Rectangle rect;
                    //Het vakje groen of wit kleuren
                    if (bord[row, col]) rect = tile.placeTiles(new SolidColorBrush(Colors.Green));
                    else rect = tile.placeTiles(new SolidColorBrush(Colors.White));
                    Canvas.SetLeft(rect, row * tile._width);
                    Canvas.SetTop(rect, col * tile._height);
                    this.Children.Add(rect);
                    
                }
            }
        }

        private bool zoekOplossing(int N, Boolean[,] bord, int huidig)
        {
            int kol = huidig;
            if (huidig > N - 1) return true;// Als je aan het einde van de rij bent, stoppen

            for (kol = 0; kol < N; kol++)//Alle kolommen overlopen.
            {
                bord[huidig, kol] = true;

                if (geenProbleem(huidig, kol, bord))
                {
                    if (zoekOplossing(N, bord, huidig + 1)) return true;
                }
                bord[huidig, kol] = false;
            }

            return false;//Het einde van de kolom.
        }

        private bool geenProbleem(int huidig, int kol, bool[,] bord)
        {
            if (!checkBoven(huidig, kol, bord)) return false;
            if (!checkLeftDiagonaal(huidig, kol, bord)) return false;
            if (!checkRightDiagonaal(huidig, kol, bord)) return false;
            return true;
        }

        private bool checkRightDiagonaal(int huidig, int kol, bool[,] bord)
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

        private bool checkLeftDiagonaal(int huidig, int kol, bool[,] bord)
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

        private bool checkBoven(int huidig, int kol, bool[,] bord)
        {
            for (int row = 0; row < huidig; row++)
            {
                if (bord[row, kol]) return false;
            }
            return true;
        }
    }
}

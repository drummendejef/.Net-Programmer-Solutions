using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NQueens
{
    class Bord:Canvas
    {
        private bool[,] oplossing;

        public bool zoekOplossing(int n, bool[,] bord, int huidigeNiveau) //n = dimentie bord // n = aantal queens
        { //bord, reference type = accumulerende parameter

            if (huidigeNiveau >= n)
                return true;

            for (int kol = 0; kol < n; kol++)
            {
                bord[huidigeNiveau, kol] = true;

                if (geenProbleemGeintroduceerd(huidigeNiveau, kol, bord))
                {
                    if (zoekOplossing(n, bord, huidigeNiveau + 1))
                    {
                        oplossing = bord;
                        return true; //oplossing gevonden
                    }
                }

                bord[huidigeNiveau, kol] = false;
            }

            return false; // geen oplossing gevonden
        }

        private bool geenProbleemGeintroduceerd(int huidigeNiveau, int kol, bool[,] bord)
        {
            if (propbleemRechtBoven(huidigeNiveau, kol, bord))
                return false;
            else if (propbleemRECHTSBoven(huidigeNiveau, kol, bord))
                return false;
            else if (propbleemLinksBoven(huidigeNiveau, kol, bord))
                return false;

            return true;
        }

        private bool propbleemLinksBoven(int huidigeNiveau, int kol, bool[,] bord)
        {
            if (huidigeNiveau == 0)
                return false;

            huidigeNiveau--;
            kol--;

            if (kol < 0)
                return false;

            while (huidigeNiveau >= 0)
            {
                if (kol < 0)
                    return false;

                if (bord[huidigeNiveau, kol] == true)
                    return true;

                huidigeNiveau--;
                kol--;
            }

            return false;
        }

        private bool propbleemRECHTSBoven(int huidigeNiveau, int kol, bool[,] bord)
        {
            if (huidigeNiveau == 0)
                return false;

            huidigeNiveau--;
            kol++;

            if (kol >= bord.GetLength(0))
                return false;

            while (huidigeNiveau >= 0)
            {
                if (kol >= bord.GetLength(0))
                    return false;

                if (bord[huidigeNiveau, kol] == true)
                    return true;

                huidigeNiveau--;
                kol++;
            }

            return false;
        }

        private bool propbleemRechtBoven(int huidigeNiveau, int kol, bool[,] bord)
        {
            if (huidigeNiveau == 0)
                return false;

            huidigeNiveau--;

            while (huidigeNiveau >= 0)
            {
                if (bord[huidigeNiveau, kol] == true)
                    return true;

                huidigeNiveau--;
            }

            return false;
        }

        public void tekenBord()
        {
            int length = oplossing.GetLength(0);
            int lengthRect = (int)this.Width / length;
            SolidColorBrush brush;

            //maakBord(length);

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    Rectangle rect = new Rectangle();
                    rect.Height = lengthRect;
                    rect.Width = lengthRect;
                    rect.StrokeThickness = 1;
                    rect.Stroke = Brushes.Black;

                    if(oplossing[i,j]==true)
                        brush = Brushes.Black;
                    else
                        brush = Brushes.Bisque;

                    rect.Fill = brush;

                    Canvas.SetLeft(rect, lengthRect * i);
                    Canvas.SetTop(rect, lengthRect * j);

                    this.Children.Add(rect);
                }
            }
        }

        //private void maakBord(int length)
        //{
        //    this.RowDefinitions.Clear();
        //    this.ColumnDefinitions.Clear();

        //    for (int i = 0; i < length; i++)
        //    {
        //        RowDefinition rowDef = new RowDefinition();
        //        ColumnDefinition colDef = new ColumnDefinition();

        //        this.RowDefinitions.Add(rowDef);
        //        this.ColumnDefinitions.Add(colDef);
        //    }
        //}
    }
}

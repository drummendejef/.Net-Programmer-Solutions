using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace labo4_oef1
{
    public class Bord : Canvas
    {
        public event EventHandler IsAanHetZoekenVeranderd;
        private Boolean _isAanHetZoeken;
        public Boolean IsAanHetZoeken
        {
            get { return _isAanHetZoeken; }
            set
            {
                if (_isAanHetZoeken == value) return;
                _isAanHetZoeken = value;
                OnIsAanHetZoekenVeranderd();
            }
        }

        protected virtual void OnIsAanHetZoekenVeranderd()
        {
            if (IsAanHetZoekenVeranderd != null) IsAanHetZoekenVeranderd(this, EventArgs.Empty);
        }
        public static int GetJDASize(DependencyObject obj)
        {
            return (int)obj.GetValue(JDASizeProperty);
        }

        public static void SetJDASize(DependencyObject obj, int value)
        {
            obj.SetValue(JDASizeProperty, value);
        }

        //Using a DependencyProperty as the backing store for JDASize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty JDASizeProperty =
            DependencyProperty.RegisterAttached("JDASize", typeof(int), typeof(Bord), new PropertyMetadata(4));

        public static readonly DependencyProperty SizeProperty = DependencyProperty.RegisterAttached("Size", typeof(int), typeof(Bord), new UIPropertyMetadata(new PropertyChangedCallback(SizeChangedCallBack)));

        public delegate void toonGrid(int i);
        private static void SizeChangedCallBack(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            Bord b = (Bord)target;
            //b.setSizeBord((int)e.NewValue);
            b.threaded((int)e.NewValue);
        }

        public delegate Boolean zoekOplossingThreaded(int N, Boolean[,] bord, int huidig);

        public int _size { get; set; }

        public Boolean[,] bord;

        //public Boolean won = false;
        public Boolean canceled = false;
        Tile tile;
        public static int GetSize(UIElement target)
        {
            return (int)target.GetValue(Bord.SizeProperty);
        }

        public static void SetSize(UIElement target, string value)
        {
            target.SetValue(Bord.SizeProperty, value);
        }

        public Bord()
        {
            this.Loaded += Bord_Loaded;
        }

        void Bord_Loaded(object sender, RoutedEventArgs e)
        {
            //setSizeBord(4);
            threaded(4);
        }

        public void threaded(int value)
        {
            IsAanHetZoeken = true;
            setSizeBord(value);

            //hier start u het zoeken naar een oplossing op een andere thread dan de ui thread
            zoekOplossingThreaded routine = new zoekOplossingThreaded(zoekOplossing);
            routine.BeginInvoke(_size, bord, 0, OplossingGevonden, null);
            //gebruik een asynccallback methode om  na het vinden van de oplossing terug te keren (nog altijd op die andere thread) ==> hij doet wat ik wil zonder dit, dus... .
            //gebruik dispatcher om het gevonden resultaat te visualiseren op de ui thread
            //this.Dispatcher.Invoke(drawGrid);
            //drawGrid();

            //toonGrid routine2 = new toonGrid(setSizeBord);
            //Dispatcher.BeginInvoke(routine, new object[] { value });
            //routine2.BeginInvoke(value, null, null);
        }


        private void OplossingGevonden(IAsyncResult ar)
        {
            //nog steeds op de andere (niet UI) thread
            this.Dispatcher.Invoke(drawGrid);
            this.Dispatcher.Invoke(ZetZoekenAf);
        }

        private void ZetZoekenAf()
        {
            IsAanHetZoeken = false;
        }


        public void setSizeBord(int size)
        {
            canceled = false;
            try
            {
                //Thread.Sleep(10);
                this._size = size;
                double h = ((Panel)Application.Current.MainWindow.Content).ActualHeight;
                double w = ((Panel)Application.Current.MainWindow.Content).ActualWidth;
                double width = (w - 20) / size;
                double height = (h - 20 - 40) / size;
                //double width = (SystemParameters.PrimaryScreenWidth - 10) / size;//(this.Width - 10) / size;
                //double height = (SystemParameters.PrimaryScreenHeight - 10 - 40) / size;//(this.Width - 10) / size;
                bord = new Boolean[size, size];
                tile = new Tile(width, height);
                //drawGrid();
                //while (!won)
                //{
                // won = zoekOplossing(_size, bord, 0);
                //Action a = new Action(drawGrid);
                //Dispatcher.BeginInvoke(a, new object[] { });
                //drawGrid();
                //}
                Console.WriteLine("success");
            }
            catch (Exception ex) { }
        }

        private void drawGrid()
        {
            //return;
            for (int row = 0; row < _size; row++)
            {
                for (int col = 0; col < _size; col++)
                {
                    Rectangle rect;
                    if (bord[row, col]) rect = tile.placeTiles(new SolidColorBrush(Colors.Green));
                    else rect = tile.placeTiles(new SolidColorBrush(Colors.White));
                    Canvas.SetLeft(rect, row * tile._width);
                    Canvas.SetTop(rect, col * tile._height);
                    this.Children.Add(rect);
                }
            }
        }

        public Boolean zoekOplossing(int N, Boolean[,] bord, int huidig)
        {
            if (!canceled)
            {
                int kol = huidig;
                if (huidig > N - 1) return true;
                for (kol = 0; kol < N; kol++)
                {
                    bord[huidig, kol] = true;
                    if (geenProbleem(huidig, kol, bord))
                    {
                        if (zoekOplossing(N, bord, huidig + 1)) return true;

                    }
                    bord[huidig, kol] = false;
                }
            }
            else
            {
                Console.WriteLine("Onderbroken");
            }
            return false;
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

        private Boolean checkBoven(int huidig, int kol, bool[,] bord)
        {
            for (int row = 0; row < huidig; row++)
            {
                if (bord[row, kol]) return false;
            }
            return true;
        }     
    }
}

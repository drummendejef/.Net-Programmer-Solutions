using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NQueens
{
    /// <summary>
    /// Interaction logic for NQueensUC.xaml
    /// </summary>
    public partial class NQueensUC : UserControl
    {
        [Browsable(true)]
        public static int GetGridSize(UIElement target)
        {
            return (int)target.GetValue(GridSizeProperty);
        }

        public static void SetGridSize(UIElement target, int value)
        {
            target.SetValue(GridSizeProperty, value);
        }

        // Using a DependencyProperty as the backing store for Dimension.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GridSizeProperty =
            DependencyProperty.Register("GridSize", typeof(int), typeof(NQueensUC), new PropertyMetadata(0,new PropertyChangedCallback(OnGridsizeChanged)));

        private static void OnGridsizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NQueensUC uc = d as NQueensUC;
            uc.Solve((int)e.NewValue);
        }

        Border[,] _cells;

        public NQueensUC()
        {
            InitializeComponent();
        }

        
        public void Solve(int rows)
        {
            DrawGrid(rows);
            
            bool[] availableColumns = new bool[rows];
            for (int i = 0; i < rows; i++)
                availableColumns[i] = true;

            NQueens(new bool[rows, rows], 0, availableColumns);
        }

        private void DrawGrid(int rows)
        {
            //reset grid
            grid.Children.Clear();
            grid.RowDefinitions.Clear();
            grid.ColumnDefinitions.Clear();

            //init grid cols and rows
            for (int i = 0; i < rows; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            //place cells
            _cells = new Border[rows, rows];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    Border cell = new Border()
                    {
                        BorderBrush = new SolidColorBrush(Colors.Black),
                        BorderThickness = new Thickness(2),
                        Background = new SolidColorBrush(Colors.White)
                    };
                    Grid.SetRow(cell, i);
                    Grid.SetColumn(cell, j);
                    grid.Children.Add(cell);
                    _cells[i, j] = cell;
                }
            }
        }

        [AOPPerformanceCounter()]
        private bool NQueens(bool[,] board, int row, bool[] availableColumns)
        {
            if (row >= board.GetLength(0)) return true;

            for (int col = 0; col < board.GetLength(0); col++)
            {
                if (availableColumns[col])
                {
                    PlaceQueen(row, col, board, availableColumns);
                    if (QueenOK(row, col, board))
                        if (NQueens(board, row + 1, availableColumns))
                            return true;
                }
                RemoveQueen(row, col, board, availableColumns);
            }

            return false;
        }

        private void RemoveQueen(int row, int col, bool[,] board, bool[] availableColumns)
        {
            board[row, col] = false;
            availableColumns[col] = true;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                if (board[i, col])
                {
                    availableColumns[col] = false;
                    break;
                }
            }

            _cells[row, col].Background = new SolidColorBrush(Colors.White);
        }


        private Boolean QueenOK(int row, int col, bool[,] board)
        {
            if (!CheckPreviousLeftDiagonal(row, col, board)) return false;
            if (!CheckPreviousRightDiagonal(row, col, board)) return false;

            return true;
        }

        private bool CheckPreviousRightDiagonal(int row, int col, bool[,] board)
        {
            if (row > 0 && col < board.GetLength(0) - 1)
            {
                if (!board[row - 1, col + 1])
                {
                    if (CheckPreviousRightDiagonal(row - 1, col + 1, board))
                        return true;
                    else
                        return false;
                } else
                    return false;
            }

            return true;
        }

        private bool CheckPreviousLeftDiagonal(int row, int col, bool[,] board)
        {
            if (row > 0 && col > 0)
            {
                if (!board[row - 1, col - 1])
                {
                    if (CheckPreviousLeftDiagonal(row - 1, col - 1, board))
                        return true;
                    else
                        return false;
                } else
                    return false;
            }

            return true;
        }

        private void PlaceQueen(int row, int col, bool[,] board, bool[] availableColumns)
        {
            board[row, col] = true;
            availableColumns[col] = false;
            _cells[row, col].Background = new SolidColorBrush(Colors.Blue);
        }
    }
}

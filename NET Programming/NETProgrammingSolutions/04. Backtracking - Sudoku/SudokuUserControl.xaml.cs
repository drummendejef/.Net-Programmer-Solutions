using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace _04.Backtracking___Sudoku
{
    /// <summary>
    /// Interaction logic for SudokuUserControl.xaml
    /// </summary>
    public partial class SudokuUserControl : UserControl
    {
        Cell[,] _cells;
        List<Cell>[] _squares = new List<Cell>[9];
        private Cell _selectedCell;
        private TaskCompletionSource<bool> _completionSource;

        public Cell SelectedCell
        {
            get { return _selectedCell; }
            set
            {
                _selectedCell = value;
                _selectedCell.Focus();
            }
        }

        public SudokuUserControl()
        {
            InitializeComponent();

            DrawGrid();
            var keyUpDelegate = new RoutedEventHandler(GridKeyUp);

            SudokuGrid.AddHandler(PreviewKeyUpEvent, keyUpDelegate);
            SudokuGrid.LostFocus += SudokuGrid_LostFocus;
        }

        void SudokuGrid_LostFocus(object sender, RoutedEventArgs e)
        {
            Cell cell = (e.Source as Cell);
            int inputtedValue;

            bool result = Int32.TryParse(cell.Text, out inputtedValue);
            if (result)
            {
                int needsToBeValue = _selectedCell.Value;
                if (inputtedValue != needsToBeValue)
                {
                    cell.Foreground = new SolidColorBrush(Colors.Red);
                }
                else if (inputtedValue == needsToBeValue)
                {
                    cell.Foreground = new SolidColorBrush(Colors.Blue);
                }
            }
            else
            {
                cell.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void GridKeyUp(object sender, RoutedEventArgs e)
        {
            var keyEvent = e as KeyEventArgs;
            switch (keyEvent.Key)
            {
                case Key.Up:
                    if (_selectedCell.Position.Y == 0)
                    {
                        SelectedCell = _cells[8, (int)_selectedCell.Position.X];
                        return;
                    }


                    SelectedCell = _cells[(int)_selectedCell.Position.Y - 1, (int)_selectedCell.Position.X];
                    break;
                case Key.Down:
                    if (_selectedCell.Position.Y == 8)
                    {
                        SelectedCell = _cells[0, (int)_selectedCell.Position.X];
                        return;
                    }


                    SelectedCell = _cells[(int)_selectedCell.Position.Y + 1, (int)_selectedCell.Position.X];
                    break;
                case Key.Left:
                    if (_selectedCell.Position.X == 0)
                    {
                        SelectedCell = _cells[(int)_selectedCell.Position.Y, 8];
                        return;
                    }


                    SelectedCell = _cells[(int)_selectedCell.Position.Y, (int)_selectedCell.Position.X - 1];
                    break;
                case Key.Right:
                    if (_selectedCell.Position.X == 8)
                    {
                        SelectedCell = _cells[(int)_selectedCell.Position.Y, 0];
                        return;
                    }


                    SelectedCell = _cells[(int)_selectedCell.Position.Y, (int)_selectedCell.Position.X + 1];
                    break;
            }

        }

        private void DrawGrid()
        {
            //make colulmns
            for (int i = 0; i < 9; i++)
            {
                SudokuGrid.RowDefinitions.Add(new RowDefinition());
                SudokuGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            //draw borders
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    Border border = new Border()
                    {
                        BorderBrush = new SolidColorBrush(Colors.Blue),
                        BorderThickness = new Thickness(2),
                    };
                    Grid.SetColumnSpan(border, 3);
                    Grid.SetRowSpan(border, 3);
                    Grid.SetColumn(border, col * 3);
                    Grid.SetRow(border, row * 3);
                    Grid.SetZIndex(border, 1);

                    SudokuGrid.Children.Add(border);
                }
            }

            //add cells
            _cells = new Cell[9, 9];
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    //create squares array
                    int rowAdder = 0;
                    if (row > 2) rowAdder = 3;
                    if (row > 5) rowAdder = 6;
                    int colAdder = 0;
                    if (col > 2) colAdder = 1;
                    if (col > 5) colAdder = 2;

                    int square = colAdder + rowAdder;
                    if (_squares[square] == null)
                        _squares[square] = new List<Cell>();

                    //create cell
                    Cell cell = new Cell()
                    {
                        Position = new Point(col, row),
                        Square = square
                    };

                    cell.GotKeyboardFocus += cell_GotKeyboardFocus;

                    //add cell to arrays
                    _cells[row, col] = cell;
                    _squares[square].Add(cell);

                    //add cell to grid
                    Grid.SetColumn(cell, col);
                    Grid.SetRow(cell, row);
                    SudokuGrid.Children.Add(cell);

                }
            }
        }

        void cell_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            SelectedCell = sender as Cell;
        }

        public void Solve()
        {
            foreach (Cell cell in _cells)
            {
                cell.WriteValue(false);
            }
        }

        public void CreateTask()
        {
            SudokuGrid.IsEnabled = true;
            ClearCells();
            InsertRandomNumbers();
            SolveSudoku(_cells[0, 0]);

            Random rnd = new Random();

            foreach (Cell cell in _cells)
            {
                int random = rnd.Next(5);

                if (random == 0)
                    cell.WriteValue(true);
            }
        }

        /// <summary>
        /// Inserts random numbers that don't interfere with each other (one number per row, never the same column twice)
        /// , so the first solve to create the task isn't always the base sudoku.
        /// </summary>
        private void InsertRandomNumbers()
        {
            List<int> availableColumns = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

            Random random = new Random();

            for (int c = 0; c < 9; c++)
            {
                for (int r = 0; r < 9; r++)
                {
                    //continue if row or column aready has a value
                    if (!availableColumns.Contains(c))
                        break;

                    //randomly decide to place a number
                    int decider = random.Next(10);
                    if (decider != 0)
                        continue;
                    //write a random number
                    WriteNumber(_cells[r, c], random.Next(1, 10));
                    availableColumns.Remove(c);
                }
            }
        }

        private void ClearCells()
        {
            foreach (Cell cell in _cells)
                cell.ClearCell();
        }

        public int[] GetTaskNumbers()
        {
            int[] values = new int[81];
            int counter = 0;
            for (int r = 0; r < 9; r++)
            {
                for (int c = 0; c < 9; c++)
                {
                    if (_cells[r, c].IsReadOnly)
                        values[counter] = _cells[r, c].Value;
                    else
                        values[counter] = 0;
                    counter++;
                }
            }

            return values;
        }

        public int[] GetSolvedSudoku()
        {
            int[] values = new int[81];
            int counter = 0;

            for (int r = 0; r < 9; r++)
            {
                for (int c = 0; c < 9; c++)
                {
                    values[counter] = _cells[r, c].Value;
                    counter++;
                }
            }

            return values;
        }

        public void LoadTask(int[] taskValues, int[] solvedValues)
        {
            ClearCells();

            int counter = -1;
            for (int r = 0; r < 9; r++)
            {
                for (int c = 0; c < 9; c++)
                {
                    counter++;

                    _cells[r, c].Value = solvedValues[counter];
                    if (taskValues[counter] != 0)
                        _cells[r, c].WriteValue(true);
                }
            }
        }

        private bool SolveSudoku(Cell cell)
        {
            if (cell == null) return true;

            //try every possible number
            for (int z = 1; z < 10; z++)
            {
                WriteNumber(cell, z);
                if (NumberOK(cell))
                    if (SolveSudoku(NextCell(cell)))
                        return true;
                EraseNumber(cell);
            }

            return false;
        }

        private void EraseNumber(Cell cell)
        {
            cell.Value = 0;
        }

        private Cell NextCell(Cell cell)
        {
            if (cell.Position.X == 8 && cell.Position.Y == 8) return null;
            if (cell.Position.X < 8)
                return _cells[(int)cell.Position.Y, (int)cell.Position.X + 1];
            else
                return _cells[(int)cell.Position.Y + 1, 0];
        }

        public bool NumberOK(Cell cell)
        {
            if (!CheckRow(cell)) return false;
            if (!CheckColumn(cell)) return false;
            if (!CheckSquare(cell)) return false;

            return true;
        }

        private bool CheckSquare(Cell cell)
        {
            foreach (Cell c in _squares[cell.Square])
            {
                if (!c.Position.Equals(cell.Position))
                    if (c.Value != 0 && c.Value == cell.Value)
                        return false;
            }

            return true;
        }

        private bool CheckColumn(Cell cell)
        {
            for (int i = 0; i < (int)cell.Position.Y; i++)
            {
                if (_cells[i, (int)cell.Position.X].Value == cell.Value)
                    return false;
            }

            return true;
        }

        private bool CheckRow(Cell cell)
        {
            for (int i = 0; i < (int)cell.Position.X; i++)
            {
                if (_cells[(int)cell.Position.Y, i].Value == cell.Value)
                    return false;
            }

            return true;
        }

        private void WriteNumber(Cell cell, int z)
        {
            if (cell.Value != 0)
                return;
            cell.Value = z;
        }

        private void SudokuSolvingCompleted(IAsyncResult asyncResult)
        {
            _completionSource.SetResult(true);
        }
    }
}

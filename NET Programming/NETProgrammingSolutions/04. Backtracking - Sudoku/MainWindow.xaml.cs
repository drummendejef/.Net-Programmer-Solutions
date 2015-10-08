using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ucSudoku.Solve();
        }

        private void btnCreateTask_Click(object sender, RoutedEventArgs e)
        {
            ucSudoku.CreateTask();
            btnSaveTask.IsEnabled = true;
            btnSolve.IsEnabled = true;
        }

        private void btnSaveTask_Click(object sender, RoutedEventArgs e)
        {
            int[] taskValues = ucSudoku.GetTaskNumbers();
            int[] solvedValues = ucSudoku.GetSolvedSudoku();
            SaveFileDialog sfd = new SaveFileDialog()
            {
                AddExtension = true,
                Filter = "Text Files (*.txt)|*.txt",
                DefaultExt = "txt",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            bool? saveResult = sfd.ShowDialog();
            if (!saveResult.HasValue)
                return;
            if (!saveResult.Value)
                return;

            string taskValuesString = "";
            foreach (int number in taskValues)
                taskValuesString += number.ToString();
            string allValuesString = "";
            foreach (int number in solvedValues)
                allValuesString += number.ToString();

            using (StreamWriter writer = new StreamWriter(sfd.FileName))
            {
                writer.WriteLine(taskValuesString);
                writer.WriteLine(allValuesString);
            }
        }

        private void btnLoadTask_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = "Text Files (*.txt)|*.txt"
            };

            bool? ofdResult = ofd.ShowDialog();
            if (!ofdResult.HasValue)
                return;
            if (!ofdResult.Value)
                return;

            using (StreamReader reader = new StreamReader(ofd.FileName))
            {
                string taskValueString = reader.ReadLine();
                int[] taskValues = GetValuesFromString(taskValueString);
                if (taskValues == null)
                    return;
                string solvedValuesString = reader.ReadLine();
                int[] solvedValues = GetValuesFromString(solvedValuesString);

                if (solvedValues == null)
                    return;

                ucSudoku.LoadTask(taskValues, solvedValues);
                btnSolve.IsEnabled = true;
            }
        }

        private int[] GetValuesFromString(string valueString)
        {
            if (valueString == null || valueString.Length != 81)
            {
                MessageBox.Show("The task file is incompatible.");
                return null;
            }

            int[] values = new int[81];
            for (int i = 0; i < 81; i++)
            {
                try
                {
                    values[i] = int.Parse(valueString.Substring(i, 1));
                }
                catch
                {
                    MessageBox.Show("The task file is incompatible.");
                }
            }

            return values;
        }
    }
}

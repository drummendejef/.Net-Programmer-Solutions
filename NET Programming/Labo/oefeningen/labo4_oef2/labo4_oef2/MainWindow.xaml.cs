﻿using System;
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

namespace labo4_oef2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            laadBord();
        }

        private void laadBord()
        {
            cnvBord.setBord();
        }

        private void btnCreateTask_Click(object sender, RoutedEventArgs e)
        {
            cnvBord.fillInRandomNumbers();
        }

        private void btnSolve_Click(object sender, RoutedEventArgs e)
        {
            cnvBord.solveSudoku();
        }
    }
}

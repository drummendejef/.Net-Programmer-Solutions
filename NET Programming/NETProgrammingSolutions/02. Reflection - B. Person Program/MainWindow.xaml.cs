﻿using _02.Reflection___A.Custom_Attributes;
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

namespace _02.Reflection___B.Person_Program
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [PluginAttribute(true, "People")]
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAddPersonClick(object sender, RoutedEventArgs e)
        {
            Person p = new Person() { FirstName = "Celine", LastName = "Gardier", Age = 27 };

            MessageBox.Show(p.FirstName + " " + p.LastName + " (" + p.Age + ")");
        }
    }
}

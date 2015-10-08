﻿using _02.Reflection___A.Custom_Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

namespace _02.Reflection___Infrastructure
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadPlugins();
        }

        private void LoadPlugins()
        {
            string[] files = SearchFilesFromFilters(
                Directory.GetCurrentDirectory() + "\\plugins"
                , new string[] { "*.exe", "*.dll" });

            foreach (string file in files)
            {
                LoadAssembly(file);
            }
        }

        private string[] SearchFilesFromFilters(string dir, string[] filters)
        {
            List<string> paths = new List<string>();

            foreach (string filter in filters)
            {
                paths.AddRange(System.IO.Directory.GetFiles(dir, filter));
            }

            return paths.ToArray();
        }

        private void LoadAssembly(string path)
        {
            Assembly ass = Assembly.LoadFile(path);
            foreach (Type t in ass.GetTypes())
            {
                CheckAttributes(t);
            }
        }

        private void CheckAttributes(Type t)
        {
            //PluginAttribute from 02. Reflection - A. Custom Attribute
            //include reference!
            foreach (PluginAttribute a in t.GetCustomAttributes(typeof(PluginAttribute)))
            {
                if (a.IsPlugin)
                {
                    MenuItemPlugin item = new MenuItemPlugin(a.Name, t);
                    mnuPlugins.Items.Add(item);
                }
            }
        }
    }
}

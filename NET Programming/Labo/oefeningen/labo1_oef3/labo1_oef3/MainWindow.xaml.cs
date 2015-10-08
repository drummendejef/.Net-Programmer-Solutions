using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Media;
using Microsoft.Win32;
using System.IO;

namespace labo1_oef3
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            readXML();
        }

        private void readXML()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Open XML Document";
            dlg.Filter = "XML Files (*.xml)|*.xml";
            dlg.InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            if (dlg.ShowDialog().Value)
            {
                //XMLViewer viewer = new XMLViewer();
                tvViewer.searchXML(dlg.FileName);
                txtPath.Text = dlg.FileName;
                tvViewer.Items.Clear();
                foreach (TreeNode item in TreeNode.viewItems)
                {
                    tvViewer.Items.Add(item);
                }
            }
        }



        private TreeNode berekenKleur(TreeNode node)
        {
            node.Foreground = Brushes.Black;
            switch (node.niveau)
            {
                case 0:
                    node.Foreground = Brushes.SlateGray;
                    break;
                case 1:
                    node.Foreground = Brushes.Blue;
                    break;
                case 2:
                    node.Foreground = Brushes.Red;
                    break;
                case 3:
                    node.Foreground = Brushes.Green;
                    break;
                default:
                    node.Foreground = Brushes.Black;
                    break;
            }
            return node;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tvViewer.ToonKleur = new toonKleurDelegate(berekenKleur);
        }

    }
}

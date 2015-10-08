using CustomAttributes;
using System.Windows;

namespace Plugins
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    [PluginAttribute(true, "Persoon-plugin")]
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            clsPersoon albert = new clsPersoon() { Naam = "Albert" };

            MessageBox.Show(albert.ToString());
        }
    }
}

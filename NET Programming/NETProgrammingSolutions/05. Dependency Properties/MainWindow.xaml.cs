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

namespace _05.Dependency_Properties
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            animateControl();
        }

        private void animateControl()
        {
            Animation anim = new Animation();
            anim.animateRotation(btninner);
            anim.animateWidth(btninner);
            anim.animateLength(btninner);
            anim.animateBackground(btninner);

            //anim.animeerRotation(btninnerBehavior);
            anim.animateWidth(btninnerBehavior);
            anim.animateLength(btninnerBehavior);
            anim.animateBackground(btninnerBehavior);

            anim.animateFadeInOut(chbXheckbox);
            anim.animateFadeInOut(cboCombobox);
            anim.animateFadeInOut(slSlider);
            anim.animateFadeInOut(imgImage);
        }
    }
}

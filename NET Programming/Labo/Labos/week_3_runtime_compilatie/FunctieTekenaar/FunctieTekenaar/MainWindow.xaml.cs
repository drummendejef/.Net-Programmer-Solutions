using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
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

namespace FunctieTekenaar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CSharpCodeProvider provider;
        private CompilerParameters compParameters;
        private string beginCode;
        private string eindCode;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            provider = new CSharpCodeProvider();
            compParameters = new CompilerParameters();
            //compParameters.ReferencedAssemblies.Add("System.dll");

            beginCode = 
            @" using System;
                namespace MyGeneratedProgram
                {
                    class MyGeneratedClass
                    {
                        public static double returnFX(double dX)
                        {
                            return ";

            eindCode = 
            @"

                           
                    }
                }
            }";
        }

        private void btnTekenFunctie_Click(object sender, RoutedEventArgs e)
        {
            string functie = txtFunctie.Text +";";
            
            string code = beginCode + functie + eindCode;

            CompilerResults compResult = provider.CompileAssemblyFromSource(compParameters, code);

            evalueerCompResult(compResult);
        }

        private void evalueerCompResult(CompilerResults compResult)
        {
            if (compResult.Errors.HasErrors)
            {
                StringBuilder sb = new StringBuilder();

                foreach (CompilerError err in compResult.Errors)
                {
                    sb.AppendLine(String.Format("Error ({0}) : {1}", err.ErrorNumber, err.ErrorText));
                }

                MessageBox.Show(sb.ToString());
            }
            else 
            {
                Assembly assembly = compResult.CompiledAssembly;
                Type klasse = assembly.GetType("MyGeneratedProgram.MyGeneratedClass");
                MethodInfo methode = klasse.GetMethod("returnFX");
                berekenDeFunctie(methode);
                
            }
        }

        private void berekenDeFunctie(MethodInfo methode)
        {
            List<double> resultatenList = new List<double>();
            double resultaat;

            for (int i = 0; i < 300; i++)
            { 
                resultaat = (double) methode.Invoke(null, new object[] { i });
                resultatenList.Add(resultaat);
            }

            toonFunctie(resultatenList);
        }

        private void toonFunctie(List<double> resultatenList)
        {
            Polyline func = new Polyline();
            func.Stroke = Brushes.Black;
            func.StrokeThickness = 1;

            canvasFuncties.Content = func;

            int xRatio = Convert.ToInt32(canvasFuncties.Width) / 300;
            int yRatio = Convert.ToInt32(canvasFuncties.Height / berekenBereik(resultatenList));

            for (int i = 0; i < resultatenList.Count; i++)
            {
                func.Points.Add(new Point((i*xRatio), (resultatenList.ElementAt(i)*yRatio+canvasFuncties.Height/2)));
            }
        }

        private double berekenBereik(List<double> resultatenList)
        {
            double bereik;

            bereik = getMax(resultatenList) - getMin(resultatenList);

            return bereik;
        }

        private double getMax(List<double> resultatenList)
        {
            double maximum = resultatenList.First();

            foreach (double getal in resultatenList)
            {
                if (getal > maximum)
                    maximum = getal;
            }

            return maximum;
        }

        private double getMin(List<double> resultatenList)
        { 
            double minimum = resultatenList.First();

            foreach(double getal in resultatenList)
            {
                if (getal < minimum)
                    minimum = getal;
            }

            return minimum;
        }

    }
}

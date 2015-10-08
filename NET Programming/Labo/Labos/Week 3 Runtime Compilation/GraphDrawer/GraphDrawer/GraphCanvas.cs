using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphDrawer
{
    class GraphCanvas : Canvas
    {
        public void DrawGraph(string function)
        {
            Children.Clear();

            Point[] points = GetPointsFromFunction(function);

            for (int i = 0; i < points.Length - 1; i++)
            {
                Line line = new Line() 
                { 
                    X1 = points[i].X, 
                    X2 = points[i + 1].X,
                    Y1 = points[i].Y,
                    Y2 = points[i + 1].Y,
                    Stroke = Brushes.Black,
                    StrokeThickness = 2
                };

                Children.Add(line);
            }
        }

        private Point[] GetPointsFromFunction(string function)
        {
            Assembly functionAss = CompileFunctionClass(function);
            //namespace verplicht te vermelden
            dynamic functionObject = functionAss.CreateInstance("GraphDrawer.Function");
            Type functionClass = functionAss.GetType("GraphDrawer.Function");

            //calculate X values
            Point[] points = new Point[(int)ActualWidth];
            points = CalculatePointValuesFromFunction(points, functionObject);

            return points;
        }

        private Point[] CalculatePointValuesFromFunction(Point[] points, dynamic functionObject)
        {
            double deltaX = 5 * System.Math.PI / ActualWidth;

            for (int i = 0; i < points.Length; i++)
            {
                points[i].Y = functionObject.RunFunction(i * deltaX);
            }

            double minY = points.Min(p => p.Y);
            double maxY = points.Max(p => p.Y);
            double yScale = ActualHeight / (maxY - minY);

            for (int i = 0; i < points.Length; i++)
            {
                points[i].X = i;
                 points[i].Y = (maxY - points[i].Y) / ((maxY - minY) / ActualHeight);
            }

            return points;
        }

        private Assembly CompileFunctionClass(string function)
        {
            string csharpCode = @"namespace GraphDrawer 
                                 { public class Function 
                                    { 
                                        public double RunFunction(double x) 
                                            { return " + function + @"; 
                                        }   } } ";
            CompilerParameters cparams = new CompilerParameters();
            cparams.GenerateInMemory = true;

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            //als er iets niet werkt (bv file not found error), check in debug de Errors property van results
            CompilerResults results = provider.CompileAssemblyFromSource(cparams, csharpCode);
            return results.CompiledAssembly;
        }
    }
}

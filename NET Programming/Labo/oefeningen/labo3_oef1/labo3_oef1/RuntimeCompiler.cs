using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using System.CodeDom;
using System.IO;
using System.Windows;
using System.Reflection;

namespace labo3_oef1
{
    class RuntimeCompiler
    {
        CodeDomProvider provider;
        string language = "csharp";
        string path = Directory.GetCurrentDirectory() + "/FunctieCompiled.cs";
        public RuntimeCompiler()
        {
            provider = CodeDomProvider.CreateProvider(language);
            CompilerInfo langCompilerInfo = CodeDomProvider.GetCompilerInfo(language);
            CompilerParameters langCompilerConfig = langCompilerInfo.CreateDefaultCompilerParameters();

            CodeCompileUnit ccunit = new CodeCompileUnit();
            /*CodeNamespace cns = new CodeNamespace(language);
            ccunit.Namespaces.Add(cns);*/

            /*CodeTypeDeclaration klas = new CodeTypeDeclaration("FunctieCompiled");
            cns.Types.Add(klas);
            klas.Attributes = MemberAttributes.Public;*/

            IndentedTextWriter tw = new IndentedTextWriter(new StreamWriter(path, false), "");
            //tw.NewLine = getCode();
            //tw.Write(getCode()); 
            provider.GenerateCodeFromCompileUnit(ccunit, tw, new CodeGeneratorOptions());
            //tw.Write(getCode());
            tw.WriteLine(getCode());
            tw.Close();
            run(langCompilerConfig);
        }

        private void run(CompilerParameters langCompilerConfig)
        {
            String textFile;
            StreamReader textReader = new StreamReader(path);
            textFile = textReader.ReadToEnd();

            CompilerResults results = provider.CompileAssemblyFromSource(langCompilerConfig, textFile);
            object o = results.CompiledAssembly.CreateInstance("FunctieCompiled");
            Type type = o.GetType();
            MethodInfo m = type.GetMethod("Main");
            m.Invoke(o, null);
            if (File.Exists("CS-Script-Tmp-Junk")) { File.Delete("CS-Script-Tmp-Junk"); }
        }

        private string getCode()
        {
            return @"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

        public class FunctieCompiled : Canvas
        {
            public static void Main()
            {
            " +
                @"polyLine = new Polyline();
                polyLine.Stroke = Brushes.Red;
                polyLine.StrokeThickness = 1;
                this.Children.Add(polyLine);
                AddPoints();"
        + @"}"
        + @"private void AddPoints()
            {
            " + 
                @"for (int x = 0; x < 90; x ++)
                 {
                  "+ 
                        @"polyLine.Points.Add(new Point(x, 50 + 50 * Math.Sin(x / Math.PI)));"
                + @"
                 }
            }
        }";
        }
        private string getCode2()
        {
            string code = "using System;";
            code += "public class Program{";
            code += "public static void Main(){";
            code += "Console.WriteLine(\"Hello, world!\");}}";
            return code;
        }

        private string getCode3()
        {
            return @"
    using System;

    namespace First
    {
        public class Program
        {
            public static void Main()
            {
            " +
                "Console.WriteLine(\"Hello, world!\");"
                + @"
            }
        }
    }
";
        }
    }
}

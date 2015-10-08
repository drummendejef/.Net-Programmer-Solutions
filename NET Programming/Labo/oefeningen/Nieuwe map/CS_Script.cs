using System;
using System.Windows.Forms;
using System.IO;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;

namespace CS_Script {
	class CS_Script {
		static void Main(string[] args) {
			if (args.Length > 0) {
				if (File.Exists(args[0])) {
					try {
						String textFile;
						using (StreamReader textReader = new StreamReader(args[0])) {
							textFile = textReader.ReadToEnd();
						}
						try {
							ExecuteSource(textFile);
						}	catch (Exception e) {
							MessageBox.Show("The file could not be executed:\n" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}	
					}	catch (Exception e) {
						MessageBox.Show("The file could not be read:\n" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				} else {
					MessageBox.Show("You need to provide an existing file as an argument!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			} else {
				MessageBox.Show("You need to provide the filename as an argument!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		} /* Main */
		
		static void ExecuteSource(String sourceText) {
			CSharpCodeProvider codeProvider = new CSharpCodeProvider();
			
			ICodeCompiler compiler = codeProvider.CreateCompiler();
			CompilerParameters parameters = new CompilerParameters();
			parameters.GenerateExecutable = false;
			parameters.GenerateInMemory = true;
			parameters.OutputAssembly = "CS-Script-Tmp-Junk";
			parameters.MainClass = "CScript.Main";
			parameters.IncludeDebugInformation = false;
			
			foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies()) {
				parameters.ReferencedAssemblies.Add(asm.Location);
			}
			
			CompilerResults results = compiler.CompileAssemblyFromSource(parameters, sourceText);
			
			if (results.Errors.Count > 0) {
				string errors = "Compilation failed:\n";
				foreach (CompilerError err in results.Errors) {
					errors += err.ToString() + "\n";
				}
				MessageBox.Show(errors, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}	else {
				object o = results.CompiledAssembly.CreateInstance("CScript");
				Type type = o.GetType();
				MethodInfo m = type.GetMethod("Main");
				m.Invoke(o, null);
				if (File.Exists("CS-Script-Tmp-Junk")) { File.Delete("CS-Script-Tmp-Junk"); }
			}
		} /* ExecuteSource */
	} /* CS_Script */
} /* CS_Script */
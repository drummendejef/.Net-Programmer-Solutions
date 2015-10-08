using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace labo2_oef1
{
    class ReflectionOld
    {
        public static ArrayList list = new ArrayList();
        public string path = "C:\\Users\\alisio\\Desktop\\howest\\3NMCT\\dotnet\\oefeningen";
        public ReflectionOld()
        {
            verwerkUi();
        }

        private List<String> DirSearch(string sDir)
        {
            List<String> files = new List<String>();
            try
            {
                foreach (string f in Directory.GetFiles(sDir))
                {
                    if (f.Contains(".exe") || f.Contains(".dll")) files.Add(f);
                }
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    files.AddRange(DirSearch(d));
                }
            }
            catch (System.Exception excpt)
            {
                MessageBox.Show(excpt.Message);
            }

            return files;
        }

        private void verwerkUi()
        {
            List<string> files = DirSearch(path);
            foreach (string file in files)
            {
                verwerkUi(file);
            }
        }

        private void verwerkUi(string sAssembly)
        {
            try
            {
                Assembly ass = Assembly.LoadFile(sAssembly);
                foreach (var t in ass.GetCustomAttributes())
                {
                    Console.WriteLine(t.TypeId);
                    list.Add(t);
                    //verwerkInWindow(t);
                    //verwerkKlasse(t);
                    //verwerkPlugIn(t);
                }
            }
            catch (Exception e)
            {

            }
        }

        /*private void verwerkPlugIn(Type t)
        {
            //Console.WriteLine(t.Name);
            //if (t.Name.Equals("PlugIn")) Console.WriteLine(t.Attributes);
            //Boolean isPluging = false;
            Attribute[] attrs = Attribute.GetCustomAttributes(t);
            if (attrs.Count() > 0 && t.Name.Equals("PlugIn"))
            {
                //Console.WriteLine(t.Attributes);
                //if (t.Attributes.Equals("isPlugIn") == true) isPluging = true;
                for (int i = 0; i < attrs.Count();i++)
                {
                    //object o = Activator.CreateInstance(t);
                    //Console.WriteLine(i + " " + attrs[i]);
                    foreach (MethodInfo mi in t.GetMethods()){
                        Console.WriteLine(i + " " + mi.Attributes);
                    }
                }
            }
        }*/

        private void verwerkPlugIn(Type t)
        {
            foreach (MethodInfo mi in t.GetMethods())
            {
                Console.WriteLine(mi);
                list.Add(mi);
            }
        }

        private void verwerkInWindow(Type t)
        {
            if (t.Name.Equals("MainWindow"))
            {
                Window win = (Window)Activator.CreateInstance(t);
                win.Show();
            }
        }

        private void verwerkKlasse(Type t)
        {
            if (t.Name.Equals("clsPersoon"))
            {
                object o = Activator.CreateInstance(t);
                t.InvokeMember("fNaam", BindingFlags.SetProperty, null, o, new object[] { "naam" });
                t.InvokeMember("vNaam", BindingFlags.SetProperty, null, o, new object[] { "voornaam" });

                string sPersoon = (string)t.InvokeMember("ToString", BindingFlags.InvokeMethod, null, o, new object[] { });
                Console.WriteLine("naam: " + sPersoon);
            }
        }
    }
}

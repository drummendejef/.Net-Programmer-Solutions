using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace labo1_oef3
{
    class XMLViewer : TreeView
    {
        public TreeNode node;
        private XmlDocument xDoc;
        public void searchXML(String path)
        {
            xDoc = new XmlDocument();
            xDoc.Load(path);
            berekenNodes(xDoc);
        }


        private void berekenNodes(XmlDocument xDoc)
        {
            node = new TreeNode(xDoc);
            node.ToonKleur = ToonKleur;
            XmlNodeList xNodeList;
            foreach (XmlNode child in xDoc.DocumentElement)
            {
                xNodeList = child.ChildNodes;
                //node.startdeligateToonTekst(child, xNodeList);
                //node.berekenSubNodes(child, xNodeList);
            }
            node.addToViewItems();
        }

        private toonKleurDelegate _toonKleur;
        public toonKleurDelegate ToonKleur
        {
            get { return _toonKleur; }
            set { _toonKleur = value; }
        }
    }
}

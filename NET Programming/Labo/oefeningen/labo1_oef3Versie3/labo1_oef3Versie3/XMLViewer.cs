using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml;

namespace labo1_oef3Versie3
{
    class XMLViewer : TreeView
    {
        public void searchXML(String path)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            berekenNodes(xDoc);
        }

        private void berekenNodes(XmlDocument xDoc)
        {
            TreeNode node = new TreeNode();
            node.ToonKleur = ToonKleur;
            this.Items.Clear();
            this.Items.Add(node.BouwBoom(xDoc));
        }

        private toonKleurDelegate _toonKleur;
        public toonKleurDelegate ToonKleur
        {
            get { return _toonKleur; }
            set { _toonKleur = value; }
        }
    }
}

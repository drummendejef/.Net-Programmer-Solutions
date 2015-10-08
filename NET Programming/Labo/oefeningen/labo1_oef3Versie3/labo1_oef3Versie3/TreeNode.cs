using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;

namespace labo1_oef3Versie3
{
    //public delegate void toonTekstDelegate(XmlDocument doc);
    public delegate TreeNode toonTekstDelegate(XmlDocument doc);
    public delegate TreeNode toonKleurDelegate(TreeNode node);
    public class TreeNode : TreeViewItem
    {
        private toonKleurDelegate _toonKleur;
        public int niveau = -1;

        public toonKleurDelegate ToonKleur
        {
            get { return _toonKleur; }
            set { _toonKleur = value; }
        }

        public TreeNode()
        {
            _toonKleur = new toonKleurDelegate(berekenKleur);
        }

        /*public void BouwBoom(XmlDocument doc)
        {
            toonTekstDelegate routine = new toonTekstDelegate(berekenSubNodes);
            routine.BeginInvoke(doc, null, null);

            berekenSubNodes(doc);
        }*/

        public TreeNode BouwBoom(XmlDocument doc)
        {
            return berekenSubNodes(doc);
        }

        public TreeNode startdeligateToonKleur(TreeNode node)
        {
            return ToonKleur.Invoke(node);
        }

        /*private void berekenSubNodes(XmlDocument doc)
        {
            item.IsExpanded = true;
            item.Header = doc.DocumentElement.Name;
            List<TreeNode> subNodes = new List<TreeNode>();
            foreach (XmlNode xmlNode in doc.DocumentElement)
            {
                subNodes.Add(maakNode(xmlNode, niveau+1));
            }
            item.niveau = niveau+1;
            item.ItemsSource = subNodes;
            if (ToonKleur != null) item = ToonKleur(item);
            else item = berekenKleur(item);
        }*/

        /*private TreeNode maakNode(XmlNode xmlNode, int niv)
        {
            int niveau = niv + 1;
            TreeNode node = new TreeNode();
            node.IsExpanded = true;
            string attribute = "";
            foreach (XmlNode n in xmlNode.Attributes)
            {
                attribute += " " + n.Name + ": " + n.Value;
            }
            node.Header = xmlNode.Name + attribute;
            List<TreeNode> subNodes = new List<TreeNode>();
            foreach (XmlNode n in xmlNode.ChildNodes)
            {
                if (n.NodeType.ToString() != "Text") subNodes.Add(maakNode(n, niveau));
            }
            node.niveau = niveau;
            node.ItemsSource = subNodes;
            if (ToonKleur != null) node = ToonKleur(node);
            else node = berekenKleur(node);
            return node;
        }*/

        /*private void berekenSubNodes(XmlDocument doc)
        {
            item.IsExpanded = true;
            item.Header = doc.DocumentElement.Name;
            foreach (XmlNode xmlNode in doc.DocumentElement)
            {
                item.AddChild(maakNode(xmlNode, niveau + 1));
            }
            item.niveau = niveau + 1;
            if (ToonKleur != null) item = ToonKleur(item);
            else item = berekenKleur(item);
        }*/

        private TreeNode berekenSubNodes(XmlDocument doc)
        {
            TreeNode node = new TreeNode();
            node.IsExpanded = true;
            node.Header = doc.DocumentElement.Name;     //dit zou een delegate moeten zijn
            foreach (XmlNode xmlNode in doc.DocumentElement)
            {
                node.AddChild(maakNode(xmlNode, niveau + 1));
            }
            node.niveau = niveau + 1;
            if (ToonKleur != null) node = ToonKleur(node);
            else node = berekenKleur(node);
            return node;
        }

        private TreeNode maakNode(XmlNode xmlNode, int niv)
        {
            int niveau = niv + 1;
            TreeNode node = new TreeNode();
            node.IsExpanded = true;
            string attribute = "";
            foreach (XmlNode n in xmlNode.Attributes)
            {
                attribute += " " + n.Name + ": " + n.Value;
            }
            node.Header = xmlNode.Name + attribute;
            foreach (XmlNode n in xmlNode.ChildNodes)
            {
                if (n.NodeType.ToString() != "Text") node.AddChild(maakNode(n, niveau));
            }
            node.niveau = niveau;
            if (ToonKleur != null) node = ToonKleur(node);
            else node = berekenKleur(node);
            return node;
        }

        private TreeNode berekenKleur(TreeNode node)
        {
            node.Foreground = Brushes.Black;
            return node;
        }
    }
}

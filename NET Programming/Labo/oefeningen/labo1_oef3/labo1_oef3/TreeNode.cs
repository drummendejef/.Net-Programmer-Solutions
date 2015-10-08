using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;
using System.Xml.Linq;

namespace labo1_oef3
{
    public delegate void toonTekstDelegate(XmlNode element, XmlNodeList nodeList);
    public delegate TreeNode toonKleurDelegate(TreeNode node);
    public class TreeNode : TreeViewItem
    {
        private static List<TreeNode> tempItems = new List<TreeNode>();
        public static List<TreeNode> viewItems = new List<TreeNode>();
        private static XmlDocument _xDoc;
        private toonKleurDelegate _toonKleur;

        public int niveau = -1;

        public TreeNode(XmlDocument xDoc) : this()
        {
            //tempItems = new List<TreeNode>();
            //viewItems = new List<TreeNode>();
            _xDoc = xDoc;
        }

        public toonKleurDelegate ToonKleur
        {
            get { return _toonKleur; }
            set { _toonKleur = value; }
        }


        public TreeNode()
        {
            _toonKleur = new toonKleurDelegate(berekenKleur);
        }

        public void startdeligateToonTekst(XmlNode element, XmlNodeList nodeList)
        {
            toonTekstDelegate routine = new toonTekstDelegate(berekenSubNodes);
            routine.BeginInvoke(element, nodeList, null, null);
        }

        public TreeNode startdeligateToonKleur(TreeNode node)
        {
            //toonKleurDelegate routine = new toonKleurDelegate(berekenKleur);
            //routine.BeginInvoke(node, null, null);
            ToonKleur.Invoke(node);
            return node;
        }

        public void addToViewItems()
        {
            TreeNode treeNode = new TreeNode();
            treeNode.Header = _xDoc.DocumentElement.Name;
            treeNode.ItemsSource = tempItems;
            treeNode.IsExpanded = true;
            treeNode.niveau = 1;
            treeNode = berekenKleur(treeNode);
            viewItems.Add(treeNode);
            Thread.Sleep(10);
        }

        public void berekenSubNodes(XmlNode element, XmlNodeList nodeList)
        {
            List<TreeNode> nodeListTemp = new List<TreeNode>();
            for (int i = 0; i < nodeList.Count; i++)
            {
                TreeNode node = new TreeNode();
                nodeListTemp.Add(berekenInnerNodes(nodeList[i], 3));
            }
            TreeNode treeNode = new TreeNode();
            string attribute = "";
            foreach (XmlNode n in element.Attributes)
            {
                attribute += " " + n.Name + ": " + n.Value;
            }
            treeNode.Header = element.Name + attribute;
            treeNode.ItemsSource = nodeListTemp;
            treeNode.IsExpanded = true;
            treeNode.niveau = 2;
            treeNode = berekenKleur(treeNode);
            tempItems.Add(treeNode);
            Thread.Sleep(10);
        }

        private TreeNode berekenInnerNodes(XmlNode node, int niveau)
        {
            List<TreeNode> nodeListTemp = new List<TreeNode>();
            TreeNode treeNode = new TreeNode();
            string attribute = "";
            foreach (XmlNode n in node.Attributes)
            {
                attribute += " " + n.Name + ": " + n.Value;
            }
            treeNode.Header = node.Name + attribute;
            foreach (XmlNode n in node.ChildNodes)
            {
                TreeNode treeN = new TreeNode();
                if (n.NodeType.ToString() != "Text")
                {
                    treeN.Header = n.Name;
                    treeN.niveau = niveau;
                    treeN = berekenKleur(treeN);
                    nodeListTemp.Add(berekenInnerNodes(n, niveau+1));
                }
                else
                {
                    treeN.Header = n.Value;
                    treeN.niveau = 0;
                    treeN = berekenKleur(treeN);
                    nodeListTemp.Add(treeN);
                }
            }
            treeNode.ItemsSource = nodeListTemp;
            treeNode.niveau = niveau;
            treeNode = berekenKleur(treeNode);
            treeNode.IsExpanded = true;
            return treeNode;
        }

        public TreeNode berekenKleur(TreeNode node)
        {
            node.Foreground = Brushes.Black;
            //switch (node.niveau)
            //{
            //    case 0:
            //        node.Foreground = Brushes.SlateGray;
            //        break;
            //    case 1:
            //        node.Foreground = Brushes.Blue;
            //        break;
            //    case 2:
            //        node.Foreground = Brushes.Red;
            //        break;
            //    case 3:
            //        node.Foreground = Brushes.Green;
            //        break;
            //    default:
            //        node.Foreground = Brushes.Black;
            //        break;
            //}
            return node;
        }
    }
}

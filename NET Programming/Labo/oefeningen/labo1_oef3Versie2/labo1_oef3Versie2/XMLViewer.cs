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
using System.Windows.Controls;
using System.Xml;

namespace labo1_oef3Versie2
{
    class XMLViewer : TreeView
    {
        public static ArrayList list = new ArrayList();
        public XMLViewer()
        {
            XML();
        }

        public void XML()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Open XML Document";
            dlg.Filter = "XML Files (*.xml)|*.xml";
            dlg.InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            if (dlg.ShowDialog().Value)
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(dlg.FileName);
                addElements(xDoc);

                this.Items.Clear();
                TreeNode tNode = new TreeNode();
                this.Items.Add(tNode.getNode(xDoc.DocumentElement.Name));
                //TreeNode tNode = new TreeNode();
                tNode = (TreeNode)this.Items[0];

                addTreeNode(xDoc.DocumentElement, tNode);

                //treeView1.ExpandAll();
            }
        }

        private void addTreeNode(XmlNode xmlNode, TreeNode treeNode)
        {
            XmlNode xNode;
            TreeNode tNode;
            XmlNodeList xNodeList;

            if (xmlNode.HasChildNodes)
            {
                xNodeList = xmlNode.ChildNodes;

                for (int x = 0; x <= xNodeList.Count - 1; x++)
                {
                    xNode = xmlNode.ChildNodes[x];
                    //TreeNode tn = new TreeNode();
                    //list.Add(tn.getNode(xNode.Name));
                    this.Items.Add(treeNode.getNode(xNode.Name));
                    //this.Items.Add("lol");
                    //tNode = treeNode.Nodes[x];
                    tNode = TreeNode.Nodes[x];
                    addTreeNode(xNode, tNode);
                }
            }
        }

        private void addElements(XmlDocument xDoc)
        {
            foreach (XmlNode no in xDoc.DocumentElement)
            {
                TreeNode.elements.Add(no);
            }
        }
    }
}

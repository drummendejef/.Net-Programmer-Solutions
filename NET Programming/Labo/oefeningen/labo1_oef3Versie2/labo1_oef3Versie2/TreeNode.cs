using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml;

namespace labo1_oef3Versie2
{
    class TreeNode : TreeViewItem
    {
        public string Text { get; set; }
        public static int index = 0;

        public static List<XmlNode> elements = new List<XmlNode>();
        public static List<TreeNode> Nodes = new List<TreeNode>();

        /*public TreeNode getNode(string name)
        {
            TreeNode tn = new TreeNode();
            tn.Header = name;
            tn.Text = name;

            for (int i = 0; i < elements.Count(); i++)
            {
                //if (index == 0 || name == elements[i].Name)
                if (name == elements[i].Name)
                {
                    if (!Nodes.Contains(tn))
                    {
                        Nodes.Add(tn);
                        index++;
                    }
                    else
                    {
                        Nodes.Remove(tn);
                    }
                }
            }
            return tn;
        }*/

        public TreeNode getNode(string name)
        {
            TreeNode tn = new TreeNode();
            tn.Header = name;
            tn.Text = name;
            Nodes.Add(tn);
            index++;
            return tn;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;

namespace XMLViewer
{
    static class XmlItemDelegates
    {
        public static Color GetNodeColor(XmlNode node)
        {
            if (node.Name == "book")
                return Colors.Green;

            return Colors.Red;
        }

        public static void ListBookNodes(XmlNodeList nodes, XmlTreeItem parent)
        {
            foreach (XmlNode node in nodes)
            {
                XmlTreeItem item = new XmlTreeItem(node, GetNodeColor, NameBooks);
                parent.Items.Add(item);
            }
        }

        public static string NameBooks(XmlNode node)
        {
            if (node.Name == "book")
            {
                return string.Format("{0}: {1} ({2})", node["author"].InnerText, node["title"].InnerText, node["publish_date"].InnerText);
            }

            return node.Name.ToUpper();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml;

namespace XMLViewer
{
    class XMLItemDelegates
    {
        public static Color GetNodeColor(XmlNode node)
        {
            if(node.Name == "book") {
                return Colors.Green;
            }
            return Colors.Red;
        }

        public static string NameBooks(XmlNode node)
        {
            if (node.Name == "book")
            {
                return string.Format("{0}: {1} ({2})", node["author"].InnerText, node["title"].InnerText, node["publish_date"].InnerText);
            }

            return node.Name.ToUpper();
        }

        public static void ListBookNodes(XmlNodeList nodes, XMLView parent)
        {
            foreach (XmlNode node in nodes)
            {
                XMLViewItem item = new XMLViewItem(node, GetNodeColor, NameBooks);
                parent.Items.Add(item);
            }
        }
    }
}

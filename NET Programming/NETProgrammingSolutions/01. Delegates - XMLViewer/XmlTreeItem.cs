using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;

namespace _01.Delegates___XMLViewer
{
    class XmlTreeItem : TreeViewItem
    {
        public XmlTreeItem(XmlNode node, Func<XmlNode, Color> getNodeColor, Func<XmlNode, string> getNodeName)
        {
            Foreground = new SolidColorBrush(getNodeColor(node));
            Header = getNodeName(node);
        }
    }
}

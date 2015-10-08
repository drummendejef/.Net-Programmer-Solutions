﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;

namespace XMLViewer
{
    class XMLViewItem : TreeViewItem
    {
        public XMLViewItem() { }
        public XMLViewItem(XmlNode node, Func<XmlNode, Color> getNodeColor, Func<XmlNode, string> getNodeName)
        {
            Foreground = new SolidColorBrush(getNodeColor(node));
            Header = getNodeName(node);
        }
    }
}

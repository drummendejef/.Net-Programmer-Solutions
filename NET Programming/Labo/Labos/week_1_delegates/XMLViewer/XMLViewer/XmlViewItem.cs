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
    class XmlViewItem:TreeViewItem
    {
        public XmlViewItem(XmlNode xmlNode, Func<XmlNode,Brush> ColorDelegate, Func<XmlNode,string> NameDelegate)
        {
            this.Foreground = ColorDelegate(xmlNode);
            this.Header = NameDelegate(xmlNode);
        }
    }
}

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
    class XMLView : TreeView
    {
        private List<XMLViewItem> items;
        public List<XMLViewItem> ItemList
        {
            get { return items; }
            set { items = value; }
        }

        public XMLView() { }
        public XMLView(string XMLfile)
        {
            items = new List<XMLViewItem>();

            XmlDocument doc = new XmlDocument();
            doc.Load(XMLfile);

            TraverseNodes(doc.ChildNodes);
        }

        private void TraverseNodes(XmlNodeList list)
        {
            foreach(XmlNode node in list) {
                AddNode(node);
                if (node.ChildNodes.Count > 0 && node.FirstChild.NodeType == XmlNodeType.Element)
                {
                    TraverseNodes(node.ChildNodes);
                }
            }
        }

        private void AddNode(XmlNode node)
        {
            XMLViewItem item = new XMLViewItem(node, ColorItemDelegate, NameItemDelegate);
            items.Add(item);
        }

        private Func<XmlNode, Color> _colorItemDelegate;
        public Func<XmlNode, Color> ColorItemDelegate
        {
            get
            {
                if (_colorItemDelegate == null)
                    return DefaultColorItem;

                return _colorItemDelegate;
            }
            set { _colorItemDelegate = value; }
        }

        private Func<XmlNode, string> _nameItemDelegate;
        public Func<XmlNode, string> NameItemDelegate
        {
            get
            {
                if (_nameItemDelegate == null)
                    return DefaultNameItem;

                return _nameItemDelegate;
            }
            set { _nameItemDelegate = value; }
        }

        //Default values
        private string DefaultNameItem(XmlNode node)
        {
            if (node.NodeType == XmlNodeType.Element)
            {
                if (node.FirstChild.NodeType == XmlNodeType.Element)
                    return node.Name;
                else
                    return node.Name.ToUpper() + " " + node.FirstChild.Value;
            }

            return "";
        }

        private Color DefaultColorItem(XmlNode node)
        {
            return Colors.Black;
        }
        

        private void findLastChild(ref XMLViewItem item, XmlNode node) {

        }
    }
}

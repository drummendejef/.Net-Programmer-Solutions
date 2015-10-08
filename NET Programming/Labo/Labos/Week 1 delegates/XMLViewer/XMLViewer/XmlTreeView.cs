using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;

namespace XMLViewer {

    class XmlTreeView : TreeView {

        private Action<XmlNodeList, XmlTreeItem> _traverseNodesDelegate;
        public Action<XmlNodeList, XmlTreeItem> TraverseNodesDelegate
        {
            get
            {
                if (_traverseNodesDelegate == null)
                    return DefaultTraverseNodes;

                return _traverseNodesDelegate;
            }
            set { _traverseNodesDelegate = value; }
        }

        private Func<XmlNode, Color> _colorItemDelegate;
        public Func<XmlNode, Color> ColorItemDelegate
        {
            get { 
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

        public void LoadXml(string path) {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            XmlTreeItem root = new XmlTreeItem(doc.DocumentElement, ColorItemDelegate, NameItemDelegate);
            Items.Add(root);
            TraverseNodesDelegate(doc.DocumentElement.ChildNodes, root);
        }

        private void DefaultTraverseNodes(XmlNodeList nodes, XmlTreeItem parent) {
            foreach (XmlNode node in nodes) {
                XmlTreeItem item = AddNode(node, parent);

                if (node.ChildNodes.Count > 0 && node.FirstChild.NodeType == XmlNodeType.Element) {
                    DefaultTraverseNodes(node.ChildNodes, item);
                }
            }
        }

        public XmlTreeItem AddNode(XmlNode node, XmlTreeItem parent) {
            XmlTreeItem item = new XmlTreeItem(node, ColorItemDelegate, NameItemDelegate);
            parent.Items.Add(item);

            return item;
        }

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
    }
}

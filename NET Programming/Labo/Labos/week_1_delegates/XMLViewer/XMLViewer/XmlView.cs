using Microsoft.Win32;
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

    class XmlView:TreeView
    {
        #region "properties"

        private Func<XmlNode,Brush> _colorDelegate;

        public Func<XmlNode,Brush> ColorDelegate
        {
            get 
            {
                if (_colorDelegate != null)
                    return _colorDelegate;
                else
                    return DefaultColorDelegate;
            }

            set { _colorDelegate = value; }
        }

        private Func<XmlNode,string> _nameDelegate;

        public Func<XmlNode,string> NameDelegate
        {
            get 
            { 
                if(_nameDelegate!=null)
                    return _nameDelegate;

                return DefaultNameDelegate;
            }
            set { _nameDelegate = value; }
        }

        private Func<XmlNode, bool> _readXmlNodeDelegate;

        public Func<XmlNode, bool> ReadXmlNodeDelegate
        {
            get 
            {
                if (_readXmlNodeDelegate != null)
                    return _readXmlNodeDelegate;

                return DefaultReadXmlNodeDelegate;
            }
            set { _readXmlNodeDelegate = value; }
        }

        private string path;

        public string Path
        {
            get { return path; }
            set 
            { 
                path = value; 

                Console.WriteLine(Path);
                if (path != "")
                    GetAndShowXmlFile(path);
            }
        }

        #endregion

        public string GetFileName()
        {
            Path = "";

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = ".xml|*.xml";
            
            if (ofd.ShowDialog() == true)
            {
                Path = ofd.FileName;
            }

            return Path;
        }

        public void GetAndShowXmlFile(string path)
        {
            //Get xmlDocument
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);

            //Read xmlDocument
            List<TreeViewItem> xmlNodes = ReadXmlDocument(xmlDoc);

            //Clear existing items
            this.Items.Clear();

            //Show xmlDocument
            foreach (TreeViewItem tvi in xmlNodes)
                this.Items.Add(tvi);
        }

        public List<TreeViewItem> ReadXmlDocument(XmlDocument xmlDoc)
        {
            List<TreeViewItem> rootXmlNodes = new List<TreeViewItem>();

            foreach (XmlNode xmlNode in xmlDoc.ChildNodes)
            {
                XmlViewItem rootXmlNode = new XmlViewItem(xmlNode,ColorDelegate,NameDelegate);

                if (ReadXmlNodeDelegate(xmlNode))
                    rootXmlNode.ItemsSource = CreateXmlViewItemsFromXmlNode(xmlNode);

                rootXmlNodes.Add(rootXmlNode);
            }

            return rootXmlNodes; 
        }

        private List<XmlViewItem> CreateXmlViewItemsFromXmlNode(XmlNode xmlNode)
        {
            List<XmlViewItem> xmlViewItems = new List<XmlViewItem>();

            foreach(XmlNode xmlChildNode in xmlNode.ChildNodes)//overloopt alle nodes in de huidige node
            {
                XmlViewItem xmlViewItem = new XmlViewItem(xmlChildNode, ColorDelegate, NameDelegate);//maakt xmlViewItem met de delegates

                if (ReadXmlNodeDelegate(xmlChildNode))//test om te kijken of de child nodes overlopen moeten worden
                {
                    xmlViewItem.ItemsSource = CreateXmlViewItemsFromXmlNode(xmlChildNode);//overloopt child nodes
                }

                xmlViewItems.Add(xmlViewItem);
            }

            return xmlViewItems;
        }

        private bool DefaultReadXmlNodeDelegate(XmlNode xmlNode)
        {
            //DEFAULT: de "CATALOG" en de "CD" nodes moeten uitgebreid worden, anders niet
            if (xmlNode.Name.Equals("CD") || xmlNode.Name.Equals("CATALOG"))
                return true;
            else
                return false;
        }

        private Brush DefaultColorDelegate(XmlNode xmlNode)
        {
            //DEFAULT: alles in het zwart
            return Brushes.Black;
        }

        private string DefaultNameDelegate(XmlNode xmlNode)
        {
            //DEFAULT: enkel "ARTIST", "TITLE", "YEAR" ... nodes krijgen een 'speciale' naamgeving, anders gewoon de name v/d node
            if (xmlNode.Name.Equals("TITLE") || xmlNode.Name.Equals("ARTIST") || xmlNode.Name.Equals("COUNTRY") || xmlNode.Name.Equals("COMPANY") || xmlNode.Name.Equals("PRICE") || xmlNode.Name.Equals("YEAR"))
                return xmlNode.Name + " " + xmlNode.InnerText;
            else
                return xmlNode.Name;
        }

    }
}

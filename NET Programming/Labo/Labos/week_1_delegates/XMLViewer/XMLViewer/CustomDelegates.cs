using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml;

namespace XMLViewer
{
    class CustomDelegates
    {
        public static Brush MyColorDelegate(XmlNode xmlnode)
        {
            if (xmlnode.Name.Equals("CATALOG"))
                return Brushes.Red;
            else if (xmlnode.Name.Equals("CD"))
                return Brushes.Green;
            else
                return Brushes.Blue;
        }

        public static string MyNameDelegate(XmlNode xmlNode)
        {
            if (xmlNode.Name.Equals("CD"))
                return String.Format("{0}: {1} ({2})", xmlNode["ARTIST"].InnerText, xmlNode["TITLE"].InnerText, xmlNode["YEAR"].InnerText);
            else
                return xmlNode.Name;
        }

        public static bool MyReadXmlNodeDelegate(XmlNode xmlNode)
        {
            if (xmlNode.Name.Equals("CATALOG"))
                return true;
            else
                return false;
        }
    }
}

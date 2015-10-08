using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace labo4_oef2
{
    public class PDFHandler
    {
        public FileStream _fs { get; set; }
        public Document _document { get; set; }
        public PdfWriter _writer { get; set; }
        public string Directory
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }
        public BaseFont Font
        {
            get 
            { 
                return BaseFont.CreateFont("c:\\windows\\fonts\\calibri.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            }
        }
    
        public void CreateFile(string path)
        {
            try
            {
                _fs = new FileStream(path, FileMode.Create);
                _document = new Document(PageSize.A4, 25, 25, 30, 30);
                _writer = PdfWriter.GetInstance(_document, _fs);
            }
            catch (Exception e)
            {

            }
        }

        public void OpenPDF()
        {
            _document.Open();
            _writer.Open();
        }

        public void ClosePDF()
        {
            _document.Close();
            _writer.Close();
            _fs.Close();
        }
    }
}

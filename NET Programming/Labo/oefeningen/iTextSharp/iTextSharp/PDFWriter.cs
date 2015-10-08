using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace iTextSharp
{
    class PDFWriter
    {
        private string path = Directory.GetCurrentDirectory();
        private FileStream fs = null;
        private Document document = null;
        private PdfWriter writer = null;

        public void writeSimpleTextToPDF()
        {
            createFile();
            addDucumentInfoToDocument();
            addContentToDocument();
        }

        private void addContentToDocument()
        {
            try
            {
                document.Open();
                document.Add(new Paragraph("Hello World!"));
                document.Close();
                writer.Close();
                fs.Close();
            }
            catch(Exception e){

            }
        }

        private void addDucumentInfoToDocument()
        {
            document.AddAuthor("Micke Blomquist");
            document.AddCreator("Sample application using iTextSharp");
            document.AddKeywords("PDF tutorial education");
            document.AddSubject("Document subject - Describing the steps creating a PDF document");
            document.AddTitle("The document title - PDF creation using iTextSharp");
        }

        private void createFile()
        {
            try
            {
                fs = new FileStream(path + "\\pdf\\first pdf.pdf", FileMode.Create);
                document = new Document(PageSize.A4, 25, 25, 30, 30);
                writer = PdfWriter.GetInstance(document, fs);
            }
            catch(Exception e){

            }
        }
    }
}

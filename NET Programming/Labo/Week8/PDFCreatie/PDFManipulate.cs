using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PDFCreatie
{
    class PDFManipulate : Button
    {
        public PDFManipulate() { }

        protected override void OnClick()
        {
            //base.OnClick();
            CombineMultiplePDFs(new string[] { "First PDF document.pdf", "noedelsoep-met-balletjes.pdf", "pdfSample.pdf" }, "pdfs/test.pdf");
            CombineMultiplePDFs(new string[] { "businesscardsFRONT.pdf", "businesscardsBACK.pdf" }, "pdfs/businesscards.pdf");
        }

        private static void CombineMultiplePDFs(string[] fileNames, string outFile)
        {
            // step 1: creation of a document-object
            Document document = new Document();

            // step 2: we create a writer that listens to the document
            PdfCopy writer = new PdfCopy(document, new FileStream(outFile, FileMode.Create));
            if (writer == null)
            {
                return;
            }

            // step 3: we open the document
            document.Open();

            foreach (string fileName in fileNames)
            {
                // we create a reader for a certain document
                PdfReader reader = new PdfReader(fileName);
                reader.ConsolidateNamedDestinations();

                // step 4: we add content
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    PdfImportedPage page = writer.GetImportedPage(reader, i);
                    writer.AddPage(page);
                }

                PRAcroForm form = reader.AcroForm;
                if (form != null)
                {
                    writer.AddDocument(reader);
                }

                reader.Close();
            }

            // step 5: we close the document and writer
            writer.Close();
            document.Close();
        }
    }
}

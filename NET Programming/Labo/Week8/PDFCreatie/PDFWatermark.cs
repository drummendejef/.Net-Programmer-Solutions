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
    class PDFWatermark : Button
    {

        protected override void OnClick()
        {
            //base.OnClick();
            createPDF();
            manipulatePdf("test2.pdf", "pdfSample.pdf", "businesscardsFRONT.pdf");
        }

          /**
     * Manipulates a PDF file src with the file dest as result
     * @param src the original PDF
     * @param stationery a PDF that will be added as background
     * @param dest the resulting PDF
     * @throws IOException
     * @throws DocumentException
     */
        private void createPDF() {
            string p = System.AppDomain.CurrentDomain.BaseDirectory;
            FileStream fs = new FileStream(p + "\\pdfs\\test.pdf", FileMode.Create);

            Console.WriteLine("PDF Create btn clicked");
            Document document = new Document(PageSize.A4, 0, 0, 0, 0);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);

            //set font
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);

            //need to open doc before writing
            document.Open();
            PdfContentByte cb = writer.DirectContent;
            cb.BeginText();
            cb.SetColorFill(BaseColor.LIGHT_GRAY);
            cb.SetFontAndSize(bfTimes, 24);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "WATERMARKED FTW", 200, 800, 45);
            cb.EndText();

            //close doc
            document.Close();
            //close writer
            writer.Close();
            //close fs
            fs.Close();
        }
        public void manipulatePdf(String src, String stationery, String dest)
        {
            // Create readers
            PdfReader reader = new PdfReader(src);
            PdfReader s_reader = new PdfReader(stationery);
            // Create the stamper
            PdfStamper stamper = new PdfStamper(reader, new FileStream(dest, FileMode.Open));
            // Add the stationery to each page
            PdfImportedPage page = stamper.GetImportedPage(s_reader, 1);
            int n = reader.NumberOfPages;
            PdfContentByte bg;
            for (int i = 1; i <= n; i+=2)
            {
                bg = stamper.GetUnderContent(i);
                //bg.AddTemplate(page, 0, 0);
            }
            // CLose the stamper
            stamper.Close();
            reader.Close();
            s_reader.Close();
        }
    }
}

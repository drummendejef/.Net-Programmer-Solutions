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
    class PDFMultiplePerPage : Button
    {
        protected override void OnClick()
        {
            manipulatePdf("test.pdf", "pdfs/testMPP.pdf", 2);
        }

        public void manipulatePdf(String src, String dest, int pow)
        {
            string p = System.AppDomain.CurrentDomain.BaseDirectory;
            FileStream fs = new FileStream(p + "\\" + dest, FileMode.Create);


            // reader for the src file
            PdfReader reader = new PdfReader(src);
            // initializations
            Rectangle pageSize = reader.GetPageSize(1);
            Rectangle newSize = (pow % 2) == 0 ?
                new Rectangle(pageSize.Width, pageSize.Height) :
                new Rectangle(pageSize.Height, pageSize.Width);
            Rectangle unitSize = new Rectangle(pageSize.Width, pageSize.Height);
            for (int i = 0; i < pow; i++)
            {
                unitSize = new Rectangle(unitSize.Height / 2, unitSize.Width);
            }
            int n = (int)Math.Pow(2, pow);
            int r = (int)Math.Pow(2, pow / 2);
            int c = n / r;
            // step 1
            Document document = new Document(newSize, 0, 0, 0, 0);
            // step 2
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            // step 3
            document.Open();
            // step 4
            PdfContentByte cb = writer.DirectContent;
            PdfImportedPage page;
            Rectangle currentSize;
            float offsetX, offsetY, factor;
            int total = reader.NumberOfPages;
            for (int i = 0; i < total; )
            {
                if (i % n == 0)
                {
                    document.NewPage();
                }
                currentSize = reader.GetPageSize(++i);
                factor = Math.Min(
                    unitSize.Width / currentSize.Width,
                    unitSize.Height / currentSize.Height);
                offsetX = unitSize.Width * ((i % n) % c)
                  + (unitSize.Width - (currentSize.Width * factor)) / 2f;
                offsetY = newSize.Height - (unitSize.Height * (((i % n) / c) + 1))
                  + (unitSize.Height - (currentSize.Height * factor)) / 2f;
                page = writer.GetImportedPage(reader, i);
                cb.AddTemplate(page, factor, 0, 0, factor, offsetX, offsetY);
            }
            // step 5
            document.Close();
            reader.Close();
        }
    }
}

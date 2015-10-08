using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Windows.Media;
using System.Diagnostics;
using labo4_oef2;

namespace iTextSharpNew
{
    public class APUPDFWriter
    {
        Random random = new Random();
        PDFHandler handler = new PDFHandler();

        public void writeSimpleTextToPDF()
        {
            createFile();
            addDucumentInfoToDocument();
            addContentToDocument();
            Process.Start(PDFFile);
        }

        public void scale()
        {
            scalePDF();
            Process.Start(handler.Directory + "\\pdf\\ProgrammerSolutions2.wm.pdf");
        }

        public void mergePDF()
        {
            mergePDF(handler.Directory + "//pdf//", handler.Directory + "//merged//merged.pdf");
            Process.Start(handler.Directory + "//merged//merged.pdf");
        }
        
        private void scalePDF()
        {
            string inPDF = handler.Directory + "\\pdf\\ProgrammerSolutions.wm.pdf";
            string outPDF = handler.Directory + "\\pdf\\ProgrammerSolutions2.wm.pdf";
            PdfReader pdfr = new PdfReader(inPDF);

            Document doc = new Document(PageSize.LETTER);
            Document.Compress = true;

            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(outPDF, FileMode.Create));
            doc.Open();

            PdfContentByte cb = writer.DirectContent;

            PdfImportedPage page;

            int corner = 0;
            for (int i = 1; i < pdfr.NumberOfPages + 1; i++)
            {
                page = writer.GetImportedPage(pdfr, i);
                if (corner == 0) cb.AddTemplate(page, (PageSize.LETTER.Width / pdfr.GetPageSize(i).Width) / 2, 0, 0, (PageSize.LETTER.Height / pdfr.GetPageSize(i).Height) / 2, 0, PageSize.A4.Height / 2);
                if (corner == 1) cb.AddTemplate(page, (PageSize.LETTER.Width / pdfr.GetPageSize(i).Width) / 2, 0, 0, (PageSize.LETTER.Height / pdfr.GetPageSize(i).Height) / 2, PageSize.A4.Width / 2, PageSize.A4.Height / 2);
                if (corner == 2) cb.AddTemplate(page, (PageSize.LETTER.Width / pdfr.GetPageSize(i).Width) / 2, 0, 0, (PageSize.LETTER.Height / pdfr.GetPageSize(i).Height) / 2, 0, 0);
                if (corner == 3) cb.AddTemplate(page, (PageSize.LETTER.Width / pdfr.GetPageSize(i).Width) / 2, 0, 0, (PageSize.LETTER.Height / pdfr.GetPageSize(i).Height) / 2, PageSize.A4.Width / 2, 0);
                if (corner < 3) corner++;
                else
                {
                    corner = 0;
                    doc.NewPage();
                }
            }

            doc.Close();
        }

        public void watermark()
        {
            addWatermark(handler.Directory + "//pdf//ProgrammerSolutions.wm.pdf", handler.Directory + "//pdf//watermark.pdf");
            Process.Start(handler.Directory + "//pdf//watermark.pdf");
        }

        private void addWatermark(string source, string target)
        {
            PdfReader pdfReader = new PdfReader(source);
            FileStream stream = new FileStream(target, FileMode.OpenOrCreate);
            PdfStamper pdfStamper = new PdfStamper(pdfReader, stream);
            for (int pageIndex = 1; pageIndex <= pdfReader.NumberOfPages; pageIndex++)
            {
                Rectangle pageRectangle = pdfReader.GetPageSizeWithRotation(pageIndex);
                PdfContentByte pdfData = pdfStamper.GetUnderContent(pageIndex);
                pdfData.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 40);
                PdfGState graphicsState = new PdfGState();
                graphicsState.FillOpacity = 0.4F;
                pdfData.SetGState(graphicsState);
                pdfData.SetColorFill(BaseColor.BLUE);
                pdfData.BeginText();
                pdfData.ShowTextAligned(Element.ALIGN_CENTER, "watermark!!!", pageRectangle.Width / 2, pageRectangle.Height / 2, 45);
                pdfData.EndText();
            }
            pdfStamper.Close();
            stream.Close();
        }

        static void mergePDF(string sourceDir, string targetPDF)
        {
            FileStream stream = new FileStream(targetPDF, FileMode.Create);
            Document pdfDoc = new Document(PageSize.A4);
            PdfCopy pdf = new PdfCopy(pdfDoc, stream);
            pdfDoc.Open();
            var files = Directory.GetFiles(sourceDir);
            int i = 1;
            foreach (string file in files)
            {
                pdf.AddDocument(new PdfReader(file));
                i++;
            }

            if (pdfDoc != null) pdfDoc.Close();
        }

        private void readFile()
        {
            string[] lines = File.ReadAllLines(handler.Directory + "//extra//info.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                placeTextInColor(lines[i], 10, 400 - (i * 10));
            }
        }

        private void placeImage(string url)
        {
            PdfContentByte cb = handler._writer.DirectContent;
            Image img = Image.GetInstance(url);
            img.SetAbsolutePosition(50, 600);
            img.ScaleAbsolute(150, 150);
            cb.AddImage(img);
            //img.ScalePercent(100);
        }

        private void placeLine(int x1, int y1, int x2, int y2)
        {
            PdfContentByte cb = handler._writer.DirectContent;
            cb.SetLineWidth(0f);
            cb.MoveTo(x1, y1);
            cb.LineTo(x2, y2);
            cb.Stroke();
        }

        private void placeTriangle(int xStart, int yStart)
        {
            int x = xStart;
            int y = yStart;
            //top
            placeLine(x + 30, y - 30, x + 50, y);
            placeLine(x + 50, y, x + 70, y - 30);
            placeLine(x + 30, y - 30, x + 70, y - 30);

            /*//left
            placeLine(x + 20, y - 60, x, y - 30);
            placeLine(x, y - 30, x + 30, y - 30);

            //right
            placeLine(x + 70, y - 30, x + 100, y - 30);
            placeLine(x + 100, y - 30, x + 80, y - 60);

            //bottomLeft
            placeLine(x + 30, y - 30, x + 30, y - 60);*/
        }

        private void placeTextInFont(string text)
        {
            BaseFont f_cb = BaseFont.CreateFont("c:\\windows\\fonts\\calibrib.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            PdfContentByte cb = handler._writer.DirectContent;
            cb.BeginText();
            cb.SetFontAndSize(handler.Font, 30);
            cb.SetTextMatrix(100, 100);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "This is a title!", PageSize.A4.Width / 2, PageSize.A4.Height - 100, 0);
            cb.EndText();
        }

        private void placeTextInColor(string text, int x, int y)
        {
            PdfContentByte cb = handler._writer.DirectContent;
            string[] words = text.Split(' ');
            cb.BeginText();
            cb.SetTextMatrix(x, y);
            cb.SetFontAndSize(handler.Font, 8);
            for (int i = 0; i < words.Length; i++)
            {
                byte r = (byte)random.Next(0, 255);
                byte g = (byte)random.Next(0, 255);
                byte b = (byte)random.Next(0, 255);
                cb.SetColorFill(new BaseColor(r, g, b));
                cb.ShowText(words[i]);
                cb.ShowText(" ");
            }
            cb.EndText();
        }

        private void addContentToDocument()
        {
            try
            {
                handler._document.Open();
                placeTextInFont("center");
                handler._document.Add(new Paragraph("Hello World!"));
                handler._document.Add(new Paragraph("derp"));
                placeImage("https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcQg6g1t3Nmh0EzZb6Q33suZwrvH1TZl0RslbfzlvGe9VxW5Yzc1B8MHtPE");
                placeTriangle(220, 680);
                placeTextInColor("kgggggggggggg jdfjdfjjdjsdfjdfs, sjsdfjsdfjsdfjdf,dsfjdfjdfjsdfsdfj. jdfjdfjdfjdfjsdfjsdf efsdfsdfsddf", 10, 500);
                readFile();
                handler._document.Close();
                handler._writer.Close();
                handler._fs.Close();
            }
            catch (Exception e)
            {

            }
        }

        private void addDucumentInfoToDocument()
        {
            if (handler._document != null)
            {
                handler._document.AddAuthor("Alisio Putman");
                handler._document.AddCreator("Sample application using iTextSharp");
                handler._document.AddKeywords("PDF tutorial education");
                handler._document.AddSubject("Document subject - Describing the steps creating a PDF document");
                handler._document.AddTitle("The document title - PDF creation using iTextSharp");
            }
        }

        private void createFile()
        {
            try
            {
                handler._fs = new FileStream(PDFFile, FileMode.Create);
                handler._document = new Document(PageSize.A4, 25, 25, 30, 30);
                handler._writer = PdfWriter.GetInstance(handler._document, handler._fs);
            }
            catch (Exception e)
            {

            }
        }

        public string PDFFile
        {
            get
            {
                return handler.Directory + "\\pdf\\first pdf.pdf"; ;
            }
        }
    }
}

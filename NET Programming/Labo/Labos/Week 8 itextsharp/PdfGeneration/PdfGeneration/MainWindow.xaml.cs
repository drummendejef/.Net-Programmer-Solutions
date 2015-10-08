using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;

namespace PdfGeneration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private FileStream OpenFileStream(string pdfName)
        {
            return File.Create(AppDomain.CurrentDomain.BaseDirectory + @"\pdf\" + pdfName + ".pdf");
        }

        private void AddMetaInfoToDocument(Document doc)
        {
            // Add meta information to the document 
            doc.AddAuthor("Thomas van den Berge");
            doc.AddCreator("Sample application using iTextSharp");
            doc.AddKeywords("PDF tutorial education");
            doc.AddSubject("Document subject - Describing the steps creating a PDF document");
            doc.AddTitle("The document title - PDF creation using iTextSharp");
        }

        private void BasicPdfClickHandler(object sender, RoutedEventArgs e)
        {
            //open file stream
            using (FileStream fs = OpenFileStream("basic"))
            {
                //define document size and margin
                Document doc = new Document(PageSize.A4, 25, 25, 30, 30);
                //get the writer for the document and filestream
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                AddMetaInfoToDocument(doc);

                // Open the document to enable you to write to the document 
                doc.Open();
                // Add a simple and wellknown phrase to the document in a flow layout manner 
                doc.Add(new iTextSharp.text.Paragraph("Hello World!"));
                // Close the document 
                doc.Close();
                // Close the writer instance 
                writer.Close();
            }
            lblOutput.Text = "Basic pdf created.";
        }

        private void ScaledImageClickHandler(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = "Image files (*.jpg, *.png) | *.jpg;*.png",
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory
            };

            bool? result = ofd.ShowDialog();

            if (result == null) return;

            if (!result.Value) return;

            string imgPath = ofd.FileName;

            using (FileStream fs = OpenFileStream("scaledImage"))
            {
                Document doc = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);

                doc.Open();

                //get the pdf content byte to allow direct positioning
                PdfContentByte contentByte = writer.DirectContent;

                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imgPath);

                img.ScaleAbsolute(150, 150);
                img.SetAbsolutePosition(PageSize.A4.Width - 75, PageSize.A4.Height - 50);

                contentByte.AddImage(img);

                doc.Close();
                writer.Close();
            }
            lblOutput.Text = "Scaled Image pdf created.";
        }

        private void TitleParagraphClickHandler(object sender, RoutedEventArgs e)
        {
            using (FileStream fs = OpenFileStream("titleParagraph"))
            {
                Document doc = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);

                doc.Open();

                PdfContentByte contentByte = writer.DirectContent;

                BaseFont titleFont = BaseFont.CreateFont(@"c:\windows\fonts\calibri.ttf", BaseFont.CP1257, BaseFont.NOT_EMBEDDED);

                contentByte.BeginText();

                contentByte.SetFontAndSize(titleFont, 30);
                contentByte.SetTextMatrix(100, 100);
                contentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "This is a title!", PageSize.A4.Width / 2,
                    PageSize.A4.Height - 100, 0);

                //Text must be ended before anything else can be added in the document
                contentByte.EndText();

                doc.Close();
                writer.Close();
            }
            lblOutput.Text = "Title paragraph pdf created.";
        }

        private void ColouredParagraphsClickHandler(object sender, RoutedEventArgs e)
        {
            using (FileStream fs = OpenFileStream("colouredParagraphs"))
            {
                Document doc = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                AddMetaInfoToDocument(doc);

                doc.Open();

                PdfContentByte contentByte = writer.DirectContent;

                doc.Add(AddColouredParagraph(new BaseColor(0, 0, 0)));
                doc.Add(AddColouredParagraph(new BaseColor(125, 0, 0)));

                doc.Close();
                writer.Close();

            }
            lblOutput.Text = "Coloured paragraphs pdf created.";
        }

        private iTextSharp.text.Paragraph AddColouredParagraph(BaseColor fontColor)
        {
            string text = @"Dit is een paragraaf in een bepaalde kleur. 
                    Dit is een paragraaf in een bepaalde kleur. 
                    Dit is een paragraaf in een bepaalde kleur. 
                    Dit is een paragraaf in een bepaalde kleur. 
                    Dit is een paragraaf in een bepaalde kleur. 
                    Dit is een paragraaf in een bepaalde kleur. 
                    Dit is een paragraaf in een bepaalde kleur. ";
            text = text.Replace(Environment.NewLine, String.Empty).Replace("  ", String.Empty);

            return new iTextSharp.text.Paragraph(text, FontFactory.GetFont("Segoe UI", 20, fontColor));

        }

        private void ColouredWordsClickHandler(object sender, RoutedEventArgs e)
        {
            using (FileStream fs = OpenFileStream("colouredWords"))
            {
                Document doc = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);

                doc.Open();
                string text = @"Dit is een paragraaf waarin elk woord een andere kleur heeft. 
                            Dit is een paragraaf waarin elk woord een andere kleur heeft. 
                            Dit is een paragraaf waarin elk woord een andere kleur heeft. 
                            Dit is een paragraaf waarin elk woord een andere kleur heeft. 
                            Dit is een paragraaf waarin elk woord een andere kleur heeft. ";
                text = text.Replace(Environment.NewLine, String.Empty).Replace("  ", String.Empty);

                doc.Add(CreateColouredWordsParagraph(text));
                doc.Close();
                writer.Close();
            }
            lblOutput.Text = "Coloured words pdf created.";
        }

        private iTextSharp.text.Paragraph CreateColouredWordsParagraph(string text)
        {
            string[] words = text.Split(' ');

            Random randomColorGenerator = new Random();

            iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph();
            foreach (string word in words)
            {
                BaseColor color = new BaseColor(randomColorGenerator.Next(255),
                    randomColorGenerator.Next(255), randomColorGenerator.Next(255));
                Font font = FontFactory.GetFont("Segoe UI", 15, color);

                Chunk chunk = new Chunk(word + " ", font);
                paragraph.Add(chunk);
            }

            return paragraph;
        }

        private void DrawLineClickHandler(object sender, RoutedEventArgs e)
        {
            using (FileStream fs = OpenFileStream("drawLine"))
            {
                Document doc = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);

                doc.Open();

                PdfContentByte contentByte = writer.DirectContent;

                contentByte.SetRGBColorStroke(125, 0, 0);
                contentByte.MoveTo(50, PageSize.A4.Height - 50);
                contentByte.LineTo(250, PageSize.A4.Height - 100);

                contentByte.Stroke();

                doc.Close();
                writer.Close();
            }

            lblOutput.Text = "Draw line pdf created.";
        }

        private async void MergeClickHandler(object sender, RoutedEventArgs e)
        {
            GenerateMergedPdfFile(await OpenPdfFiles(true));

            lblOutput.Text = "Pdf files merged.";
        }

        private async Task<List<byte[]>> OpenPdfFiles(bool multiselect)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = "Pdf files (*.pdf) | *.pdf",
                Multiselect = multiselect
            };

            bool? ofdResult = ofd.ShowDialog();

            if (!ofdResult.HasValue || !ofdResult.Value)
                return null;

            string[] filePaths = ofd.FileNames;

            if (filePaths == null)
                return null;

            //read files into byte arrays
            List<byte[]> files = new List<byte[]>();
            foreach (string path in filePaths)
            {
                using (FileStream fs = File.OpenRead(path))
                {
                    byte[] file = new byte[fs.Length];
                    await fs.ReadAsync(file, 0, file.Length);
                    files.Add(file);
                }
            }

            return files;
        }


        private void GenerateMergedPdfFile(List<byte[]> files)
        {
            using (FileStream fs = OpenFileStream("mergedPdfs"))
            {
                Document doc = new Document();
                PdfWriter writer = new PdfSmartCopy(doc, fs);

                doc.Open();

                PdfReader reader = null;
                for (int i = 0; i < files.Count; i++)
                {
                    reader = new PdfReader(files[i]);
                    for (int p = 1; p < reader.NumberOfPages + 1; p++)
                    {
                        PdfImportedPage page = writer.GetImportedPage(reader, p);
                        ((PdfSmartCopy)writer).AddPage(page);
                    }
                    writer.FreeReader(reader);
                }

                reader.Close();
                writer.Close();
                doc.Close();
            }
        }

        private async void TileClickHandler(object sender, RoutedEventArgs e)
        {
            List<byte[]> files = await OpenPdfFiles(true);
            if (files == null)
                return;

            using (FileStream fs = OpenFileStream("tiledPages"))
            {
                try
                {
                    using (Document doc = new Document(PageSize.A4, 25, 25, 25, 25))
                    {
                        using (PdfWriter writer = PdfWriter.GetInstance(doc, fs))
                        {
                            doc.Open();

                            List<PdfImportedPage> pages = new List<PdfImportedPage>();

                            //loop over all documents to load all pages
                            for (int i = 0; i < files.Count; i++)
                            {
                                PdfReader reader = new PdfReader(files[i]);
                                for (int p = 1; p <= reader.NumberOfPages; p++)
                                    pages.Add(writer.GetImportedPage(reader, p));
                            }

                            //tile pages
                            for (int i = 0; i < pages.Count; i++)
                            {
                                /*
                                if (i % 4 == 0)
                                    doc.NewPage();

                                var matrix = new System.Drawing.Drawing2D.Matrix();
                                matrix.Scale(0.5f, 0.5f);

                                //position page
                                if (i % 4 == 0)
                                    matrix.Translate(0, doc.PageSize.Height);
                                else if (i % 4 == 1)
                                    matrix.Translate(doc.PageSize.Width, doc.PageSize.Height);
                                else if (i % 4 == 2)
                                    matrix.Translate(0, 0);
                                else if (i % 4 == 3)
                                    matrix.Translate(doc.PageSize.Width, 0);
                                 * */
                                if (i % 2 == 0)
                                    doc.NewPage();

                                var matrix = new System.Drawing.Drawing2D.Matrix();
                                matrix.Scale(0.5f, 0.5f);

                                //position page
                                if (i % 2 == 0)
                                    matrix.Translate(0, doc.PageSize.Height);
                                else if (i % 2 == 1)
                                    matrix.Translate(0, 0);

                                writer.DirectContent.AddTemplate(pages[i], matrix);
                            }
                        }
                    }
                } catch(Exception ex) {
                    Console.WriteLine(ex);
                }
            }

            lblOutput.Text = "Pdf pages tiled.";
        }

        private async void WatermarkClickHandler(object sender, RoutedEventArgs e)
        {
            List<byte[]> files = await OpenPdfFiles(false);
            if (files == null)
                return;

            using (FileStream fs = OpenFileStream("watermarked"))
            {
                using (Document doc = new Document(PageSize.A4, 25, 25, 25, 25))
                {
                    using (PdfWriter writer = PdfWriter.GetInstance(doc, fs))
                    {
                        PdfContentByte content = writer.DirectContent;

                        PdfReader reader = new PdfReader(files[0]);

                        for (int i = 1; i <= reader.NumberOfPages; i++)
                        {
                            //add watermark to every page
                        }

                    }
                }
            }
        }
    }
}

using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace iTextSharpExample
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

        //button click
        private async void PdfClickHandler(object sender, RoutedEventArgs e)
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
                                if (i % 2 == 0)
                                    doc.NewPage();

                                PdfContentByte contentByte = writer.DirectContent;
                                BaseFont font = BaseFont.CreateFont(
                                    @"c:\windows\fonts\calibri.ttf", 
                                    BaseFont.CP1257, 
                                    BaseFont.NOT_EMBEDDED
                                );

                                //add line
                                contentByte.SetRGBColorStroke(0, 0, 0);
                                contentByte.MoveTo(PageSize.A4.Width / 2, 0);
                                contentByte.LineTo(PageSize.A4.Width / 2, PageSize.A4.Height);
                                contentByte.Stroke();

                                var matrix = new System.Drawing.Drawing2D.Matrix();
                                matrix.Scale(0.48f, 0.48f);

                                //position page
                                if (i % 2 == 0)
                                {
                                    matrix.Translate(0, doc.PageSize.Height);
                                }
                                else if (i % 2 == 1)
                                {
                                    matrix.Translate(0, 0);
                                }
                                writer.DirectContent.AddTemplate(pages[i], matrix);

                                //add text
                                contentByte.BeginText();
                                contentByte.SetFontAndSize(font, 14);
                                contentByte.SetTextMatrix(10, PageSize.A4.Width / 2 + 10);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Notities", PageSize.A4.Width / 2 + 50, PageSize.A4.Height - 100, 0);
                                contentByte.EndText();
                                contentByte.BeginText();
                                contentByte.SetFontAndSize(font, 14);
                                contentByte.SetTextMatrix(10, PageSize.A4.Width / 2 + 10);
                                contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Notities 2", PageSize.A4.Width / 2 + 50, PageSize.A4.Height / 2 - 100, 0);
                                contentByte.EndText();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        //handle filestream
        private FileStream OpenFileStream(string pdfName)
        {
            return File.Create(AppDomain.CurrentDomain.BaseDirectory + @"\pdf\" + pdfName + ".pdf");
        }

        //add meta information
        private void AddMetaInfoToDocument(Document doc)
        {
            // Add meta information to the document 
            doc.AddAuthor("Celine Gardier");
            doc.AddCreator("Sample application using iTextSharp");
            doc.AddKeywords("PDF tutorial education");
            doc.AddSubject("PDF created for lab exam Programming solutions");
            doc.AddTitle("Lab exam Programming solutions");
        }

        //open multiple pdf's (or not)
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
    }
}

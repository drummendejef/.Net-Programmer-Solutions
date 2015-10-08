using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PDFCreatie
{
    class PDFCreate : Button
    {
        private int _y = 829; //line height 12p

        public string Title { get; set; }
        public string Author { get; set; }
        public string Keywords { get; set; }
        public string Subject { get; set; }

        public Rectangle Pagesize { get; set; }
        public int MarginLeft { get; set; }
        public int MarginRight { get; set; }
        public int MarginTop { get; set; }
        public int MarginBottom { get; set; }

        public PDFCreate()
        {
            Console.WriteLine("PDF Create btn created");
        }

        public void StandardValues()
        {
            this.Pagesize = PageSize.A4;
            this.MarginLeft = 25;
            this.MarginRight = this.MarginLeft;
            this.MarginTop = 30;
            this.MarginBottom = this.MarginTop;
        }

        protected override void OnClick()
        {
            string p = System.AppDomain.CurrentDomain.BaseDirectory;
            FileStream fs = new FileStream(
                p + "\\pdfs\\" + EscapeName(Title) + ".pdf", 
                FileMode.Create);

            //base.OnClick();
            Console.WriteLine("PDF Create btn clicked");
            // create document class instance ( = pdf)
            Document document = new Document(
                Pagesize, 
                MarginLeft, 
                MarginRight, 
                MarginTop, 
                MarginBottom);
            //writer class using doc and filestream in constructor
            PdfWriter writer = PdfWriter.GetInstance(document, fs);

            //meta information
            document.AddAuthor(Author);
            document.AddCreator("Application by Celine Gardier using iTextSharp");
            document.AddKeywords(Keywords);
            document.AddSubject(Subject);
            document.AddTitle(Title);

            //set font
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            Font fontTitle = new Font(bfTimes, 24, Font.ITALIC);
            Font fontText = new Font(bfTimes, 12, Font.NORMAL);
            Font blackListTextFont = FontFactory.GetFont("Arial", 28);
            Font redListTextFont = FontFactory.GetFont("Arial", 28);

            //need to open doc before writing
            document.Open();
            Paragraph title = new Paragraph(Title, fontTitle);
            title.Alignment = Element.ALIGN_CENTER;
            document.Add(title);
            document.Add(new Paragraph("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent interdum nunc ac aliquam finibus.", fontText));
            Chunk c1 = new Chunk("A chunk represents an isolated string. ");
            for (int i = 1; i < 4; i++)
            {
                document.Add(c1);
            }


            //place stuff where you want to 
            PdfContentByte cb = writer.DirectContent;
            cb.BeginText();
            
            //add image
            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance("http://student.howest.be/celine.gardier/img/gallery/3dcar/001.jpg");
            img.SetAbsolutePosition(-200, 0);
            cb.AddImage(img);
            
            img.ScaleAbsolute(150, 150);
            //img.ScalePercent(50);

            cb.EndText();

            //close doc
            document.Close();
            //close writer
            writer.Close();
            //close fs
            fs.Close();
        }

        /// <summary>
        /// Convert a name into a string that can be appended to a Uri.
        /// </summary>
        private static string EscapeName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                name = NormalizeString(name);

                // Replaces all non-alphanumeric character by a space
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < name.Length; i++)
                {
                    builder.Append(char.IsLetterOrDigit(name[i]) ? name[i] : ' ');
                }

                name = builder.ToString();

                // Replace multiple spaces into a single dash
                name = Regex.Replace(name, @"[ ]{1,}", @"-", RegexOptions.None);
            }

            return name;
        }

        /// <summary>
        /// Strips the value from any non english character by replacing thoses with their english equivalent.
        /// </summary>
        /// <param name="value">The string to normalize.</param>
        /// <returns>A string where all characters are part of the basic english ANSI encoding.</returns>
        /// <seealso cref="http://stackoverflow.com/questions/249087/how-do-i-remove-diacritics-accents-from-a-string-in-net"/>
        private static string NormalizeString(string value)
        {
            string normalizedFormD = value.Normalize(NormalizationForm.FormD);
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < normalizedFormD.Length; i++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(normalizedFormD[i]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    builder.Append(normalizedFormD[i]);
                }
            }

            return builder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}

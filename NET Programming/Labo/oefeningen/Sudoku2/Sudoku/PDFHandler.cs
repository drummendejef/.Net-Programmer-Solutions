using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class PDFHandler
    {
        public FileStream _fs { get; set; }
        public Document _document { get; set; }
        public PdfWriter _writer { get; set; }

        private string _directory;
        public string Directory
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        private BaseFont _font;
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

        public void MakeGrid(int[,] bord, int sizeTile, int size, Boolean[,] bordIsEditable)
        {
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    MakeRectangle(row * sizeTile / 2, col * sizeTile / 2, sizeTile, bord[row, col], bordIsEditable[row, col]);
                }
            }
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    if (row < 3 && col < 3) MakeOuterRectangle(row * sizeTile / 2 * 3, col * sizeTile / 2 * 3, sizeTile * 3);
                }
            }
        }

        private void MakeOuterRectangle(int row, int col, int size)
        {
            row += 650;
            size /= 2;
            col += 10;
            PlaceLine(col, row, col + size, row, new BaseColor(0, 0, 255));
            PlaceLine(col + size, row, col + size, row - size, new BaseColor(0, 0, 255));
            PlaceLine(col + size, row - size, col, row - size, new BaseColor(0, 0, 255));
            PlaceLine(col, row - size, col, row, new BaseColor(0, 0, 255));
        }

        private void MakeRectangle(int row, int col, int size, int tile, Boolean isEditable)
        {
            row += 600;
            size /= 2;
            col += 10;
            PlaceLine(col, row, col + size, row, new BaseColor(0, 0, 0));
            PlaceLine(col + size, row, col + size, row - size, new BaseColor(0, 0, 0));
            PlaceLine(col + size, row - size, col, row - size, new BaseColor(0, 0, 0));
            PlaceLine(col, row - size, col, row, new BaseColor(0, 0, 0));

            PdfContentByte cb = _writer.DirectContent;
            cb.BeginText();
            cb.SetFontAndSize(Font, 8);
            if (isEditable) cb.SetColorFill(new BaseColor(0, 0, 0));
            else cb.SetColorFill(new BaseColor(0, 0, 255));
            cb.SetTextMatrix(col + (size / 2) - 2, row - (size / 2) - 2);
            cb.ShowText(tile.ToString());
            cb.EndText();
        }

        private void PlaceLine(int x1, int y1, int x2, int y2, BaseColor color)
        {
            PdfContentByte cb = _writer.DirectContent;
            cb.SetColorStroke(color);
            cb.SetLineWidth(1f);
            cb.MoveTo(x1, y1);
            cb.LineTo(x2, y2);
            cb.Stroke();
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

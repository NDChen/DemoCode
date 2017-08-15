using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace PdfGenerate
{
    class Program
    {
        private static readonly Font BodyFont = new Font(Font.FontFamily.HELVETICA, 12.0f, Font.NORMAL, BaseColor.BLACK);

        static void Main(string[] args)
        {
            var doc = new Document(PageSize.A4, 60, 60, 60, 60);
            string path = "E:/Test";
            PdfWriter.GetInstance(doc, new FileStream(path + "/Doc2.pdf", FileMode.Create));
            doc.Open();

            BaseFont baseFont = null;
            var headlingFont = new Font(baseFont, 16.0f, Font.NORMAL, BaseColor.BLACK);
            var bodyFont = new Font(baseFont, 12.0f, Font.NORMAL, BaseColor.BLACK);

            Image pngCtrip = Image.GetInstance(path + "/test.png");
            pngCtrip.ScalePercent(50);
            doc.Add(pngCtrip);


            PdfPTable table = new PdfPTable(2)
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                SpacingBefore = 16f,
                WidthPercentage = 100
            };

            table.AddCell(GetCell("From: sha sha da jiudian sha sha da jiudian sha sha da jiudian", BodyFont));
            table.AddCell(GetCell("NO: 20170807151123", BodyFont));
            table.AddCell(GetCell("To: sha sha da jiudian", BodyFont));
            table.AddCell(GetCell("Date: 2017-08-07", BodyFont));
            table.AddCell(GetCell("Address: sha sha da jiudian", BodyFont));
            table.AddCell(GetCell("", BodyFont));
            doc.Add(table);

            doc.Add(SetNewLine());

            Chunk chunk = new Chunk("Invoice", headlingFont);
            chunk.SetUnderline(1, -6);
            Paragraph para = new Paragraph(chunk) { Alignment = Rectangle.ALIGN_CENTER };
            doc.Add(para);

            table = new PdfPTable(4)
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                SpacingBefore = 16f,
                WidthPercentage = 100
            };
            table.SetWidths(new int[] { 25, 25, 25, 25 });

            table.AddCell(GetTitleCell("Date", bodyFont));
            table.AddCell(GetTitleCell("Description", bodyFont));
            table.AddCell(GetTitleCell("Currency", bodyFont));
            table.AddCell(GetTitleCell("Amount", bodyFont));

            table.AddCell(GetContentCell("201708", BodyFont));
            table.AddCell(GetContentCell("Commission", bodyFont));
            table.AddCell(GetContentCell("EUR", BodyFont));
            table.AddCell(GetContentCell("39.14", BodyFont));

            doc.Add(table);
            doc.Add(SetNewLine());

            chunk = new Chunk("Our accounting details:", headlingFont);
            chunk.SetUnderline(1, -6);
            para = new Paragraph(chunk);
            doc.Add(para);

            doc.Add(GetPara("Bank Name: bank of America we Res zheme de shijian risk ", BodyFont));
            doc.Add(GetPara("Bank Address: RM, HongKong wersfhH SDs shk", BodyFont));
            doc.Add(GetPara("Account No: 714534212", BodyFont));
            doc.Add(GetPara("Account Name: Ctrip International", BodyFont));

            Image pngCWB = Image.GetInstance(path + "/stamp.png");
            pngCWB.SetAbsolutePosition(420, 360);
            doc.Add(pngCWB);

            doc.Close();

        }

        private static Paragraph SetNewLine()
        {
            return new Paragraph(new Chunk(" ", BodyFont));
        }

        private static Paragraph GetPara(string value, Font font)
        {
            Chunk chunk = new Chunk(value, font);
            return new Paragraph(chunk);
        }

        private static PdfPCell GetCell(string value, Font font)
        {
            var phrase = new Phrase(value, font);
            var cell = new PdfPCell(phrase) { Border = 0 };
            cell.Padding = 4f;
            return cell;
        }

        private static PdfPCell GetTitleCell(string value, Font font)
        {
            var phrase = new Phrase(value, font);
            var cell = new PdfPCell(phrase);
            cell.Padding = 4f;
            return cell;
        }

        private static PdfPCell GetContentCell(string value, Font font)
        {
            var phrase = new Phrase(value, font);
            var cell = new PdfPCell(phrase);
            cell.Padding = 3f;
            return cell;
        }
    }
}

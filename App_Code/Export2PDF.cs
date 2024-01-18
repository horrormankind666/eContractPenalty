/*
=============================================
Author      : <ยุทธภูมิ ตวันนา>
Create date : <๑๔/๑๑/๒๕๖๔>
Modify date : <๑๖/๐๑/๒๕๖๗>
Description : <สำหรับใช้งานเกี่ยวกับการใช้งานฟังก์ชั่นการส่งออกข้อมูลเป็นเอกสาร PDF>
=============================================
*/

using System;
using System.IO;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace NExport2PDF {
    public class Export2PDF {
        private static PdfReader reader;
        private static Document document;
        private static PdfWriter writer;
        private static PdfContentByte cb;
        private static Image img;
        private static FileStream fs;

        public void ExportToPDFConnect(string fileName) {
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
        }

        public void ExportToPDFConnectWithSaveFile(string fileName) {
            fs = new FileStream(fileName, FileMode.Create);
        }

        public void PDFConnectTemplate(
            string template,
            string type,
            bool newPage = false
        ) {
            string pdfTemplate = HttpContext.Current.Server.MapPath(template);

            if (type.Equals("pdf") ||
                type.Equals("pdfwithsavefile")) {
                reader = new PdfReader(pdfTemplate);

                if (newPage.Equals(false)) {
                    document = new Document();

                    if (type.Equals("pdf"))
                        writer = PdfWriter.GetInstance(document, HttpContext.Current.Response.OutputStream);

                    if (type.Equals("pdfwithsavefile"))
                        writer = PdfWriter.GetInstance(document, fs);
                
                    document.Open();
                    cb = writer.DirectContent;
                }
            }
        }

        public void PDFAddTemplate(
            string type,
            int pageNumber,
            int rotation
        ) {
            if (type.Equals("pdf")) {
                document.SetPageSize(rotation.Equals(1) ? PageSize.A4 : PageSize.A4.Rotate());
                PdfImportedPage _page = writer.GetImportedPage(reader, pageNumber);
                PDFNewPage();
                cb.AddTemplate(_page, 0, 0);
            }
        }

        public void PDFNewPage() {
            document.NewPage();
        }

        public void FillForm(
            string font,
            float fontSize,
            int align,
            string text,
            float x,
            float y,
            float width,
            float height
        ) {
            PdfPTable table = new PdfPTable(1);
            PdfPCell cell;
            string pdfFont = HttpContext.Current.Server.MapPath(font);

            table.TotalWidth = width;
            BaseFont bf = BaseFont.CreateFont(pdfFont, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            cell = new PdfPCell(new Phrase(text, new Font(bf, fontSize, Font.NORMAL, BaseColor.BLACK)));
            cell.SetLeading(0.0f, 0.7f);
            cell.Border = 0;
            /*
            cell.BorderWidthLeft = 1;
            cell.BorderWidthTop = 1;
            cell.BorderWidthRight = 1;
            cell.BorderWidthBottom = 1;
            */
            cell.HorizontalAlignment = align;
            cell.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
            cell.NoWrap = false;
            cell.FixedHeight = height;
            cell.MinimumHeight = height;
            table.AddCell(cell);
            table.WriteSelectedRows(0, -1, x, y, cb);
        }

        public void FillFormCellTop(
            string font,
            float fontSize,
            int align,
            string text,
            float x,
            float y,
            float width,
            float height
        ) {
            PdfPTable table = new PdfPTable(1);
            PdfPCell cell;
            string pdfFont = HttpContext.Current.Server.MapPath(font);

            table.TotalWidth = width;
            BaseFont _bf = BaseFont.CreateFont(pdfFont, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            cell = new PdfPCell(new Phrase(text, new Font(_bf, fontSize, Font.NORMAL, BaseColor.BLACK)));
            cell.Border = 0;
            cell.HorizontalAlignment = align;
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
            cell.NoWrap = false;
            cell.FixedHeight = height;
            cell.MinimumHeight = height;
            table.AddCell(cell);
            table.WriteSelectedRows(0, -1, x, y, cb);
        }

        public void ShowMessage(
            string font,
            float fontSize,
            int align,
            string text,
            float x,
            float y,
            float width,
            float height,
            float lineHeight
        ) {
            string pdfFont = HttpContext.Current.Server.MapPath(font);
            BaseFont bf = BaseFont.CreateFont(pdfFont, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            ColumnText ct = new ColumnText(cb);
            Phrase myText = new Phrase(text, new Font(bf, fontSize, Font.NORMAL, BaseColor.BLACK));
            ct.SetSimpleColumn(myText, x, y, width, height, lineHeight, align);
            ct.Go();
        }

        public void FillFormImage(
            int align,
            string source,
            string imgFile,
            float x,
            float y,
            float width,
            float height
        ) {
            try {
                PdfPTable table = new PdfPTable(1);
                PdfPCell cell;

                switch (source) {
                    case "url":
                        img = Image.GetInstance(new Uri(imgFile));
                        break;
                    case "file":
                        img = Image.GetInstance(HttpContext.Current.Server.MapPath(imgFile));
                        break;
                }

                table.TotalWidth = width;
                img.ScaleToFit(width, height);
                cell = new PdfPCell(img);
                cell.Border = 0;
                cell.HorizontalAlignment = align;
                cell.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
                cell.NoWrap = false;
                cell.FixedHeight = height;
                cell.MinimumHeight = height;
                cell.AddElement(img);
                table.AddCell(cell);
                table.WriteSelectedRows(0, -1, x, y, cb);
            }
            catch {
            }
        }

        public void CreateTable(
            float x,
            float y,
            float width,
            float height,
            float borderLeft,
            float borderTop,
            float borderRight,
            float borderBottom
        ) {
            PdfPTable table = new PdfPTable(1);
            PdfPCell cell;

            table.TotalWidth = width;
            cell = new PdfPCell();
            cell.BorderWidthLeft = borderLeft;
            cell.BorderWidthTop = borderTop;
            cell.BorderWidthRight = borderRight;
            cell.BorderWidthBottom = borderBottom;
            cell.NoWrap = false;
            cell.FixedHeight = height;
            cell.MinimumHeight = height;
            table.AddCell(cell);
            table.WriteSelectedRows(0, -1, x, y, cb);
        }

        public void ExportToPdfDisconnect() {
            reader.Close();
            document.Close();
            writer.Close();

            HttpContext.Current.Response.End();
        }

        public void ExportToPdfDisconnectWithSaveFile() {
            reader.Close();
            document.Close();
            writer.Close();
        }
    }
}

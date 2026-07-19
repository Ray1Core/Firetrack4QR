using Firetrack.Models;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.IO;

namespace Firetrack.Services
{
    public class PdfGenerationService
    {
        public byte[] GenerateIcsPdf(EquipmentModel equipment, UserModel officer, UserModel issuer)
        {
            using var document = new PdfDocument();
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);

            // Set up fonts and positions
            var titleFont = new XFont("Arial", 18, XFontStyleEx.Bold);
            var headerFont = new XFont("Arial", 12, XFontStyleEx.Bold);
            var bodyFont = new XFont("Arial", 10, XFontStyleEx.Regular);
            var labelFont = new XFont("Arial", 10, XFontStyleEx.Bold);

            double yPos = 40;
            const double leftMargin = 50;
            const double rightMargin = 50;
            double pageWidth = page.Width;

            // ===== HEADER =====
            // Title
            gfx.DrawString("BUREAU OF FIRE PROTECTION", titleFont, XBrushes.Black,
                new XRect(0, yPos, pageWidth, 30), XStringFormats.TopCenter);
            yPos += 35;

            gfx.DrawString("CEBU CITY FIRE STATION", headerFont, XBrushes.Black,
                new XRect(0, yPos, pageWidth, 25), XStringFormats.TopCenter);
            yPos += 30;

            gfx.DrawString("INVENTORY CUSTODIAN SLIP (ICS)", headerFont, XBrushes.Black,
                new XRect(0, yPos, pageWidth, 25), XStringFormats.TopCenter);
            yPos += 35;

            // Horizontal line
            gfx.DrawLine(XPens.Black, leftMargin, yPos, pageWidth - rightMargin, yPos);
            yPos += 20;

            // ===== ISSUANCE DETAILS =====
            // ICS Number (auto-generated)
            var icsNumber = $"ICS-{DateTime.Now:yyyyMMdd}-{equipment.EquipmentId:D4}";
            gfx.DrawString($"ICS No.: {icsNumber}", bodyFont, XBrushes.Black,
                new XRect(leftMargin, yPos, pageWidth - leftMargin - rightMargin, 20), XStringFormats.TopLeft);
            yPos += 25;

            gfx.DrawString($"Date Issued: {DateTime.Now:MMMM dd, yyyy}", bodyFont, XBrushes.Black,
                new XRect(leftMargin, yPos, pageWidth - leftMargin - rightMargin, 20), XStringFormats.TopLeft);
            yPos += 30;

            // ===== EQUIPMENT DETAILS =====
            gfx.DrawString("EQUIPMENT DETAILS", headerFont, XBrushes.Black,
                new XRect(leftMargin, yPos, pageWidth - leftMargin - rightMargin, 20), XStringFormats.TopLeft);
            yPos += 25;

            // Draw a simple table
            double col1Width = 120;
            double col2Width = pageWidth - leftMargin - rightMargin - col1Width - 10;

            // Row 1: QR Code
            gfx.DrawString("QR Code:", labelFont, XBrushes.Black,
                new XRect(leftMargin, yPos, col1Width, 20), XStringFormats.TopLeft);
            gfx.DrawString(equipment.QRCode, bodyFont, XBrushes.Black,
                new XRect(leftMargin + col1Width + 10, yPos, col2Width, 20), XStringFormats.TopLeft);
            yPos += 22;

            // Row 2: Name
            gfx.DrawString("Equipment Name:", labelFont, XBrushes.Black,
                new XRect(leftMargin, yPos, col1Width, 20), XStringFormats.TopLeft);
            gfx.DrawString(equipment.Name, bodyFont, XBrushes.Black,
                new XRect(leftMargin + col1Width + 10, yPos, col2Width, 20), XStringFormats.TopLeft);
            yPos += 22;

            // Row 3: Type
            gfx.DrawString("Type:", labelFont, XBrushes.Black,
                new XRect(leftMargin, yPos, col1Width, 20), XStringFormats.TopLeft);
            gfx.DrawString(equipment.Type, bodyFont, XBrushes.Black,
                new XRect(leftMargin + col1Width + 10, yPos, col2Width, 20), XStringFormats.TopLeft);
            yPos += 22;

            // Row 4: Status
            gfx.DrawString("Status:", labelFont, XBrushes.Black,
                new XRect(leftMargin, yPos, col1Width, 20), XStringFormats.TopLeft);
            gfx.DrawString(equipment.Status, bodyFont, XBrushes.Black,
                new XRect(leftMargin + col1Width + 10, yPos, col2Width, 20), XStringFormats.TopLeft);
            yPos += 30;

            // ===== CUSTODIAN DETAILS =====
            gfx.DrawString("CUSTODIAN INFORMATION", headerFont, XBrushes.Black,
                new XRect(leftMargin, yPos, pageWidth - leftMargin - rightMargin, 20), XStringFormats.TopLeft);
            yPos += 25;

            // Row 1: Name
            gfx.DrawString("Custodian Name:", labelFont, XBrushes.Black,
                new XRect(leftMargin, yPos, col1Width, 20), XStringFormats.TopLeft);
            gfx.DrawString(officer.FullName, bodyFont, XBrushes.Black,
                new XRect(leftMargin + col1Width + 10, yPos, col2Width, 20), XStringFormats.TopLeft);
            yPos += 22;

            // Row 2: Username / ID
            gfx.DrawString("Officer ID:", labelFont, XBrushes.Black,
                new XRect(leftMargin, yPos, col1Width, 20), XStringFormats.TopLeft);
            gfx.DrawString(officer.Username, bodyFont, XBrushes.Black,
                new XRect(leftMargin + col1Width + 10, yPos, col2Width, 20), XStringFormats.TopLeft);
            yPos += 22;

            // Row 3: Role
            gfx.DrawString("Role:", labelFont, XBrushes.Black,
                new XRect(leftMargin, yPos, col1Width, 20), XStringFormats.TopLeft);
            gfx.DrawString(officer.Role, bodyFont, XBrushes.Black,
                new XRect(leftMargin + col1Width + 10, yPos, col2Width, 20), XStringFormats.TopLeft);
            yPos += 30;

            // ===== ISSUING OFFICER =====
            gfx.DrawString("ISSUING OFFICER", headerFont, XBrushes.Black,
                new XRect(leftMargin, yPos, pageWidth - leftMargin - rightMargin, 20), XStringFormats.TopLeft);
            yPos += 25;

            gfx.DrawString($"Name: {issuer.FullName}", bodyFont, XBrushes.Black,
                new XRect(leftMargin, yPos, pageWidth - leftMargin - rightMargin, 20), XStringFormats.TopLeft);
            yPos += 22;

            gfx.DrawString($"Position: {issuer.Role}", bodyFont, XBrushes.Black,
                new XRect(leftMargin, yPos, pageWidth - leftMargin - rightMargin, 20), XStringFormats.TopLeft);
            yPos += 30;

            // ===== SIGNATURE SECTION =====
            yPos += 10;
            gfx.DrawLine(XPens.Black, leftMargin, yPos, leftMargin + 150, yPos);
            gfx.DrawString("Custodian Signature", bodyFont, XBrushes.Black,
                new XRect(leftMargin, yPos + 5, 150, 15), XStringFormats.TopCenter);

            gfx.DrawLine(XPens.Black, pageWidth - rightMargin - 150, yPos, pageWidth - rightMargin, yPos);
            gfx.DrawString("Issuing Officer Signature", bodyFont, XBrushes.Black,
                new XRect(pageWidth - rightMargin - 150, yPos + 5, 150, 15), XStringFormats.TopCenter);

            // ===== FOOTER =====
            yPos = page.Height - 40;
            gfx.DrawLine(XPens.Black, leftMargin, yPos, pageWidth - rightMargin, yPos);
            yPos += 15;

            gfx.DrawString("This is a system-generated document. Wet signature required for official use.",
                bodyFont, XBrushes.Black,
                new XRect(0, yPos, pageWidth, 15), XStringFormats.TopCenter);

            // ===== GENERATE PDF =====
            using var stream = new MemoryStream();
            document.Save(stream, false);
            return stream.ToArray();
        }
    }
}
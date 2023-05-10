using System.IO;
using QRCoder;
using iTextSharp.text;
using iTextSharp.text.pdf;

public class MakePDF
{
    public void createBallot(string candidateName, string electionName, int ballotId)
    {
        Document document = new Document(PageSize.A4);

        using (FileStream stream = new FileStream("Vote.pdf", FileMode.Create))
        {

            PdfWriter.GetInstance(document, stream);
            document.Open();

            PdfPTable table = new PdfPTable(1);
            PdfPCell cell = new PdfPCell();
            cell.Border = PdfPCell.NO_BORDER;

            cell.AddElement(new Paragraph("Voting Information"));
            cell.AddElement(new Paragraph("You have voted for the following election: " + electionName));
            cell.AddElement(new Paragraph("You have voted for the following candidate: " + candidateName));
            cell.AddElement(new Paragraph("To see your ballot on the bullentin board, scan the QR once approved, this QR code will only be active for 10 minutes"));
            table.AddCell(cell);
            document.Add(table);

            string qrCodeText = "Your ballotId is: " + ballotId;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrCodeText, QRCodeGenerator.ECCLevel.Q);
            BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData);
            byte[] qrCodeImage = qrCode.GetGraphic(20);

            iTextSharp.text.Image qrCodePdfImage = iTextSharp.text.Image.GetInstance(qrCodeImage);
            qrCodePdfImage.Alignment = Element.ALIGN_MIDDLE;
            document.Add(qrCodePdfImage);

            document.Close();
        }
    }
}

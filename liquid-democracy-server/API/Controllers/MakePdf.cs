namespace Server.Controllers;

using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Core;
using UglyToad.PdfPig.Fonts.Standard14Fonts;
using UglyToad.PdfPig.Writer;

public class MakePdf{

    public void createBallot(string providerId, string electionName, string candidateName)
    {
        PdfDocumentBuilder builder = new PdfDocumentBuilder();

        PdfDocumentBuilder.AddedFont times = builder.AddStandard14Font(Standard14Font.TimesRoman);

        PdfPageBuilder page = builder.AddPage(PageSize.A4);

        PdfPoint closeToTop = new PdfPoint(15, page.PageSize.Top - 25);

        page.AddText("VotingInformation", 12, closeToTop, times);
        page.AddText("This Is your Id: " + providerId , 10, closeToTop.Translate(0, -20), times);
        page.AddText("Election you are voting in: "+ electionName, 10, closeToTop.Translate(0, -40), times);
        page.AddText("The Candidate you are voting for is: " + candidateName, 10, closeToTop.Translate(0, -60), times);

        File.WriteAllBytes("../../liquid-democracy-server/API/Vote.pdf", builder.Build());
        
    }
}
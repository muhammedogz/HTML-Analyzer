namespace html_analyzer.Services;

using html_analyzer.Models;
using HtmlAgilityPack;
using static html_analyzer.Services.HTMLDocumentService;
public class HTMLAnalyzerService
{
  public HTMLAnalyzerService()
  {
  }

  public HTMLAnalyze AnalyzeHTML(string html)
  {
    var htmlDocument = new HtmlDocument();
    htmlDocument.LoadHtml(html);
    var htmlAnalyze = new HTMLAnalyze();
    htmlAnalyze.HTML = htmlDocument.DocumentNode.OuterHtml;
    htmlAnalyze.Title = htmlDocument.DocumentNode.SelectSingleNode("//title")?.InnerText;
    htmlAnalyze.Description = htmlDocument.DocumentNode.SelectSingleNode("//meta[@name='description']")?.Attributes["content"]?.Value;
    htmlAnalyze.Keywords = GetKeywords(htmlDocument);
    htmlAnalyze.H1 = GetHeadings(htmlDocument, "h1");


    var errors = htmlDocument.ParseErrors;
    foreach (var error in errors)
    {
      var htmlError = new HTMLError();
      htmlError.Code = error.Code.ToString();
      htmlError.Line = error.Line.ToString();
      htmlError.LinePosition = error.LinePosition.ToString();
      htmlError.Reason = error.Reason;
      htmlError.SourceText = error.SourceText;
      htmlError.StreamPosition = error.StreamPosition.ToString();
      htmlAnalyze.Errors.Add(htmlError);
    }
    return htmlAnalyze;
  }
}

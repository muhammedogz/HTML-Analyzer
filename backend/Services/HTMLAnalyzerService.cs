namespace html_analyzer.Services;

using html_analyzer.Models;
public class HTMLAnalyzerService
{
  private readonly HTMLDocumentService _htmlDocumentService;
  public HTMLAnalyzerService(string html)
  {
    _htmlDocumentService = new HTMLDocumentService(html);
  }

  public HTMLAnalyzerService(string url, bool isUrl)
  {
    _htmlDocumentService = new HTMLDocumentService(url, isUrl);
  }

  public HTMLAnalyzeSimple AnalyzeHTML()
  {
    var htmlAnalyze = new HTMLAnalyzeSimple();
    var htmlDocumentService = _htmlDocumentService;
    htmlAnalyze.HTML = htmlDocumentService.GetHTML();
    htmlAnalyze.Errors = htmlDocumentService.GetErrors();
    htmlAnalyze.Rate = htmlDocumentService.CalculateRate();
    return htmlAnalyze;
  }

  public HTMLAnalyzeSimple FixErrors()
  {
    var htmlAnalyze = new HTMLAnalyzeSimple();
    var htmlDocumentService = _htmlDocumentService;
    htmlDocumentService.FixAllErrors();
    htmlAnalyze.HTML = htmlDocumentService.GetHTML();
    htmlAnalyze.Errors = htmlDocumentService.GetErrors();
    htmlAnalyze.Rate = htmlDocumentService.CalculateRate();
    return htmlAnalyze;
  }
}

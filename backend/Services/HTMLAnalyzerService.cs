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

    var htmlAnalyze = new HTMLAnalyze();
    var htmlDocumentService = new HTMLDocumentService(html);
    htmlAnalyze.HTML = htmlDocumentService.GetHTML();
    htmlAnalyze.Title = htmlDocumentService.GetTitle();
    htmlAnalyze.Description = htmlDocumentService.GetDescription();
    htmlAnalyze.Keywords = htmlDocumentService.GetKeywords();
    htmlAnalyze.H1 = htmlDocumentService.GetNodes("h1");
    htmlAnalyze.H2 = htmlDocumentService.GetNodes("h2");
    htmlAnalyze.H3 = htmlDocumentService.GetNodes("h3");
    htmlAnalyze.H4 = htmlDocumentService.GetNodes("h4");
    htmlAnalyze.H5 = htmlDocumentService.GetNodes("h5");
    htmlAnalyze.H6 = htmlDocumentService.GetNodes("h6");
    htmlAnalyze.Links = htmlDocumentService.GetNodes("a");
    htmlAnalyze.Images = htmlDocumentService.GetNodes("img");
    htmlAnalyze.Scripts = htmlDocumentService.GetNodes("script");
    htmlAnalyze.Styles = htmlDocumentService.GetNodes("style");
    htmlAnalyze.Forms = htmlDocumentService.GetNodes("form");
    htmlAnalyze.Inputs = htmlDocumentService.GetNodes("input");
    htmlAnalyze.Lists = htmlDocumentService.GetNodes("ul");
    htmlAnalyze.Tables = htmlDocumentService.GetNodes("table");
    htmlAnalyze.Buttons = htmlDocumentService.GetNodes("button");
    htmlAnalyze.Metas = htmlDocumentService.GetNodes("meta");
    htmlAnalyze.Divs = htmlDocumentService.GetNodes("div");
    htmlAnalyze.Spans = htmlDocumentService.GetNodes("span");
    htmlAnalyze.Paragraphs = htmlDocumentService.GetNodes("p");
    htmlAnalyze.Headers = htmlDocumentService.GetNodes("header");
    htmlAnalyze.Footers = htmlDocumentService.GetNodes("footer");
    htmlAnalyze.Navs = htmlDocumentService.GetNodes("nav");
    htmlAnalyze.Asides = htmlDocumentService.GetNodes("aside");
    htmlAnalyze.Sections = htmlDocumentService.GetNodes("section");
    htmlAnalyze.Articles = htmlDocumentService.GetNodes("article");
    htmlAnalyze.Main = htmlDocumentService.GetNodes("main");
    htmlAnalyze.Text = htmlDocumentService.GetText();
    htmlAnalyze.HTMLVersion = htmlDocumentService.GetHTMLVersion();
    htmlAnalyze.Errors = htmlDocumentService.GetErrors();
    return htmlAnalyze;
  }
}

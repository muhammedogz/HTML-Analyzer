using html_analyzer.Services;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
namespace html_analyzer.Controllers;


[ApiController]
[Route("[controller]")]
public class HTMLAnalyzerController : ControllerBase
{
  [HttpGet()]
  public ActionResult GetHTML()
  {
    var htmlAnalyzerService = new HTMLAnalyzerService();
    var html = @"<!DOCTYPE html>
<html lang=""en"">
<head>
  <meta charset=""UTF-8"">
  <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
  <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
  <title>Document</title>
</head>
<body>
  <h1>HTML Analyzer</h1>
  <h1>Domates</h1>
  <p>HTML Analyzer is a tool that analyzes HTML code and returns the following information:</p>
  <ul>
    <f>HTML version</f>
    <ff1>Title</f>
    <f>Description</f>
    <f>Keywords</f>
    <f>H1</f>
    <f>H2</f>
    <li>H3</li>
    <li>H4</li>
    <li>H5</li>
    <li>H6</li>
    <li>Links</li>
    <li>Images</li>
    <li>Scripts</li>
    <li>Styles</li>
    <li>Forms</li>
    <li>Inputs</li>
    <li>Text</li>
  </ul>
</body>
</html>";
    var htmlAnalyze = htmlAnalyzerService.AnalyzeHTML(html);
    return Ok(htmlAnalyze);
  }

  [HttpPost()]
  public ActionResult PostHTML([FromBody] string html)
  {
    var htmlAnalyzerService = new HTMLAnalyzerService();
    var htmlAnalyze = htmlAnalyzerService.AnalyzeHTML(html);
    return Ok(htmlAnalyze);
  }

}
using html_analyzer.Models;
using html_analyzer.Services;
using Microsoft.AspNetCore.Mvc;
namespace html_analyzer.Controllers;


[ApiController]
// allow all origins
[Route("[controller]")]
public class HTMLAnalyzerController : ControllerBase
{

  [HttpGet()]
  public ActionResult GetHTML()
  {
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
    var htmlAnalyzerService = new HTMLAnalyzerService(html);
    var htmlAnalyze = htmlAnalyzerService.AnalyzeHTML();
    return Ok(htmlAnalyze);
  }

  // post method with json object and html field
  [HttpPost("analyze-html")]
  public ActionResult PostHTML([FromBody] HTMLPostModel htmlPostModel)
  {
    if (htmlPostModel.HTML == null)
    {
      return BadRequest("HTML field is required");
    }

    var htmlAnalyzerService = new HTMLAnalyzerService(htmlPostModel.HTML);
    var htmlAnalyze = htmlAnalyzerService.AnalyzeHTML();
    return Ok(new ResponseModel
    {
      Status = 200,
      Data = htmlAnalyze,
      Message = "HTML Analyzed"
    });

  }

  // new api endpoint
  // post /html-analyzer/analyze-url
  [HttpPost("analyze-url")]
  public ActionResult PostURL([FromBody] HTMLPostModel htmlPostModel)
  {
    if (htmlPostModel.HTML == null)
    {
      return BadRequest("URL field is required");
    }

    var htmlAnalyzerService = new HTMLAnalyzerService(htmlPostModel.HTML, true);
    var htmlAnalyze = htmlAnalyzerService.AnalyzeHTML();
    return Ok(new ResponseModel
    {
      Status = 200,
      Data = htmlAnalyze,
      Message = "HTML Analyzed"
    });
  }
}
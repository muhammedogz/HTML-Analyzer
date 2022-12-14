namespace html_analyzer.Services;

using html_analyzer.Models;
using HtmlAgilityPack;

public class HTMLDocumentService
{
  private readonly HtmlDocument _htmlDocument;

  public HTMLDocumentService(string html)
  {
    _htmlDocument = new HtmlDocument();
    _htmlDocument.LoadHtml(html);
  }

  public HTMLDocumentService(HtmlDocument htmlDocument)
  {
    _htmlDocument = htmlDocument;
  }

  public HTMLDocumentService(string url, bool isUrl)
  {
    HtmlWeb web = new HtmlWeb();
    _htmlDocument = new HtmlDocument();
    _htmlDocument = web.Load(url);
  }

  public string GetHTML()
  {
    return _htmlDocument.DocumentNode.OuterHtml;
  }

  public string? GetTitle()
  {
    return _htmlDocument.DocumentNode.SelectSingleNode("//title")?.InnerText;
  }

  public string? GetDescription()
  {
    return _htmlDocument.DocumentNode.SelectSingleNode("//meta[@name='description']")?.Attributes["content"].Value;
  }

  public List<string> GetKeywords()
  {
    var keywords = new List<string>();
    var metaTags = _htmlDocument.DocumentNode.SelectNodes("//meta[@name='keywords']");
    if (metaTags != null)
    {
      foreach (var metaTag in metaTags)
      {
        var content = metaTag.Attributes["content"]?.Value;
        if (content != null)
        {
          var keywordList = content.Split(',');
          foreach (var keyword in keywordList)
          {
            keywords.Add(keyword.Trim());
          }
        }
      }
    }
    return keywords;

  }

  public List<HTMLNodeModel> GetNodes(string node)
  {
    var nodes = new List<HTMLNodeModel>();
    var htmlNodes = _htmlDocument.DocumentNode.SelectNodes($"//{node}");
    if (htmlNodes != null)
    {
      foreach (var htmlNode in htmlNodes)
      {
        var attributes = new List<HTMLAttributeModel>();
        foreach (var attribute in htmlNode.Attributes)
        {
          attributes.Add(new HTMLAttributeModel(attribute.Name, attribute.Value));
        }
        nodes.Add(new HTMLNodeModel(htmlNode.Name, htmlNode.InnerText, htmlNode.OuterHtml, htmlNode.InnerHtml, attributes));
      }
    }

    return nodes;
  }

  public string GetText()
  {
    return _htmlDocument.DocumentNode.InnerText;
  }

  public string GetHTMLVersion()
  {
    var htmlVersion = "HTML 5";
    var htmlNode = _htmlDocument.DocumentNode.SelectSingleNode("//html");
    if (htmlNode != null)
    {
      var version = htmlNode.Attributes["version"]?.Value;
      if (version != null)
      {
        htmlVersion = $"HTML {version}";
      }
    }
    return htmlVersion;
  }

  public List<HTMLError> GetErrors()
  {
    const string DOCTYPE = "<!DOCTYPE html>";
    var errors = new List<HTMLError>();

    var doctypeValue = _htmlDocument.DocumentNode.OuterHtml.TrimStart().TrimEnd().Replace("\r", "").Replace("\t", "").Substring(0, DOCTYPE.Length > _htmlDocument.Text.Length ? _htmlDocument.Text.Length : DOCTYPE.Length);
    if (!doctypeValue.Contains(DOCTYPE))
    {
      errors.Add(new HTMLError("HTML must start with a DOCTYPE declaration", "Add <!DOCTYPE html> to the top of the document"));
      errors.Add(new HTMLError("HTML version is not HTML 5", "Add <!DOCTYPE html> to the top of the document to make sure the HTML version is HTML 5"));
    }

    var headNodes = _htmlDocument.DocumentNode.SelectNodes("//head");
    if (headNodes == null)
    {
      errors.Add(new HTMLError("HTML does not contain a head tag", "Add a head tag to the document"));
    }

    var titleNodes = _htmlDocument.DocumentNode.SelectNodes("//title");
    if (titleNodes == null)
    {
      errors.Add(new HTMLError("HTML does not contain a title tag", "Add a title tag to the document"));
    }

    var bodyNodes = _htmlDocument.DocumentNode.SelectNodes("//body");
    if (bodyNodes == null)
    {
      errors.Add(new HTMLError("HTML does not contain a body tag", "Add a body tag to the document"));
    }

    var h1Nodes = _htmlDocument.DocumentNode.SelectNodes("//h1");
    if (h1Nodes == null)
    {
      errors.Add(new HTMLError("HTML does not contain a h1 tag", "Add a h1 tag to the document"));
    }

    var htmlErrors = _htmlDocument.ParseErrors;
    if (htmlErrors != null)
    {
      foreach (var htmlError in htmlErrors)
      {
        errors.Add(new HTMLError(
        code: htmlError.Code.ToString(),
        reason: htmlError.Reason,
        line: htmlError.Line,
        linePosition: htmlError.LinePosition,
        sourceText: htmlError.SourceText,
        streamPosition: htmlError.StreamPosition
        ));
      }
    }

    return errors;
  }

  // solutions for errors
  public void fixDoctypeError()
  {
    var doctypeValue = _htmlDocument.DocumentNode.OuterHtml.TrimStart().TrimEnd().Replace("\r", "").Replace("\t", "").Substring(0, 15);
    if (!doctypeValue.Contains("<!DOCTYPE html>"))
    {
      var html = _htmlDocument.DocumentNode.OuterHtml;
      html = html.Insert(0, "<!DOCTYPE html>");
      _htmlDocument.LoadHtml(html);
    }
  }

  public void fixHeadError()
  {
    var headNodes = _htmlDocument.DocumentNode.SelectNodes("//head");
    if (headNodes == null)
    {
      var html = _htmlDocument.DocumentNode.OuterHtml;
      html = html.Insert(0, "<head></head>");
      _htmlDocument.LoadHtml(html);
    }
  }

  public void fixTitleError()
  {
    var titleNodes = _htmlDocument.DocumentNode.SelectNodes("//title");
    if (titleNodes == null)
    {
      var html = _htmlDocument.DocumentNode.OuterHtml;
      html = html.Insert(0, "<title></title>");
      _htmlDocument.LoadHtml(html);
    }
  }

  public void fixBodyError()
  {
    var bodyNodes = _htmlDocument.DocumentNode.SelectNodes("//body");
    if (bodyNodes == null)
    {
      var html = _htmlDocument.DocumentNode.OuterHtml;
      html = html.Insert(0, "<body></body>");
      _htmlDocument.LoadHtml(html);
    }
  }

  public void fixH1Error()
  {
    var h1Nodes = _htmlDocument.DocumentNode.SelectNodes("//h1");
    if (h1Nodes == null)
    {
      var html = _htmlDocument.DocumentNode.OuterHtml;
      html = html.Insert(0, "<h1></h1>");
      _htmlDocument.LoadHtml(html);
    }
  }



}
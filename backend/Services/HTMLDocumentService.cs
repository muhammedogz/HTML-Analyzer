using System.Text.RegularExpressions;
namespace html_analyzer.Services;

using html_analyzer.Enums;
using html_analyzer.Models;
using HtmlAgilityPack;

public class HTMLDocumentService
{
  const string DOCTYPE_VALUE = "<!DOCTYPE html>";

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

  public HTMLError? getDocTypeError()
  {
    var doctypeRegex = new Regex("<!DOCTYPE\\s+html\\s*>", RegexOptions.IgnoreCase);
    // check if the document contains a doctype
    if (!doctypeRegex.IsMatch(_htmlDocument.DocumentNode.OuterHtml))
    {
      return new HTMLError(errorType: ErrorEnums.DOCTYPE_INVALID, reason: "The HTML document does not contain a doctype.", solution: "Add a doctype to the document.",
      errorLevel: ErrorLevelEnums.ERROR
      );
    }

    return null;
  }

  public HTMLError? getHTMLTagError()
  {
    var htmlNodes = _htmlDocument.DocumentNode.SelectNodes("//html");
    if (htmlNodes == null)
    {
      return new HTMLError(errorType: ErrorEnums.HTML_TAG_MISSING, reason: "The HTML document does not contain an html tag.", solution: "Add an html tag to the document.",
      errorLevel: ErrorLevelEnums.ERROR
      );
    }
    return null;
  }

  public HTMLError? getHeadError()
  {
    var headNodes = _htmlDocument.DocumentNode.SelectNodes("//head");
    if (headNodes == null)
    {
      return new HTMLError(errorType: ErrorEnums.HEAD_TAG_MISSING, reason: "The HTML document does not contain a head tag.", solution: "Add a head tag to the document.",
      errorLevel: ErrorLevelEnums.ERROR
      );
    }
    return null;
  }

  public HTMLError? getTitleError()
  {
    var titleNodes = _htmlDocument.DocumentNode.SelectNodes("//title");
    if (titleNodes == null)
    {
      return new HTMLError(errorType: ErrorEnums.TITLE_TAG_MISSING, reason: "The HTML document does not contain a title tag.", solution: "Add a title tag to the document.",
      errorLevel: ErrorLevelEnums.ERROR
      );
    }
    return null;
  }

  public HTMLError? getBodyError()
  {
    var bodyNodes = _htmlDocument.DocumentNode.SelectNodes("//body");
    if (bodyNodes == null)
    {
      return new HTMLError(errorType: ErrorEnums.BODY_TAG_MISSING, reason: "The HTML document does not contain a body tag.", solution: "Add a body tag to the document.",
      errorLevel: ErrorLevelEnums.ERROR
      );
    }
    return null;
  }

  public HTMLError? getH1Error()
  {
    var h1Nodes = _htmlDocument.DocumentNode.SelectNodes("//h1");
    if (h1Nodes == null)
    {
      return new HTMLError(errorType: ErrorEnums.H1_TAG_MISSING, reason: "The HTML document does not contain an h1 tag.", solution: "Add an h1 tag to the document.",
      errorLevel: ErrorLevelEnums.SEO
      );
    }
    return null;
  }

  //   <input type="button">
  // <input type="checkbox">
  // <input type="color">
  // <input type="date">
  // <input type="datetime-local">
  // <input type="email">
  // <input type="file">
  // <input type="hidden">
  // <input type="image">
  // <input type="month">
  // <input type="number">
  // <input type="password">
  // <input type="radio">
  // <input type="range">
  // <input type="reset">
  // <input type="search">
  // <input type="submit">
  // <input type="tel">
  // <input type="text">
  // <input type="time">
  // <input type="url">
  // <input type="week">

  public List<HTMLError> getAttrErrors()
  {
    var attrErrors = new List<HTMLError>();

    foreach (var node in _htmlDocument.DocumentNode.SelectNodes("//*"))
    {
      var allAttributes = node.Attributes;
      var allAttributeNames = new HashSet<string>();
      foreach (var attribute in allAttributes)
      {
        if (allAttributeNames.Contains(attribute.Name))
        {
          // Duplicate attribute
          attrErrors.Add(new HTMLError { ErrorType = ErrorEnums.DUPLICATE_ATTRIBUTES, ErrorLevel = ErrorLevelEnums.WARNING, Reason = $"Duplicate attribute '{attribute.Name}' on the {node.Name} element. Line: {attribute.Line}, Column: {attribute.LinePosition}. ", Solution = $"Remove the duplicate attribute '{attribute.Name}'." });
        }
        else
        {
          allAttributeNames.Add(attribute.Name);
        }
      }

      if (node.Name == "input")
      {
        var typeAttribute = node.Attributes["type"];
        if (typeAttribute != null)
        {
          var typeValue = typeAttribute.Value;
          if (typeValue != "text" && typeValue != "password" && typeValue != "checkbox" && typeValue != "radio" && typeValue != "submit" && typeValue != "reset" && typeValue != "button" && typeValue != "file" && typeValue != "hidden" && typeValue != "image" && typeValue != "email" && typeValue != "number" && typeValue != "range" && typeValue != "search" && typeValue != "tel" && typeValue != "url" && typeValue != "date" && typeValue != "datetime-local" && typeValue != "month" && typeValue != "time" && typeValue != "week" && typeValue != "color")
          {
            // Invalid value for the type attribute
            var reason = $"Invalid value '{typeValue}' for the type attribute on the {node.Name} element. Line: {typeAttribute.Line}, Column: {typeAttribute.LinePosition}";
            var solution = $"Change the value of the type attribute to 'text', 'password' or 'checkbox'.";
            var err = new HTMLError(errorType: ErrorEnums.INPUT_TYPE_INVALID, errorLevel: ErrorLevelEnums.WARNING, reason: reason, solution: solution);
            attrErrors.Add(err);
            // remove the invalid attribute
            // node.Attributes.Remove(typeAttribute);
          }
        }
      }
      else if (node.Name == "img")
      {
        var altAttr = node.Attributes["alt"];
        if (altAttr == null)
        {
          // Missing alt attribute
          var reason = $"The {node.Name} element is missing the alt attribute. Line: {node.Line}, Column: {node.LinePosition}";
          var solution = $"Add an alt attribute to the {node.Name} element.";
          var err = new HTMLError(errorType: ErrorEnums.IMAGE_ALT_MISSING, errorLevel: ErrorLevelEnums.ACCESSIBILITY, reason: reason, solution: solution);
          attrErrors.Add(err);
        }
        var srcAttr = node.Attributes["src"];
        if (srcAttr == null)
        {
          // Missing src attribute
          var reason = $"The {node.Name} element is missing the src attribute. Line: {node.Line}, Column: {node.LinePosition}";
          var solution = $"Add an src attribute to the {node.Name} element.";
          var err = new HTMLError(errorType: ErrorEnums.IMAGE_SRC_MISSING, errorLevel: ErrorLevelEnums.ERROR, reason: reason, solution: solution);
          attrErrors.Add(err);
        }
      }
      else if (node.Name == "a")
      {
        var hrefAttribute = node.Attributes["href"];
        if (hrefAttribute == null)
        {
          // Missing href attribute
          var reason = $"The {node.Name} element is missing the href attribute. Line: {node.Line}, Column: {node.LinePosition}";
          var solution = $"Add an href attribute to the {node.Name} element.";
          var err = new HTMLError(errorType: ErrorEnums.HREF_MISSING, errorLevel: ErrorLevelEnums.WARNING, reason: reason, solution: solution);
          attrErrors.Add(err);
        }
      }
      else if (node.Name == "form")
      {
        var actionAttribute = node.Attributes["action"];
        if (actionAttribute == null)
        {
          // Missing action attribute
          var reason = $"The {node.Name} element is missing the action attribute. Line: {node.Line}, Column: {node.LinePosition}";
          var solution = $"Add an action attribute to the {node.Name} element.";
          var err = new HTMLError(errorType: ErrorEnums.FORM_ACTION_MISSING, errorLevel: ErrorLevelEnums.WARNING, reason: reason, solution: solution);
          attrErrors.Add(err);
        }
      }
      else if (node.Name == "table")
      {
        var summaryAttribute = node.Attributes["summary"];
        if (summaryAttribute == null)
        {
          // Missing summary attribute
          var reason = $"The {node.Name} element is missing the summary attribute. Line: {node.Line}, Column: {node.LinePosition}";
          var solution = $"Add a summary attribute to the {node.Name} element.";
          var err = new HTMLError(errorType: ErrorEnums.FORM_ACTION_MISSING, errorLevel: ErrorLevelEnums.WARNING, reason: reason, solution: solution);
          attrErrors.Add(err);
        }
      }
    }

    return attrErrors;
  }

  public List<HTMLError> GetErrors()
  {
    var errors = new List<HTMLError>();

    var doctypeError = getDocTypeError();
    if (doctypeError != null)
    {
      errors.Add(doctypeError);
    }

    var htmlTagError = getHTMLTagError();
    if (htmlTagError != null)
    {
      errors.Add(htmlTagError);
    }

    var headError = getHeadError();
    if (headError != null)
    {
      errors.Add(headError);
    }

    var titleError = getTitleError();
    if (titleError != null)
    {
      errors.Add(titleError);
    }

    var bodyError = getBodyError();
    if (bodyError != null)
    {
      errors.Add(bodyError);
    }

    var h1Error = getH1Error();
    if (h1Error != null)
    {
      errors.Add(h1Error);
    }

    var invalidInputAttr = getAttrErrors();
    if (invalidInputAttr != null)
    {
      foreach (var error in invalidInputAttr)
      {
        errors.Add(error);
      }
    }

    var htmlErrors = _htmlDocument.ParseErrors;
    if (htmlErrors != null)
    {
      foreach (var htmlError in htmlErrors)
      {
        var errorType = ErrorEnums.TAG_NOT_CLOSED;
        var errorLevel = ErrorLevelEnums.ERROR;
        var solution = "Close the tag";
        var reason = $"Line {htmlError.Line}, position {htmlError.StreamPosition}: {htmlError.Reason}";
        if (htmlError.Code == HtmlAgilityPack.HtmlParseErrorCode.CharsetMismatch)
        {
          errorType = ErrorEnums.CHARSET_MISMATCH;
          errorLevel = ErrorLevelEnums.WARNING;
          solution = "Change the charset to UTF-8";
        }
        else if (htmlError.Code == HtmlAgilityPack.HtmlParseErrorCode.EndTagNotRequired)
        {
          errorType = ErrorEnums.END_TAG_NOT_REQUIRED;
          errorLevel = ErrorLevelEnums.ERROR;
          solution = $"Remove the end tag for {htmlError.SourceText}";
        }
        else if (htmlError.Code == HtmlAgilityPack.HtmlParseErrorCode.EndTagInvalidHere)
        {
          errorType = ErrorEnums.END_TAG_INVALID;
          errorLevel = ErrorLevelEnums.ERROR;
          solution = $"Remove the end tag for {htmlError.SourceText}";
        }
        else if (htmlError.Code == HtmlAgilityPack.HtmlParseErrorCode.TagNotClosed)
        {
          errorType = ErrorEnums.TAG_NOT_CLOSED;
          errorLevel = ErrorLevelEnums.ERROR;
          solution = $"Close the tag for {htmlError.SourceText}";
        }
        else if (htmlError.Code == HtmlAgilityPack.HtmlParseErrorCode.TagNotOpened)
        {
          errorType = ErrorEnums.TAG_NOT_OPENED;
          errorLevel = ErrorLevelEnums.ERROR;
          solution = $"Open the tag for {htmlError.SourceText}";
        }

        errors.Add(new HTMLError(
        errorType: errorType,
        errorLevel: errorLevel,
        solution: solution,
        reason: reason,
        code: htmlError.Code.ToString(),
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
    var doctypeError = getDocTypeError();
    if (doctypeError != null)
    {
      var html = GetHTML();
      var fixedHtml = DOCTYPE_VALUE + html;
      _htmlDocument.LoadHtml(fixedHtml);
    }
  }

  public void fixHTMLTagError()
  {
    var htmlTagError = getHTMLTagError();
    if (htmlTagError != null)
    {
      var html = GetHTML();

      // Add the <html> tag after the doctype declaration
      var doctypeIndex = html.IndexOf(DOCTYPE_VALUE);
      html = html.Insert(doctypeIndex + DOCTYPE_VALUE.Length, "<html>");

      // Add the </html> tag at the end of the document
      html = html + "</html>";

      _htmlDocument.LoadHtml(html);
    }
  }

  public void fixHeadError()
  {
    var headError = getHeadError();
    if (headError != null)
    {
      var html = GetHTML();
      // insert after the index of the <html> tag
      var htmlIndex = html.IndexOf("<html>");
      html = html.Insert(htmlIndex + "<html>".Length, "<head></head>");

      _htmlDocument.LoadHtml(html);
    }
  }

  public void fixTitleError()
  {
    var titleError = getTitleError();
    if (titleError != null)
    {
      var html = GetHTML();
      // insert after the index of the <head> tag
      var headIndex = html.IndexOf("<head>");
      html = html.Insert(headIndex + "<head>".Length, "<title></title>");

      _htmlDocument.LoadHtml(html);
    }
  }

  public void fixBodyError()
  {
    var bodyError = getBodyError();
    if (bodyError != null)
    {
      var html = GetHTML();
      // insert before the index of the </html> tag
      var htmlIndex = html.IndexOf("</html>");
      Console.WriteLine("htmlIndex: " + htmlIndex + "\n");
      if (htmlIndex == -1)
      {
        htmlIndex = html.Length;
      }
      html = html.Insert(htmlIndex, "<body></body>");

      _htmlDocument.LoadHtml(html);
    }
  }

  public void fixH1Error()
  {
    var h1Error = getH1Error();
    if (h1Error != null)
    {
      var html = GetHTML();
      // insert after the index of the <body> tag
      var bodyIndex = html.IndexOf("<body>");
      html = html.Insert(bodyIndex + 6, "<h1></h1>");

      _htmlDocument.LoadHtml(html);
    }
  }

  public void fixErrors(List<HTMLError> errors)
  {
    foreach (var error in errors)
    {
      if (error.ErrorType == ErrorEnums.DOCTYPE_INVALID)
      {
        fixDoctypeError();
      }
      else if (error.ErrorType == ErrorEnums.HEAD_TAG_MISSING)
      {
        fixHeadError();
      }
      else if (error.ErrorType == ErrorEnums.TITLE_TAG_MISSING)
      {
        fixTitleError();
      }
      else if (error.ErrorType == ErrorEnums.BODY_TAG_MISSING)
      {
        fixBodyError();
      }
      else if (error.ErrorType == ErrorEnums.H1_TAG_MISSING)
      {
        fixH1Error();
      }
    }
  }

  public void fixParsedErrors()
  {
    List<HTMLError> errors = GetErrors();
    if (errors == null || errors.Count == 0) return;
    var html = GetHTML();

    foreach (HTMLError error in errors)
    {
      if (error == null) continue;
      else if (error.ErrorType == ErrorEnums.TAG_NOT_OPENED)
      {
        var closeTag = error.SourceText;
        if (closeTag == null) continue;
        var openTag = "<" + closeTag.Substring(2);
        html = html.Replace(closeTag, openTag + closeTag);
      }

      else if (error.ErrorType == ErrorEnums.TAG_NOT_CLOSED)
      {
        var linePosition = error.LinePosition;
        if (linePosition == null) continue;

        var openTag = error?.SourceText?.Substring(0, (int)linePosition);
        if (openTag == null) continue;
        var closeTag = "</" + openTag.Substring(1);
        html = html.Replace(openTag, openTag + closeTag);

      }
      else if (error.ErrorType == ErrorEnums.END_TAG_NOT_REQUIRED)
      {
        var endTag = error.SourceText;
        if (endTag == null) continue;
        html = html.Replace(endTag, "");
      }
      else if (error.ErrorType == ErrorEnums.END_TAG_INVALID)
      {
        var endTag = error.SourceText;
        var openTag = error?.SourceText?.Substring(2);
        if (endTag == null || openTag == null) continue;
        html = html.Replace(endTag, "</" + openTag + ">");

      }

    }
    _htmlDocument.LoadHtml(html);

  }

  public void wrapImagesWithDiv()
  {
    var imageDiv = @"
    <div
    style=""display:block; width: 100%; height: 100%;""
    ></div>
    ";
    var images = _htmlDocument.DocumentNode.SelectNodes("//img");
    if (images == null) return;

    foreach (var image in images)
    {
      // create a new div with the same attributes as the imageDiv
      var predefined = HtmlNode.CreateNode(imageDiv);
      // wrap the image with the new div
      image.ParentNode.ReplaceChild(predefined, image);
      predefined.AppendChild(image);
    }
  }

  public void wrapTableWithDiv()
  {
    var tableDiv = @"
    <div class=""divTable""
    style=""display: table; width: 100%;""
    ></div>
    ";
    var tables = _htmlDocument.DocumentNode.SelectNodes("//img");
    if (tables == null) return;

    foreach (var table in tables)
    {
      // create a new div with the same attributes as the tableDiv
      var predefined = HtmlNode.CreateNode(tableDiv);
      // wrap the table with the new div
      table.ParentNode.ReplaceChild(predefined, table);
      predefined.AppendChild(table);
    }
  }

  public void wrapThWithDiv()
  {
    var thDiv = @"
    <div class=""divTh""
    style=""display: th; width: 100%;""
    ></div>
    ";
    var ths = _htmlDocument.DocumentNode.SelectNodes("//img");
    if (ths == null) return;

    foreach (var th in ths)
    {
      // create a new div with the same attributes as the thDiv
      var predefined = HtmlNode.CreateNode(thDiv);
      // wrap the th with the new div
      th.ParentNode.ReplaceChild(predefined, th);
      predefined.AppendChild(th);
    }
  }

  public void FixAllErrors()
  {
    fixDoctypeError();
    fixHTMLTagError();
    fixHeadError();
    fixTitleError();
    fixBodyError();
    fixH1Error();
    fixParsedErrors();
    wrapImagesWithDiv();
    wrapTableWithDiv();
  }

  public int CalculateRate()
  {
    var errors = GetErrors();
    var rate = 100;
    foreach (var error in errors)
    {
      if (error.ErrorLevel == ErrorLevelEnums.ERROR)
      {
        rate -= 10;
      }
      else if (error.ErrorLevel == ErrorLevelEnums.WARNING)
      {
        rate -= 5;
      }
      else if (error.ErrorLevel == ErrorLevelEnums.SEO)
      {
        rate -= 2;
      }
      else if (error.ErrorLevel == ErrorLevelEnums.ACCESSIBILITY)
      {
        rate -= 1;
      }
    }

    return rate < 0 ? 0 : rate;
  }


}
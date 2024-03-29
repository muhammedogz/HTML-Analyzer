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

  public List<HTMLError> getMetaErrors()
  {
    var metaErrors = new List<HTMLError>();

    var metaNodes = _htmlDocument.DocumentNode.SelectNodes("//meta");
    if (metaNodes == null)
    {
      var err = new HTMLError(errorType: ErrorEnums.META_MISSING, reason: "The HTML document does not contain a meta tag.", solution: "Add a meta tag to the document.",
      errorLevel: ErrorLevelEnums.SEO
      );
      metaErrors.Add(err);
    }

    var descriptionNodes = _htmlDocument.DocumentNode.SelectNodes("//meta[@name='description']");
    if (descriptionNodes == null)
    {
      var err = new HTMLError(errorType: ErrorEnums.META_MISSING, reason: "The HTML document does not contain a meta description tag.", solution: "Add a meta description tag to the document.",
      errorLevel: ErrorLevelEnums.SEO
      );
      metaErrors.Add(err);
    }
    var keywordsNodes = _htmlDocument.DocumentNode.SelectNodes("//meta[@name='keywords']");
    if (keywordsNodes == null)
    {
      var err = new HTMLError(errorType: ErrorEnums.META_MISSING, reason: "The HTML document does not contain a meta keywords tag.", solution: "Add a meta keywords tag to the document.",
      errorLevel: ErrorLevelEnums.SEO
      );
      metaErrors.Add(err);
    }
    var authorNodes = _htmlDocument.DocumentNode.SelectNodes("//meta[@name='author']");
    if (authorNodes == null)
    {
      var err = new HTMLError(errorType: ErrorEnums.META_MISSING, reason: "The HTML document does not contain a meta author tag.", solution: "Add a meta author tag to the document.",
      errorLevel: ErrorLevelEnums.SEO
      );
      metaErrors.Add(err);
    }
    var viewportNodes = _htmlDocument.DocumentNode.SelectNodes("//meta[@name='viewport']");
    if (viewportNodes == null)
    {
      var err = new HTMLError(errorType: ErrorEnums.META_MISSING, reason: "The HTML document does not contain a meta viewport tag.", solution: "Add a meta viewport tag to the document.",
      errorLevel: ErrorLevelEnums.WARNING
      );
      metaErrors.Add(err);
    }
    var charsetNodes = _htmlDocument.DocumentNode.SelectNodes("//meta[@charset]");
    if (charsetNodes == null)
    {
      var err = new HTMLError(errorType: ErrorEnums.META_MISSING, reason: "The HTML document does not contain a meta charset tag.", solution: "Add a meta charset tag to the document.",
      errorLevel: ErrorLevelEnums.WARNING
      );
      metaErrors.Add(err);
    }
    var httpEquivNodes = _htmlDocument.DocumentNode.SelectNodes("//meta[@http-equiv]");
    if (httpEquivNodes == null)
    {
      var err = new HTMLError(errorType: ErrorEnums.META_MISSING, reason: "The HTML document does not contain a meta http-equiv tag.", solution: "Add a meta http-equiv tag to the document.",
      errorLevel: ErrorLevelEnums.SEO
      );
      metaErrors.Add(err);
    }
    // og:title
    var ogTitleNodes = _htmlDocument.DocumentNode.SelectNodes("//meta[@property='og:title']");
    if (ogTitleNodes == null)
    {
      var err = new HTMLError(errorType: ErrorEnums.META_MISSING, reason: "The HTML document does not contain a meta og:title tag.", solution: "Add a meta og:title tag to the document.",
      errorLevel: ErrorLevelEnums.SEO
      );
      metaErrors.Add(err);
    }
    // og:description
    var ogDescriptionNodes = _htmlDocument.DocumentNode.SelectNodes("//meta[@property='og:description']");
    if (ogDescriptionNodes == null)
    {
      var err = new HTMLError(errorType: ErrorEnums.META_MISSING, reason: "The HTML document does not contain a meta og:description tag.", solution: "Add a meta og:description tag to the document.",
      errorLevel: ErrorLevelEnums.SEO
      );
      metaErrors.Add(err);
    }
    // og:image
    var ogImageNodes = _htmlDocument.DocumentNode.SelectNodes("//meta[@property='og:image']");
    if (ogImageNodes == null)
    {
      var err = new HTMLError(errorType: ErrorEnums.META_MISSING, reason: "The HTML document does not contain a meta og:image tag.", solution: "Add a meta og:image tag to the document.",
      errorLevel: ErrorLevelEnums.SEO
      );
      metaErrors.Add(err);
    }

    return metaErrors;
  }

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
      else if (node.Name == "button" || node.Name == "select" || node.Name == "textarea")
      {
        var ariaLabelAttribute = node.Attributes["aria-label"];
        if (ariaLabelAttribute == null)
        {
          var reason = $"The {node.Name} element does not contain an aria-label attribute. Line: {node.Line}, Column: {node.LinePosition}";
          var solution = $"Add an aria-label attribute to the {node.Name} element.";
          var err = new HTMLError(errorType: ErrorEnums.ARIA_LABEL_MISSING, errorLevel: ErrorLevelEnums.ACCESSIBILITY, reason: reason, solution: solution);
          attrErrors.Add(err);
        }
      }
      // meta is missing
      else if (node.Name == "meta")
      {
        var nameAttribute = node.Attributes["name"];
        if (nameAttribute == null)
        {
          continue;
        }

        var nameValue = nameAttribute.Value;
        if (nameValue != "description" && nameValue != "keywords" && nameValue != "author" && nameValue != "viewport" && nameValue != "robots" && nameValue != "googlebot" && nameValue != "google" && nameValue != "google-site-verification" && nameValue != "msvalidate.01" && nameValue != "yandex-verification" && nameValue != "p:domain_verify" && nameValue != "generator" && nameValue != "application-name" && nameValue != "theme-color" && nameValue != "referrer" && nameValue != "twitter:card" && nameValue != "twitter:site" && nameValue != "twitter:creator" && nameValue != "twitter:url" && nameValue != "twitter:title" && nameValue != "twitter:description" && nameValue != "twitter:image" && nameValue != "twitter:image:alt" && nameValue != "twitter:player" && nameValue != "twitter:player:width" && nameValue != "twitter:player:height" && nameValue != "twitter:player:stream" && nameValue != "twitter:app:name:iphone" && nameValue != "twitter:app:id:iphone" && nameValue != "twitter:app:url:iphone" && nameValue != "twitter:app:name:ipad" && nameValue != "twitter:app:id:ipad" && nameValue != "twitter:app:url:ipad" && nameValue != "twitter:app:name:googleplay" && nameValue != "twitter:app:id:googleplay" && nameValue != "twitter:app:url:googleplay" && nameValue != "og:type" && nameValue != "og:title" && nameValue != "og:description")
        {
          // Invalid value for the name attribute
          var reason = $"Invalid value '{nameValue}' for the name attribute on the {node.Name} element. Line: {nameAttribute.Line}, Column: {nameAttribute.LinePosition}";
          var solution = $"Change the value of the name attribute to 'description', 'keywords', 'author', 'viewport', 'robots' or another valid value.";
          var err = new HTMLError(errorType: ErrorEnums.META_MISSING, errorLevel: ErrorLevelEnums.SEO, reason: reason, solution: solution);
          attrErrors.Add(err);
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

    var invalidMetaTags = getMetaErrors();
    if (invalidMetaTags != null)
    {
      foreach (var error in invalidMetaTags)
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

  void fixDuplicateAttributes()
  {
    var nodes = _htmlDocument.DocumentNode.SelectNodes("//*");
    if (nodes != null)
    {
      foreach (var node in nodes)
      {
        var attributeNames = new HashSet<string>();
        var attributesToRemove = new List<HtmlAttribute>();
        foreach (var attribute in node.Attributes)
        {
          if (attributeNames.Contains(attribute.Name))
          {
            attributesToRemove.Add(attribute);
          }
          else
          {
            attributeNames.Add(attribute.Name);
          }
        }

        foreach (var attribute in attributesToRemove)
        {
          node.Attributes.Remove(attribute);
        }
      }
    }
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

  public void convertTableToDiv()
  {
    var tableNodes = _htmlDocument.DocumentNode.SelectNodes("//table");
    if (tableNodes != null)
    {
      foreach (var tableNode in tableNodes)
      {
        tableNode.Name = "div";
        tableNode.Attributes.Remove("class");
        tableNode.Attributes.Add("class", "divTable");
        tableNode.Attributes.Remove("style");
        tableNode.Attributes.Add("style", "display: table; width: 100%;");
      }
    }
  }

  public void convertTHeadToDiv()
  {
    var tHeadNodes = _htmlDocument.DocumentNode.SelectNodes("//thead");
    if (tHeadNodes != null)
    {
      foreach (var tHeadNode in tHeadNodes)
      {
        tHeadNode.Name = "div";
        tHeadNode.Attributes.Remove("class");
        tHeadNode.Attributes.Add("class", "divTableHeading");
        tHeadNode.Attributes.Remove("style");
        tHeadNode.Attributes.Add("style", "background-color: #EEE; display: table-header-group;");
      }
    }
  }

  public void convertTrToDiv()
  {
    var trNodes = _htmlDocument.DocumentNode.SelectNodes("//tr");
    if (trNodes != null)
    {
      foreach (var trNode in trNodes)
      {
        trNode.Name = "div";
        trNode.Attributes.Remove("class");
        trNode.Attributes.Add("class", "divTableRow");
        trNode.Attributes.Remove("style");
        trNode.Attributes.Add("style", "display: table-row;");
      }
    }
  }

  public void convertThToDiv()
  {
    var thNodes = _htmlDocument.DocumentNode.SelectNodes("//th");
    if (thNodes != null)
    {
      foreach (var thNode in thNodes)
      {
        thNode.Name = "div";
        thNode.Attributes.Remove("class");
        thNode.Attributes.Add("class", "divTableHead");
        thNode.Attributes.Remove("style");
        thNode.Attributes.Add("style", "border: 1px solid #999999; display: table-cell; padding: 3px 10px;");
      }
    }
  }

  public void convertTBodyToDiv()
  {
    var tBodyNodes = _htmlDocument.DocumentNode.SelectNodes("//tbody");
    if (tBodyNodes != null)
    {
      foreach (var tBodyNode in tBodyNodes)
      {
        tBodyNode.Name = "div";
        tBodyNode.Attributes.Remove("class");
        tBodyNode.Attributes.Add("class", "divTableBody");
        tBodyNode.Attributes.Remove("style");
        tBodyNode.Attributes.Add("style", "display: table-row-group;");
      }
    }
  }

  public void convertTdToDiv()
  {
    var tdNodes = _htmlDocument.DocumentNode.SelectNodes("//td");
    if (tdNodes != null)
    {
      foreach (var tdNode in tdNodes)
      {
        tdNode.Name = "div";
        tdNode.Attributes.Remove("class");
        tdNode.Attributes.Add("class", "divTableCell");
        tdNode.Attributes.Remove("style");
        tdNode.Attributes.Add("style", "border: 1px solid #999999; display: table-cell; padding: 3px 10px;");
      }
    }
  }

  public void convertTFooterToDiv()
  {
    var tFooterNodes = _htmlDocument.DocumentNode.SelectNodes("//tfoot");
    if (tFooterNodes != null)
    {
      foreach (var tdNode in tFooterNodes)
      {
        tdNode.Name = "div";
        tdNode.Attributes.Remove("class");
        tdNode.Attributes.Add("class", "divTableFoot");
        tdNode.Attributes.Remove("style");
        tdNode.Attributes.Add("style", "background-color: #EEE; display: table-footer-group; font-weight: bold;");
      }
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
    fixDuplicateAttributes();
    wrapImagesWithDiv();

    // tables
    convertTableToDiv();
    convertTHeadToDiv();
    convertTrToDiv();
    convertThToDiv();
    convertTBodyToDiv();
    convertTdToDiv();
    convertTFooterToDiv();

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
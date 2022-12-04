using System.Text.RegularExpressions;
using html_analyzer.Models;

namespace html_analyzer.Services;

public class RegexService
{
  public static string? GetHTMLVersion(string? html)
  {
    if (html == null)
    {
      return null;
    }

    var regex = new Regex(@"<!DOCTYPE html>");
    var match = regex.Match(html);

    if (match.Success)
    {
      return "HTML5";
    }

    regex = new Regex(@"<html xmlns=""http://www.w3.org/1999/xhtml"">");
    match = regex.Match(html);

    if (match.Success)
    {
      return "HTML 4.01 Transitional";
    }

    regex = new Regex(@"<html xmlns=""http://www.w3.org/1999/xhtml"" xml:lang=""en"" lang=""en"">");
    match = regex.Match(html);

    if (match.Success)
    {
      return "HTML 4.01 Strict";
    }

    regex = new Regex(@"<html xmlns=""http://www.w3.org/1999/xhtml"" xml:lang=""en"" lang=""en"" dir=""ltr"">");
    match = regex.Match(html);

    if (match.Success)
    {
      return "HTML 4.01 Frameset";
    }

    regex = new Regex(@"<html xmlns=""http://www.w3.org/1999/xhtml"" xml:lang=""en"" lang=""en"" dir=""ltr"" version=""XHTML+RDFa 1.0"">");
    match = regex.Match(html);

    if (match.Success)
    {
      return "XHTML 1.0 Strict";
    }

    regex = new Regex(@"<html xmlns=""http://www.w3.org/1999/xhtml"" xml:lang=""en"" lang=""en"" dir=""ltr"" version=""XHTML+RDFa 1.0"" xmlns:og=""http://ogp.me/ns#"">");
    match = regex.Match(html);

    if (match.Success)
    {
      return "XHTML 1.0 Transitional";
    }

    regex = new Regex(@"<html xmlns=""http://www.w3.org/1999/xhtml"" xml:lang=""en"" lang=""en"" dir=""ltr"" version=""XHTML+RDFa 1.0"" xmlns:og=""http://ogp.me/ns#"" xmlns:fb=""http://www.facebook.com/2008/fbml"">");
    match = regex.Match(html);

    if (match.Success)
    {
      return "XHTML 1.0 Frameset";
    }

    regex = new Regex(@"<html xmlns=""http://www.w3.org/1999/xhtml"" xml:lang=""en"" lang=""en"" dir=""ltr"" version=""XHTML+RDFa 1.1"">");
    match = regex.Match(html);

    if (match.Success)
    {
      return "XHTML 1.1";
    }

    return "HTML 5";
  }

  public static List<HTMLNodeModel>? GetNodes(string? html, string node)
  {
    if (html == null)
    {
      return null;
    }

    var nodes = new List<HTMLNodeModel>();
    var regex = new Regex($"<{node}.*?>(.*?)</{node}>");
    var matches = regex.Matches(html);

    foreach (Match match in matches)
    {
      var attributes = new List<HTMLAttributeModel>();
      var attributeRegex = new Regex(@"(\w+)=""(.*?)""");
      var attributeMatches = attributeRegex.Matches(match.Value);
      var innerHtml = match.Groups[1].Value;
      var outerHtml = match.Value;
      var content = match.Value.Replace($"<{node}", "").Replace($"</{node}>", "");


      foreach (Match attributeMatch in attributeMatches)
      {
        attributes.Add(new HTMLAttributeModel(attributeMatch.Groups[1].Value, attributeMatch.Groups[2].Value));
      }

      nodes.Add(new HTMLNodeModel(node, innerHtml, outerHtml, content, attributes));
    }
    return null;
  }

  public static string? GetDescription(string? html)
  {
    if (html == null)
    {
      return null;
    }

    var regex = new Regex(@"<meta name=""description"" content=""(.*?)""");
    var match = regex.Match(html);

    if (match.Success)
    {
      return match.Groups[1].Value;
    }

    return null;
  }
}
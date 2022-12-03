namespace html_analyzer.Services;

using html_analyzer.Models;
using HtmlAgilityPack;

public class HTMLDocumentService
{
  public static List<string> GetKeywords(HtmlDocument document)
  {
    var keywords = new List<string>();
    var metaTags = document.DocumentNode.SelectNodes("//meta[@name='keywords']");
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

  public static List<string> GetHeadings(HtmlDocument document, string heading)
  {
    var headings = new List<string>();
    var headingTags = document.DocumentNode.SelectNodes($"//{heading}");
    if (headingTags != null)
    {
      foreach (var headingTag in headingTags)
      {
        headings.Add(headingTag.InnerText);
      }
    }
    return headings;
  }
}
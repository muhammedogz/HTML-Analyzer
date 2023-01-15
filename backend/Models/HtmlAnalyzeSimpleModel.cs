namespace html_analyzer.Models;

public class HTMLAnalyzeSimple
{
  public List<HTMLError> Errors { get; set; }
  public string? HTML { get; set; }

  public HTMLAnalyzeSimple()
  {
    Errors = new List<HTMLError>();

  }

  public static explicit operator HTMLAnalyzeSimple(HTMLAnalyze v)
  {
    var res = new HTMLAnalyzeSimple();
    res.Errors = v.Errors;
    res.HTML = v.HTML;
    return res;
  }
}
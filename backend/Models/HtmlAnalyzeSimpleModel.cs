namespace html_analyzer.Models;

public class HTMLAnalyzeSimple
{
  public List<HTMLError> Errors { get; set; }
  public string HTML { get; set; }
  public int Rate { get; set; }

  public HTMLAnalyzeSimple()
  {
    Errors = new List<HTMLError>();
    HTML = "";

  }
}
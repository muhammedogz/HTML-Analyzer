namespace html_analyzer.Models;

public class HTMLAnalyze
{
  public List<HTMLError> Errors { get; set; }
  public string? HTML { get; set; }
  public string? Title { get; set; }
  public string? Description { get; set; }
  public List<string> Keywords { get; set; }
  public List<string> H1 { get; set; }
  public List<string> H2 { get; set; }
  public List<string> H3 { get; set; }
  public List<string> H4 { get; set; }
  public List<string> H5 { get; set; }
  public List<string> H6 { get; set; }
  public List<string> Links { get; set; }
  public List<string> Images { get; set; }
  public List<string> Scripts { get; set; }
  public List<string> Styles { get; set; }
  public List<string> Forms { get; set; }
  public List<string> Inputs { get; set; }
  public List<string> Lists { get; set; }
  public List<string> Tables { get; set; }
  public string? Text { get; set; }
  public string? HTMLVersion { get; set; }

  // constructro
  public HTMLAnalyze()
  {
    Errors = new List<HTMLError>();
    Keywords = new List<string>();
    H1 = new List<string>();
    H2 = new List<string>();
    H3 = new List<string>();
    H4 = new List<string>();
    H5 = new List<string>();
    H6 = new List<string>();
    Links = new List<string>();
    Images = new List<string>();
    Scripts = new List<string>();
    Styles = new List<string>();
    Forms = new List<string>();
    Inputs = new List<string>();
    Lists = new List<string>();
    Tables = new List<string>();
  }

}
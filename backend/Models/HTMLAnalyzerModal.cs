namespace html_analyzer.Models;

public class HTMLAnalyze
{
  public List<HTMLError> Errors { get; set; }
  public string? HTML { get; set; }
  public string? Title { get; set; }
  public string? Description { get; set; }
  public List<string> Keywords { get; set; }
  public List<HTMLNodeModel> H1 { get; set; }
  public List<HTMLNodeModel> H2 { get; set; }
  public List<HTMLNodeModel> H3 { get; set; }
  public List<HTMLNodeModel> H4 { get; set; }
  public List<HTMLNodeModel> H5 { get; set; }
  public List<HTMLNodeModel> H6 { get; set; }
  public List<HTMLNodeModel> Links { get; set; }
  public List<HTMLNodeModel> Images { get; set; }
  public List<HTMLNodeModel> Scripts { get; set; }
  public List<HTMLNodeModel> Styles { get; set; }
  public List<HTMLNodeModel> Forms { get; set; }
  public List<HTMLNodeModel> Inputs { get; set; }
  public List<HTMLNodeModel> Lists { get; set; }
  public List<HTMLNodeModel> Tables { get; set; }
  public string? Text { get; set; }
  public string? HTMLVersion { get; set; }

  // constructro
  public HTMLAnalyze()
  {
    Errors = new List<HTMLError>();
    Keywords = new List<string>();
    H1 = new List<HTMLNodeModel>();
    H2 = new List<HTMLNodeModel>();
    H3 = new List<HTMLNodeModel>();
    H4 = new List<HTMLNodeModel>();
    H5 = new List<HTMLNodeModel>();
    H6 = new List<HTMLNodeModel>();
    Links = new List<HTMLNodeModel>();
    Images = new List<HTMLNodeModel>();
    Scripts = new List<HTMLNodeModel>();
    Styles = new List<HTMLNodeModel>();
    Forms = new List<HTMLNodeModel>();
    Inputs = new List<HTMLNodeModel>();
    Lists = new List<HTMLNodeModel>();
    Tables = new List<HTMLNodeModel>();
  }

}
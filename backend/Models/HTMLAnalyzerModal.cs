namespace html_analyzer.Models;

public class HTMLAnalyze
{
  public List<HTMLError> Errors { get; set; }
  public string? HTML { get; set; }
  public string? Text { get; set; }
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
  public List<HTMLNodeModel> Buttons { get; set; }
  public List<HTMLNodeModel> Metas { get; set; }
  public List<HTMLNodeModel> Divs { get; set; }
  public List<HTMLNodeModel> Spans { get; set; }
  public List<HTMLNodeModel> Paragraphs { get; set; }
  public List<HTMLNodeModel> Headers { get; set; }
  public List<HTMLNodeModel> Footers { get; set; }
  public List<HTMLNodeModel> Navs { get; set; }
  public List<HTMLNodeModel> Asides { get; set; }
  public List<HTMLNodeModel> Sections { get; set; }
  public List<HTMLNodeModel> Articles { get; set; }
  public List<HTMLNodeModel> Main { get; set; }
  public List<HTMLNodeModel> Details { get; set; }
  public List<HTMLNodeModel> Summary { get; set; }
  public List<HTMLNodeModel> Figures { get; set; }
  public List<HTMLNodeModel> FigCaptions { get; set; }
  public List<HTMLNodeModel> Iframes { get; set; }
  public List<HTMLNodeModel> Labels { get; set; }
  public List<HTMLNodeModel> Selects { get; set; }

  public string? HTMLVersion { get; set; }

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
    Buttons = new List<HTMLNodeModel>();
    Metas = new List<HTMLNodeModel>();
    Divs = new List<HTMLNodeModel>();
    Spans = new List<HTMLNodeModel>();
    Paragraphs = new List<HTMLNodeModel>();
    Headers = new List<HTMLNodeModel>();
    Footers = new List<HTMLNodeModel>();
    Navs = new List<HTMLNodeModel>();
    Asides = new List<HTMLNodeModel>();
    Sections = new List<HTMLNodeModel>();
    Articles = new List<HTMLNodeModel>();
    Main = new List<HTMLNodeModel>();
    Details = new List<HTMLNodeModel>();
    Summary = new List<HTMLNodeModel>();
    Figures = new List<HTMLNodeModel>();
    FigCaptions = new List<HTMLNodeModel>();
    Iframes = new List<HTMLNodeModel>();
    Labels = new List<HTMLNodeModel>();
    Selects = new List<HTMLNodeModel>();
  }

}
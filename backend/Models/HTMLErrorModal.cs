namespace html_analyzer.Models
{
  public class HTMLError
  {
    public string? Code { get; set; }
    public string? Reason { get; set; }
    public string? Line { get; set; }
    public string? LinePosition { get; set; }
    public string? SourceText { get; set; }
    public string? StreamPosition { get; set; }

  }
}
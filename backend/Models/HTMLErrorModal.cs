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
    public string? Solution { get; set; }

    public HTMLError(string? code, string? reason, string? line, string? linePosition, string? sourceText, string? streamPosition)
    {
      Code = code;
      Reason = reason;
      Line = line;
      LinePosition = linePosition;
      SourceText = sourceText;
      StreamPosition = streamPosition;
    }

    public HTMLError(string reason)
    {
      Reason = reason;
    }

    public HTMLError(string reason, string solution)
    {
      Reason = reason;
      Solution = solution;
    }

  }
}
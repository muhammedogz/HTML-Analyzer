namespace html_analyzer.Models
{
  public class HTMLError
  {
    public string? Code { get; set; }
    public string? Reason { get; set; }
    public int? Line { get; set; }
    public int? LinePosition { get; set; }
    public string? SourceText { get; set; }
    public int? StreamPosition { get; set; }
    public string? Solution { get; set; }

    public HTMLError(string? code, string? reason, int? line, int? linePosition, string? sourceText, int? streamPosition)
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
using html_analyzer.Enums;

namespace html_analyzer.Models
{
  public class HTMLError
  {
    public ErrorEnums ErrorType { get; set; }
    public ErrorLevelEnums ErrorLevel { get; set; }
    public string? Code { get; set; }
    public string? Reason { get; set; }
    public int? Line { get; set; }
    public int? LinePosition { get; set; }
    public string? SourceText { get; set; }
    public int? StreamPosition { get; set; }
    public string? Solution { get; set; }

    public HTMLError()
    {
    }

    public HTMLError(ErrorEnums errorType, ErrorLevelEnums errorLevel, string? code, string reason, int? line, int? linePosition, string? sourceText, int? streamPosition, string solution)
    {
      ErrorType = errorType;
      Reason = reason;
      Solution = solution;
      ErrorLevel = errorLevel;
      Code = code;
      Line = line;
      LinePosition = linePosition;
      SourceText = sourceText;
      StreamPosition = streamPosition;
    }

    public HTMLError(ErrorEnums errorType, ErrorLevelEnums errorLevel, string reason, string solution)
    {
      ErrorType = errorType;
      Reason = reason;
      Solution = solution;
      ErrorLevel = errorLevel;
    }

  }
}
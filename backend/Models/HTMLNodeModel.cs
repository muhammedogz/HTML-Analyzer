namespace html_analyzer.Models;

public class HTMLNodeModel
{
  public string Name { get; set; }
  public string InnerText { get; set; }
  public string OuterHTML { get; set; }
  public string InnerHTML { get; set; }
  public List<HTMLAttributeModel> Attributes { get; set; }

  public HTMLNodeModel(string name, string innerText, string outerHTML, string innerHTML, List<HTMLAttributeModel> attributes)
  {
    Name = name;
    InnerText = innerText;
    OuterHTML = outerHTML;
    InnerHTML = innerHTML;
    Attributes = attributes;
  }

}
namespace html_analyzer.Models;

public class HTMLAttributeModel
{
    public string Name { get; set; }
    public string Value { get; set; }

    public HTMLAttributeModel(string name, string value)
    {
        Name = name;
        Value = value;
    }
}
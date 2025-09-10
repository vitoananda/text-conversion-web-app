namespace Domain.Entities;

public class ConversionResponse
{
    public string OriginalText { get; set; } = "";
    public string FirstResult { get; set; } = "";
    public int SecondResult { get; set; }
    public string ThirdResult { get; set; } = "";
    public string FourthResult { get; set; } = "";
    public string FifthResult { get; set; } = "";
}

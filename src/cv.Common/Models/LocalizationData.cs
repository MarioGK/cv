namespace cv.Common.Models;

public class LocalizationData
{
    public required string DateFormat { get; set; }
    public required Dictionary<string, string> Translations { get; set; }
}
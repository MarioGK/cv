namespace cv.Data;

public class LanguageData
{
    public string Language { get; set; } = null!;

    public string Introduction { get; set; } = null!;

    public Dictionary<string,string> Translations { get; set; } = new();

    public List<ExperienceData> Experiences { get; set; } = new();
}
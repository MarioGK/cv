namespace cv.Data;

public class CVData
{
    public string Language { get; set; } = null!;

    public string Introduction { get; set; } = null!;
    
    public string DateFormat { get; set; } = null!;

    public Dictionary<string, string> Translations { get; set; } = new();

    public List<ExperienceData> Experiences { get; set; } = new();
    
    public string GetTranslation(string key)
    {
        try
        {
            return Translations[key];
        }
        catch (Exception e)
        {
            return key;
        }
    }
}
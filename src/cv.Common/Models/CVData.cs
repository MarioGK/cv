namespace cv.Common.Models;

public class CVData
{
    public string Introduction { get; set; } = null!;

    public Language Language { get; set; }
    
    public List<ExperienceData> Experiences { get; set; } = new();
}
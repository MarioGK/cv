using YamlDotNet.Serialization;

namespace cv.Common.Models;

public class CVData
{
    public string Introduction { get; set; } = null!;

    public CVType Type { get; set; }
    
    public Language Language { get; set; }
    
    public List<ExperienceData> Experiences { get; set; } = new();
}
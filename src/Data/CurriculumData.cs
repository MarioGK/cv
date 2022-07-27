namespace cv.Data;

public class CurriculumData
{
    public string Language { get; set; }

    public Dictionary<string,string> Translations { get; set; }
    public List<ExperienceData> Experiences { get; set; }
}
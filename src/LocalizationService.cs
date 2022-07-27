using cv.Data;

namespace cv;

public class LocalizationService
{
    public List<ExperienceData> ExperienceDatas        { get; set; }
    public ExperienceData       SelectedExperienceData { get; set; }
    public SkillsData           SkillsData             { get; set; }

    public LocalizationService()
    {
        ExperienceDatas        = new List<ExperienceData>();
        SelectedExperienceData = new ExperienceData();
        SkillsData             = new SkillsData();
    }
}
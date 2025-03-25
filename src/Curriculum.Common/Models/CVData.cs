using Curriculum.Common.Enums;

namespace Curriculum.Common.Models;

public class CVData
{
    public required string Introduction { get; set; }

    public required CurriculumType Type { get; set; } = CurriculumType.Developer;

    public Language Language { get; set; } = Language.English;

    /// <summary>
    /// Experience data
    /// </summary>
    public List<ExperienceData> Experiences { get; set; } = [];
    
    public List<SkillData> SkillsData { get; set; } = [];
    public List<SkillData> LanguageData { get; set; } = [];
    
    public Dictionary<string, string>? SelectedPersonalInfo { get; set; }
}
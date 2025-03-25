using Curriculum.Common.Enums;
using Curriculum.Common.Models;

namespace Curriculum.Common.Interfaces;

public interface ICurriculumDataProvider
{
    ILocalizationProvider LocalizationProvider { get; }
    CVData? SelectedCVData { get; set; }
    Dictionary<string, string>? SelectedPersonalInfo { get; set; }
    CurriculumType SelectedType { get; set; }
    List<SkillData> SkillsData { get; set; }
    List<SkillData> LanguageData { get; set; }
    List<CVData>? CVData { get; set; }
    Task<T> GetFromYamlAsync<T>(string path);
}
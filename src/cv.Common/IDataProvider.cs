using cv.Common.Models;

namespace cv.Common;

public interface IDataProvider
{
    public delegate void LocalizationChangedDelegate();

    Task    ChangeLanguage(Language    language = Language.English);
    Task<T> GetFromYamlAsync<T>(string url);
    string  Get(string                 id);

    CVData                                 SelectedCVData       { get; set; }
    Dictionary<string, string>             SelectedPersonalInfo { get; set; }
    LocalizationData                       SelectedLocalization { get; set; }
    Dictionary<Language, LocalizationData> LocalizationData     { get; set; }
    List<SkillData>                        SkillsData           { get; set; }
    List<SkillData>                        LanguageData         { get; set; }

    event LocalizationChangedDelegate LocalizationChanged;
}
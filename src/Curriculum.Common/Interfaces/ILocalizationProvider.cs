using Curriculum.Common.Enums;
using Curriculum.Common.Models;

namespace Curriculum.Common.Interfaces;

public interface ILocalizationProvider
{
    public delegate void LocalizationChangedDelegate();
    event LocalizationChangedDelegate LocalizationChanged;
    
    Task ChangeLanguage(Language language = Language.English);
    string Get(string id);
    LocalizationData? SelectedLocalization { get; set; }
    Dictionary<Language, LocalizationData> LocalizationData { get; set; }
    Dictionary<Language, Dictionary<string, string>> PersonalData { get; set; }
    
    // Method to notify when localization has changed
    void NotifyLocalizationChange(Language language);
}
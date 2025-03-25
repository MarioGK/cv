using Curriculum.Common.Enums;
using Curriculum.Common.Interfaces;
using Curriculum.Common.Models;

namespace Curriculum.Common.Implementations;

public class LocalLocalizationProvider : ILocalizationProvider
{
    public event ILocalizationProvider.LocalizationChangedDelegate? LocalizationChanged;
    
    public LocalizationData? SelectedLocalization { get; set; }
    public Dictionary<Language, LocalizationData> LocalizationData { get; set; } = new();
    public Dictionary<Language, Dictionary<string, string>> PersonalData { get; set; } = new();

    public virtual async Task ChangeLanguage(Language language = Language.English)
    {
        // Implementation would need to load data from somewhere
        throw new NotImplementedException("This base implementation does not provide data loading functionality");
    }

    public string Get(string id)
    {
        if (SelectedLocalization == null)
        {
            return id;
        }

        if (!SelectedLocalization.Translations.TryGetValue(id, out var value))
        {
            return id;
        }

        return value;
    }

    public void NotifyLocalizationChange(Language language)
    {
        if (!LocalizationData.TryGetValue(language, out var localization))
        {
            return;
        }

        SelectedLocalization = localization;
        LocalizationChanged?.Invoke();
    }
}
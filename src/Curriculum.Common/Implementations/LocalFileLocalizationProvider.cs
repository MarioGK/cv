using Curriculum.Common.Enums;
using Curriculum.Common.Interfaces;
using Curriculum.Common.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Curriculum.Common.Implementations;

public class LocalFileLocalizationProvider : ILocalizationProvider
{
    protected static readonly IDeserializer YamlDeserializer =
        new DeserializerBuilder().WithNamingConvention(PascalCaseNamingConvention.Instance).Build();
    
    public LocalFileLocalizationProvider()
    {
        LocalizationData = new Dictionary<Language, LocalizationData>();
        PersonalData = new Dictionary<Language, Dictionary<string, string>>();
    }

    public event ILocalizationProvider.LocalizationChangedDelegate? LocalizationChanged;
    public LocalizationData? SelectedLocalization { get; set; }
    public Dictionary<Language, LocalizationData> LocalizationData { get; set; }
    public Dictionary<Language, Dictionary<string, string>> PersonalData { get; set; }

    public async Task ChangeLanguage(Language language = Language.English)
    {
        if (!PersonalData.ContainsKey(language))
        {
            var personalInfo = await GetFromLocalYamlAsync<Dictionary<string, string>>($"Data/PersonalInfo/{language}.yaml");
            PersonalData.Add(language, personalInfo);
        }

        if (!LocalizationData.ContainsKey(language))
        {
            var localizationData = await GetFromLocalYamlAsync<LocalizationData>($"Data/Localizations/{language}.yaml");
            LocalizationData.Add(language, localizationData);
        }

        NotifyLocalizationChange(language);
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

    private async Task<T> GetFromLocalYamlAsync<T>(string path)
    {
        var content = await File.ReadAllTextAsync(Path.Combine(AppContext.BaseDirectory, path));
        var data = YamlDeserializer.Deserialize<T>(content);
        return data;
    }
}
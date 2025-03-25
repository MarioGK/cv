using Curriculum.Common.Enums;
using Curriculum.Common.Interfaces;
using Curriculum.Common.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Curriculum.Implementations;

public class WebLocalizationProvider(HttpClient httpClient) : ILocalizationProvider
{
    protected static readonly IDeserializer YamlDeserializer =
        new DeserializerBuilder().WithNamingConvention(PascalCaseNamingConvention.Instance).Build();

    public event ILocalizationProvider.LocalizationChangedDelegate? LocalizationChanged;
    public LocalizationData? SelectedLocalization { get; set; }
    public Dictionary<Language, LocalizationData> LocalizationData { get; set; } = new();
    public Dictionary<Language, Dictionary<string, string>> PersonalData { get; set; } = new();

    public async Task ChangeLanguage(Language language = Language.English)
    {
        if (!PersonalData.ContainsKey(language))
        {
            var personalInfo = await GetFromYamlAsync<Dictionary<string, string>>($"Data/PersonalInfo/{language}.yaml");
            PersonalData.Add(language, personalInfo);
        }

        if (!LocalizationData.ContainsKey(language))
        {
            var localizationData = await GetFromYamlAsync<LocalizationData>($"Data/Localizations/{language}.yaml");
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

        return SelectedLocalization.Translations.GetValueOrDefault(id, id);
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

    private async Task<T> GetFromYamlAsync<T>(string url)
    {
        var data = await httpClient.GetStringAsync(url);
        return YamlDeserializer.Deserialize<T>(data);
    }
}
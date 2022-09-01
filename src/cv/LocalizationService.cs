using cv.Data;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace cv;

public class LocalizationService
{
    private readonly HttpClient                       _httpClient;
    public           Dictionary<string, LanguageData> LanguageDatas        { get; set; }
    public           LanguageData                    SelectedLanguageData { get; set; }
    public           List<SkillsData>?                SkillsData           { get; set; }

    public List<string> AvailableLanguages = new()
    {
        "English",
        "Português"
    };

    public LocalizationService(HttpClient httpClient)
    {
        _httpClient   = httpClient;
        LanguageDatas = new Dictionary<string, LanguageData>();
    }

    public string Get(string id)
    {
        if (SelectedLanguageData?.Translations.TryGetValue(id, out var value) ?? false)
        {
            return value;
        }
        return $"|{id}| does not exists";
    }

    private static readonly IDeserializer YamlDeserializer =
        new DeserializerBuilder().WithNamingConvention(PascalCaseNamingConvention.Instance).Build();

    public async Task ChangeLanguage(string language = "english")
    {
        SkillsData ??= await GetFromYamlAsync<List<SkillsData>>("data/skills.yaml");
        if (LanguageDatas.ContainsKey(language))
        {
            NotifyLanguageChange(LanguageDatas[language]);
            Console.WriteLine($"Language changed to {language}!!");
            return;
        }
        
        var languageData = await GetFromYamlAsync<LanguageData>($"data/languages/{language.ToLowerInvariant()}.yaml");

        LanguageDatas.Add(language, languageData);
        NotifyLanguageChange(languageData);
    }

    private async Task<T>  GetFromYamlAsync<T>(string url)
    {
        var data         = await _httpClient.GetStringAsync(url);
        return YamlDeserializer.Deserialize<T>(data);
    }

    public delegate void LanguageChangedDelegate();

    public event LanguageChangedDelegate? LanguageChanged;

    private void NotifyLanguageChange(LanguageData data)
    {
        SelectedLanguageData = data;
        LanguageChanged?.Invoke();
    }
}
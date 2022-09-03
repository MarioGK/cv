using cv.Data;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace cv;

public class LocalizationService
{
    public delegate void LanguageChangedDelegate();

    private static readonly IDeserializer YamlDeserializer =
        new DeserializerBuilder().WithNamingConvention(PascalCaseNamingConvention.Instance).Build();

    private readonly HttpClient _httpClient;

    public List<string> AvailableLanguages = new()
    {
        "English",
        "Português"
    };

    public LocalizationService(HttpClient httpClient)
    {
        _httpClient   = httpClient;
        LanguageDatas = new Dictionary<string, CVData>();
    }

    public Dictionary<string, CVData> LanguageDatas  { get; set; }
    public CVData                     SelectedCVData { get; set; }
    public List<SkillsData>?          SkillsData     { get; set; }

    public string Get(string id)
    {
        //Console.WriteLine(string.Join(',', SelectedLanguageData.Translations.Keys));
        if (!SelectedCVData.Translations.TryGetValue(id, out var value))
        {
            return id;
        }

        //Console.WriteLine($"{id} = {value}");
        return value;
    }

    public async Task ChangeLanguage(string language = "english")
    {
        SkillsData ??= await GetFromYamlAsync<List<SkillsData>>("data/skills.yaml");
        if (LanguageDatas.ContainsKey(language))
        {
            NotifyLanguageChange(LanguageDatas[language]);
            //Console.WriteLine($"Language changed to {language}!!");
            return;
        }

        var languageData = await GetFromYamlAsync<CVData>($"data/cv/{language.ToLowerInvariant()}.yaml");

        LanguageDatas.Add(language, languageData);
        NotifyLanguageChange(languageData);
    }

    private async Task<T> GetFromYamlAsync<T>(string url)
    {
        var data = await _httpClient.GetStringAsync(url);
        return YamlDeserializer.Deserialize<T>(data);
    }

    public event LanguageChangedDelegate? LanguageChanged;

    private void NotifyLanguageChange(CVData data)
    {
        SelectedCVData = data;
        LanguageChanged?.Invoke();
    }
}
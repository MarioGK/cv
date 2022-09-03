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
        _httpClient  = httpClient;
        CVDatas      = new Dictionary<string, CVData>();
        PersonalData = new Dictionary<string, Dictionary<string, string>>();
    }

    public Dictionary<string, CVData>                   CVDatas        { get; set; }
    public CVData                                       SelectedCVData { get; set; }
    public Dictionary<string,string>                    SelectedPersonalInfo { get; set; }
    public List<SkillsData>?                            SkillsData     { get; set; }
    public List<SkillsData>?                            LanguageData   { get; set; }
    public Dictionary<string,Dictionary<string,string>> PersonalData   { get; set; }

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
        language = language.ToLowerInvariant();
        //If null fetch information from server, if not ignore
        SkillsData   ??= await GetFromYamlAsync<List<SkillsData>>("data/skills.yaml");
        LanguageData ??= await GetFromYamlAsync<List<SkillsData>>("data/languages.yaml");
        
        if (CVDatas.ContainsKey(language))
        {
            NotifyLanguageChange(CVDatas[language]);
            //Console.WriteLine($"Language changed to {language}!!");
            return;
        }

        var languageData = await GetFromYamlAsync<CVData>($"data/cv/{language}.yaml");
        CVDatas.Add(language, languageData);
        
        var personalInfo = await GetFromYamlAsync<Dictionary<string,string>>($"data/personalInfo/{language}.yaml");
        PersonalData.Add(language, personalInfo);
        
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
        SelectedPersonalInfo = PersonalData[data.Language.ToLowerInvariant()];
        LanguageChanged?.Invoke();
    }
}
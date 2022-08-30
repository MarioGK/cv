using System.Net.Http.Json;
using cv.Data;

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

    public async Task ChangeLanguage(string language = "english")
    {
        SkillsData ??= await _httpClient.GetFromJsonAsync<List<SkillsData>>("data/skills.json");
        if (LanguageDatas.ContainsKey(language))
        {
            NotifyLanguageChange(LanguageDatas[language]);
            Console.WriteLine($"Language changed to {language}!!");
            return;
        }

        var data = await _httpClient.GetFromJsonAsync<LanguageData>($"data/languages/{language.ToLowerInvariant()}.json");
        if (data == null)
        {
            return;
        }

        LanguageDatas.Add(language, data);
        NotifyLanguageChange(data);
    }

    public delegate void LanguageChangedDelegate();

    public event LanguageChangedDelegate? LanguageChanged;

    private void NotifyLanguageChange(LanguageData data)
    {
        SelectedLanguageData = data;
        LanguageChanged?.Invoke();
    }
}
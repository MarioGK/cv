using cv.Common;
using cv.Common.Models;

namespace cv;

public class WebDataProvider : BaseDataProvider, IDataProvider
{
    private readonly HttpClient _httpClient;

    public WebDataProvider(HttpClient httpClient)
    {
        _httpClient  = httpClient;
    }

    public override async Task<T> GetFromYamlAsync<T>(string url)
    {
        var data = await _httpClient.GetStringAsync(url);
        return YamlDeserializer.Deserialize<T>(data);
    }

    private async Task LoadStaticData()
    {
        if (!SkillsData.Any())
        {
            SkillsData = await GetFromYamlAsync<List<SkillData>>("Data/Skills.yaml");
        }

        if (!LanguageData.Any())
        {
            LanguageData = await GetFromYamlAsync<List<SkillData>>("Data/Languages.yaml");
        }
    }

    public override async Task ChangeLanguage(Language language = Language.English)
    {
        await LoadStaticData();
        await base.ChangeLanguage(language);
    }
}
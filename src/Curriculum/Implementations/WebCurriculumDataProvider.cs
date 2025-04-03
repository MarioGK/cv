using Curriculum.Common.Base;
using Curriculum.Common.Implementations;
using Curriculum.Common.Interfaces;
using Curriculum.Common.Models;

namespace Curriculum.Implementations;

public class WebCurriculumDataProvider : BaseCurriculumDataProvider, ICurriculumDataProvider
{
    private readonly HttpClient _httpClient;

    public WebCurriculumDataProvider(HttpClient httpClient, ILocalizationProvider localizationProvider) : base(localizationProvider)
    {
        _httpClient = httpClient;
    }

    public override async Task<T> GetFromYamlAsync<T>(string url)
    {
        var data = await _httpClient.GetStringAsync(url);
        return YamlSerializer.Deserialize<T>(data);
    }

    public async Task LoadStaticData()
    {
        if (SkillsData.Count == 0)
        {
            SkillsData = await GetFromYamlAsync<List<SkillData>>("Data/Skills.yaml");
        }

        if (LanguageData.Count == 0)
        {
            LanguageData = await GetFromYamlAsync<List<SkillData>>("Data/Languages.yaml");
        }
    }
}
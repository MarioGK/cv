using cv.Common.Models;

namespace cv.Common;

public class LocalDataProvider : BaseDataProvider, IDataProvider
{
    public LocalDataProvider()
    {
        SkillsData   = YamlDeserializer.Deserialize<List<SkillData>>(File.ReadAllText("Data/Skills.yaml"));
        LanguageData = YamlDeserializer.Deserialize<List<SkillData>>(File.ReadAllText("Data/Languages.yaml"));
    }

    public override async Task<T> GetFromYamlAsync<T>(string url)
    {
        var content      = await File.ReadAllTextAsync(url);
        var data = YamlDeserializer.Deserialize<T>(content);
        return data;
    }
}
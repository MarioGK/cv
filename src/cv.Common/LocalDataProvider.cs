using cv.Common.Models;

namespace cv.Common;

public class LocalDataProvider : BaseDataProvider, IDataProvider
{
    private readonly string _dataDir = Path.Combine(AppContext.BaseDirectory, "Data");

    public LocalDataProvider()
    {
        SkillsData   = YamlDeserializer.Deserialize<List<SkillData>>(File.ReadAllText(Path.Combine(_dataDir, "CV", "Skills.yaml")));
        LanguageData = YamlDeserializer.Deserialize<List<SkillData>>(File.ReadAllText(Path.Combine(_dataDir, "Languages.yaml")));
    }

    public override async Task<T> GetFromYamlAsync<T>(string url)
    {
        var content = await File.ReadAllTextAsync(Path.Combine(AppContext.BaseDirectory, url));
        var data    = YamlDeserializer.Deserialize<T>(content);
        return data;
    }
}
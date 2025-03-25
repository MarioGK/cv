using Curriculum.Common.Base;
using Curriculum.Common.Interfaces;
using Curriculum.Common.Models;
using Curriculum.Common.Services;
using Microsoft.Extensions.Logging;

namespace Curriculum.Common;

public class LocalCurriculumDataProvider : BaseCurriculumDataProvider, ICurriculumDataProvider
{
    private readonly string _dataDir = Path.Combine(AppContext.BaseDirectory, "Data");

    public LocalCurriculumDataProvider(
        ILocalizationProvider localizationProvider,
        IYamlSerializerService yamlSerializer,
        ILogger<LocalCurriculumDataProvider> logger) 
        : base(localizationProvider, yamlSerializer, logger)
    {
        _logger.LogInformation("Initializing LocalCVCVDataProvider from {DataDir}", _dataDir);
        
        try
        {
            var skillsPath = Path.Combine(_dataDir, "Skills.yaml");
            _logger.LogDebug("Loading skills from {Path}", skillsPath);
            var skillsContent = File.ReadAllText(skillsPath);
            SkillsData = _yamlSerializer.Deserialize<List<SkillData>>(skillsContent);
            
            var languagesPath = Path.Combine(_dataDir, "Languages.yaml");
            _logger.LogDebug("Loading languages from {Path}", languagesPath);
            var languageContent = File.ReadAllText(languagesPath);
            LanguageData = _yamlSerializer.Deserialize<List<SkillData>>(languageContent);
            
            _logger.LogInformation("Loaded {SkillCount} skills and {LanguageCount} languages", 
                SkillsData.Count, LanguageData.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading initial data for LocalCVCVDataProvider");
            throw;
        }
    }

    public override async Task<T> GetFromYamlAsync<T>(string url)
    {
        var fullPath = Path.Combine(AppContext.BaseDirectory, url);
        _logger.LogDebug("Loading YAML data from {Path} for type {Type}", fullPath, typeof(T).Name);
        
        try
        {
            var content = await File.ReadAllTextAsync(fullPath);
            var data = _yamlSerializer.Deserialize<T>(content);
            return data;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading YAML data from {Path} for type {Type}", fullPath, typeof(T).Name);
            throw;
        }
    }
}
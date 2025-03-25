using Curriculum.Common.Enums;
using Curriculum.Common.Interfaces;
using Curriculum.Common.Models;
using Curriculum.Common.Services;
using Microsoft.Extensions.Logging;

namespace Curriculum.Common.Base;

public abstract class BaseCurriculumDataProvider : ICurriculumDataProvider
{
    protected readonly IYamlSerializerService _yamlSerializer;
    protected readonly ILogger<BaseCurriculumDataProvider> _logger;

    protected BaseCurriculumDataProvider(
        ILocalizationProvider localizationProvider,
        IYamlSerializerService yamlSerializer,
        ILogger<BaseCurriculumDataProvider> logger)
    {
        LocalizationProvider = localizationProvider;
        _yamlSerializer = yamlSerializer;
        _logger = logger;
        
        SkillsData = [];
        LanguageData = [];
        CVData = [];

        // Subscribe to localization changed events to update selected data
        LocalizationProvider.LocalizationChanged += () => 
        {
            if (LocalizationProvider.SelectedLocalization == null) return;
            
            var language = LocalizationProvider.SelectedLocalization.Language;
            if (CVData == null) return;

            var cvData = CVData.FirstOrDefault(x => x.Language == language && x.Type == SelectedType);
            if (cvData == null) 
            {
                _logger.LogWarning("No CV data found for language {Language} and type {Type}", language, SelectedType);
                return;
            }

            SelectedCVData = cvData;
            SelectedPersonalInfo = LocalizationProvider.PersonalData.TryGetValue(language, out var personalInfo) ? personalInfo : null;
            _logger.LogInformation("Updated selected CV data for language {Language}", language);
        };
    }

    public ILocalizationProvider LocalizationProvider { get; }
    
    public CVData? SelectedCVData { get; set; }
    public Dictionary<string, string>? SelectedPersonalInfo { get; set; }
    public List<SkillData> SkillsData { get; set; }
    public List<SkillData> LanguageData { get; set; }
    public CurriculumType SelectedType { get; set; } = CurriculumType.Developer;
    public List<CVData>? CVData { get; set; }

    public abstract Task<T> GetFromYamlAsync<T>(string path);
}
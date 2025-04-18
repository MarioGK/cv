﻿using Curriculum.Common.Enums;
using Curriculum.Common.Implementations;
using Curriculum.Common.Interfaces;
using Curriculum.Common.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Curriculum.Common.Base;

public abstract class BaseCurriculumDataProvider : ICurriculumDataProvider
{
    protected readonly ILogger<BaseCurriculumDataProvider> _logger;

    protected BaseCurriculumDataProvider(
        ILocalizationProvider localizationProvider,
        ILogger<BaseCurriculumDataProvider> logger)
    {
        LocalizationProvider = localizationProvider;
        _logger = logger;
        
        SkillsData = [];
        LanguageData = [];
        CurriculumData = [];

        // Subscribe to localization changed events to update selected data
        LocalizationProvider.LocalizationChanged += () => 
        {
            if (LocalizationProvider.SelectedLocalization == null) return;
            
            var language = LocalizationProvider.SelectedLocalization.Language;
            if (CurriculumData == null) return;

            var cvData = CurriculumData.FirstOrDefault(x => x.Language == language && x.Type == SelectedType);
            if (cvData == null) 
            {
                _logger.LogWarning("No CV data found for language {Language} and type {Type}", language, SelectedType);
                return;
            }

            SelectedCurriculumData = cvData;
            SelectedPersonalInfo = LocalizationProvider.PersonalData.TryGetValue(language, out var personalInfo) ? personalInfo : null;
            _logger.LogInformation("Updated selected CV data for language {Language}", language);
        };
    }

    // Constructor overload for backward compatibility
    protected BaseCurriculumDataProvider(
        ILocalizationProvider localizationProvider)
    {
        LocalizationProvider = localizationProvider;
        _logger = NullLogger<BaseCurriculumDataProvider>.Instance;
        
        SkillsData = [];
        LanguageData = [];
        CurriculumData = [];
    }

    public ILocalizationProvider LocalizationProvider { get; }
    
    public CurriculumData? SelectedCurriculumData { get; set; }
    public Dictionary<string, string>? SelectedPersonalInfo { get; set; }
    public List<SkillData> SkillsData { get; set; }
    public List<SkillData> LanguageData { get; set; }
    public CurriculumType SelectedType { get; set; } = CurriculumType.Developer;
    public List<CurriculumData>? CurriculumData { get; set; }

    public abstract Task<T> GetFromYamlAsync<T>(string path);
}
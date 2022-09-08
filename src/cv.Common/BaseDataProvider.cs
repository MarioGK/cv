using cv.Common.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace cv.Common;

public abstract class BaseDataProvider
{
    protected static readonly IDeserializer YamlDeserializer =
        new DeserializerBuilder().WithNamingConvention(PascalCaseNamingConvention.Instance).Build();
    
    public CVData                     SelectedCVData       { get; set; }
    public Dictionary<string, string> SelectedPersonalInfo { get; set; }
    public LocalizationData           SelectedLocalization { get; set; }

    public List<SkillData> SkillsData   { get; set; }
    public List<SkillData> LanguageData { get; set; }

    public CVType SelectedType { get; set; } = CVType.Developer;
    
    /// <summary>
    /// List of all the localizations, key being the language
    /// </summary>
    public Dictionary<Language, LocalizationData> LocalizationData { get; set; }
    
    /// <summary>
    /// List of all the personal data, key being the language
    /// </summary>
    public Dictionary<Language, Dictionary<string, string>> PersonalData { get; set; }
    
    /// <summary>
    /// List of all the cv data, key being the language
    /// </summary>
    public List<CVData> CVData { get; set; }

    public BaseDataProvider()
    {
        SkillsData       = new List<SkillData>();
        LanguageData     = new List<SkillData>();
        LocalizationData = new Dictionary<Language, LocalizationData>();
        CVData           = new List<CVData>();
        PersonalData     = new Dictionary<Language, Dictionary<string, string>>();
    }
    
    public string Get(string id)
    {
        //Console.WriteLine(string.Join(',', SelectedLanguageData.Translations.Keys));
        if (!SelectedLocalization.Translations.TryGetValue(id, out var value))
        {
            return id;
        }

        //Console.WriteLine($"{id} = {value}");
        return value;
    }
    
    public event IDataProvider.LocalizationChangedDelegate? LocalizationChanged;

    protected void NotifyLocalizationChange(Language language)
    {
        var cvData = CVData.FirstOrDefault(x => x.Language == language && x.Type == SelectedType);
        if (cvData == null)
        {
            Console.WriteLine($"No CV data for {language} and {SelectedType}");
            return;
        }
        
        SelectedLocalization = LocalizationData[language];
        SelectedCVData       = cvData;
        SelectedPersonalInfo = PersonalData[language];
        LocalizationChanged?.Invoke();
    }
    
    public abstract Task<T> GetFromYamlAsync<T>(string path);

    public virtual async Task ChangeLanguage(Language language = Language.English)
    {
        if (!PersonalData.ContainsKey(language))
        {
            var personalInfo = await GetFromYamlAsync<Dictionary<string, string>>($"Data/PersonalInfo/{language}.yaml");
            PersonalData.Add(language, personalInfo);
        }
        
        if (!LocalizationData.ContainsKey(language))
        {
            var localizationData = await GetFromYamlAsync<LocalizationData>($"Data/Localizations/{language}.yaml");
            LocalizationData.Add(language, localizationData);
        }

        var existingCvData = CVData.FirstOrDefault(x => x.Language == language && x.Type == SelectedType);
        if (existingCvData != null)
        {
            NotifyLocalizationChange(language);
            return;
        }

        var cvData = await GetFromYamlAsync<CVData>($"Data/CVs/{SelectedType.ToString()}/{language}.yaml");
        cvData.Language = language;
        CVData.Add(cvData);
        NotifyLocalizationChange(language);
    }
}
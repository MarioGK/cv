using Curriculum.Common.Enums;

namespace Curriculum.Common.Models;

public class LocalizationData
{
    /// <summary>
    /// Localized date format, because we are using Invariant
    /// </summary>
    public required string DateFormat { get; set; }
    public required Dictionary<string, string> Translations { get; set; } = [];
    public Language Language { get; set; }
}
namespace Curriculum.Common.Models;

public class ExperienceData
{
    /// <summary>
    /// Used to display an avatar for the experience
    /// </summary>
    public string? ExperienceAvatarLink { get; set; }
    public required string Company { get; set; }
    
    public string? CompanyLogoLink { get; set; }
    
    public string? CompanyLinkedinLink { get; set; }
    public required string Position { get; set; }
    public required string Description { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    
    /// <summary>
    /// Images for the gallery
    /// </summary>
    public string? ImagesDir { get; set; }
    
    public List<string> Images =>
        Directory.Exists(ImagesDir) ? Directory.GetFiles(ImagesDir).ToList() : [];

    public string Title => $"{Company} - {Position}";
}
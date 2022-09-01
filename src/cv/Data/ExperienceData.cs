namespace cv.Data;

public class ExperienceData
{
    public string   Company     { get; set; } = null!;
    public string   Position    { get; set; } = null!;
    public string   Description { get; set; } = null!;
    public DateTime StartDate   { get; set; } = default!;
    public DateTime EndDate     { get; set; } = default!;

    public string Title => $"{Company} - {Position}";
}
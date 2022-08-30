using System.Text.Json;
using cv.Data;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;


var pdfDir      = $"{Directory.GetCurrentDirectory()}/publish/wwwroot/pdfs/";
var dataDir     = $"{Directory.GetCurrentDirectory()}/src/cv/wwwroot/data/";
var languageDir = $"{dataDir}languages";

var languages = Directory.EnumerateFiles(languageDir, "*.json")
                         .Select(File.ReadAllText)
                         .Select(json => JsonSerializer.Deserialize<LanguageData>(json)!)
                         .ToList();

var skills      = JsonSerializer.Deserialize<List<SkillsData>>(File.ReadAllText($"{dataDir}skills.json"));

foreach (var language in languages)
{
    var document = Document.Create(container =>
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(2, Unit.Centimetre);
            page.PageColor(Colors.White);
            page.DefaultTextStyle(x => x.FontSize(20));

            page.Header()
                .Text(language.Language)
                .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

            page.Content()
                .PaddingVertical(1, Unit.Centimetre)
                .Column(x =>
                 {
                     x.Spacing(20);
                     x.Item().Image(Placeholders.Image(200, 100));
                 });
        });
    });
    
    //document.ShowInPreviewer();
    
    Directory.CreateDirectory(pdfDir);
    document.GeneratePdf($"{pdfDir}{language.Language.ToLower()}.pdf");
}
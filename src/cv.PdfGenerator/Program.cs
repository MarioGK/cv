using System.Text.Json;
using cv.Data;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;

#if DEBUG
var dataDir = @"F:\Projects\cv\src\cv\wwwroot\data\";
var pdfDir  = "pdfs\\";
#else
var dataDir     = $"{Directory.GetCurrentDirectory()}/src/cv/wwwroot/data/";
var pdfDir      = $"{Directory.GetCurrentDirectory()}/publish/wwwroot/pdfs/";
#endif

var languageDir = $"{dataDir}languages";

var languages = Directory.EnumerateFiles(languageDir, "*.json")
                         .Select(File.ReadAllText)
                         .Select(json => JsonSerializer.Deserialize<LanguageData>(json)!)
                         .ToList();

var skills      = JsonSerializer.Deserialize<List<SkillsData>>(File.ReadAllText($"{dataDir}skills.json"));

var fontFiles = Directory.EnumerateFiles("Fonts", "*.ttf")
         .ToList();
fontFiles.ForEach(font => FontManager.RegisterFont(File.OpenRead(font)));

foreach (var language in languages)
{
    Console.WriteLine($"Generating PDF for {language.Language}...");
    var document = Document.Create(container =>
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(1, Unit.Centimetre);
            page.PageColor(Colors.White);
            page.DefaultTextStyle(x => x.FontSize(16)
                                        .FontFamily("Roboto")
                                        .FontColor(Colors.Black));

            page.Header()
                .Text(language.Language)
                .Bold().FontSize(24).FontColor(Colors.Blue.Accent2);


            page.Content()
                .PaddingVertical(1, Unit.Centimetre)
                .Column(column =>
                 {
                     column.Spacing(10);
                     foreach (var exp in language.Experiences)
                     {
                         column.Item().Text(exp.Title).SemiBold().FontColor(Colors.Blue.Accent1);
                         column.Item().Text(exp.Description).FontSize(12);
                     }
                 });

            page.Footer()
                .AlignCenter()
                .Text(x =>
                 {
                     x.Span("Page ");
                     x.CurrentPageNumber();
                 });
        });
    });
    
    Directory.CreateDirectory(pdfDir);
    var fileName = $"{language.Language.ToLower()}.pdf";
    document.GeneratePdf($"{pdfDir}{fileName}");
    Console.WriteLine($"Generated {fileName}!");
}

Console.WriteLine("Finished!");
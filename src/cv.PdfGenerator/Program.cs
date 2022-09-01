﻿using System.Text.Json;
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

var fontFiles = Directory.EnumerateFiles(Path.Combine(AppContext.BaseDirectory, "Fonts"), "*.ttf")
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
            //page.Margin(1, Unit.Centimetre);
            page.PageColor(Colors.White);
            page.DefaultTextStyle(x => x.FontSize(12)
                                        .FontFamily("Roboto")
                                        .FontColor(Colors.Black));

            page.Header()
                .PaddingTop(1, Unit.Centimetre)
                .PaddingHorizontal(1, Unit.Centimetre)
                .Column(c =>
                 {
                     c.Item().Text(language.Language)
                      .Bold().FontSize(24).FontColor(Colors.Blue.Accent2);

                     c.Item().Text(language.Introduction);
                     c.Item().PaddingBottom(5);
                 });


            page.Content()
                .PaddingHorizontal(1, Unit.Centimetre)
                .PaddingBottom(10)
                .Column(column =>
                 {
                     column.Spacing(10);
                     foreach (var exp in language.Experiences)
                     {
                         column.Item()
                               .Text(exp.Title)
                               .SemiBold()
                               .FontSize(14)
                               .FontColor(Colors.Blue.Accent1);
                         column.Item().Text(exp.Description);
                     }
                 });
            
            page.Footer()
                .DefaultTextStyle(text => text.FontSize(9).Light())
                .AlignRight()
                .AlignBottom()
                .Row(row =>
                 {
                     row.AutoItem().Text($"*This PDF was automatically generated from ");
                     row.AutoItem().Hyperlink("https://cv.mariogk.top/").Text("cv.mariogk.top").FontColor(Colors.Blue.Darken1);
                     row.AutoItem().Text(" for a better experience please visit the website.");
                 });
        });
    });
    
    Directory.CreateDirectory(pdfDir);
    var fileName = $"{language.Language.ToLower()}.pdf";
    document.GeneratePdf($"{pdfDir}{fileName}");
    Console.WriteLine($"Generated {fileName}!");
}

Console.WriteLine("Finished!");
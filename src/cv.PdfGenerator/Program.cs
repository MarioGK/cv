﻿// See https://aka.ms/new-console-template for more information

// code in your main method

using System.Text.Json;
using cv.Data;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;

var pdfDir = @"publish\wwwroot\pdfs\";

var languages = Directory.EnumerateFiles(@"src\cv\wwwroot\data\languages", "*.json")
                           .Select(File.ReadAllText)
                           .Select(json => JsonSerializer.Deserialize<LanguageData>(json)!)
                           .ToList();

var skills      = JsonSerializer.Deserialize<List<SkillsData>>(File.ReadAllText(@"src\cv\wwwroot\data\skills.json"));

foreach(var language in languages)
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
                
                     x.Item().Text(Placeholders.LoremIpsum());
                     x.Item().Image(Placeholders.Image(200, 100));
                 });
        });
    });

    #if RELEASE
    Directory.CreateDirectory(pdfDir);
    document.GeneratePdf($"{pdfDir}{language.Language.ToLower()}.pdf");
    #endif

    #if DEBUG
    //document.ShowInPreviewer();
    #endif
}
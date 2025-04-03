using Curriculum.Common;
using Curriculum.Common.Enums;
using Curriculum.Common.Implementations;
using Curriculum.Common.Interfaces;
using Curriculum.Common.Models;
using Curriculum.PdfGenerator.Components;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Microsoft.Extensions.Logging;

QuestPDF.Settings.License = LicenseType.Community;

#if DEBUG
var outputDir = "pdfs\\";
#else
var outputDir = $"{Directory.GetCurrentDirectory()}/publish/wwwroot/pdfs/";
#endif

var fontsDir = Path.Combine(AppContext.BaseDirectory, "Fonts");
var profileImage = Path.Combine(AppContext.BaseDirectory, "Data", "Images", "Profile.jpg");

// Create the localization provider first
var localizationProvider = new LocalFileLocalizationProvider();

// Configure logging
var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

// Set the logger for YamlSerializer
YamlSerializer.SetLogger(loggerFactory.CreateLogger("YamlSerializer"));

// Create a logger for the data provider
var logger = loggerFactory.CreateLogger<LocalCurriculumDataProvider>();

// Then create CV data provider that uses the localization provider
var cvDataProvider = new LocalCurriculumDataProvider(localizationProvider, logger);

var languages = Enum.GetValues(typeof(Language)).Cast<Language>();

foreach (var language in languages)
{
    // Use the localization provider to change language
    await localizationProvider.ChangeLanguage(language);
    
    // Load CV data for this language
    await LoadCVDataForLanguage(cvDataProvider, language);
    
    var cv = cvDataProvider.SelectedCurriculumData ?? throw new Exception($"No CV data for {language} and {cvDataProvider.SelectedType}");
    Console.WriteLine($"Generating PDF for {language}...");

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
                     //Title
                     //c.Item().Text(cv.Language).Bold().FontSize(24).FontColor(Colors.Blue.Accent2);
                 });

            page.Content()
                .PaddingHorizontal(1, Unit.Centimetre)
                .PaddingBottom(10)
                .Column(column =>
                 {
                     //Introduction and contacts
                     column.Item().Row(introRow =>
                     {
                         introRow.Spacing(10);
                         introRow.ConstantItem(100).Container().Image(profileImage);

                         introRow.RelativeItem().Column(c =>
                         {
                             if (cvDataProvider.SelectedPersonalInfo != null)
                             {
                                 foreach (var p in cvDataProvider.SelectedPersonalInfo)
                                 {
                                     c.Item().Row(r =>
                                     {
                                         //r.Spacing(5);
                                         r.RelativeItem().Text($"{localizationProvider.Get(p.Key)}:").SemiBold();
                                         r.RelativeItem().Text(p.Value);
                                     });
                                 }
                             }
                         });

                         introRow.AutoItem().AlignRight().Column(cr =>
                         {
                             cr.IconLink("web", "WebSite", "https://cv.mariogk.com/");
                             cr.IconLink("github", "Github", "https://github.com/MarioGK");
                             cr.IconLink("linkedin", "Linkedin",
                                         "https://www.linkedin.com/in/m%C3%A1rio-gabriell-karaziaki-belchior-0a271814b/");
                             cr.IconLink("mail", "mariogk01@gmail.com", "mailto:mariogk01@gmail.com");
                             cr.IconLink("phone", "+351 933557967", "tel:+5544999758367");
                             cr.IconLink("whatsapp", "WhatsApp",
                                         "https://api.whatsapp.com/send?phone=5544999758367&text=Oi, Mario");
                         });
                     });

                     //Introduction
                     column.Title("Introduction");
                     column.Item().Text(cv?.Introduction);

                     column.Spacing(5);

                     //Skills
                     column.Title(localizationProvider.Get("Skills"));
                     var skillsText =
                         string.Join(", ", cvDataProvider.SkillsData.OrderByDescending(s => s.Level).Select(s => s.Name));
                     column.Item().Text(skillsText);

                     column.Title(localizationProvider.Get("Experiences"));
                     //Experiences
                     foreach (var exp in cv!.Experiences)
                     {
                         column.Experience(exp, localizationProvider.SelectedLocalization!.DateFormat);
                     }
                 });

            page.Footer()
                .DefaultTextStyle(text => text.FontSize(8.2f).Italic().Light())
                .AlignRight()
                .AlignBottom()
                .Column(c =>
                 {
                     c.Item().LineHorizontal(1);
                     c.Item().Row(row =>
                     {
                         row.AutoItem().Text("*This PDF was automatically generated from ");
                         row.AutoItem().Hyperlink("https://cv.mariogk.com/").Text("cv.mariogk.com")
                            .FontColor(Colors.Blue.Darken1);
                         row.AutoItem()
                            .Text(" for a better experience and the most updated information or other languages please visit the ");
                         row.AutoItem().Hyperlink("https://cv.mariogk.com/").Text("website.")
                            .FontColor(Colors.Blue.Darken1);
                     });
                 });
        });
    });

    var fontFiles = Directory.EnumerateFiles(fontsDir)
                             .ToList();
    fontFiles.ForEach(font => FontManager.RegisterFont(File.OpenRead(font)));

    var metaData = DocumentMetadata.Default;
    //metaData.ImageQuality = 90;
    document.WithMetadata(metaData);

    Directory.CreateDirectory(outputDir);
    var fileName = $"{cv.Language.ToString()}.pdf";
    document.GeneratePdf($"{outputDir}{fileName}");
    Console.WriteLine($"Generated {fileName}!");
}

Console.WriteLine("Finished!");

// Helper method to load CV data for a specific language
async Task LoadCVDataForLanguage(ICurriculumDataProvider provider, Language language)
{
    // If CV data doesn't exist for this language and type, load it
    if (provider.CurriculumData == null || !provider.CurriculumData.Any(x => x.Language == language && x.Type == provider.SelectedType))
    {
        var curriculumData = await provider.GetFromYamlAsync<CurriculumData>($"Data/CVs/{provider.SelectedType}/{language}.yaml");
        curriculumData.Language = language;
        provider.CurriculumData ??= [];
        provider.CurriculumData.Add(curriculumData);
        provider.SelectedCurriculumData = curriculumData;
    }
    else
    {
        provider.SelectedCurriculumData = provider.CurriculumData.FirstOrDefault(x => x.Language == language && x.Type == provider.SelectedType);
    }
}
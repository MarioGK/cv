using cv.Data;
using cv.PdfGenerator.Components;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

var yamlDeserializer =
    new DeserializerBuilder().WithNamingConvention(PascalCaseNamingConvention.Instance).Build();

#if DEBUG
var dataDir = @"F:\Projects\cv\src\cv\wwwroot\data\";
var pdfDir  = "pdfs\\";
#else
var dataDir = $"{Directory.GetCurrentDirectory()}/src/cv/wwwroot/data/";
var pdfDir = $"{Directory.GetCurrentDirectory()}/publish/wwwroot/pdfs/";
#endif

var languageDir = $"{dataDir}languages";

var languages = Directory.EnumerateFiles(languageDir, "*.yaml")
                         .Select(File.ReadAllText)
                         .Select(yaml => yamlDeserializer.Deserialize<LanguageData>(yaml)!)
                         .ToList();

var skills = yamlDeserializer.Deserialize<List<SkillsData>>(File.ReadAllText($"{dataDir}skills.yaml"));

var fontFiles = Directory.EnumerateFiles(Path.Combine(AppContext.BaseDirectory, "Fonts"))
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
                     //Title
                     c.Item().Text(language.Language)
                      .Bold().FontSize(24).FontColor(Colors.Blue.Accent2);

                     //Introduction and contacts
                     c.Item().Row(introRow =>
                     {
                         introRow.Spacing(10);
                         introRow.ConstantItem(100).Image("Images/Profile.jpg");
                         introRow.RelativeItem().Text(language.Introduction);
                         
                         introRow.AutoItem().AlignRight().Column(cr =>
                         {
                             cr.IconLink("web",   "WebSite",              "https://github.com/MarioGK");
                             cr.IconLink("github",   "Github",              "https://github.com/MarioGK");
                             cr.IconLink("linkedin", "Linkedin",            "https://github.com/MarioGK");
                             cr.IconLink("mail",     "mariogk01@gmail.com", "https://github.com/MarioGK");
                             cr.IconLink("phone",    "+55 44 999758367",    "https://github.com/MarioGK");
                             cr.IconLink("whatsapp", "WhatsApp",            "https://github.com/MarioGK");
                         });
                     });
                     
                     c.Item().PaddingBottom(5);
                 });
            
            page.Content()
                .PaddingHorizontal(1, Unit.Centimetre)
                .PaddingBottom(10)
                .Column(column =>
                 {
                     //Experiences
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
                     row.AutoItem().Text("*This PDF was automatically generated from ");
                     row.AutoItem().Hyperlink("https://cv.mariogk.top/").Text("cv.mariogk.top")
                        .FontColor(Colors.Blue.Darken1);
                     row.AutoItem().Text(" for a better experience please visit the website.");
                 });
        });
    });

    var metaData = DocumentMetadata.Default;
    metaData.ImageQuality = 95;
    document.WithMetadata(metaData);
    
    Directory.CreateDirectory(pdfDir);
    var fileName = $"{language.Language.ToLower()}.pdf";
    document.GeneratePdf($"{pdfDir}{fileName}");
    Console.WriteLine($"Generated {fileName}!");
}

Console.WriteLine("Finished!");
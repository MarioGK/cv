
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

var cvDir = $"{dataDir}cv";
var fontsDir   = Path.Combine(AppContext.BaseDirectory, "Fonts");
var imagesDir   = Path.Combine(AppContext.BaseDirectory, "Images");

var cvs = Directory.EnumerateFiles(cvDir, "*.yaml")
                         .Select(File.ReadAllText)
                         .Select(yaml => yamlDeserializer.Deserialize<CVData>(yaml)!)
                         .ToList();

var skills = yamlDeserializer.Deserialize<List<SkillsData>>(File.ReadAllText($"{dataDir}skills.yaml"));

var fontFiles = Directory.EnumerateFiles(fontsDir)
                         .ToList();
fontFiles.ForEach(font => FontManager.RegisterFont(File.OpenRead(font)));


foreach (var cv in cvs)
{
    Console.WriteLine($"Generating PDF for {cv.Language}...");
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

            /*page.Header()
                .PaddingTop(1, Unit.Centimetre)
                .PaddingHorizontal(1, Unit.Centimetre)
                .Column(c =>
                 {
                     //Title
                     //c.Item().Text(cv.Language).Bold().FontSize(24).FontColor(Colors.Blue.Accent2);
                 });*/
            
            page.Content()
                .PaddingHorizontal(1, Unit.Centimetre)
                .PaddingBottom(10)
                .Column(column =>
                 {
                     //Introduction and contacts
                     column.Item().Row(introRow =>
                     {
                         introRow.Spacing(10);
                         introRow.ConstantItem(100).Image(Path.Join(imagesDir, "Profile.jpg"));
                         introRow.RelativeItem().Text(cv.Introduction);
                         
                         introRow.AutoItem().AlignRight().Column(cr =>
                         {
                             cr.IconLink("web",      "WebSite",             "https://cv.mariogk.top/");
                             cr.IconLink("github",   "Github",              "https://github.com/MarioGK");
                             cr.IconLink("linkedin", "Linkedin",            "https://www.linkedin.com/in/m%C3%A1rio-gabriell-karaziaki-belchior-0a271814b/");
                             cr.IconLink("mail",     "mariogk01@gmail.com", "mailto:mariogk01@gmail.com");
                             cr.IconLink("phone",    "+55 44 999758367",    "tel:+5544999758367");
                             cr.IconLink("whatsapp", "WhatsApp",            "https://api.whatsapp.com/send?phone=5544999758367&text=Oi, Mario");
                         });
                     });
                     
                     column.Spacing(5);
                     
                     //Skills
                     column.Item().Text("Skills:")
                           .Bold().FontSize(18).FontColor(Colors.Blue.Accent2);

                     var skillsText = string.Join(", ", skills.OrderByDescending(s => s.Level).Select(s => s.Name));
                     column.Item().Text(skillsText);

                     column.Item().Text("Experiences:")
                           .Bold().FontSize(18).FontColor(Colors.Blue.Accent2);
                     //Experiences
                     foreach (var exp in cv.Experiences)
                     {
                        column.Experience(exp, cv.DateFormat);
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
                     row.AutoItem().Text(" for a better experience and the most updated information please visit the website.");
                 });
        });
    });

    var metaData = DocumentMetadata.Default;
    metaData.ImageQuality = 95;
    document.WithMetadata(metaData);
    
    Directory.CreateDirectory(pdfDir);
    var fileName = $"{cv.Language.ToLower()}.pdf";
    document.GeneratePdf($"{pdfDir}{fileName}");
    Console.WriteLine($"Generated {fileName}!");
}

Console.WriteLine("Finished!");
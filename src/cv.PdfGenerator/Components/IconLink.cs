using QuestPDF.Fluent;

namespace cv.PdfGenerator.Components;

public static class IconLinkComponent
{
    private static readonly Dictionary<string, string> Icons = new()
    {
        ["mail"] = "",
        ["phone"] = "",
        ["github"] = "",
        ["whatsapp"] = "",
        ["linkedin"] = "",
        ["web"] = ""
    };

    public static void IconLink(this ColumnDescriptor container, string icon, string text, string url)
    {
        container.Item().Row(rr =>
        {
            rr.AutoItem().Hyperlink(url).Text(Icons[icon]).FontFamily("icons").FontSize(20);
            rr.AutoItem().PaddingLeft(4).PaddingTop(3).Hyperlink(url).Text(text);
        });
    }
}
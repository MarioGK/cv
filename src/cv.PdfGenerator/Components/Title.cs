using MudBlazor;
using QuestPDF.Fluent;

namespace cv.PdfGenerator.Components;

public static class TitleComponent
{
    public static void Title(this ColumnDescriptor container, string title)
    {
        container.Item().Text($"{title}:")
                 .Bold().FontSize(18).FontColor(Colors.Blue.Accent2);
    }
}
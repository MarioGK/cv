using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace Curriculum.PdfGenerator.Components;

public static class TitleComponent
{
    public static void Title(this ColumnDescriptor container, string title)
    {
        container.Item().Text($"{title}:")
                 .Bold().FontSize(18).FontColor(Colors.Blue.Darken2);
    }
}
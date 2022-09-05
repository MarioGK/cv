using cv.Data;
using MudBlazor;
using QuestPDF.Fluent;

namespace cv.PdfGenerator.Components;

public static class ExperienceComponent
{
    public static void Experience(this ColumnDescriptor container, ExperienceData exp, string dateFormat)
    {
        container.Item().Column(column =>
        {
            column.Item().Row(row =>
            {
                row.RelativeItem().Text(exp.Title)
                .Bold()
                .FontSize(16)
                .FontColor(Colors.Blue.Default);

                row.AutoItem().AlignRight().Text($"{exp.StartDate.ToString(dateFormat)} - {exp.EndDate.ToString(dateFormat)}").SemiBold();
            });
                    
            column.Item().Text(exp.Description);
        });
    }
}
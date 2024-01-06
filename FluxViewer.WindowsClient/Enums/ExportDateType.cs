namespace FluxViewer.WindowsClient.Enums;

public enum ExportDateType
{
    G,
    g,
    d,
    F,
    f,
    D,
    M,
    Y,
    O,
    R,
    s,
    u,
    T,
    t,
}


public static class ExportDateTypeHelper
{
    public static string ExampleByType(ExportDateType exportDateType)
    {
        return exportDateType switch
        {
             ExportDateType.G => "06.01.2022 14:45:20",
             ExportDateType.g => "06.01.2022 14:45",
             ExportDateType.d => "06.01.2022",
             ExportDateType.F => "6 января 2022 г. 14:45:20",
             ExportDateType.f => "6 января 2022 г. 14:45",
             ExportDateType.D => "6 января 2022 г.",
             ExportDateType.M => "6 января",
             ExportDateType.Y => "январь 2022 г.",
             ExportDateType.O => "2022-01-06T14:45:20.3942344+04:00",
             ExportDateType.R => "Thu, 06 Jan 2022 14:45:20 GMT",
             ExportDateType.s => "2022-01-06T14:45:20",
             ExportDateType.u => "2022-01-06 14:45:20Z",
             ExportDateType.T => "14:45:20",
             ExportDateType.t => "14:45",
            _ => throw new ArgumentOutOfRangeException(nameof(exportDateType), exportDateType, null)
        };
    }

    public static ExportDateType FromExample(string example)
    {
        return example switch
        {
            "06.01.2022 14:45:20" => ExportDateType.G,
            "06.01.2022 14:45" => ExportDateType.g,
            "06.01.2022" => ExportDateType.d,
            "6 января 2022 г. 14:45:20" => ExportDateType.F,
            "6 января 2022 г. 14:45" => ExportDateType.f,
            "6 января 2022 г." => ExportDateType.D,
            "6 января" => ExportDateType.M,
            "январь 2022 г." => ExportDateType.Y,
            "2022-01-06T14:45:20.3942344+04:00" => ExportDateType.O,
            "Thu, 06 Jan 2022 14:45:20 GMT" => ExportDateType.R,
            "2022-01-06T14:45:20" => ExportDateType.s,
            "2022-01-06 14:45:20Z" => ExportDateType.u,
            "14:45:20" => ExportDateType.T,
            "14:45" => ExportDateType.t,
            _ => throw new ArgumentException("Неизвестный пример")
        };
    }
}
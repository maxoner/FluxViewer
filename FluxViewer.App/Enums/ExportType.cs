namespace FluxViewer.App.Enums;

public enum ExportType
{
    PlainText,
    Csv,
    Json,
}

public static class ExportTypeHelper
{
    public static string ToString(ExportType exportType)
    {
        return exportType switch
        {
            ExportType.PlainText => "Plain Text",
            ExportType.Csv => "CSV",
            ExportType.Json => "JSON",
            _ => throw new ArgumentOutOfRangeException(nameof(exportType), exportType, null)
        };
    }

    public static ExportType FromString(string value)
    {
        return value switch
        {
            "Plain Text" => ExportType.PlainText,
            "CSV" => ExportType.Csv,
            "JSON" => ExportType.Json,
            _ => throw new ArgumentException("Неизвестный тип экпорта")
        };
    }
}
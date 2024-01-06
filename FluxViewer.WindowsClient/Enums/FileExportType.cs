using FluxViewer.Core.Export;

namespace FluxViewer.WindowsClient.Enums;

public static class FileExporterTypeHelper
{
    public static string ToString(FileExporterType fileExporterType)
    {
        return fileExporterType switch
        {
            FileExporterType.PlainTextExporter => "Plain Text",
            FileExporterType.CsvExporter => "CSV",
            FileExporterType.JsonExporter => "JSON",
            _ => throw new ArgumentOutOfRangeException(nameof(fileExporterType), fileExporterType, null)
        };
    }

    public static FileExporterType FromString(string value)
    {
        return value switch
        {
            "Plain Text" => FileExporterType.PlainTextExporter,
            "CSV" => FileExporterType.CsvExporter,
            "JSON" => FileExporterType.JsonExporter,
            _ => throw new ArgumentException("Неизвестный тип экпорта")
        };
    }
}
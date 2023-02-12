using System;
using FluxViewer.DataAccess.Export.Exporters;

namespace FluxViewer.DataAccess.Export;

public enum FileExporterType
{
    PlainTextExporter,
    CsvExporter,
    JsonExporter,
}

public static class FileExporterFabric
{
    public static FileExporter GetFileExporterByType(
        FileExporterType fileExporterType,
        string dateTimeFormat,
        bool dateTimeConvert,
        bool fluxConvert,
        bool tempConvert,
        bool presConvert,
        bool hummConvert)
    {
        return fileExporterType switch
        {
            FileExporterType.PlainTextExporter => new PlainTextFileExporter(dateTimeFormat, dateTimeConvert,
                fluxConvert, tempConvert, presConvert, hummConvert),
            FileExporterType.CsvExporter => new CsvFileExporter(dateTimeFormat, dateTimeConvert, fluxConvert,
                tempConvert, presConvert, hummConvert),
            FileExporterType.JsonExporter => new JsonFileExporter(dateTimeFormat, dateTimeConvert, fluxConvert,
                tempConvert, presConvert, hummConvert),
            _ => throw new ArgumentOutOfRangeException(nameof(fileExporterType), fileExporterType, null)
        };
    }
}
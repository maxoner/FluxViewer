using System;
using System.Collections.Generic;
using System.IO;
using FluxViewer.DataAccess.Models;

namespace FluxViewer.DataAccess.Exporters;

public class CsvFileExporter : FileExporter
{
    private bool _isFirstLine = true;

    public CsvFileExporter(
        string pathToFile,
        string dateTimeFormat,
        bool dateTimeConvert,
        bool fluxConvert,
        bool tempConvert,
        bool presConvert,
        bool hummConvert) :
        base(pathToFile, dateTimeFormat, dateTimeConvert, fluxConvert, tempConvert, presConvert, hummConvert)
    {
    }


    public override void Export(NewData data)
    {
        using var stream = new StreamWriter(PathToFile, true);
        if (_isFirstLine)
        {
            stream.WriteLine(GetTitleLine());
            _isFirstLine = false;
        }

        var exportLine = GetExportLine(data);
        stream.WriteLine(exportLine);
    }

    public override void Export(IEnumerable<NewData> data)
    {
        using var stream = new StreamWriter(PathToFile, true);
        if (_isFirstLine)
        {
            stream.WriteLine(GetTitleLine());
            _isFirstLine = false;
        }

        foreach (var record in data)
        {
            var exportLine = GetExportLine(record);
            stream.WriteLine(exportLine);
        }
    }

    private string GetTitleLine()
    {
        var csvTitle = "";
        if (DateTimeConvert) csvTitle += $"Дата и время;";
        if (FluxConvert) csvTitle += $"Электростатическое поле;";
        if (TempConvert) csvTitle += $"Температура;";
        if (PresConvert) csvTitle += $"Давление;";
        if (HummConvert) csvTitle += $"Влажность;";
        return csvTitle[..^1]; // Удаляем последнюю ';'
    }

    private string GetExportLine(NewData data)
    {
        var csvLine = "";
        if (DateTimeConvert) csvLine += $"{data.DateTime.ToString(DateTimeFormat)};";
        if (FluxConvert) csvLine += $"{data.FluxSensorData};";
        if (TempConvert) csvLine += $"{data.TempSensorData};";
        if (PresConvert) csvLine += $"{data.PressureSensorData};";
        if (HummConvert) csvLine += $"{data.HumiditySensorData};";
        return csvLine[..^1]; // Удаляем последнюю ';'
    }
}
using System.Collections.Generic;
using System.IO;
using FluxViewer.DataAccess.Models;

namespace FluxViewer.DataAccess.Export.Exporters;

public class PlainTextFileExporter: FileExporter
{
    public PlainTextFileExporter(
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

    public override void Export(IEnumerable<NewData> dataBatch)
    {
        using var stream = new StreamWriter(PathToFile, true);
        foreach (var record in dataBatch)
        {
            var exportLine = GetExportLine(record);
            stream.WriteLine(exportLine);
        }
    }
    
    private string GetExportLine(NewData data)
    {
        var plainTextLine = "";
        if (DateTimeConvert) plainTextLine += $"{data.DateTime.ToString(DateTimeFormat)}\t";
        if (FluxConvert) plainTextLine += $"{data.FluxSensorData}\t";
        if (TempConvert) plainTextLine += $"{data.TempSensorData}\t";
        if (PresConvert) plainTextLine += $"{data.PressureSensorData}\t";
        if (HummConvert) plainTextLine += $"{data.HumiditySensorData}\t";
        return plainTextLine[..^1]; // Удаляем последний '\t'
    }
}
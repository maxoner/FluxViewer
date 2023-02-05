using System.Collections.Generic;
using System.IO;
using FluxViewer.DataAccess.Models;

namespace FluxViewer.DataAccess.Exporters;

public class JsonFileExporter : FileExporter
{
    private bool _isFirstLine = true;

    public JsonFileExporter(
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
        stream.WriteLine("{\n");
        foreach (var record in dataBatch)
        {
            var exportLine = GetExportLine(record);
            stream.WriteLine(exportLine);
        }

        stream.WriteLine("\n}\n");
    }

    private string GetExportLine(NewData data)
    {
        var jsonLine = "";
        if (_isFirstLine)
        {
            jsonLine = "\t{\n";
            _isFirstLine = false;
        }
        else
        {
            jsonLine = ",\n\t{\n";
        }
        if (DateTimeConvert) jsonLine += $"\t\t\"DateTime\":\"{data.DateTime.ToString(DateTimeFormat)}\",\n";
        if (FluxConvert) jsonLine += $"\t\t\"Flux\":\"{data.FluxSensorData}\",\n";
        if (TempConvert) jsonLine += $"\t\t\"Temp\":\"{data.TempSensorData}\",\n";
        if (PresConvert) jsonLine += $"\t\t\"Pres\":\"{data.PressureSensorData}\",\n";
        if (HummConvert) jsonLine += $"\t\t\"Humm\":\"{data.HumiditySensorData}\",\n";
        jsonLine = jsonLine[..^2]; // Удаляем последние ',\n'
        jsonLine += "\n\t}";
        return jsonLine;
    }
}
using System.Collections.Generic;
using System.Text;
using FluxViewer.DataAccess.Models;

namespace FluxViewer.DataAccess.Export.Exporters;

public class JsonFileExporter : FileExporter
{
    private bool _isFirstLine = true;

    public JsonFileExporter(
        string dateTimeFormat,
        bool dateTimeConvert,
        bool fluxConvert,
        bool tempConvert,
        bool presConvert,
        bool hummConvert) :
        base(dateTimeFormat, dateTimeConvert, fluxConvert, tempConvert, presConvert, hummConvert)
    {
    }

    protected override void WriteDataBatch(IEnumerable<Data> dataBatch)
    {
        FileExporterStream.Write(Encoding.ASCII.GetBytes("{\n"));
        foreach (var record in dataBatch)
        {
            var exportLine = GetExportLine(record);
            FileExporterStream.Write(Encoding.ASCII.GetBytes(exportLine));
        }

        FileExporterStream.Write(Encoding.ASCII.GetBytes("\n}\n"));
    }

    public override long CalculateApproximateExportSizeInBytes(long numOfPoint)
    {
        long numOfBytesInOneElement = 0; // Главные скобочки и переносы кареток
        // Всё берём по максимуму
        if (DateTimeConvert) numOfBytesInOneElement += 45;
        if (FluxConvert) numOfBytesInOneElement += 23;
        if (TempConvert) numOfBytesInOneElement += 23;
        if (PresConvert) numOfBytesInOneElement += 23;
        if (HummConvert) numOfBytesInOneElement += 23;
        return 4 + numOfBytesInOneElement * numOfPoint; // Где '4' - главные скобки + переносы кареток
    }

    private string GetExportLine(Data data)
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
        return jsonLine + '\n'; // Добавляем '\n'
    }
}
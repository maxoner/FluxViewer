using System.Collections.Generic;
using System.Text;
using FluxViewer.DataAccess.Storage;

namespace FluxViewer.DataAccess.Export.Exporters;

public class PlainTextFileExporter : FileExporter
{
    public PlainTextFileExporter(
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
        foreach (var record in dataBatch)
        {
            var exportLine = GetExportLine(record);
            FileExporterStream.Write(Encoding.ASCII.GetBytes(exportLine));
        }
    }

    public override long CalculateApproximateExportSizeInBytes(long numOfPoint)
    {
        long numOfBytesInOneElement = 0;
        // Всё берём по максимуму
        if (DateTimeConvert) numOfBytesInOneElement += 28;
        if (FluxConvert) numOfBytesInOneElement += 10;  
        if (TempConvert) numOfBytesInOneElement += 10;
        if (PresConvert) numOfBytesInOneElement += 10;
        if (HummConvert) numOfBytesInOneElement += 10;
        return numOfBytesInOneElement * numOfPoint;
    }

    private string GetExportLine(Data data)
    {
        var plainTextLine = "";
        if (DateTimeConvert) plainTextLine += $"{data.DateTime.ToString(DateTimeFormat)}\t";
        if (FluxConvert) plainTextLine += $"{data.FluxSensorData}\t";
        if (TempConvert) plainTextLine += $"{data.TempSensorData}\t";
        if (PresConvert) plainTextLine += $"{data.PressureSensorData}\t";
        if (HummConvert) plainTextLine += $"{data.HumiditySensorData}\t";
        return plainTextLine[..^1] + '\n'; // Удаляем последнюю ';' и добавляем '\n'
    }
}
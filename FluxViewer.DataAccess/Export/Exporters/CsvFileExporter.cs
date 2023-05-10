using System.Collections.Generic;
using System.Text;
using FluxViewer.DataAccess.Storage;


namespace FluxViewer.DataAccess.Export.Exporters;

public class CsvFileExporter : FileExporter
{
    private bool _isFirstLine = true;

    public CsvFileExporter(
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
        if (_isFirstLine)
        {
            var titleLine = GetTitleLine();
            FileExporterStream.Write(Encoding.UTF8.GetBytes(titleLine)); // UTF-8 для заголовка на русском
            _isFirstLine = false;
        }

        foreach (var data in dataBatch)
        {
            var exportLine = GetExportLine(data);
            FileExporterStream.Write(Encoding.UTF8.GetBytes(exportLine));
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

    private string GetTitleLine()
    {
        var csvTitle = "";
        if (DateTimeConvert) csvTitle += $"Дата и время;";
        if (FluxConvert) csvTitle += $"Электростатическое поле;";
        if (TempConvert) csvTitle += $"Температура;";
        if (PresConvert) csvTitle += $"Давление;";
        if (HummConvert) csvTitle += $"Влажность;";
        return csvTitle[..^1] + '\n'; // Удаляем последнюю ';' и добавляем '\n'
    }

    private string GetExportLine(Data data)
    {
        var csvLine = "";
        if (DateTimeConvert) csvLine += $"{data.DateTime.ToString(DateTimeFormat)};";
        if (FluxConvert) csvLine += $"{data.FluxSensorData};";
        if (TempConvert) csvLine += $"{data.TempSensorData};";
        if (PresConvert) csvLine += $"{data.PressureSensorData};";
        if (HummConvert) csvLine += $"{data.HumiditySensorData};";
        return csvLine[..^1] + '\n'; // Удаляем последнюю ';' и добавляем '\n'
    }
}
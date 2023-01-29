using System;
using System.Collections.Generic;
using System.IO;
using FluxViewer.DataAccess.Models;

namespace FluxViewer.DataAccess.Exporters;

public class CsvExporter : Exporter
{
    private StreamWriter _file;
    private bool _isOpen;

    public CsvExporter(
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

    public override void Open()
    {
        try
        {
            _file = new StreamWriter(PathToFile); // В случае повторного открытия пересоздаём файл!
            _isOpen = true;

            var csvTitle = "";
            if (DateTimeConvert) csvTitle += $"Дата и время;";
            if (FluxConvert) csvTitle += $"Электростатическое поле;";
            if (TempConvert) csvTitle += $"Температура;";
            if (PresConvert) csvTitle += $"Давление;";
            if (HummConvert) csvTitle += $"Влажность;";
            var csvTitleWithoutLastComma = csvTitle[..^1]; // Удаляем последнюю ';'
            _file.WriteLine(csvTitleWithoutLastComma);
        }
        catch (Exception e)
        {
            throw new Exception("Файл не открыт"); // TODO: создай класс-исключение
        }
    }

    public override void Close()
    {
        if (_isOpen && _file is not null)
        {
            _file.Close();
            _isOpen = false;
        }
    }

    public override void Write(NewData data)
    {
        if (!_isOpen || _file is null)
            throw new Exception("Файл не открыт"); // TODO: тот же класс-исключение

        var csvLine = "";
        if (DateTimeConvert) csvLine += $"{data.DateTime.ToString(DateTimeFormat)};";
        if (FluxConvert) csvLine += $"{data.FluxSensorData};";
        if (TempConvert) csvLine += $"{data.TempSensorData};";
        if (PresConvert) csvLine += $"{data.PressureSensorData};";
        if (HummConvert) csvLine += $"{data.HumiditySensorData};";
        var csvLineWithoutLastComma = csvLine[..^1]; // Удаляем последнюю ';'

        _file.WriteLine(csvLineWithoutLastComma);
    }

    public override void Write(IEnumerable<NewData> data)
    {
        foreach (var record in data)
        {
            Write(record);
        }
    }
}
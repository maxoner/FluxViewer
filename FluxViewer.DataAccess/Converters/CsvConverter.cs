using System;
using System.Collections.Generic;
using System.IO;
using FluxViewer.DataAccess.Models;

namespace FluxViewer.DataAccess.Converters;

public class CsvConverter : Converter
{
    private StreamWriter _file;
    private bool _isOpen;

    public CsvConverter(string pathToFile, bool dateTimeConvert, bool fluxConvert, bool tempConvert, bool presConvert,
        bool hummConvert) : base(pathToFile, dateTimeConvert, fluxConvert, tempConvert, presConvert, hummConvert)
    {
    }

    public override void Open()
    {
        try
        {
            _file = new StreamWriter(PathToFile); // В случае повторного открытия пересоздаём файл!
            _isOpen = true;
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
        if (DateTimeConvert) csvLine += $"{data.DateTime};";
        if (FluxConvert) csvLine += $"{data.FluxSensorData};";
        if (TempConvert) csvLine += $"{data.TempSensorData};";
        if (PresConvert) csvLine += $"{data.PressureSensorData};";
        if (HummConvert) csvLine += $"{data.HumiditySensorData};";
        var csvLineWithoutLastComma = csvLine.Remove(0, csvLine.Length - 1); // Удаляем последнюю ';'

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
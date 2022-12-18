using System;
using System.Collections.Generic;
using System.IO;
using FluxViewer.DataAccess.Models;

namespace FluxViewer.DataAccess.Converters;

public class CsvConverter : Converter
{
    private StreamWriter _file;
    private bool _isOpen;

    public CsvConverter(string pathToFile) : base(pathToFile)
    {
    }

    public override void Open()
    {
        try
        {
            _file = new StreamWriter(_pathToFile); // В случае повторного открытия пересоздаём файл!
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

    public override void Write(Data data)
    {
        if (!_isOpen || _file is null)
            throw new Exception("Файл не открыт"); // TODO: тот же класс-исключение

        _file.WriteLine($"{data.Id};{data.DateTime};{data.FluxSensorData};{data.TempSensorData};" +
                        $"{data.PressureSensorData};{data.HumiditySensorData}");
    }

    public override void Write(IEnumerable<Data> data)
    {
        foreach (var record in data)
        {
            Write(record);
        }
    }
}
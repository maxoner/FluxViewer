using System;
using System.Collections.Generic;
using System.IO;
using FluxViewer.DataAccess.Models;

namespace FluxViewer.DataAccess.Converters;

public class CsvConverter : IConverter
{
    private readonly string _pathToFile;
    private StreamWriter _file;
    private bool _isOpen;

    /// <summary>
    /// Конвертер в CSV формат
    /// </summary>
    /// <param name="pathToFile">Путь до файла CSV файла, куда будет записан результат конвертации</param>
    public CsvConverter(string pathToFile)
    {
        _pathToFile = pathToFile;
    }

    public void Open()
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

    public void Close()
    {
        if (_isOpen && _file is not null)
        {
            _file.Close();
            _isOpen = false;
        }
    }

    public void Write(NewData data)
    {
        if (!_isOpen || _file is null)
            throw new Exception("Файл не открыт"); // TODO: тот же класс-исключение

        _file.WriteLine($"{data.Id};{data.DateTime};{data.FluxSensorData};{data.TempSensorData};" +
                        $"{data.PressureSensorData};{data.HumiditySensorData}");
    }

    public void Write(IEnumerable<NewData> data)
    {
        foreach (var record in data)
        {
            Write(record);
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using FluxViewer.DataAccess.Models;

namespace FluxViewer.DataAccess.Converters;

public abstract class Converter
{
    protected string _pathToFile;

    /// <summary>
    /// Создать конвертер, который будет записывать результат в переданный файл.
    /// </summary>
    /// <param name="pathToFile">Путь до файла, куда будет записан результат конвертации</param>
    protected Converter(string pathToFile)
    {
        _pathToFile = pathToFile;
    }

    /// <summary>
    /// Открыть конвертер и подготовить к записи.
    /// </summary>
    public abstract void Open();


    /// <summary>
    /// Закрыть конвертер и сохранить данные.
    /// </summary>
    public abstract void Close();

    /// <summary>
    /// Конвертировать и записать показание прибора.
    /// </summary>
    /// <param name="data">Показание прибора, которые будут конвертированы и записаны</param>
    public abstract void Write(Data data);


    /// <summary>
    /// Конвертировать и записать коллекцию с показаниями прибора.
    /// </summary>
    /// <param name="data">Коллекция с показаниями прибора, которые будут конвертированы и записаны</param>
    public abstract void Write(IEnumerable<Data> data);
}
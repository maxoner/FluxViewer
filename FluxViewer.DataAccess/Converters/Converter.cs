using System.Collections.Generic;
using FluxViewer.DataAccess.Models;

namespace FluxViewer.DataAccess.Converters;

public abstract class Converter
{
    protected readonly string PathToFile;
    protected readonly bool DateTimeConvert;
    protected readonly bool FluxConvert;
    protected readonly bool TempConvert;
    protected readonly bool PresConvert;
    protected readonly bool HummConvert;
    
    
    /// <summary>
    /// Абстрактный конвертер.
    /// </summary>
    /// <param name="pathToFile">Путь до файла файла, куда будет записан результат конвертации</param>
    /// <param name="dateTimeConvert">Нужно ли экспортировать дату и время?</param>
    /// <param name="fluxConvert">Нужно ли конвертировать электростатическое поле?</param>
    /// <param name="tempConvert">Нужно ли конвертировать температуру?</param>
    /// <param name="presConvert">Нужно ли конвертировать давление?</param>
    /// <param name="hummConvert">Нужно ли конвертировать влажность?</param>
    protected Converter(string pathToFile, bool dateTimeConvert, bool fluxConvert, bool tempConvert,
        bool presConvert, bool hummConvert)
    {
        PathToFile = pathToFile;
        DateTimeConvert = dateTimeConvert; 
        FluxConvert = fluxConvert;
        TempConvert = tempConvert;
        PresConvert = presConvert;
        HummConvert = hummConvert;
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
    public abstract void Write(NewData data);


    /// <summary>
    /// Конвертировать и записать коллекцию с показаниями прибора.
    /// </summary>
    /// <param name="data">Коллекция с показаниями прибора, которые будут конвертированы и записаны</param>
    public abstract void Write(IEnumerable<NewData> data);
}
using System.Collections.Generic;
using FluxViewer.DataAccess.Models;

namespace FluxViewer.DataAccess.Exporters;

public abstract class FileExporter
{
    protected readonly string PathToFile;
    protected readonly string DateTimeFormat;
    protected readonly bool DateTimeConvert;
    protected readonly bool FluxConvert;
    protected readonly bool TempConvert;
    protected readonly bool PresConvert;
    protected readonly bool HummConvert;


    /// <summary>
    /// Абстрактный экспортёр.
    /// </summary>
    /// <param name="pathToFile">Путь до файла файла, куда будет записан результат экспорта</param>
    /// <param name="dateTimeFormat">Формат даты и времени</param>
    /// <param name="dateTimeConvert">Нужно ли экспортировать дату и время?</param>
    /// <param name="fluxConvert">Нужно ли экспортировать электростатическое поле?</param>
    /// <param name="tempConvert">Нужно ли экспортировать температуру?</param>
    /// <param name="presConvert">Нужно ли экспортировать давление?</param>
    /// <param name="hummConvert">Нужно ли экспортировать влажность?</param>
    protected FileExporter(
        string pathToFile,
        string dateTimeFormat,
        bool dateTimeConvert,
        bool fluxConvert,
        bool tempConvert,
        bool presConvert,
        bool hummConvert)
    {
        PathToFile = pathToFile;
        DateTimeFormat = dateTimeFormat;
        DateTimeConvert = dateTimeConvert;
        FluxConvert = fluxConvert;
        TempConvert = tempConvert;
        PresConvert = presConvert;
        HummConvert = hummConvert;
    }
    
    /// <summary>
    /// Экспортировать коллекцию с показаниями прибора.
    /// </summary>
    /// <param name="dataBatch">Коллекция с показаниями прибора, которые будут экспортированы</param>
    public abstract void Export(IEnumerable<NewData> dataBatch);
}
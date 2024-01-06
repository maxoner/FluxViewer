using System.Collections.Generic;
using System.IO;
using FluxViewer.Core.Storage;

namespace FluxViewer.Core.Export.Exporters;

public abstract class FileExporter
{
    protected readonly string DateTimeFormat;
    protected readonly bool DateTimeConvert;
    protected readonly bool FluxConvert;
    protected readonly bool TempConvert;
    protected readonly bool PresConvert;
    protected readonly bool HummConvert;

    protected string PathToFile;
    protected FileStream FileExporterStream;

    // protected 

    /// <summary>
    /// Абстрактный экспортёр.
    /// </summary>
    /// <param name="dateTimeFormat">Формат даты и времени</param>
    /// <param name="dateTimeConvert">Нужно ли экспортировать дату и время?</param>
    /// <param name="fluxConvert">Нужно ли экспортировать электростатическое поле?</param>
    /// <param name="tempConvert">Нужно ли экспортировать температуру?</param>
    /// <param name="presConvert">Нужно ли экспортировать давление?</param>
    /// <param name="hummConvert">Нужно ли экспортировать влажность?</param>
    protected FileExporter(
        string dateTimeFormat,
        bool dateTimeConvert,
        bool fluxConvert,
        bool tempConvert,
        bool presConvert,
        bool hummConvert)
    {
        DateTimeFormat = dateTimeFormat;
        DateTimeConvert = dateTimeConvert;
        FluxConvert = fluxConvert;
        TempConvert = tempConvert;
        PresConvert = presConvert;
        HummConvert = hummConvert;
    }

    /// <summary>
    /// Открыть экспортёр для экспорта
    /// </summary>
    /// <param name="pathToFile">Путь до файла, куда будет сохранён результат экспорта.
    /// Если файла не существует - он будет создан. Если файл существует - он будет перезаписан!</param>
    public void Open(string pathToFile)
    {
        PathToFile = pathToFile;
        if (File.Exists(pathToFile))
            File.Delete(pathToFile);
    }

    /// <summary>
    /// Экспортировать коллекцию с показаниями прибора в файл.
    /// </summary>
    /// <param name="dataBatch">Коллекция с показаниями прибора, которые будут экспортированы</param>
    public void Export(IEnumerable<Data> dataBatch)
    {
        // Каждый раз переоткрываем файл, потому что только так буфер сбрасывается на диск:( 
        // .Flush() эффекта почему то не даёт
        FileExporterStream = new FileStream(PathToFile, FileMode.Append, FileAccess.Write);
        WriteDataBatch(dataBatch);
        FileExporterStream.Close();
    }

    protected abstract void WriteDataBatch(IEnumerable<Data> dataBatch);

    /// <summary>
    /// Рассчитать ориентировочный размер файла (в байтах) после экспорта по кол-ву показаний 
    /// </summary>
    /// <param name="numOfPoint">Кол-во показаний, которое будет учавствовать в экспорте</param>
    /// <returns>Ориентировочное кол-во байт, которое займёт файл после экспорта</returns>
    public abstract long CalculateApproximateExportSizeInBytes(long numOfPoint);

    /// <summary>
    /// Закрыть экспортёр после успешного экспорта
    /// </summary>
    public void Close()
    {
    }
}
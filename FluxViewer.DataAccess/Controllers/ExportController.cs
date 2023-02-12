using System;
using System.Collections.Generic;
using System.Linq;
using FluxViewer.DataAccess.Export;
using FluxViewer.DataAccess.Models;
using FluxViewer.DataAccess.Storage;

namespace FluxViewer.DataAccess.Controllers;

/// <summary>
/// Класс с бизнес-логикой, отвечающей за экспорт показаний прибора в файл,
/// обладающий дополнительными полезными методами.
/// </summary>
public class ExportController
{
    private readonly DateTime _beginDate;
    private readonly DateTime _endDate;
    private readonly IStorage _storage;
    private readonly bool _fillHoles;
    private readonly string _dateTimeFormat;
    private readonly bool _dateTimeConvert;
    private readonly bool _fluxConvert;
    private readonly bool _tempConvert;
    private readonly bool _presConvert;
    private readonly bool _hummConvert;

    /// <summary>
    /// Конструктор класса 
    /// </summary>
    /// <param name="beginDate">Дата начала, с которой начинается экспорт показаний прибора</param>
    /// <param name="endDate">Дата конца, по которую происходит экспорт показаний прибора</param>
    /// <param name="storage">Хранилище, из которого происходит экспорт</param>
    /// <param name="fillHoles">Заполнять ли пробелы в процессе экспорта?</param>
    /// <param name="dateTimeFormat">Формат даты и времени</param>
    /// <param name="dateTimeConvert">Нужно ли экспортировать дату и время?</param>
    /// <param name="fluxConvert">Нужно ли экспортировать электростатическое поле?</param>
    /// <param name="tempConvert">Нужно ли экспортировать температуру?</param>
    /// <param name="presConvert">Нужно ли экспортировать давление?</param>
    /// <param name="hummConvert">Нужно ли экспортировать влажность?</param>
    public ExportController(DateTime beginDate, DateTime endDate, IStorage storage, bool fillHoles,
        string dateTimeFormat, bool dateTimeConvert, bool fluxConvert, bool tempConvert,
        bool presConvert, bool hummConvert)
    {
        _beginDate = beginDate;
        _endDate = endDate;
        _storage = storage;
        _fillHoles = fillHoles;
        _dateTimeFormat = dateTimeFormat;
        _dateTimeConvert = dateTimeConvert;
        _fluxConvert = fluxConvert;
        _tempConvert = tempConvert;
        _presConvert = presConvert;
        _hummConvert = hummConvert;
    }

    /// <summary>
    /// Получить кол-во итераций экспорта (за сколько шагов он выполнится)
    /// </summary>
    /// <returns>Кол-во итераций</returns>
    public int NumberOfExportIterations()
    {
        var allDatesWithData = _storage.GetAllDatesWithDataBetweenTwoDates(_beginDate, _endDate);
        return _fillHoles ? (allDatesWithData.Last() - allDatesWithData.First()).Days : allDatesWithData.Count;
    }

    /// <summary>
    /// Получить кол-во точек, доступное для экспорта между заданным диапазоном дат.
    /// </summary>
    /// <returns>Количество точек</returns>
    public int GetDataCount()
    {
        return _storage.GetDataCountBetweenTwoDates(_beginDate, _endDate);
    }

    /// <summary>
    /// Получить все даты, в которые прибор записывал показания
    /// </summary>
    /// <returns>Количество точек</returns>
    public List<DateTime> GetAllDatesWithData()
    {
        return _storage.GetAllDatesWithDataBetweenTwoDates(_beginDate, _endDate);
    }

    /// <summary>
    /// Экспорт показаний прибора в файл.
    /// </summary>
    /// <param name="pathToFile">Путь до файла, в который будет произвёден экспорт</param>
    /// <param name="fileExporterType">Тип экспортёра, при помощи которого будет производиться экспорт</param>
    /// <returns>В процессе (пока длится экспорт) будут возвращаться итерации экспорта.</returns>
    public IEnumerable<int> Export(string pathToFile, FileExporterType fileExporterType)
    {
        var allDatesWithData = _storage.GetAllDatesWithDataBetweenTwoDates(_beginDate, _endDate);
        var currentDate = new DateTime(_beginDate.Year, _beginDate.Month, _beginDate.Day, 00, 00, 00);
        var iteration = 0;
        List<NewData> prevDataBatch = null;

        var fileExporter = FileExporterFabric.GetFileExporterByType(fileExporterType, _dateTimeFormat,
            _dateTimeConvert, _fluxConvert, _tempConvert, _presConvert, _hummConvert);
        fileExporter.Open(pathToFile);

        while (currentDate <= _endDate)
        {
            if (_storage.HasDataForThisDate(currentDate))
            {
                var dataBatch = _storage.GetDataBatchByDate(currentDate);
                fileExporter.Export(_fillHoles ? PlaceHolder.FillHoles(dataBatch) : dataBatch);

                if (_fillHoles) // Сохраняем батч, дабы опять его не запрашивать!
                    prevDataBatch = dataBatch;
            }
            else
            {
                // Если надо заполнять пробелы и мы находимся в диапазоне дат, в которые прибор записывал
                // (чтобы иметь гарантии всегда получить предыдущий и следующий батч данных), то можем генерировать батч
                if (_fillHoles && currentDate >= allDatesWithData.First() && currentDate <= allDatesWithData.Last())
                {
                    try
                    {
                        var nextDataBatch = _storage.GetNextDataBatchAfterThisDate(currentDate);

                        var firstTimeShift = PlaceHolder.GetMeanTimeShift(prevDataBatch);
                        var secondTimeShift = PlaceHolder.GetMeanTimeShift(nextDataBatch);

                        var dataBatch = DataBatchGenerator.GenerateDataBatch(
                            currentDate,
                            (float)(firstTimeShift + secondTimeShift) / 2,
                            (prevDataBatch.Last().FluxSensorData + nextDataBatch.First().FluxSensorData) / 2,
                            (prevDataBatch.Last().TempSensorData + nextDataBatch.First().TempSensorData) / 2,
                            (prevDataBatch.Last().PressureSensorData + nextDataBatch.First().PressureSensorData) / 2,
                            (prevDataBatch.Last().HumiditySensorData + nextDataBatch.First().HumiditySensorData) / 2
                        );
                        fileExporter.Export(_fillHoles ? PlaceHolder.FillHoles(dataBatch) : dataBatch);
                    }
                    catch (PrevDataBatchNotFoundException)
                    {
                        // TODO: в логи, т.к. странная ситуация    
                    }
                    catch (NextDataBatchNotFoundException)
                    {
                        // TODO: в логи, т.к. странная ситуация    
                    }
                }
            }

            currentDate = currentDate.AddDays(1);
            yield return iteration++;
        }

        fileExporter.Close();
    }
}
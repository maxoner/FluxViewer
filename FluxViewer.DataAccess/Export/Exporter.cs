using System;
using System.Collections.Generic;
using System.Linq;
using FluxViewer.DataAccess.Export.Exporters;
using FluxViewer.DataAccess.Storage;

namespace FluxViewer.DataAccess.Export;

public class Exporter
{
    private readonly DateTime _beginDate;
    private readonly DateTime _endDate;
    private readonly IStorage _storage;
    private readonly FileExporter _fileExporter;
    private readonly bool _fillHoles;
    
    /// <summary>
    /// Конструктор класса 
    /// </summary>
    /// <param name="beginDate">Дата начала, с которой начинается экспорт показаний прибора</param>
    /// <param name="endDate">Дата конца, по которую происходит экспорт показаний прибора</param>
    /// <param name="storage">Хранилище, из которого происходит экспорт</param>
    /// <param name="fileExporter">Экспортёр, при помощи которого показания будут записываться в файл</param>
    /// <param name="fillHoles">Заполнять ли пробелы в процессе экспорта?</param>
    public Exporter(DateTime beginDate, DateTime endDate, IStorage storage, FileExporter fileExporter, bool fillHoles)
    {
        _beginDate = beginDate;
        _endDate = endDate;
        _storage = storage;
        _fileExporter = fileExporter;
        _fillHoles = fillHoles;
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
    /// Экспорт показаний прибора в файл.
    /// </summary>
    /// <returns>В процессе (пока длится экспорт) будут возвращаться итерации экспорта.</returns>
    public IEnumerable<int> Export()
    {
        var allDatesWithData = _storage.GetAllDatesWithDataBetweenTwoDates(_beginDate, _endDate);
        var currentDate = new DateTime(_beginDate.Year, _beginDate.Month, _beginDate.Day, 00, 00, 00);
        var iteration = 0;
        while (currentDate <= _endDate)
        {
            if (_storage.HasDataForThisDate(currentDate))
            {
                var dataBatch = _storage.GetDataBatchByDate(currentDate);
                _fileExporter.Export(_fillHoles ? PlaceHolder.FillHoles(dataBatch) : dataBatch);
            }
            else
            {
                // Если надо заполнять пробелы и мы находимся в диапазоне дат, в которые прибор записывал
                // (чтобы иметь гарантии всегда получить предыдущий и следующий батч данных), то можем генерировать батч
                if (_fillHoles && currentDate >= allDatesWithData.First() && currentDate <= allDatesWithData.Last())
                {
                    try
                    {
                        var prevDataBatch = _storage.GetPrevDataBatchAfterThisDate(currentDate);
                        var nextDataBatch = _storage.GetNextDataBatchAfterThisDate(currentDate);
                        var dataBatch = DataBatchGenerator.GenerateDataBatch(
                            currentDate,
                            250, // TODO: высчитывать на основе двух батчей
                            (prevDataBatch.Last().FluxSensorData + nextDataBatch.First().FluxSensorData) / 2,
                            (prevDataBatch.Last().TempSensorData + nextDataBatch.First().TempSensorData) / 2,
                            (prevDataBatch.Last().PressureSensorData + nextDataBatch.First().PressureSensorData) / 2,
                            (prevDataBatch.Last().HumiditySensorData + nextDataBatch.First().HumiditySensorData) / 2
                        );
                        _fileExporter.Export(_fillHoles ? PlaceHolder.FillHoles(dataBatch) : dataBatch);
                    }
                    catch (PrevDataBatchNotFoundException exception)
                    {
                        // TODO: в логи, т.к. странная ситуация    
                    }
                    catch (NextDataBatchNotFoundException exception)
                    {
                        // TODO: в логи, т.к. странная ситуация    
                    }
                }
            }
            
            currentDate = currentDate.AddDays(1);
            yield return iteration++;
        }
    }
}
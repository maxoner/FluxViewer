using System;
using System.Collections.Generic;
using System.Linq;
using FluxViewer.DataAccess.Export;
using FluxViewer.DataAccess.Export.Exporters;
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
    
    /// <summary>
    /// Конструктор класса 
    /// </summary>
    /// <param name="beginDate">Дата начала, с которой начинается экспорт показаний прибора</param>
    /// <param name="endDate">Дата конца, по которую происходит экспорт показаний прибора</param>
    /// <param name="storage">Хранилище, из которого происходит экспорт</param>
    /// <param name="fillHoles">Заполнять ли пробелы в процессе экспорта?</param>
    public ExportController(DateTime beginDate, DateTime endDate, IStorage storage, bool fillHoles)
    {
        _beginDate = beginDate;
        _endDate = endDate;
        _storage = storage;
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
    /// <param name="fileExporter">Экспортёр, которым показания прибора будут конвертироваться в файл</param>
    /// <returns>В процессе (пока длится экспорт) будут возвращаться итерации экспорта.</returns>
    public IEnumerable<int> Export(FileExporter fileExporter)
    {
        var allDatesWithData = _storage.GetAllDatesWithDataBetweenTwoDates(_beginDate, _endDate);
        var currentDate = new DateTime(_beginDate.Year, _beginDate.Month, _beginDate.Day, 00, 00, 00);
        var iteration = 0;
        List<NewData> prevDataBatch = null;

        while (currentDate <= _endDate)
        {
            if (_storage.HasDataForThisDate(currentDate))
            {
                var dataBatch = _storage.GetDataBatchByDate(currentDate);
                fileExporter.Export(_fillHoles ? PlaceHolder.FillHoles(dataBatch) : dataBatch);

                if (_fillHoles)     // Сохраняем батч, дабы опять его не запрашивать!
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
                            (float) (firstTimeShift + secondTimeShift) / 2,
                            (prevDataBatch.Last().FluxSensorData + nextDataBatch.First().FluxSensorData) / 2,
                            (prevDataBatch.Last().TempSensorData + nextDataBatch.First().TempSensorData) / 2,
                            (prevDataBatch.Last().PressureSensorData + nextDataBatch.First().PressureSensorData) / 2,
                            (prevDataBatch.Last().HumiditySensorData + nextDataBatch.First().HumiditySensorData) / 2
                        );
                        fileExporter.Export(_fillHoles ? PlaceHolder.FillHoles(dataBatch) : dataBatch);
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
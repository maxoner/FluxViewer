using System;
using System.Collections.Generic;
using FluxViewer.DataAccess.Storage;

namespace FluxViewer.DataAccess.Export;

public static class DataBatchGenerator
{
    /// <summary>
    /// Генерирует фиксированные показания прибора за определённую дату.
    /// Такое может потребоваться, для того, чтобы заполнить пробел длиной в день!
    /// </summary>
    /// <param name="date">Дата, за которую будут сгенерированны показания</param>
    /// <param name="timeShift">Время (в мс.) между двумя показаниями прибора</param>
    /// <param name="fixedFlux">Фиксированное электростатическое поле</param>
    /// <param name="fixedTemp">Фиксированная температура</param>
    /// <param name="fixedPres">Фиксированное давление</param>
    /// <param name="fixedHumm">Фиксированная влажность</param>
    /// <returns></returns>
    public static List<Data> GenerateDataBatch(DateTime date, float timeShift, float fixedFlux,
        float fixedTemp, float fixedPres, float fixedHumm)
    {
        var dataBatch = new List<Data>();
        var currentDate = new DateTime(date.Year, date.Month, date.Day, 00, 00, 00);
        var finishDate = currentDate.AddDays(1);
        while (currentDate < finishDate)
        {
            dataBatch.Add(new Data(currentDate, fixedFlux, fixedTemp, fixedPres, fixedHumm));
            currentDate = currentDate.AddMilliseconds(timeShift);
        }

        return dataBatch;
    }
}
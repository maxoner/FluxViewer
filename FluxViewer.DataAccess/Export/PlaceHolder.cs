using System;
using System.Collections.Generic;
using System.Linq;
using FluxViewer.DataAccess.Models;

namespace FluxViewer.DataAccess.Export;

public static class PlaceHolder
{
    /// <summary>
    /// Максимальная дельта времени между двумя показаниями (в мс.).
    /// Всё, что больше данного значения будет считаться пробелом, и будет в конечном итоге заполнено средним значением!
    /// </summary>
    private const int MaxTimeDeltaBetweenTwoData = 1500; // TODO: скок миллисекунд должно стоять?
    
    /// <summary>
    /// Заполнить пробелы в показаний прибора.
    /// Заполнение происходит так:
    ///     - С 00:00:00 до даты первой записи заполняются ПЕРВЫМ значением.
    ///     - Если между двумя записями нет пробела -> возвращаются эти записи.
    ///     - Если между двумя записями есть пробел -> возвращаются эти записи + пробелы заполняется СРЕДНИМ.
    ///     - С даты последней записи до 23:59:59 заполняются ПОСЛЕДНИМ значением.
    /// </summary>
    /// <param name="dataBatch">Набор показаний прибора</param>
    /// <returns>Все показания прибора, но уже с заполненными пробелами.</returns>
    public static IEnumerable<NewData> FillHoles(List<NewData> dataBatch)
    {
        var timeShift = GetMeanTimeShift(dataBatch); // Время (в мс.) между двумя показаниями (т.н. timedelta)
        var firstData = dataBatch.First();
        var lastData = dataBatch.Last();

        // Если показания начинаются не с 00:00:00, то генерируем их с 00:00:00 до 'firstData.DateTime' 
        foreach (var headData in GenerateHead(firstData, timeShift))
            yield return headData;
        yield return firstData; // Генерируем первое значение
        // Генерируем все значения из батча и заполняем найденные в них пробелы
        foreach (var batchData in GenerateBatchWithoutHolder(dataBatch, timeShift))
            yield return batchData;
        yield return lastData; // Генерируем последнее значение
        // Если показания кончаются не в 23:59:59, то генерируем их с 'lastData.DateTime' до 23:59:59
        foreach (var tailData in GenerateTail(lastData, timeShift))
            yield return tailData;
    }
    
    /// <summary>
    /// Получить среднее время (в мс.) между двумя показаниями прибора в наборе показаний.
    /// </summary>
    /// <param name="dataBatch">Набор показаний прибора</param>
    /// <returns>Среднее время между двумя показаниями</returns>
    public static double GetMeanTimeShift(IReadOnlyList<NewData> dataBatch)
    {
        double meanTimeShift = 0; // Полученная средняя дельта между двумя показаниями
        var num = 0; // Кол-во показаний, которые учитывались для рассчёта средней дельты
        for (var i = 0; i < dataBatch.Count - 1; i++)
        {
            var firstData = dataBatch[i];
            var secondData = dataBatch[i + 1];
            // Учитываем только показания, которые не подходят под определение "пробела",
            // а любые "пробелы" пропускаем!
            if (!((secondData.DateTime - firstData.DateTime).TotalMilliseconds <= MaxTimeDeltaBetweenTwoData))
                continue;
            meanTimeShift += (secondData.DateTime - firstData.DateTime).TotalMilliseconds;
            num++;
        }

        return meanTimeShift / num;
    }

    private static IEnumerable<NewData> GenerateHead(NewData firstData, double timeShift)
    {
        var firstDateTime = firstData.DateTime;
        if (firstDateTime.Hour == 0 && firstDateTime.Minute == 0 && firstDateTime.Second == 0)
            yield break; // Если начинаются с 00:00:00, то всё ок, ничего генерировать дополнительно не нужно
        var currentTime = new DateTime(firstDateTime.Year, firstDateTime.Month, firstDateTime.Day, 0, 0, 0);
        while (currentTime < firstDateTime)
        {
            yield return new NewData(
                currentTime,
                firstData.FluxSensorData,
                firstData.TempSensorData,
                firstData.PressureSensorData,
                firstData.HumiditySensorData
            );
            currentTime = currentTime.AddMilliseconds(timeShift);
        }
    }

    private static IEnumerable<NewData> GenerateBatchWithoutHolder(IReadOnlyList<NewData> dataBatch, double timeShift)
    {
        // 'dataBatch.Count - 2' - потому что 'lastData' вернём за пределами метода
        for (var i = 0; i < dataBatch.Count - 2; i++)
        {
            var startData = dataBatch[i];
            var endData = dataBatch[i + 1];

            // Если обнаружен пробел, то заполняем его средним
            if ((endData.DateTime - startData.DateTime).TotalMilliseconds > MaxTimeDeltaBetweenTwoData)
            {
                var currentDateTime = startData.DateTime.AddMilliseconds(timeShift);
                var meanFluxSensorData = (startData.FluxSensorData + endData.FluxSensorData) / 2;
                var meanTempSensorData = (startData.TempSensorData + endData.TempSensorData) / 2;
                var meanPressureSensorData = (startData.PressureSensorData + endData.PressureSensorData) / 2;
                var meanHumiditySensorData = (startData.HumiditySensorData + endData.HumiditySensorData) / 2;
                while (currentDateTime < endData.DateTime)
                {
                    yield return new NewData(
                        currentDateTime,
                        meanFluxSensorData,
                        meanTempSensorData,
                        meanPressureSensorData,
                        meanHumiditySensorData
                    );
                    currentDateTime = currentDateTime.AddMilliseconds(timeShift);
                }
            }

            // Генерируем всегда только вторую запись, т.к. первая была сгенерирована или на предыдущей итерации,
            // или же за пределами цикла!
            yield return endData;
        }
    }

    private static IEnumerable<NewData> GenerateTail(NewData lastData, double timeShift)
    {
        var lastDateTime = lastData.DateTime;
        if (lastDateTime.Hour == 23 && lastDateTime.Minute == 59 && lastDateTime.Second == 59)
            yield break; // Если кончаются в 23:59:59, то всё ок, ничего генерировать дополнительно не нужно
        var requiredEndDateTime =
            new DateTime(lastDateTime.Year, lastDateTime.Month, lastDateTime.Day, 0, 0, 0).AddDays(1);
        var currentTime = lastDateTime.AddMilliseconds(timeShift);
        while (currentTime <= requiredEndDateTime)
        {
            yield return new NewData(
                currentTime,
                lastData.FluxSensorData,
                lastData.TempSensorData,
                lastData.PressureSensorData,
                lastData.HumiditySensorData
            );
            currentTime = currentTime.AddMilliseconds(timeShift);
        }
    }
}
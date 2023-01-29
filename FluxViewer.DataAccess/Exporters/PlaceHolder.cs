using System;
using System.Collections.Generic;
using System.Linq;
using FluxViewer.DataAccess.Models;

namespace FluxViewer.DataAccess.Exporters;

public static class PlaceHolder
{
    // TODO: скок миллисекунд должно стоять?
    private const int MaxTimeDeltaBetweenTwoData = 1500; // Максимальная дельта времени между двумя показаниями (в мс.)

    public static List<NewData> FillHoles(List<NewData> dataBatch)
    {
        var dataBatchWithoutHoles = new List<NewData>();
        var timeShift = GetMeanTimeShift(dataBatch); // Время между двумя показаниями (т.н. timedelta)

        var firstData = dataBatch.First();
        var lastData = dataBatch.Last();
        var firstDateTime = firstData.DateTime;
        var lastDateTime = lastData.DateTime;

        // Если показания начинаются не с 00:00:00, то заполняем их ПЕРВЫМ значением!
        if (firstDateTime.Hour != 0 || firstDateTime.Minute != 0 || firstDateTime.Second != 0)
        {
            var currentTime = new DateTime(firstDateTime.Year, firstDateTime.Month, firstDateTime.Day, 0, 0, 0);
            while (currentTime < firstDateTime)
            {
                dataBatchWithoutHoles.Add(new NewData(
                    currentTime,
                    firstData.FluxSensorData,
                    firstData.TempSensorData,
                    firstData.PressureSensorData,
                    firstData.HumiditySensorData
                ));
                currentTime = currentTime.AddMilliseconds(timeShift);
            }
        }
        dataBatchWithoutHoles.Add(firstData);   // Добавляем первую запись из батча
        
        // Копируем все значения из батча и заполняем найденные пробелы
        for (var i = 0; i < dataBatch.Count - 1; i++)
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
                    dataBatchWithoutHoles.Add(new NewData(
                        currentDateTime,
                        meanFluxSensorData,
                        meanTempSensorData,
                        meanPressureSensorData,
                        meanHumiditySensorData
                    ));
                    currentDateTime = currentDateTime.AddMilliseconds(timeShift);
                }
            }
            
            // Всегда добавляем только вторую запись, т.к первая запись была уже добавлена или на пред. итерации цикла,
            // или за пределами цикла
            dataBatchWithoutHoles.Add(endData);   
        }


        // Если показания кончаются не в 23:59:59, то заполняем их ПОСЛЕДНИМ значением!
        if (lastDateTime.Hour != 23 || lastDateTime.Minute != 59 || lastDateTime.Second != 59)
        {
            var requiredEndDateTime = new DateTime(lastDateTime.Year, lastDateTime.Month, lastDateTime.Day, 23, 59, 59);
            var currentTime = lastDateTime.AddMilliseconds(timeShift);
            while (currentTime <= requiredEndDateTime)
            {
                dataBatchWithoutHoles.Add(new NewData(
                    currentTime,
                    lastData.FluxSensorData,
                    lastData.TempSensorData,
                    lastData.PressureSensorData,
                    lastData.HumiditySensorData
                ));
                currentTime = currentTime.AddMilliseconds(timeShift);
            }
        }

        return dataBatchWithoutHoles;
    }

    private static double GetMeanTimeShift(IReadOnlyList<NewData> dataBatch)
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
}
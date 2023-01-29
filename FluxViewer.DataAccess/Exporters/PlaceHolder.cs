using System;
using System.Collections.Generic;
using System.Linq;
using FluxViewer.DataAccess.Models;

namespace FluxViewer.DataAccess.Exporters;

public static class PlaceHolder
{
    // TODO: скок миллисекунд должно стоять?
    private const int MaxTimeDeltaBetweenTwoData = 1500; // Максимальная дельта времени между двумя показаниями (в мс.)

    public static IEnumerable<NewData> FillHoles(List<NewData> dataBatch)
    {
        var dataBatchWithoutHoles = new List<NewData>();
        var timeShift = GetMeanTimeShift(dataBatch); // Время между двумя показаниями (т.н. timedelta)

        var startData = dataBatch.First();
        var endData = dataBatch.Last();
        var startDateTime = startData.DateTime;
        var endDateTime = endData.DateTime;

        // Если показания начинаются не с 00:00:00, то заполняем их ПЕРВЫМ значением!
        if (startDateTime.Hour != 0 || startDateTime.Minute != 0 || startDateTime.Second != 0)
        {
            var currentTime = new DateTime(startDateTime.Year, startDateTime.Month, startDateTime.Day, 0, 0, 0);
            while (currentTime < startDateTime)
            {
                dataBatchWithoutHoles.Add(new NewData(
                    currentTime,
                    startData.FluxSensorData,
                    startData.TempSensorData,
                    startData.PressureSensorData,
                    startData.HumiditySensorData
                ));
                currentTime = currentTime.AddMilliseconds(timeShift);
            }
        }

        // Заполняем все пробелы между стартовым и конечным показаниями
        for (var i = 0; i < dataBatch.Count - 1; i++)
        {
            var firstData = dataBatch[i];
            var secondData = dataBatch[i + 1];

            dataBatchWithoutHoles.Add(firstData);

            // Если обнаружен пробел, то заполняем его средним
            if ((secondData.DateTime - firstData.DateTime).TotalMilliseconds > MaxTimeDeltaBetweenTwoData)
            {
                var currentDateTime = firstData.DateTime.AddMilliseconds(timeShift);
                var meanFluxSensorData = (firstData.FluxSensorData + secondData.FluxSensorData) / 2;
                var meanTempSensorData = (firstData.TempSensorData + secondData.TempSensorData) / 2;
                var meanPressureSensorData = (firstData.PressureSensorData + secondData.PressureSensorData) / 2;
                var meanHumiditySensorData = (firstData.HumiditySensorData + secondData.HumiditySensorData) / 2;
                while (currentDateTime < secondData.DateTime)
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

            dataBatchWithoutHoles.Add(secondData);
        }


        // Если показания кончаются не в 23:59:59, то заполняем их ПОСЛЕДНИМ значением!
        if (endDateTime.Hour != 23 || endDateTime.Minute != 59 || endDateTime.Second != 59)
        {
            var requiredEndDateTime = new DateTime(endDateTime.Year, endDateTime.Month, endDateTime.Day, 23, 59, 59);
            var currentTime = endDateTime.AddMilliseconds(timeShift);
            while (currentTime <= requiredEndDateTime)
            {
                dataBatchWithoutHoles.Add(new NewData(
                    currentTime,
                    endData.FluxSensorData,
                    endData.TempSensorData,
                    endData.PressureSensorData,
                    endData.HumiditySensorData
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
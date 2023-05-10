using System;
using System.Linq;

namespace FluxViewer.DataAccess.Models;

public struct Data
{
    /// <summary>
    /// DateTime (8 байт) + FluxSensorData (4 байта) + TempSensorData (4 байта) +
    /// PressureSensorData (4 байта) + HumiditySensorData (4 байта) = 24 байта
    /// </summary>
    public const int ByteLenght = 24;

    /// <summary>
    /// Дата и время с миллисекундами
    /// </summary>
    public DateTime DateTime;

    /// <summary>
    /// Информация с датчиков флюкс метра
    /// </summary>
    public readonly float FluxSensorData;

    /// <summary>
    /// Информация с датчиков температуры
    /// </summary>
    public readonly float TempSensorData;

    /// <summary>
    /// Информация с датчиков давления
    /// </summary>
    public readonly float PressureSensorData;

    /// <summary>
    /// Информация с датчиков влажности
    /// </summary>
    public readonly float HumiditySensorData;

    public Data(DateTime dateTime, float fluxSensorData, float tempSensorData, float pressureSensorData,
        float humiditySensorData)
    {
        DateTime = dateTime;
        FluxSensorData = fluxSensorData;
        TempSensorData = tempSensorData;
        PressureSensorData = pressureSensorData;
        HumiditySensorData = humiditySensorData;
    }

    public byte[] Serialize()
    {
        return BitConverter.GetBytes(((DateTimeOffset)DateTime).ToUnixTimeMilliseconds()).Concat(
            BitConverter.GetBytes(FluxSensorData).Concat(
                BitConverter.GetBytes(TempSensorData).Concat(
                    BitConverter.GetBytes(PressureSensorData).Concat(
                        BitConverter.GetBytes(HumiditySensorData))))).ToArray();
    }

    public static Data Deserialize(byte[] bytes)
    {
        var dateTimeBytes = new byte[8];
        Array.Copy(bytes, 0, dateTimeBytes, 0, 8);
        var fluxSensorDataBytes = new byte[4];
        Array.Copy(bytes, 8, fluxSensorDataBytes, 0, 4);
        var tempSensorDataDataBytes = new byte[4];
        Array.Copy(bytes, 12, tempSensorDataDataBytes, 0, 4);
        var pressureSensorDataBytes = new byte[4];
        Array.Copy(bytes, 16, pressureSensorDataBytes, 0, 4);
        var humiditySensorDataBytes = new byte[4];
        Array.Copy(bytes, 20, humiditySensorDataBytes, 0, 4);

        return new Data(
            DateTimeOffset.FromUnixTimeMilliseconds(BitConverter.ToInt64(dateTimeBytes)).LocalDateTime,
            BitConverter.ToSingle(fluxSensorDataBytes),
            BitConverter.ToSingle(tempSensorDataDataBytes),
            BitConverter.ToSingle(pressureSensorDataBytes),
            BitConverter.ToSingle(humiditySensorDataBytes));
    }
}
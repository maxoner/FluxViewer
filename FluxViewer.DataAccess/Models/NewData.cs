using System;
using System.Linq;

namespace FluxViewer.DataAccess.Models;

public struct NewData
{
    public static int ByteLenght = 28;
    
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id;

    /// <summary>
    /// Дата и время с миллисекундами
    /// </summary>
    public DateTime DateTime;

    /// <summary>
    /// Информация с датчиков флюкс метра
    /// </summary>
    public float FluxSensorData;

    /// <summary>
    /// Информация с датчиков температуры
    /// </summary>
    public float TempSensorData;

    /// <summary>
    /// Информация с датчиков давления
    /// </summary>
    public float PressureSensorData;

    /// <summary>
    /// Информация с датчиков влажности
    /// </summary>
    public float HumiditySensorData;

    public NewData(int id, DateTime dateTime, float fluxSensorData, float tempSensorData, float pressureSensorData,
        float humiditySensorData)
    {
        Id = id;
        DateTime = dateTime;
        FluxSensorData = fluxSensorData;
        TempSensorData = tempSensorData;
        PressureSensorData = pressureSensorData;
        HumiditySensorData = humiditySensorData;
    }

    public byte[] Serialize()
    {
        return BitConverter.GetBytes(Id).Concat(
            BitConverter.GetBytes(((DateTimeOffset)DateTime).ToUnixTimeMilliseconds()).Concat(
                BitConverter.GetBytes(FluxSensorData).Concat(
                    BitConverter.GetBytes(TempSensorData).Concat(
                        BitConverter.GetBytes(PressureSensorData).Concat(
                            BitConverter.GetBytes(HumiditySensorData)))))).ToArray();
    }


    public static NewData Deserialize(byte[] bytes)
    {
        var idBytes = new byte[4];
        Array.Copy(bytes, 0, idBytes, 0, 4);
        var dateTimeBytes = new byte[8];
        Array.Copy(bytes, 4, dateTimeBytes, 0, 8);
        var fluxSensorDataBytes = new byte[4];
        Array.Copy(bytes, 12, fluxSensorDataBytes, 0, 4);
        var tempSensorDataDataBytes = new byte[4];
        Array.Copy(bytes, 16, tempSensorDataDataBytes, 0, 4);
        var pressureSensorDataBytes = new byte[4];
        Array.Copy(bytes, 20, pressureSensorDataBytes, 0, 4);
        var humiditySensorDataBytes = new byte[4];
        Array.Copy(bytes, 24, humiditySensorDataBytes, 0, 4);
        
        return new NewData(
            BitConverter.ToInt32(idBytes),
            DateTimeOffset.FromUnixTimeMilliseconds(BitConverter.ToInt64(dateTimeBytes)).LocalDateTime,
            BitConverter.ToSingle(fluxSensorDataBytes),
            BitConverter.ToSingle(tempSensorDataDataBytes),
            BitConverter.ToSingle(pressureSensorDataBytes),
            BitConverter.ToSingle(humiditySensorDataBytes));
    }
}
using System;

namespace FluxViewer.DataAccess.Models
{
    /// <summary>
    /// Информация с прибора
    /// </summary>
    public class Data
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Дата и время с миллисекундами
        /// </summary>
        public DateTime DateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Информация с датчиков флюкс метра
        /// </summary>
        public float FluxSensorData {get;set;}

        /// <summary>
        /// Информация с датчиков температуры
        /// </summary>
        public float TempSensorData {get;set;}

        /// <summary>
        /// Информация с датчиков давления
        /// </summary>
        public float PressureSensorData { get;set;}

        /// <summary>
        /// Информация с датчиков влажности
        /// </summary>
        public float HumiditySensorData { get;set;}

        public override string ToString()
        {
            return $"{Id}\t{DateTime}\t{FluxSensorData}\t{TempSensorData}\t{PressureSensorData}\t{HumiditySensorData}";
        }
    }
}

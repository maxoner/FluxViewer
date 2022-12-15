using FluxViewer.DataAccess.Enums;
using System;

namespace FluxViewer.DataAccess.Models
{
    /// <summary>
    /// Информационная запись
    /// </summary>
    public class Log
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Уровень записи
        /// </summary>
        public LogLevel Level { get; set; }

        /// <summary>
        /// Время записи
        /// </summary>
        public DateTime DateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Собщение
        /// </summary>
        public string Message { get; set; }
    }
}

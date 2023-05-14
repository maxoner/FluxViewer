using System;

namespace FluxViewer.DataAccess.Log;

/// <summary>
/// Интерфейс взаимодействия с системой логгирования.
/// В нём доступны методы чтения/записи логов.
/// </summary>
public interface ILogger
{
    /// <summary>
    /// Сделать запись лога. 
    /// </summary>
    /// <param name="logLevel">Уровень важности лога</param>
    /// <param name="logInitiator">Инициатор лога</param>
    /// <param name="message">Сообщение лога</param>
    void WriteLog(LogLevel logLevel, LogInitiator logInitiator, string message);


    /// <summary>
    /// Получить все логи за определённую дату
    /// </summary>
    /// <param name="date">Дата, за которую необходимо получить логи</param>
    /// <returns>Список логов, где каждая строка соответствует логу</returns>
    string[] GetLogsByDate(DateTime date);
}
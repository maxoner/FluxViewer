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
    
}
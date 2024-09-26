namespace FluxViewer.Core.Log;

public enum LogLevel
{
    /// <summary>
    /// Отладочная информация
    /// </summary>
    Debug = 10,

    /// <summary>
    /// Просто информация, требующая фиксации в работе приложения
    /// </summary>
    Info = 20,

    /// <summary>
    /// Важные предупреждения
    /// </summary>
    Warning = 30,

    /// <summary>
    /// Информация об ошибке
    /// </summary>
    Error = 40,

    /// <summary>
    /// Информация о критической ошибки, после которой невозможна корректная работа приложения
    /// </summary>
    Critical = 50
}
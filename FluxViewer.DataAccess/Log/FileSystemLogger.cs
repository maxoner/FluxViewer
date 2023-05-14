using System;
using System.IO;

namespace FluxViewer.DataAccess.Log;

public class FileSystemLogger : ILogger
{
    private const string FilenameDateFormat = "yyyy_MM_dd";
    private const string FilenameExtension = "log";

    public void WriteLog(LogLevel logLevel, LogInitiator logInitiator, string message)
    {
        var pathToStorageDir = Path.Combine(Directory.GetCurrentDirectory(), "logs");
        if (!Directory.Exists(pathToStorageDir))
            Directory.CreateDirectory(pathToStorageDir);
        var filename = DateTime.Today.ToString(FilenameDateFormat) + "." + FilenameExtension;
        var pathToCurrentFile = Path.Combine(pathToStorageDir, filename);

        using var file = File.AppendText(pathToCurrentFile);
        file.WriteLine(MakeLogMessage(logLevel, logInitiator, message));
    }

    public string[] GetLogsByDate(DateTime date)
    {
        var pathToStorageDir = Path.Combine(Directory.GetCurrentDirectory(), "logs");
        if (!Directory.Exists(pathToStorageDir))
            return Array.Empty<string>(); // Нет дириктории с логами -> возвращаем пустой список логов

        var filename = date.ToString(FilenameDateFormat) + "." + FilenameExtension;
        var pathToCurrentFile = Path.Combine(pathToStorageDir, filename);
        if (!File.Exists(pathToCurrentFile))
            return Array.Empty<string>(); // Нет нужного файла с логами -> возвращаем пустой список логов

        return File.ReadAllLines(pathToCurrentFile);
    }

    private static string MakeLogMessage(LogLevel logLevel, LogInitiator logInitiator, string message)
    {
        return $"[{DateTime.Now} | {logInitiator.ToString()} | {logLevel.ToString()}] {message}";
    }
}
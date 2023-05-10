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
        
        using var file = new StreamWriter(pathToCurrentFile);
        file.Write(MakeLogMessage(logLevel, logInitiator, message));
    }

    private static string MakeLogMessage(LogLevel logLevel, LogInitiator logInitiator, string message)
    {
        return $"[{DateTime.Now} | {logInitiator.ToString()} | {logLevel.ToString()}] {message}";
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using FluxViewer.DataAccess.Models;

namespace FluxViewer.DataAccess.Storage;

public class FileSystemStorage : IStorage
{
    private const string FilenameDateFormat = "yyyy_MM_dd";
    private const string FilenameExtension = "flux";
    private string _pathToStorageDir;

    public void Open()
    {
        _pathToStorageDir = Path.Combine(Directory.GetCurrentDirectory(), "data");
        if (!Directory.Exists(_pathToStorageDir))
            Directory.CreateDirectory(_pathToStorageDir);
    }

    public void Close()
    {
        // Для файловой системы никакое закрытие не требуется.
    }

    public void WriteData(NewData data)
    {
        var filename = DateTime.Today.ToString(FilenameDateFormat) + "." + FilenameExtension;
        var pathToCurrentFile = Path.Combine(_pathToStorageDir, filename);

        var formatter = new BinaryFormatter();
        using var file = new FileStream(pathToCurrentFile, FileMode.Append);
        file.Write(data.Serialize());
    }

    public int GetDataCount()
    {
        var paths = GetFilePaths();
        return paths.Sum(GetDataCountFromFile);
    }

    public int GetDataCountBetweenTwoDates(DateTime beginDate, DateTime endDate) 
    {
        var paths = GetFilePathsBetweenTwoDates(beginDate, endDate);
        return paths.Sum(GetDataCountFromFile);
    }

    public List<DateTime> GetAllDatesWithDataBetweenTwoDates(DateTime beginDate, DateTime endDate)
    {
        var paths = GetFilePathsBetweenTwoDates(beginDate, endDate);
        return paths.Select(path => DateTime.ParseExact(
            Path.GetFileNameWithoutExtension(path), FilenameDateFormat, null)).ToList();
    }

    public bool HasDataForThisDate(DateTime date)
    {
        var filename = date.ToString(FilenameDateFormat) + "." + FilenameExtension;
        var pathToFile = Path.Combine(_pathToStorageDir, filename);
        return File.Exists(pathToFile);
    }

    public List<NewData> GetDataBatchByDate(DateTime date)
    {
        var filename = date.ToString(FilenameDateFormat) + "." + FilenameExtension;
        var pathToFile = Path.Combine(_pathToStorageDir, filename);

        try
        {
            using var file = new FileStream(pathToFile, FileMode.Open, FileAccess.Read);
            var data = new List<NewData>();
            var buffer = new byte[NewData.ByteLenght];
            while (true)
            {
                var numOfReadBytes = file.Read(buffer);
                if (numOfReadBytes == 0)
                    break;

                data.Add(NewData.Deserialize(buffer));
            }

            return data;
        }
        catch (Exception e)
        {
            throw new Exception($"Файл '{pathToFile}' не найден"); // TODO: уникальная ошибка!
        }
    }

    public List<NewData> GetNextDataBatchAfterThisDate(DateTime date) 
    {
        var allFilePaths = Directory.GetFiles(_pathToStorageDir);
        foreach (var fullPath in allFilePaths)
        {
            var filename = Path.GetFileNameWithoutExtension(fullPath);
            var foundDate = DateTime.ParseExact(filename, FilenameDateFormat, null);
            if (foundDate > date)
                return GetDataBatchByDate(foundDate);
        }

        throw new NextDataBatchNotFoundException();
    }

    public List<NewData> GetPrevDataBatchAfterThisDate(DateTime date)
    {
        var allFilePaths = Directory.GetFiles(_pathToStorageDir);
        foreach (var fullPath in allFilePaths)
        {
            var filename = Path.GetFileNameWithoutExtension(fullPath);
            var foundDate = DateTime.ParseExact(filename, FilenameDateFormat, null);
            if (foundDate < date)
                return GetDataBatchByDate(foundDate);
        }
        throw new PrevDataBatchNotFoundException();
    }

    private List<string> GetFilePathsBetweenTwoDates(DateTime beginDate, DateTime endDate, int? step = null)
    {
        var goalPaths = new List<string>();
        foreach (var fullPath in GetFilePaths())
        {
            var filename = Path.GetFileNameWithoutExtension(fullPath);
            var date = DateTime.ParseExact(filename, FilenameDateFormat, null);
            if (date >= beginDate && date <= endDate)
                goalPaths.Add(fullPath);
        }

        return goalPaths;
    }
    
    private IEnumerable<string> GetFilePaths()
    {
        var goalFilePath = new List<string>();
        var allFilePaths = Directory.GetFiles(_pathToStorageDir);
        foreach (var fullPath in allFilePaths)
        {
            var filename = Path.GetFileNameWithoutExtension(fullPath);
            try
            {
                DateTime.ParseExact(filename, FilenameDateFormat, null);
                goalFilePath.Add(fullPath);
            }
            catch
            {
            }
        }

        return goalFilePath;
    }
    
    private static int GetDataCountFromFile(string pathToFile)
    {
        return (int)(new FileInfo(pathToFile).Length / NewData.ByteLenght);
    }
}
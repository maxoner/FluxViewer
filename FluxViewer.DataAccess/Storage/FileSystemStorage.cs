using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FluxViewer.DataAccess.Models;

namespace FluxViewer.DataAccess.Storage;

public class FileSystemStorage : IStorage
{
    private string _pathToStorageDir;

    public void Open()
    {
        _pathToStorageDir = Directory.GetCurrentDirectory() + "/data";
        if (!Directory.Exists(_pathToStorageDir))
            Directory.CreateDirectory(_pathToStorageDir);
    }

    public void Close()
    {
        // Для файловой системы никакое закрытие не требуется.
    }

    public void WriteData(Data data)
    {
        // Структура файла следующая:
        // N - кол-во элементов в файле
        // Элемент №1
        // Элемент №2
        // ...
        // Элемент №N

        var pathToCurrentFile = _pathToStorageDir + "/" + DateTime.Today.ToString("yyyy_MM_dd") + ".flux";
        var dataCount = (File.Exists(pathToCurrentFile)) ? GetDataCountFromFile(pathToCurrentFile) : 0;
        var newDataCount = dataCount + 1;

        using var fileToWrite = new FileStream(pathToCurrentFile, FileMode.Append);
        WriteDataInFile(fileToWrite, data);
        WriteDataCountInFile(fileToWrite, newDataCount);
    }

    private static int GetDataCountFromFile(string pathToCurrentFile)
    {
        using var fileToRead = new FileStream(pathToCurrentFile, FileMode.Open);
        fileToRead.Seek(0, SeekOrigin.End);
        byte symbol = 0;
        string dataCountString = "";
        while (symbol != (byte) '\n')
        {
            symbol = (byte) fileToRead.ReadByte();
            dataCountString += symbol.ToString();
            fileToRead.Seek(-1, SeekOrigin.Current);
        }

        dataCountString = dataCountString.Reverse().ToString();
        return Convert.ToInt32(dataCountString);
    }

    private void WriteDataInFile(Stream file, Data data)
    {
        var streamWriter = new StreamWriter(file, Encoding.UTF8);
        streamWriter.WriteLine(data.ToString());

        // var bytesToWrite = Encoding.UTF8.GetBytes($"{data}\n");
        // file.Seek(file.Length, SeekOrigin.Begin); // Курсор в конец файла
        // file.Write(bytesToWrite, 0, bytesToWrite.Length);
        // file.Flush();
    }

    private static void WriteDataCountInFile(Stream file, int newDateCount)
    {
        var lineToWrite = $"{newDateCount}\n";
        var streamWriter = new StreamWriter(file, Encoding.UTF8);
        streamWriter.WriteLine(lineToWrite);
    }


    public List<Data> GetDataBatchBetweenTwoDates(DateTime beginDate, DateTime endDate, int batchNumber, int batchSize)
    {
        throw new NotImplementedException();
    }
}
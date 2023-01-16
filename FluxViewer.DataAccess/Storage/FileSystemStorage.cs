using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FluxViewer.DataAccess.Models;

namespace FluxViewer.DataAccess.Storage;

public class FileSystemStorage : IStorage
{
    private const string FilenameDateFormat = "yyyy_MM_dd";
    private const string FilenameExtension = "flux";
    private string _pathToStorageDir;
    private string _pathToCurrentFile;

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

    public void WriteData(Data data)
    {
        // Структура файла следующая:
        // Элемент №1
        // Элемент №2
        // ...
        // Элемент №N
        // N - кол-во элементов в файле
        //

        var filename = DateTime.Today.ToString(FilenameDateFormat) + "." + FilenameExtension;
        _pathToCurrentFile = Path.Combine(_pathToStorageDir, filename);
        using var file = new FileStream(_pathToCurrentFile, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        var oldDataCount = GetDataCountFromFile(file);
        var newDataCount = oldDataCount + 1;

        if (oldDataCount != 0)
            RemoveDataCountInFile(file, oldDataCount);
        WriteDataInFile(file, data, newDataCount);
    }

    private long GetDataCountFromFile(Stream file)
    {
        // На последней строке находится число - кол-во элементов в файле. Это число и пытаемся достать

        if (file.Length == 0)
            return 0;
        
        if (file.Length < 2)
            throw new Exception($"В файле {_pathToCurrentFile} нарушена структура!"); // TODO: написать своё исключение
        
        file.Seek(-2, SeekOrigin.End); // Курсор в конец файла, пропустив символы EOF и '\n'
        var dataCountReverse = "";
        while (true)
        {
            var currentByte = (byte)file.ReadByte();
            file.Seek(-1, SeekOrigin.Current); // Назад оступили (-1)

            var currentSymbol = Convert.ToChar(currentByte);
            if (currentSymbol == '\n') // Дошли до предыдущей строки?
                break;
            dataCountReverse += currentSymbol;

            if (file.Position == 0) // Дошли до начала файла?
                break;
            file.Seek(-1, SeekOrigin.Current); // Сделали сдвиг для следующей итерации (-1)
        }

        var dataCount = new string(dataCountReverse.Reverse().ToArray());
        try
        {
            return Convert.ToInt64(dataCount);
        }
        catch
        {
            throw new Exception($"В файле {_pathToCurrentFile} нарушена структура!" +
                                       $"Не удаётся найти блок с кол-во элементов в файле."); // TODO: написать своё исключение
        }
    }

    private static void RemoveDataCountInFile(Stream file, long oldDataCount)
    {
        var countOfNumbers = oldDataCount.ToString().Length + 1; // +1, т.к. считаем ещё и символ '\n'
        file.Seek(-countOfNumbers, SeekOrigin.End);
    }

    private static void WriteDataInFile(Stream file, Data data, long newDataCount)
    {
        var dataBinary = Encoding.ASCII.GetBytes($"{data}\n");
        file.Write(dataBinary, 0, dataBinary.Length);
        var newDataCountBinary = Encoding.ASCII.GetBytes($"{newDataCount}\n");
        file.Write(newDataCountBinary, 0, newDataCountBinary.Length);
    }


    public List<Data> GetDataBatchBetweenTwoDates(DateTime beginDate, DateTime endDate, int batchNumber, int batchSize)
    {
        throw new NotImplementedException();
    }
}
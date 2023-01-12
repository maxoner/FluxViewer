using System.Diagnostics;
using FluxViewer.DataAccess.LiteDbb;
using FluxViewer.DataAccess.Models;

void CreateBigDatabase(DateTime beginDate, DateTime endDate, int batchSize)
{
    var currentDir = Directory.GetCurrentDirectory();
    var pathToDatabase = $"{currentDir}/database.litedb";
    if (File.Exists(pathToDatabase))
        File.Delete(pathToDatabase);

    var pathToLogDatabase = $"{currentDir}/database_logs.litedb";
    if (File.Exists(pathToLogDatabase))
        File.Delete(pathToLogDatabase);

    var liteDbService = new LiteDbService();
    liteDbService.ConnectOrCreateDataBase(pathToDatabase);

    var data = new List<Data>(batchSize);
    var currentDate = new DateTime(beginDate.Year, beginDate.Month, beginDate.Day);
    var batchNumber = 1;
    while (currentDate <= endDate)
    {
        data.Add(new Data()
            {
                DateTime = currentDate,
                FluxSensorData = (float)currentDate.Millisecond % 20 / 100,
                HumiditySensorData = (float)currentDate.Millisecond % 100 / 100,
                PressureSensorData = (float)currentDate.Millisecond % 10 / 100,
                TempSensorData = (float)currentDate.Millisecond % 50 / 100
            }
        );
        currentDate = currentDate.AddMilliseconds(400);

        if (data.Count == batchSize)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            liteDbService.WriteData(data);
            stopwatch.Stop();
            var writeTime = stopwatch.Elapsed;

            data.Clear();
            Console.WriteLine($"{currentDate}\tЗаписан {batchNumber} батч (затрачено: {writeTime})");
            batchNumber++;
        }
    }

    liteDbService.DisconnectFromDataBase();
}


void ReadBigDatabase(DateTime beginDate, DateTime endDate, int batchSize)
{
    var currentDir = Directory.GetCurrentDirectory();
    var pathToDatabase = $"{currentDir}/database.litedb";
    var liteDbService = new LiteDbService();
    liteDbService.ConnectOrCreateDataBase(pathToDatabase);

    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();
    var dataCountBetweenTwoDates = liteDbService.GetDataCountBetweenTwoDates(beginDate, endDate);
    stopwatch.Stop();
    var readDataCountTime = stopwatch.Elapsed;
    Console.WriteLine($"Количество данных: {dataCountBetweenTwoDates} (затрачено: {readDataCountTime})");

    stopwatch.Reset();
    stopwatch.Start();
    liteDbService.CreateDateTimeIndex();
    stopwatch.Stop();
    var createIndexTime = stopwatch.Elapsed;
    Console.WriteLine($"Создан индекс на DateTime (затрачено: {createIndexTime})");


    var batchCount = Math.Ceiling((float)dataCountBetweenTwoDates / batchSize);
    for (var batchNumber = 0; batchNumber < batchCount; batchNumber++)
    {
        stopwatch.Reset();
        stopwatch.Start();
        var data = liteDbService.GetDataBatchBetweenTwoDates(beginDate, endDate, batchNumber, batchSize);
        stopwatch.Stop();
        var readBatchTime = stopwatch.Elapsed;

        var firstDate = data.First();
        var lastDate = data.Last();
        Console.WriteLine($"{firstDate} -- {lastDate}\tПрочитан {batchNumber} батч (затрачено: {readBatchTime})");
    }

    liteDbService.DisconnectFromDataBase();
}


var beginDate = new DateTime(2022, 11, 24);
var endDate = new DateTime(2022, 12, 24);
const int batchSize = 100000;
CreateBigDatabase(beginDate, endDate, batchSize);
ReadBigDatabase(beginDate, endDate, batchSize);